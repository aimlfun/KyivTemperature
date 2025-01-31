using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms.Design;
using System.Collections.Concurrent;

namespace Temperature;

/// <summary>
/// A Machine Learning graph of temperatures.
/// </summary>
[Designer(typeof(ControlDesigner))]
public partial class MLTemperatureGraph : TemperatureGraph
{
    #region ENUMS
    /// <summary>
    /// Batch or normal training. Batch trains chunks at a time, normal trains all points in one go (think of batch of all).
    /// </summary>
    public enum TrainingTypes { Batch, Normal };

    /// <summary>
    /// Adam is an optimiser that adjusts the learning rate. None means no optimiser.
    /// </summary>
    public enum OptimiserTypes { Adam, none };

    /// <summary>
    /// Defines how we adjust learning over time.
    /// </summary>
    public enum LearningRateTypes { StepDecay, ExpoDecay, ReduceOnPlateau, CosineAnnealing, Cyclical, none };
    #endregion

    #region CONSTANTS
    /// <summary>
    /// Cache Arial 10 font used for overall rank (top right) and temperature error etc.
    /// </summary>
    private readonly static Font s_arial10font = new("Arial", 10);

    /// <summary>
    /// Cache Arial 20 font used for big rank number.
    /// </summary>
    private readonly static Font s_arial20font = new("Arial", 20);

    /// <summary>
    /// How often we update the graph.
    /// </summary>
    private const int c_updateFrequency = 10;

    /// <summary>
    /// For reduce on plateau
    /// </summary>
    private const int c_patience = 5;

    /// <summary>
    /// Define the decay rate for learning rate.
    /// </summary>
    private const float c_decayRate = 0.995f;

    /// <summary>
    /// For step decay and cyclical learning rate.
    /// </summary>
    private const int c_stepSize = 13;

    /// <summary>
    /// Initial learning rate.
    /// </summary>
    private const float c_initialLearningRate = 0.01f;

    /// <summary>
    /// The minimum delta for reduce on plateau, and in general. Any smaller won't be useful.
    /// </summary>
    private const float c_minDelta = 0.0001f;

    /// <summary>
    /// For cyclical learning rate
    /// </summary>
    private const float c_baseLearningRate = 0.0001f;

    /// <summary>
    /// For cyclical learning rate
    /// </summary>
    private const float c_maxLearningRate = 0.01f;

    /// <summary>
    /// It's unlikely to match perfectly, so we have a desired accuracy level. 0.8f across 12 months is 0.8/12 per month, or 0.07 degrees.
    /// </summary>
    private const float c_desiredAccuracyLevel = 0.8f;

    /// <summary>
    /// ADAM has a tendency to oscillate. Set this to 1 and the moment it matches, it stops.
    /// </summary>
    private const int c_numberOfStableAccurateReadings = 3;

    /// <summary>
    /// The number of previous readings to store.
    /// </summary>
    private readonly List<float> _lastNReadings = [];

    /// <summary>
    /// Overall Rank of the neural network.
    /// </summary>
    private float _overallRank;

    /// <summary>
    /// For cosine annealing.
    /// </summary>
    private static int s_EpochLimit = 5000;

    /// <summary>
    /// Allows the user to set the epoch limit.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // required to stop designer serialising the data
    internal static int EpochLimit
    {
        get
        {
            return s_EpochLimit;
        }
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value), "Epoch limit must be at least 1");

            s_EpochLimit = value;
        }
    }
    #endregion

    #region NEURAL NETWORK PROPERTIES
    /// <summary>
    /// Number of layers in the neural network.
    /// </summary>
    private int[] _layers = [1, 5, 5, 5, 1]; // Define the layers

    /// <summary>
    /// Size of the batch used for training.
    /// </summary>
    private int _batchSize = 12; // Define the batch size

    /// <summary>
    /// Which part of the batch we're currently training (effectively wrap around).
    /// </summary>
    private int _batchIndex = 0;

    /// <summary>
    /// Batch or normal training.
    /// </summary>
    private TrainingTypes _trainingType = TrainingTypes.Normal;

    /// <summary>
    /// How we adjust the learning rate over time.
    /// </summary>
    private LearningRateTypes _learningRateType = LearningRateTypes.Cyclical;

    /// <summary>
    /// Which optimiser to use.
    /// </summary>
    private OptimiserTypes _optimiserType = OptimiserTypes.none;

    /// <summary>
    /// How the weights are initialised.
    /// </summary>
    private NeuralNetwork.InitType _weightInitType = NeuralNetwork.InitType.Random;

    /// <summary>
    /// Whether to shuffle the training data each time.
    /// </summary>
    private bool _shuffleTrainingData = false;

    #region REDUCE ON PLATEAU SETTINGS
    /// <summary>
    /// For reduce on plateau
    /// </summary>
    private double bestValidationLoss = double.MaxValue;

    /// <summary>
    /// For reduce on plateau
    /// </summary>
    private int epochsSinceImprovement = 0;
    #endregion

    /// <summary>
    /// The neural network.
    /// </summary>
    private NeuralNetwork? _neuralNetwork;
    #endregion

    #region Brushes and Pens
    /// <summary>
    /// Used to draw the predicted temperatures.
    /// </summary>
    private readonly Pen _penPredictedTemperaturePen = new(Color.FromArgb(190, 255, 0, 0), 2);

    /// <summary>
    /// Used to draw the previous predicted temperatures.
    /// </summary>
    private readonly Pen _previousPredictedTemperaturesPen = new(Color.FromArgb(100, 255, 255, 255), 3);
    #endregion

    /// <summary>
    /// Data containing crosses, to train.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // required to stop designer serialising the data
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public List<double[]> TrainingData { get; set; } = [];

    /// <summary>
    /// Epoch is the generation.
    /// </summary>
    private int _epoch = 0;

    /// <summary>
    /// Contains the last n predictions.
    /// </summary>
    private readonly List<List<PointF>> _lastPredictionsListOfTemperatures = [];

    /// <summary>
    /// Number of previous predictions to show. We draw the last 30 predictions in a lighter shade (lightest=newest).
    /// </summary>
    private int _numberOfPreviousPredictions = 20;

    /// <summary>
    /// Indicates whether the ML graph is enabled.
    /// </summary>
    private bool _enabled = false;
    
    /// <summary>
    /// Contains the total error for the epoch.
    /// </summary>
    private double _totalError = 0;
    
    #region SHUFFLED RANDOM NUMBER
    /// <summary>
    /// Used to ensure the same shuffle order is used for each neural network for the same epoch.
    /// </summary>
    private static readonly ConcurrentDictionary<int /*epoch*/, int /*random number*/> s_shuffledRandomNumber = [];
    #endregion

    /// <summary>
    /// Reset static variables, currently the shuffled random number.
    /// </summary>
    internal static void ResetStaticProperties()
    {
        s_shuffledRandomNumber.Clear();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public MLTemperatureGraph() : base()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Initialise the neural network.
    /// </summary>
    private void Initialise()
    {
        if (KnownTemperaturesGraphPoints.Count == 0) throw new InvalidOperationException("KnownTemperatures is empty");

        // define training data based on points
        foreach (PointF p in KnownTemperaturesGraphPoints) TrainingData.Add([p.X / _width, p.Y / _height]);

        ResetLearning();
    }

    #region DESIGNER VISIBLE PROPERTIES
    [Description("If disabled, no ML occurs."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool Enabled
    {
        get
        {
            return _enabled;
        }
        set
        {
            if (_enabled == value) return; // don't do anything if it's the same

            _enabled = value;

            if (_enabled)
            {
                StartTraining();
            }
        }
    }

    /// <summary>
    /// Allows the user to set the weight initialization type.
    /// </summary>
    [Description("Method to default initial weights / bias."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public NeuralNetwork.InitType WeightInitType
    {
        get
        {
            return _weightInitType;
        }
        set
        {
            if (_weightInitType == value) return; // don't do anything if it's the same

            bool isEnabled = Enabled;

            Enabled = false; // stop learning, as we're changing the weight init type
            _weightInitType = value;
            Enabled = isEnabled;
        }
    }


    /// <summary>
    /// Allows the user to set whether the shuffle training data.
    /// </summary>
    [Description("Whether to shuffle data each batch."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShuffleTrainingData
    {
        get
        {
            return _shuffleTrainingData;
        }
        set
        {
            if (_shuffleTrainingData == value) return;

            bool isEnabled = Enabled;

            Enabled = false; // stop learning, as we're changing the weight init type
            _shuffleTrainingData = value;
            Enabled = isEnabled;
        }
    }

    /// <summary>
    /// Allows the user to set the optimiser type.
    /// </summary>
    [Description("Determines which optimiser type to use."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public OptimiserTypes OptimiserType
    {
        get
        {
            return _optimiserType;
        }
        set
        {
            if (_optimiserType == value) return;

            bool isEnabled = Enabled;

            Enabled = false; // stop learning, as we're changing the weight init type
            _optimiserType = value;
            Enabled = isEnabled;
        }
    }

    /// <summary>
    /// Allows the user to set the training type.
    /// </summary>
    [Description("Determines whether to train in batches or not."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public TrainingTypes TrainingType
    {
        get
        {
            return _trainingType;
        }
        set
        {
            if (_trainingType == value) return;

            bool isEnabled = Enabled;

            Enabled = false; // stop learning, as we're changing the weight init type
            _trainingType = value;
            Enabled = isEnabled;
        }
    }

    /// <summary>
    /// Allows the user to set the learning rate type.
    /// </summary>
    [Description("Determines what training type to use (reduce on plateau etc.)."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public LearningRateTypes LearningRateType
    {
        get
        {
            return _learningRateType;
        }
        set
        {
            if (_learningRateType == value) return;

            bool isEnabled = Enabled;

            Enabled = false; // stop learning, as we're changing the weight init type
            _learningRateType = value;
            Enabled = isEnabled;
        }
    }

    /// <summary>
    /// Allows the user to set the batch size.
    /// </summary>
    [Description("Determines size of batch when training by batch."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int BatchSize
    {
        get
        {
            return _batchSize;
        }
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value), "Batch size must be at least 1");
            if (value > TrainingData.Count && TrainingData.Count > 0) throw new ArgumentOutOfRangeException(nameof(value), "Batch size must be less than or equal to the training data size.");

            if (_batchSize == value) return;

            bool isEnabled = Enabled;

            Enabled = false; // stop learning, as we're changing the weight init type
            _batchSize = value;
            Enabled = isEnabled;
        }
    }

    /// <summary>
    /// Allows the user to determine the number of previous predictions to plot.
    /// </summary>
    [Description("How many prior prediction lines to plot."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int NumberOfPreviousPredictionsToPlot
    {
        get
        {
            return _numberOfPreviousPredictions;
        }
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "A negative number makes no sense!");
            if (value > 30) throw new ArgumentOutOfRangeException(nameof(value), "Limit is 30, which is excessive in itself.");

            if (_numberOfPreviousPredictions == value) return;

            _numberOfPreviousPredictions = value;
        }
    }

    /// <summary>
    /// Allows the user to set the layers.
    /// </summary>
    [Description("This is the number of neurons in each layer."), Category("Machine Learning")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int[] Layers
    {
        get
        {
            return _layers;
        }
        set
        {
            if (_layers.SequenceEqual(value)) return;

            bool isEnabled = Enabled;

            Enabled = false; // stop learning, as we're changing the layers
            _layers = value;
            Enabled = isEnabled; // start learning again
        }
    }
    #endregion

    /// <summary>
    /// Starts the training by starting the timer. Done as a method to make the code more readable.
    /// </summary>
    private void StartTraining()
    {
        ResetLearning(false);
    }

    /// <summary>
    /// Resets the learning. It waits for the current tick to finish, before resetting the learning.
    /// </summary>
    /// <param name="recreateNN">Whether to recreate the neural network.</param>
    internal void ResetLearning(bool recreateNN = false)
    {
        if (recreateNN)
        {
            _neuralNetwork = new NeuralNetwork(_layers, _weightInitType, _optimiserType == OptimiserTypes.Adam);
            _neuralNetwork!.LearningRate = c_initialLearningRate;
        }

        // something changed that impacts the learning, so reset our "epoch" and batch.
        _epoch = 0;
        _batchIndex = 0;

        bestValidationLoss = double.MaxValue;
        _batchSize = 12;
        _shuffleTrainingData = false;
        epochsSinceImprovement = 0;
        s_shuffledRandomNumber.Clear();

        // last predictions are voided, as we're starting again
        _lastPredictionsListOfTemperatures.Clear();

        if (_neuralNetwork is not null) _neuralNetwork!.LearningRate = c_initialLearningRate;
    }

    /// <summary>
    /// Used to rank the neural networks.
    /// </summary>
    /// <returns></returns>
    internal (float, int) GetErrorAndEpoch()
    {
        return ((float)_totalError, _epoch);
    }

    /// <summary>
    /// Train the neural network in a random order using training data.
    /// </summary>
    public double TrainTheNeuralNetwork()
    {
        if (_neuralNetwork is null) return 0; // shouldn't happen, but just in case!

        if (_epoch >= s_EpochLimit)
        {
            return 0;
        }

        try
        {
            if (_trainingType == TrainingTypes.Batch)
            {
                TrainInBatches(TrainingData);
            }
            else
            {
                for (int i = 0; i < TrainingData.Count; i++)
                {
                    double[] d = TrainingData[i];

                    _neuralNetwork.BackPropagate([d[0]], [d[1]]);
                }

                ShuffleTheTrainingDataIfRequired();
            }

            for (int i = 0; i < TrainingData.Count; i++)
            {
                double[] d = TrainingData[i];

                double[] result = _neuralNetwork.FeedForward([d[0]]); /*d[0]=input*/
                _totalError += Math.Abs(d[1] - result[0]) * _height; /*d[1]=expected, result[0]=actual*/
            }

            _totalError /= TrainingData.Count;

            ApplyLearningRateAdjustments(_totalError / _height);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message + ex.StackTrace);
        }

        return _totalError;
    }

    /// <summary>
    /// Apply learning rate adjustments. We support a number of standard approaches.
    /// The idea is to adjust (usually reduce) the learning rate based on the error, or the epoch.
    /// This leads to a stable learning process.
    /// </summary>
    /// <param name="totalError"></param>
    private void ApplyLearningRateAdjustments(double totalError)
    {
        switch (_learningRateType)
        {
            case LearningRateTypes.StepDecay:
                ApplyStepDecay();
                break;

            case LearningRateTypes.ExpoDecay:
                ApplyExponentialDecay();
                break;

            case LearningRateTypes.ReduceOnPlateau:
                ApplyReduceOnPlateau((float)totalError);
                break;

            case LearningRateTypes.CosineAnnealing:
                ApplyCosineAnnealing();
                break;

            case LearningRateTypes.Cyclical:
                ApplyCyclicalLearningRate();
                break;

            default:
                break; // no adjustment
        }
    }

    /// <summary>
    /// Train in batches.
    /// </summary>
    /// <param name="data"></param>
    /// <returns>Error (expected vs. actual) for the batch.</returns>
    private void TrainInBatches(List<double[]> data)
    {
        if (_neuralNetwork is null) return;

        for (int i = _batchIndex; i < _batchIndex + _batchSize; i++)
        {
            int index = i >= data.Count ? i - data.Count : i;

            double[] d = data[index];
            _neuralNetwork!.BackPropagate([d[0]], [d[1]]);
        }

        _batchIndex += _batchSize;

        // if we've gone past the end, shuffle if required.
        if (_batchIndex >= data.Count)
        {
            if (_shuffleTrainingData)
            {
                ShuffleTheTrainingDataIfRequired();
                _batchIndex = 0;
            }
            else
            {
                _batchIndex -= data.Count;
            }
        }
    }

    /// <summary>
    /// Shuffle the training data, if required.
    /// </summary>
    private void ShuffleTheTrainingDataIfRequired()
    {
        if (!_shuffleTrainingData) return;

        List<double[]> newData = [];

        // ensure shuffle for an epoch is the same each time (for each neural network)

        if (!s_shuffledRandomNumber.ContainsKey(_epoch))
        {
            s_shuffledRandomNumber.TryAdd(_epoch, ReproduceablePseudoRandomNumberGenerator.GetNextRandomInt());
        }

        Random random = new(s_shuffledRandomNumber[_epoch]);

        newData.AddRange((List<double[]>)([.. TrainingData.OrderBy(x => random.Next())]));

        TrainingData.Clear();
        TrainingData.AddRange(newData);

        // remove the previous epoch's random number, as it's no longer needed
        if (s_shuffledRandomNumber.ContainsKey(_epoch - 2))
        {
            s_shuffledRandomNumber.Remove(_epoch - 2, out _);
        }
    }

    /// <summary>
    /// Every "stepSize" epochs, the learning rate is multiplied by "decayRate".
    /// </summary>
    private void ApplyStepDecay()
    {
        if (_epoch % c_stepSize != 0) return;

        float currentLearningRate = _neuralNetwork!.LearningRate * c_decayRate;
        currentLearningRate = Math.Max(currentLearningRate, c_minDelta / 10);

        _neuralNetwork.LearningRate = currentLearningRate;
    }

    /// <summary>
    /// When the overall loss is small enough, the learning rate is reduced by "minDelta". 
    /// If the loss doesn't improve after "patience" epochs, the learning rate is multiplied by "decayRate".
    /// </summary>
    /// <param name="validationLoss">The sum(abs(expected-actual))</param>
    private void ApplyReduceOnPlateau(double validationLoss)
    {
        float currentLearningRate = _neuralNetwork!.LearningRate;

        // if the loss is less than the best loss, we're improving
        if (validationLoss < bestValidationLoss - c_minDelta)
        {
            bestValidationLoss = validationLoss;
            epochsSinceImprovement = 0;

            currentLearningRate -= c_minDelta;
        }
        else
        {
            // we're not improving, so reduce the learning rate
            epochsSinceImprovement++;

            if (epochsSinceImprovement >= c_patience)
            {
                currentLearningRate *= c_decayRate;
                epochsSinceImprovement = 0;
            }
        }

        // don't let the learning rate get too small
        if (currentLearningRate < c_minDelta) currentLearningRate = c_minDelta;

        _neuralNetwork.LearningRate = currentLearningRate;
    }

    /// <summary>
    /// A cosine annealing learning rate. The learning rate oscillates between "initialLearningRate" and "0".
    /// It is a cosine wave, with the period being "totalEpochs".
    /// </summary>
    private void ApplyCosineAnnealing()
    {
        float currentLearningRate = c_initialLearningRate * (float)(0.5 * (1 + Math.Cos(Math.PI * _epoch / s_EpochLimit)));

        _neuralNetwork!.LearningRate = currentLearningRate;
    }

    /// <summary>
    /// Learning rate is scaled every epoch, raising the decay rate to the epoch.
    /// </summary>
    private void ApplyExponentialDecay()
    {
        float currentLearningRate = Math.Max(c_initialLearningRate * (float)Math.Pow(c_decayRate, _epoch), c_minDelta / 100); // max to ensure it doesn't go too small.

        _neuralNetwork!.LearningRate = currentLearningRate;
    }

    /// <summary>
    /// A cyclical learning rate. The learning rate oscillates between "baseLearningRate" and "maxLearningRate".
    /// It is a triangular wave, with the period being "2 * stepSize".
    /// </summary>
    private void ApplyCyclicalLearningRate()
    {
        float cycle = (float)Math.Floor((double)1 + _epoch / (2 * c_stepSize));
        float x = Math.Abs(_epoch / (float)c_stepSize - 2 * cycle + 1);

        float currentLearningRate = c_baseLearningRate + (c_maxLearningRate - c_baseLearningRate) * Math.Max(0, 1 - x);

        _neuralNetwork!.LearningRate = currentLearningRate;
    }

    /// <summary>
    /// Draws the predicted temperatures as a line graph, as well as the older predictions in a lighter shade.
    /// </summary>
    /// <param name="pointsPredictedTemperature"></param>
    /// <param name="error"></param>
    private void PlotPredictedTemperatures(List<PointF> pointsPredictedTemperature, ref double error)
    {
        if (BitmapBaseGraph is null) return;

        Bitmap bitmap = new(BitmapBaseGraph); // don't change to "using". This gets assigned to a PictureBox.Image

        // we've cloned an image with "x" and bars, that we update
        using Graphics graphics = Graphics.FromImage(bitmap);

        graphics.SmoothingMode = SmoothingMode.HighQuality; // we don't want jagged lines

        #region PLOT PREVIOUS PREDICTIONS
        // this draws older predictions in a lighter shade, so you can see the learning
        int cnt = _numberOfPreviousPredictions - _lastPredictionsListOfTemperatures.Count;

        foreach (var priorPoints in _lastPredictionsListOfTemperatures)
        {
            int argbValue = 80 + cnt * 3; // new predictions are stronger in shade

            _previousPredictedTemperaturesPen.Color = Color.FromArgb(argbValue - 75, argbValue, argbValue, argbValue);

            graphics.DrawLines(pen: _previousPredictedTemperaturesPen, priorPoints.ToArray());

            ++cnt;
        }
        #endregion

        #region PLOT LINE OF TEMPERATURES PER MONTH GENERATED BY THE NEURAL NETWORK
        // make a list of predicted temperature points, so we can issue a GDI draw lines
        List<PointF> points = [];

        foreach (PointF point in pointsPredictedTemperature)
        {
            if (point.Y < 0 || point.Y > _height || point.X < 0 || point.X > _width) continue; // provide some leeway

            points.Add(new PointF(point.X, _height - point.Y));
        }

        // draw the current prediction (line graph)
        if (points.Count > 1) // if all are out of bounds, 0 => error during DrawLines(), you can't draw a line with less than 2 points
        {
            graphics.DrawLines(pen: _penPredictedTemperaturePen, points.ToArray());
        }

        // write the amount of error spread across the 12 months. More than 1000 isn't worth writing in full (it's a end-training event), it could be a 16 digit number!
        string errorAmount = error > 1000 ? "> 1000°F" : $"{error:F1}°F";
        SizeF sizeOfErrorAmount = graphics.MeasureString($"{errorAmount}", s_arial10font);

        // write error at the centre/bottom of the bitmap
        graphics.DrawString(errorAmount, s_arial10font, Brushes.White, new PointF(_width / 2 - sizeOfErrorAmount.Width / 2, _height - 20));

        graphics.Flush();

        UpdateGraph(bitmap);
        #endregion

        if (_overallRank > 0)
        {
            graphics.DrawString($"{_overallRank:F2}", s_arial10font, Brushes.White, new PointF(5, 5));
        }

        if (points.Count < 2) return; // nothing to add of value, min of 2 required

        // we maintain a list of the last 30 predictions, so we can draw them in a lighter shade
        _lastPredictionsListOfTemperatures.Add(points);

        if (_lastPredictionsListOfTemperatures.Count > _numberOfPreviousPredictions) _lastPredictionsListOfTemperatures.RemoveAt(0);

        // maintain a list of last readings, so we can stop only if they are close enough
        _lastNReadings.Add((float)error);

        // limit the number of readings we keep.
        if (_lastNReadings.Count > c_numberOfStableAccurateReadings) _lastNReadings.RemoveAt(0);

        error = _lastNReadings.Max(); // "N" readings, so we only stop if they are all close enough
    }

    /// <summary>
    /// Train and plot.
    /// </summary>
    public void IterativelyTrainAndDrawGraph()
    {
        if (_neuralNetwork is null) return; // we require this to predict, and plot
        if (!_enabled) return;

        if (_height < 12 || _width < 12) return; // don't draw if too small

        double error = TrainTheNeuralNetwork();

        // we've reached the epoch limit?
        // we're close enough or we're way off?
        if (_epoch >= s_EpochLimit)
        {
            _enabled = false; // this is our last paint of the graph, we're done
        }

        if (_enabled &&  ++_epoch % c_updateFrequency != 0)
        {
            return; // if we're enabled, we plot every 5 epochs or so. If we're not enabled, it happened in this iteration, we draw it one last time.
        }

        UpdateGraphCaptionToShowEpochAndSettings();

        List<PointF> pointsPredictedTemperature = [];

        int lastX = -1;

        float minX = KnownTemperaturesGraphPoints[0].X - BarWidthForAMonth / 2;
        float maxX = KnownTemperaturesGraphPoints[^1].X + BarWidthForAMonth / 2;

        // Plot the "predicted"/"learnt" output.
        // Note: the neural network is trained on the average known temperatures for a MONTH, and then we predict the temperatures
        // for each DAY of the year.
        for (float x = minX; x < maxX; x += _width / 366f) // 366 days in 2012
        {
            // reduce the number of points we plot, so it's faster
            if ((int)x == lastX || (int)x == lastX + 1) continue;

            lastX = (int)x;
            pointsPredictedTemperature.Add(new PointF(x, (int)Math.Round(_neuralNetwork.FeedForward([x / _width])[0] * _height)));
        }

        PlotPredictedTemperatures(pointsPredictedTemperature, ref error);

        if ((error > 0 && error <= c_desiredAccuracyLevel) || error > 200f)
        {
            _enabled = false; // this is our last paint of the graph, we're done
        }
    }

    /// <summary>
    /// Updates the label based on the ML settings. This ensures it is configured as intended.
    /// </summary>
    private void UpdateGraphCaptionToShowEpochAndSettings()
    {
        string label = $"{_epoch}";

        if (_optimiserType == OptimiserTypes.Adam) label += " Adam";
        if (_trainingType == TrainingTypes.Batch) label += " Batch";
        if (_shuffleTrainingData) label += " Shuffle";

        label += $" {_weightInitType} {_learningRateType} lr={_neuralNetwork!.LearningRate:F6}";

        UpdateLabel(label);
    }

    /// <summary>
    /// Handles the resize event.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnSizeChanged(EventArgs e)
    {
        bool isEnabled = Enabled;

        // stop it updating whilst we're changing the size
        Enabled = false;

        _lastPredictionsListOfTemperatures.Clear();

        base.OnSizeChanged(e);

        // now we've changed the size, we can start again

        Enabled = isEnabled;
    }

    /// <summary>
    /// When the control is loaded, we initialise the neural network and start the timer.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Initialise();
    }

    /// <summary>
    /// Outputs the weights and biases of the neural network.
    /// </summary>
    /// <returns></returns>
    internal string DumpNetwork()
    {
        return _neuralNetwork?.DumpOfWeightingsAndBiases() ?? "";
    }

    /// <summary>
    /// Set the rank number on the graph.
    /// </summary>
    /// <param name="rankNumber"></param>
    internal void SetRank(int rankNumber, float overallRank = 0)
    {
        Bitmap bitmap = GetGraphImage();

        // we've cloned an image with "x" and bars, that we update
        using Graphics graphics = Graphics.FromImage(bitmap);

        graphics.SmoothingMode = SmoothingMode.HighQuality; // we don't want jagged lines

        // write error at the centre/bottom of the bitmap
        SizeF sizeOfRank = graphics.MeasureString($"{rankNumber}", s_arial20font);
        graphics.DrawString($"{rankNumber}", s_arial20font, Brushes.White, new PointF(_width / 2 - sizeOfRank.Width / 2, _height / 2 - sizeOfRank.Height / 2));

        if (overallRank > 0)
        {
            graphics.DrawString($"{overallRank}", s_arial10font, Brushes.White, new PointF(5, 5));
            _overallRank = overallRank;
        }

        graphics.Flush();

        UpdateGraph(bitmap);
    }    
}