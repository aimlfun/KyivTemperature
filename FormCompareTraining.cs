using Timer = System.Windows.Forms.Timer;

namespace Temperature;

/// <summary>
/// Comparing optimiser, and learning approaches.
/// From posting 
/// </summary>
public partial class FormLinearRegression : Form
{
    #region BENCHMARK RANKING
    /// <summary>
    /// Tracks overall average of ranks per graph
    /// </summary>
    private readonly Dictionary<int, float> _ranksPerTag = [];

    /// <summary>
    /// Number of runs of the benchmark, used to compute average rank.
    /// </summary>
    private int _benchmarkRuns = 0;

    /// <summary>
    /// Indicates whether it is in benchmark mode.
    /// </summary>
    private bool _benchmarkMode = false;
    #endregion

    /// <summary>
    /// Used to check if all graphs have finished.
    /// </summary>
    private readonly Timer _trainingTimer = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public FormLinearRegression()
    {
        ReproduceablePseudoRandomNumberGenerator.SetSeed(ReproduceablePseudoRandomNumberGenerator.c_seedForPseudoRandomNumberGenerator);
        InitializeComponent();

        _trainingTimer.Tick += TrainingTimer_Tick;
        _trainingTimer.Interval = 1; // 2 seconds
    }

    /// <summary>
    /// Used to lock whilst we are attempting to prevent re-entrancy.
    /// </summary>
    private readonly Lock _tickLock = new();

    /// <summary>
    /// Used to prevent re-entrancy.
    /// </summary>
    private bool _inTick = false;

    /// <summary>
    /// Timer event, to train the neural networks.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrainingTimer_Tick(object? sender, EventArgs e)
    {
        lock (_tickLock)
        {
            if (_inTick) return; // we took too long, ignore this tick.

            _inTick = true;
        }

        bool finished = true;

        // iterate over each graph and train it
        foreach (MLTemperatureGraph graph in Controls.OfType<MLTemperatureGraph>())
        {
            graph.IterativelyTrainAndDrawGraph();
            finished &= !graph.Enabled; // when all are not enabled, we are done
        }

        // check if all graphs have finished
        if (finished) NetworkTrainingHasFinished();

        _inTick = false;
    }

    /// <summary>
    /// Rank them, and try again.
    /// </summary>
    private void NetworkTrainingHasFinished()
    {
        _trainingTimer.Stop();

        RankNetworks();

        // in benchmark mode, we infinitely loop, trying different seeds.
        if (_benchmarkMode)
        {
            textBoxSeed.Text = (int.Parse(textBoxSeed.Text) + 1).ToString();
            Application.DoEvents();

            ButtonApply_Click(null, null);
        }
    }
    
    /// <summary>
    /// Display the rank of each neural network.
    /// </summary>
    private void RankNetworks()
    {
        // iterate over each graph and compare the results
        Dictionary<double, List<(int, MLTemperatureGraph)>> ranks = [];

        foreach (MLTemperatureGraph graph in Controls.OfType<MLTemperatureGraph>())
        {
            (float, int) result = graph.GetErrorAndEpoch();

            // do something with the result

            double score = result.Item1;

            if (ranks.TryGetValue(score, out List<(int, MLTemperatureGraph)>? existingListOfGraphsWithRank))
            {
                existingListOfGraphsWithRank.Add((result.Item2, graph));
            }
            else
            {
                ranks.Add(score, [(result.Item2, graph)]);

                // sort the AIs by rank
                ranks[score].Sort((x, y) => x.Item1.CompareTo(y.Item1));
            }
        }

        ++_benchmarkRuns;

        // sort the ranks
        int rankNumber = 1;

        foreach (var rank in ranks.OrderBy(x => x.Key))
        {
            foreach ((_, MLTemperatureGraph graph) in rank.Value)
            {
                if(graph.Tag == null) continue;

                int tag = int.Parse((string)graph.Tag);

                if (_ranksPerTag.ContainsKey(tag))
                {
                    _ranksPerTag[tag] += rankNumber;
                }
                else
                {
                    _ranksPerTag[tag] = rankNumber;
                }

                graph.SetRank(rankNumber, _ranksPerTag[tag] / _benchmarkRuns);
                rankNumber++;
            }
        }

        labelRun.Text = $"Run: {_benchmarkRuns}";
    }


    /// <summary>
    /// Load event. Set layers for each MLTemperatureGraph, so all have the same neural network.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormLinearRegression_Load(object sender, EventArgs e)
    {
        EnableDisableGraph(true);
    }

    /// <summary>
    /// Enable the user to change the neural network layers and seed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonApply_Click(object? sender, EventArgs? e)
    {
        if (!IsValidLayersAndSeed(out int[] layers, out int seed, out int epochLimit)) return;

        // disable the graphs while we update ALL of them

        EnableDisableGraph(false);

        // prefix layers with 1 and suffix with 1 for input and output layers.
        layers = [1, .. layers, 1];

        // set the seed so all the networks are the same.
        MLTemperatureGraph.ResetStaticProperties();
        ReproduceablePseudoRandomNumberGenerator.SetSeed(seed);
        BiasWeightingCache.Reset();
        MLTemperatureGraph.EpochLimit = epochLimit; // this is a limit for ALL graphs

        // reset the neural network based on layers and seed
        foreach (MLTemperatureGraph graph in Controls.OfType<MLTemperatureGraph>())
        {
            graph.Layers = layers; // update the layers
            graph.ResetLearning(true);
        }

        // back to epoching...
        EnableDisableGraph(true);
        
        _trainingTimer.Start();
    }

    /// <summary>
    /// Enable or disable the graphs.
    /// </summary>
    /// <param name="enable">true: enable</param>
    private void EnableDisableGraph(bool enable)
    {
        foreach (MLTemperatureGraph graph in Controls.OfType<MLTemperatureGraph>())
        {
            graph.Enabled = enable;
        }
    }

    /// <summary>
    /// Validate the layers and seed.
    /// </summary>
    /// <param name="layers"></param>
    /// <param name="seed"></param>
    /// <param name="epochLimit"></param>
    /// <returns></returns>
    private bool IsValidLayersAndSeed(out int[] layers, out int seed, out int epochLimit)
    {
        layers = [];
        seed = 1;
        epochLimit = 1;

        // validate textBoxNetworkLayers, it should be a comma delimited list of integers. None of the integers may be above 20. There can be 5 items in the list maximum. There must be at least 1 item

        if (string.IsNullOrWhiteSpace(textBoxNetworkLayers.Text))
        {
            textBoxNetworkLayers.Focus();
            MessageBox.Show("Please enter a list of hidden neuron layers.");
            return false;
        }

        string[] layerStrings = textBoxNetworkLayers.Text.Split(',');
        List<int> tempLayers = [];

        foreach (var layerString in layerStrings)
        {
            if (!int.TryParse(layerString, out int layer) || layer > 50 || layer < 1)
            {
                textBoxNetworkLayers.Focus();
                MessageBox.Show("Each layer must be an integer have been 1 and 50 neurons.");
                return false;
            }
            tempLayers.Add(layer);
        }

        if (tempLayers.Count > 5)
        {
            textBoxNetworkLayers.Focus();
            MessageBox.Show("There can be a maximum of 5 layers.");
            return false;
        }

        if (tempLayers.Count == 0)
        {
            textBoxNetworkLayers.Focus();
            MessageBox.Show("There must be at least 1 layer.");
            return false;
        }

        layers = [.. tempLayers];

        // validate textBoxSeed, it should be an integer.
        if (!int.TryParse(textBoxSeed.Text, out seed))
        {
            textBoxSeed.Focus();
            MessageBox.Show("Please enter a valid seed (between int.MinValue and int.MaxValue).");
            return false;
        }

        // this is how many epochs we will run for.
        if (!int.TryParse(textBoxEpochLimit.Text, out epochLimit))
        {
            textBoxEpochLimit.Focus();
            MessageBox.Show("Please enter a valid epoch limit (between 1 and int.MaxValue).");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Stop the graphs from running when the form is closing.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormLinearRegression_FormClosing(object sender, FormClosingEventArgs e)
    {
        _trainingTimer.Stop();
        _benchmarkMode = false;

        EnableDisableGraph(false);
    }

    /// <summary>
    /// This will repeatedly run the benchmark, trying different seeds.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonBenchmark_Click(object sender, EventArgs e)
    {
        // toggle the benchmark mode

        // if we are in benchmark mode, stop it, if we are not in benchmark mode, start it.
        // note: it won't stop immediately, it will stop after the current run is finished.
        if (_benchmarkMode)
        {
            _benchmarkMode = false;
            buttonBenchmark.Text = "Benchmark";
            return;
        }

        _benchmarkMode = true;
        labelRun.Text = "Running...";
        buttonBenchmark.Text = "Stop";

        // start the benchmark
        ButtonApply_Click(null, null);
    }
}