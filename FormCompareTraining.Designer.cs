namespace Temperature;

partial class FormLinearRegression
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        mlTemperatureGraph1 = new MLTemperatureGraph();
        mlTemperatureGraph2 = new MLTemperatureGraph();
        mlTemperatureGraph3 = new MLTemperatureGraph();
        mlTemperatureGraph4 = new MLTemperatureGraph();
        mlTemperatureGraph5 = new MLTemperatureGraph();
        mlTemperatureGraph6 = new MLTemperatureGraph();
        mlTemperatureGraph7 = new MLTemperatureGraph();
        mlTemperatureGraph8 = new MLTemperatureGraph();
        mlTemperatureGraph9 = new MLTemperatureGraph();
        mlTemperatureGraph10 = new MLTemperatureGraph();
        mlTemperatureGraph11 = new MLTemperatureGraph();
        mlTemperatureGraph12 = new MLTemperatureGraph();
        mlTemperatureGraph13 = new MLTemperatureGraph();
        mlTemperatureGraph14 = new MLTemperatureGraph();
        mlTemperatureGraph15 = new MLTemperatureGraph();
        mlTemperatureGraph16 = new MLTemperatureGraph();
        mlTemperatureGraph17 = new MLTemperatureGraph();
        mlTemperatureGraph18 = new MLTemperatureGraph();
        mlTemperatureGraph19 = new MLTemperatureGraph();
        mlTemperatureGraph20 = new MLTemperatureGraph();
        mlTemperatureGraph21 = new MLTemperatureGraph();
        mlTemperatureGraph22 = new MLTemperatureGraph();
        mlTemperatureGraph23 = new MLTemperatureGraph();
        mlTemperatureGraph24 = new MLTemperatureGraph();
        mlTemperatureGraph25 = new MLTemperatureGraph();
        mlTemperatureGraph26 = new MLTemperatureGraph();
        mlTemperatureGraph27 = new MLTemperatureGraph();
        mlTemperatureGraph28 = new MLTemperatureGraph();
        mlTemperatureGraph29 = new MLTemperatureGraph();
        mlTemperatureGraph30 = new MLTemperatureGraph();
        mlTemperatureGraph31 = new MLTemperatureGraph();
        mlTemperatureGraph32 = new MLTemperatureGraph();
        mlTemperatureGraph33 = new MLTemperatureGraph();
        mlTemperatureGraph34 = new MLTemperatureGraph();
        mlTemperatureGraph35 = new MLTemperatureGraph();
        mlTemperatureGraph36 = new MLTemperatureGraph();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        label6 = new Label();
        label7 = new Label();
        label8 = new Label();
        label9 = new Label();
        label10 = new Label();
        label11 = new Label();
        label12 = new Label();
        label13 = new Label();
        label14 = new Label();
        label15 = new Label();
        label16 = new Label();
        label17 = new Label();
        label18 = new Label();
        label19 = new Label();
        label20 = new Label();
        label21 = new Label();
        textBoxNetworkLayers = new TextBox();
        label22 = new Label();
        textBoxSeed = new TextBox();
        buttonApply = new Button();
        textBoxEpochLimit = new TextBox();
        label23 = new Label();
        buttonBenchmark = new Button();
        labelRun = new Label();
        toolTip1 = new ToolTip(components);
        SuspendLayout();
        // 
        // mlTemperatureGraph1
        // 
        mlTemperatureGraph1.BatchSize = 12;
        mlTemperatureGraph1.Enabled = false;
        mlTemperatureGraph1.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph1.LearningRateType = MLTemperatureGraph.LearningRateTypes.none;
        mlTemperatureGraph1.Location = new Point(68, 191);
        mlTemperatureGraph1.Name = "mlTemperatureGraph1";
        mlTemperatureGraph1.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph1.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph1.ShuffleTrainingData = false;
        mlTemperatureGraph1.Size = new Size(237, 103);
        mlTemperatureGraph1.TabIndex = 4;
        mlTemperatureGraph1.Tag = "6";
        mlTemperatureGraph1.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph1.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph2
        // 
        mlTemperatureGraph2.BatchSize = 12;
        mlTemperatureGraph2.Enabled = false;
        mlTemperatureGraph2.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph2.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph2.Location = new Point(311, 191);
        mlTemperatureGraph2.Name = "mlTemperatureGraph2";
        mlTemperatureGraph2.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph2.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph2.ShuffleTrainingData = false;
        mlTemperatureGraph2.Size = new Size(237, 103);
        mlTemperatureGraph2.TabIndex = 3;
        mlTemperatureGraph2.Tag = "7";
        mlTemperatureGraph2.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph2.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph3
        // 
        mlTemperatureGraph3.BatchSize = 12;
        mlTemperatureGraph3.Enabled = false;
        mlTemperatureGraph3.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph3.LearningRateType = MLTemperatureGraph.LearningRateTypes.none;
        mlTemperatureGraph3.Location = new Point(68, 443);
        mlTemperatureGraph3.Name = "mlTemperatureGraph3";
        mlTemperatureGraph3.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph3.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph3.ShuffleTrainingData = false;
        mlTemperatureGraph3.Size = new Size(237, 104);
        mlTemperatureGraph3.TabIndex = 8;
        mlTemperatureGraph3.Tag = "18";
        mlTemperatureGraph3.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph3.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph4
        // 
        mlTemperatureGraph4.BatchSize = 12;
        mlTemperatureGraph4.Enabled = false;
        mlTemperatureGraph4.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph4.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph4.Location = new Point(311, 443);
        mlTemperatureGraph4.Name = "mlTemperatureGraph4";
        mlTemperatureGraph4.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph4.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph4.ShuffleTrainingData = false;
        mlTemperatureGraph4.Size = new Size(237, 104);
        mlTemperatureGraph4.TabIndex = 7;
        mlTemperatureGraph4.Tag = "19";
        mlTemperatureGraph4.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph4.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph5
        // 
        mlTemperatureGraph5.BatchSize = 12;
        mlTemperatureGraph5.Enabled = false;
        mlTemperatureGraph5.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph5.LearningRateType = MLTemperatureGraph.LearningRateTypes.none;
        mlTemperatureGraph5.Location = new Point(68, 309);
        mlTemperatureGraph5.Name = "mlTemperatureGraph5";
        mlTemperatureGraph5.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph5.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph5.ShuffleTrainingData = false;
        mlTemperatureGraph5.Size = new Size(237, 103);
        mlTemperatureGraph5.TabIndex = 6;
        mlTemperatureGraph5.Tag = "12";
        mlTemperatureGraph5.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph5.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph6
        // 
        mlTemperatureGraph6.BatchSize = 12;
        mlTemperatureGraph6.Enabled = false;
        mlTemperatureGraph6.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph6.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph6.Location = new Point(311, 309);
        mlTemperatureGraph6.Name = "mlTemperatureGraph6";
        mlTemperatureGraph6.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph6.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph6.ShuffleTrainingData = false;
        mlTemperatureGraph6.Size = new Size(237, 103);
        mlTemperatureGraph6.TabIndex = 5;
        mlTemperatureGraph6.Tag = "13";
        mlTemperatureGraph6.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph6.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph7
        // 
        mlTemperatureGraph7.BatchSize = 12;
        mlTemperatureGraph7.Enabled = false;
        mlTemperatureGraph7.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph7.LearningRateType = MLTemperatureGraph.LearningRateTypes.none;
        mlTemperatureGraph7.Location = new Point(68, 661);
        mlTemperatureGraph7.Name = "mlTemperatureGraph7";
        mlTemperatureGraph7.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph7.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph7.ShuffleTrainingData = false;
        mlTemperatureGraph7.Size = new Size(237, 104);
        mlTemperatureGraph7.TabIndex = 12;
        mlTemperatureGraph7.Tag = "30";
        mlTemperatureGraph7.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph7.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph8
        // 
        mlTemperatureGraph8.BatchSize = 12;
        mlTemperatureGraph8.Enabled = false;
        mlTemperatureGraph8.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph8.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph8.Location = new Point(311, 661);
        mlTemperatureGraph8.Name = "mlTemperatureGraph8";
        mlTemperatureGraph8.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph8.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph8.ShuffleTrainingData = false;
        mlTemperatureGraph8.Size = new Size(237, 104);
        mlTemperatureGraph8.TabIndex = 11;
        mlTemperatureGraph8.Tag = "31";
        mlTemperatureGraph8.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph8.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph9
        // 
        mlTemperatureGraph9.BatchSize = 12;
        mlTemperatureGraph9.Enabled = false;
        mlTemperatureGraph9.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph9.LearningRateType = MLTemperatureGraph.LearningRateTypes.none;
        mlTemperatureGraph9.Location = new Point(68, 552);
        mlTemperatureGraph9.Name = "mlTemperatureGraph9";
        mlTemperatureGraph9.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph9.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph9.ShuffleTrainingData = false;
        mlTemperatureGraph9.Size = new Size(237, 104);
        mlTemperatureGraph9.TabIndex = 10;
        mlTemperatureGraph9.Tag = "24";
        mlTemperatureGraph9.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph9.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph10
        // 
        mlTemperatureGraph10.BatchSize = 12;
        mlTemperatureGraph10.Enabled = false;
        mlTemperatureGraph10.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph10.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph10.Location = new Point(311, 552);
        mlTemperatureGraph10.Name = "mlTemperatureGraph10";
        mlTemperatureGraph10.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph10.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph10.ShuffleTrainingData = false;
        mlTemperatureGraph10.Size = new Size(237, 104);
        mlTemperatureGraph10.TabIndex = 9;
        mlTemperatureGraph10.Tag = "25";
        mlTemperatureGraph10.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph10.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph11
        // 
        mlTemperatureGraph11.BatchSize = 12;
        mlTemperatureGraph11.Enabled = false;
        mlTemperatureGraph11.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph11.LearningRateType = MLTemperatureGraph.LearningRateTypes.StepDecay;
        mlTemperatureGraph11.Location = new Point(554, 661);
        mlTemperatureGraph11.Name = "mlTemperatureGraph11";
        mlTemperatureGraph11.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph11.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph11.ShuffleTrainingData = false;
        mlTemperatureGraph11.Size = new Size(237, 104);
        mlTemperatureGraph11.TabIndex = 24;
        mlTemperatureGraph11.Tag = "32";
        mlTemperatureGraph11.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph11.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph12
        // 
        mlTemperatureGraph12.BatchSize = 12;
        mlTemperatureGraph12.Enabled = false;
        mlTemperatureGraph12.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph12.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph12.Location = new Point(797, 661);
        mlTemperatureGraph12.Name = "mlTemperatureGraph12";
        mlTemperatureGraph12.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph12.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph12.ShuffleTrainingData = false;
        mlTemperatureGraph12.Size = new Size(237, 104);
        mlTemperatureGraph12.TabIndex = 23;
        mlTemperatureGraph12.Tag = "33";
        mlTemperatureGraph12.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph12.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph13
        // 
        mlTemperatureGraph13.BatchSize = 12;
        mlTemperatureGraph13.Enabled = false;
        mlTemperatureGraph13.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph13.LearningRateType = MLTemperatureGraph.LearningRateTypes.StepDecay;
        mlTemperatureGraph13.Location = new Point(554, 552);
        mlTemperatureGraph13.Name = "mlTemperatureGraph13";
        mlTemperatureGraph13.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph13.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph13.ShuffleTrainingData = false;
        mlTemperatureGraph13.Size = new Size(237, 104);
        mlTemperatureGraph13.TabIndex = 22;
        mlTemperatureGraph13.Tag = "26";
        mlTemperatureGraph13.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph13.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph14
        // 
        mlTemperatureGraph14.BatchSize = 12;
        mlTemperatureGraph14.Enabled = false;
        mlTemperatureGraph14.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph14.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph14.Location = new Point(797, 552);
        mlTemperatureGraph14.Name = "mlTemperatureGraph14";
        mlTemperatureGraph14.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph14.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph14.ShuffleTrainingData = false;
        mlTemperatureGraph14.Size = new Size(237, 104);
        mlTemperatureGraph14.TabIndex = 21;
        mlTemperatureGraph14.Tag = "27";
        mlTemperatureGraph14.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph14.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph15
        // 
        mlTemperatureGraph15.BatchSize = 12;
        mlTemperatureGraph15.Enabled = false;
        mlTemperatureGraph15.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph15.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph15.Location = new Point(311, 82);
        mlTemperatureGraph15.Name = "mlTemperatureGraph15";
        mlTemperatureGraph15.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph15.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph15.ShuffleTrainingData = false;
        mlTemperatureGraph15.Size = new Size(237, 103);
        mlTemperatureGraph15.TabIndex = 1;
        mlTemperatureGraph15.Tag = "1";
        mlTemperatureGraph15.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph15.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph16
        // 
        mlTemperatureGraph16.BatchSize = 12;
        mlTemperatureGraph16.Enabled = false;
        mlTemperatureGraph16.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph16.LearningRateType = MLTemperatureGraph.LearningRateTypes.none;
        mlTemperatureGraph16.Location = new Point(68, 82);
        mlTemperatureGraph16.Name = "mlTemperatureGraph16";
        mlTemperatureGraph16.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph16.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph16.ShuffleTrainingData = false;
        mlTemperatureGraph16.Size = new Size(237, 103);
        mlTemperatureGraph16.TabIndex = 2;
        mlTemperatureGraph16.Tag = "0";
        mlTemperatureGraph16.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph16.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph17
        // 
        mlTemperatureGraph17.BatchSize = 12;
        mlTemperatureGraph17.Enabled = false;
        mlTemperatureGraph17.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph17.LearningRateType = MLTemperatureGraph.LearningRateTypes.StepDecay;
        mlTemperatureGraph17.Location = new Point(554, 443);
        mlTemperatureGraph17.Name = "mlTemperatureGraph17";
        mlTemperatureGraph17.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph17.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph17.ShuffleTrainingData = false;
        mlTemperatureGraph17.Size = new Size(237, 104);
        mlTemperatureGraph17.TabIndex = 20;
        mlTemperatureGraph17.Tag = "20";
        mlTemperatureGraph17.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph17.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph18
        // 
        mlTemperatureGraph18.BatchSize = 12;
        mlTemperatureGraph18.Enabled = false;
        mlTemperatureGraph18.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph18.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph18.Location = new Point(797, 443);
        mlTemperatureGraph18.Name = "mlTemperatureGraph18";
        mlTemperatureGraph18.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph18.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph18.ShuffleTrainingData = false;
        mlTemperatureGraph18.Size = new Size(237, 104);
        mlTemperatureGraph18.TabIndex = 19;
        mlTemperatureGraph18.Tag = "21";
        mlTemperatureGraph18.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph18.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph19
        // 
        mlTemperatureGraph19.BatchSize = 12;
        mlTemperatureGraph19.Enabled = false;
        mlTemperatureGraph19.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph19.LearningRateType = MLTemperatureGraph.LearningRateTypes.StepDecay;
        mlTemperatureGraph19.Location = new Point(554, 309);
        mlTemperatureGraph19.Name = "mlTemperatureGraph19";
        mlTemperatureGraph19.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph19.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph19.ShuffleTrainingData = false;
        mlTemperatureGraph19.Size = new Size(237, 103);
        mlTemperatureGraph19.TabIndex = 18;
        mlTemperatureGraph19.Tag = "14";
        mlTemperatureGraph19.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph19.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph20
        // 
        mlTemperatureGraph20.BatchSize = 12;
        mlTemperatureGraph20.Enabled = false;
        mlTemperatureGraph20.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph20.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph20.Location = new Point(797, 309);
        mlTemperatureGraph20.Name = "mlTemperatureGraph20";
        mlTemperatureGraph20.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph20.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph20.ShuffleTrainingData = false;
        mlTemperatureGraph20.Size = new Size(237, 103);
        mlTemperatureGraph20.TabIndex = 17;
        mlTemperatureGraph20.Tag = "15";
        mlTemperatureGraph20.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph20.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph21
        // 
        mlTemperatureGraph21.BatchSize = 12;
        mlTemperatureGraph21.Enabled = false;
        mlTemperatureGraph21.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph21.LearningRateType = MLTemperatureGraph.LearningRateTypes.StepDecay;
        mlTemperatureGraph21.Location = new Point(554, 191);
        mlTemperatureGraph21.Name = "mlTemperatureGraph21";
        mlTemperatureGraph21.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph21.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph21.ShuffleTrainingData = false;
        mlTemperatureGraph21.Size = new Size(237, 103);
        mlTemperatureGraph21.TabIndex = 16;
        mlTemperatureGraph21.Tag = "8";
        mlTemperatureGraph21.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph21.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph22
        // 
        mlTemperatureGraph22.BatchSize = 12;
        mlTemperatureGraph22.Enabled = false;
        mlTemperatureGraph22.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph22.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph22.Location = new Point(797, 191);
        mlTemperatureGraph22.Name = "mlTemperatureGraph22";
        mlTemperatureGraph22.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph22.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph22.ShuffleTrainingData = false;
        mlTemperatureGraph22.Size = new Size(237, 103);
        mlTemperatureGraph22.TabIndex = 15;
        mlTemperatureGraph22.Tag = "9";
        mlTemperatureGraph22.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph22.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph23
        // 
        mlTemperatureGraph23.BatchSize = 12;
        mlTemperatureGraph23.Enabled = false;
        mlTemperatureGraph23.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph23.LearningRateType = MLTemperatureGraph.LearningRateTypes.StepDecay;
        mlTemperatureGraph23.Location = new Point(554, 82);
        mlTemperatureGraph23.Name = "mlTemperatureGraph23";
        mlTemperatureGraph23.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph23.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph23.ShuffleTrainingData = false;
        mlTemperatureGraph23.Size = new Size(237, 103);
        mlTemperatureGraph23.TabIndex = 14;
        mlTemperatureGraph23.Tag = "2";
        mlTemperatureGraph23.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph23.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph24
        // 
        mlTemperatureGraph24.BatchSize = 12;
        mlTemperatureGraph24.Enabled = false;
        mlTemperatureGraph24.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph24.LearningRateType = MLTemperatureGraph.LearningRateTypes.Cyclical;
        mlTemperatureGraph24.Location = new Point(797, 82);
        mlTemperatureGraph24.Name = "mlTemperatureGraph24";
        mlTemperatureGraph24.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph24.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph24.ShuffleTrainingData = false;
        mlTemperatureGraph24.Size = new Size(237, 103);
        mlTemperatureGraph24.TabIndex = 13;
        mlTemperatureGraph24.Tag = "3";
        mlTemperatureGraph24.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph24.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph25
        // 
        mlTemperatureGraph25.BatchSize = 12;
        mlTemperatureGraph25.Enabled = false;
        mlTemperatureGraph25.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph25.LearningRateType = MLTemperatureGraph.LearningRateTypes.ReduceOnPlateau;
        mlTemperatureGraph25.Location = new Point(1040, 661);
        mlTemperatureGraph25.Name = "mlTemperatureGraph25";
        mlTemperatureGraph25.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph25.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph25.ShuffleTrainingData = false;
        mlTemperatureGraph25.Size = new Size(237, 104);
        mlTemperatureGraph25.TabIndex = 42;
        mlTemperatureGraph25.Tag = "34";
        mlTemperatureGraph25.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph25.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph26
        // 
        mlTemperatureGraph26.BatchSize = 12;
        mlTemperatureGraph26.Enabled = false;
        mlTemperatureGraph26.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph26.LearningRateType = MLTemperatureGraph.LearningRateTypes.ReduceOnPlateau;
        mlTemperatureGraph26.Location = new Point(1040, 552);
        mlTemperatureGraph26.Name = "mlTemperatureGraph26";
        mlTemperatureGraph26.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph26.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph26.ShuffleTrainingData = false;
        mlTemperatureGraph26.Size = new Size(237, 104);
        mlTemperatureGraph26.TabIndex = 41;
        mlTemperatureGraph26.Tag = "28";
        mlTemperatureGraph26.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph26.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph27
        // 
        mlTemperatureGraph27.BatchSize = 12;
        mlTemperatureGraph27.Enabled = false;
        mlTemperatureGraph27.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph27.LearningRateType = MLTemperatureGraph.LearningRateTypes.ReduceOnPlateau;
        mlTemperatureGraph27.Location = new Point(1040, 443);
        mlTemperatureGraph27.Name = "mlTemperatureGraph27";
        mlTemperatureGraph27.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph27.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph27.ShuffleTrainingData = false;
        mlTemperatureGraph27.Size = new Size(237, 104);
        mlTemperatureGraph27.TabIndex = 40;
        mlTemperatureGraph27.Tag = "22";
        mlTemperatureGraph27.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph27.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph28
        // 
        mlTemperatureGraph28.BatchSize = 12;
        mlTemperatureGraph28.Enabled = false;
        mlTemperatureGraph28.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph28.LearningRateType = MLTemperatureGraph.LearningRateTypes.ReduceOnPlateau;
        mlTemperatureGraph28.Location = new Point(1040, 309);
        mlTemperatureGraph28.Name = "mlTemperatureGraph28";
        mlTemperatureGraph28.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph28.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph28.ShuffleTrainingData = false;
        mlTemperatureGraph28.Size = new Size(237, 103);
        mlTemperatureGraph28.TabIndex = 39;
        mlTemperatureGraph28.Tag = "16";
        mlTemperatureGraph28.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph28.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph29
        // 
        mlTemperatureGraph29.BatchSize = 12;
        mlTemperatureGraph29.Enabled = false;
        mlTemperatureGraph29.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph29.LearningRateType = MLTemperatureGraph.LearningRateTypes.ReduceOnPlateau;
        mlTemperatureGraph29.Location = new Point(1040, 191);
        mlTemperatureGraph29.Name = "mlTemperatureGraph29";
        mlTemperatureGraph29.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph29.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph29.ShuffleTrainingData = false;
        mlTemperatureGraph29.Size = new Size(237, 103);
        mlTemperatureGraph29.TabIndex = 38;
        mlTemperatureGraph29.Tag = "10";
        mlTemperatureGraph29.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph29.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph30
        // 
        mlTemperatureGraph30.BatchSize = 12;
        mlTemperatureGraph30.Enabled = false;
        mlTemperatureGraph30.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph30.LearningRateType = MLTemperatureGraph.LearningRateTypes.ReduceOnPlateau;
        mlTemperatureGraph30.Location = new Point(1040, 82);
        mlTemperatureGraph30.Name = "mlTemperatureGraph30";
        mlTemperatureGraph30.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph30.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph30.ShuffleTrainingData = false;
        mlTemperatureGraph30.Size = new Size(237, 103);
        mlTemperatureGraph30.TabIndex = 37;
        mlTemperatureGraph30.Tag = "4";
        mlTemperatureGraph30.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph30.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph31
        // 
        mlTemperatureGraph31.BatchSize = 12;
        mlTemperatureGraph31.Enabled = false;
        mlTemperatureGraph31.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph31.LearningRateType = MLTemperatureGraph.LearningRateTypes.CosineAnnealing;
        mlTemperatureGraph31.Location = new Point(1283, 661);
        mlTemperatureGraph31.Name = "mlTemperatureGraph31";
        mlTemperatureGraph31.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph31.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph31.ShuffleTrainingData = false;
        mlTemperatureGraph31.Size = new Size(237, 104);
        mlTemperatureGraph31.TabIndex = 49;
        mlTemperatureGraph31.Tag = "35";
        mlTemperatureGraph31.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph31.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph32
        // 
        mlTemperatureGraph32.BatchSize = 12;
        mlTemperatureGraph32.Enabled = false;
        mlTemperatureGraph32.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph32.LearningRateType = MLTemperatureGraph.LearningRateTypes.CosineAnnealing;
        mlTemperatureGraph32.Location = new Point(1283, 552);
        mlTemperatureGraph32.Name = "mlTemperatureGraph32";
        mlTemperatureGraph32.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph32.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph32.ShuffleTrainingData = false;
        mlTemperatureGraph32.Size = new Size(237, 104);
        mlTemperatureGraph32.TabIndex = 48;
        mlTemperatureGraph32.Tag = "29";
        mlTemperatureGraph32.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph32.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph33
        // 
        mlTemperatureGraph33.BatchSize = 12;
        mlTemperatureGraph33.Enabled = false;
        mlTemperatureGraph33.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph33.LearningRateType = MLTemperatureGraph.LearningRateTypes.CosineAnnealing;
        mlTemperatureGraph33.Location = new Point(1283, 443);
        mlTemperatureGraph33.Name = "mlTemperatureGraph33";
        mlTemperatureGraph33.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph33.OptimiserType = MLTemperatureGraph.OptimiserTypes.Adam;
        mlTemperatureGraph33.ShuffleTrainingData = false;
        mlTemperatureGraph33.Size = new Size(237, 104);
        mlTemperatureGraph33.TabIndex = 47;
        mlTemperatureGraph33.Tag = "23";
        mlTemperatureGraph33.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph33.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // mlTemperatureGraph34
        // 
        mlTemperatureGraph34.BatchSize = 12;
        mlTemperatureGraph34.Enabled = false;
        mlTemperatureGraph34.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph34.LearningRateType = MLTemperatureGraph.LearningRateTypes.CosineAnnealing;
        mlTemperatureGraph34.Location = new Point(1283, 309);
        mlTemperatureGraph34.Name = "mlTemperatureGraph34";
        mlTemperatureGraph34.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph34.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph34.ShuffleTrainingData = false;
        mlTemperatureGraph34.Size = new Size(237, 103);
        mlTemperatureGraph34.TabIndex = 46;
        mlTemperatureGraph34.Tag = "17";
        mlTemperatureGraph34.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph34.WeightInitType = NeuralNetwork.InitType.Random;
        // 
        // mlTemperatureGraph35
        // 
        mlTemperatureGraph35.BatchSize = 12;
        mlTemperatureGraph35.Enabled = false;
        mlTemperatureGraph35.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph35.LearningRateType = MLTemperatureGraph.LearningRateTypes.CosineAnnealing;
        mlTemperatureGraph35.Location = new Point(1283, 191);
        mlTemperatureGraph35.Name = "mlTemperatureGraph35";
        mlTemperatureGraph35.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph35.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph35.ShuffleTrainingData = false;
        mlTemperatureGraph35.Size = new Size(237, 103);
        mlTemperatureGraph35.TabIndex = 45;
        mlTemperatureGraph35.Tag = "11";
        mlTemperatureGraph35.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph35.WeightInitType = NeuralNetwork.InitType.Gaussian;
        // 
        // mlTemperatureGraph36
        // 
        mlTemperatureGraph36.BatchSize = 12;
        mlTemperatureGraph36.Enabled = false;
        mlTemperatureGraph36.Layers = new int[]
{
    1,
    4,
    5,
    1
};
        mlTemperatureGraph36.LearningRateType = MLTemperatureGraph.LearningRateTypes.CosineAnnealing;
        mlTemperatureGraph36.Location = new Point(1283, 82);
        mlTemperatureGraph36.Name = "mlTemperatureGraph36";
        mlTemperatureGraph36.NumberOfPreviousPredictionsToPlot = 30;
        mlTemperatureGraph36.OptimiserType = MLTemperatureGraph.OptimiserTypes.none;
        mlTemperatureGraph36.ShuffleTrainingData = false;
        mlTemperatureGraph36.Size = new Size(237, 103);
        mlTemperatureGraph36.TabIndex = 44;
        mlTemperatureGraph36.Tag = "5";
        mlTemperatureGraph36.TrainingType = MLTemperatureGraph.TrainingTypes.Normal;
        mlTemperatureGraph36.WeightInitType = NeuralNetwork.InitType.Xavier;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        label1.Location = new Point(8, 63);
        label1.Name = "label1";
        label1.Size = new Size(99, 19);
        label1.TabIndex = 25;
        label1.Text = "No Optimiser";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        label2.Location = new Point(8, 423);
        label2.Name = "label2";
        label2.Size = new Size(119, 19);
        label2.TabIndex = 26;
        label2.Text = "Adam Optimiser";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(25, 498);
        label3.Name = "label3";
        label3.Size = new Size(39, 15);
        label3.TabIndex = 27;
        label3.Text = "Xavier";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(10, 607);
        label4.Name = "label4";
        label4.Size = new Size(54, 15);
        label4.TabIndex = 28;
        label4.Text = "Gaussian";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(12, 716);
        label5.Name = "label5";
        label5.Size = new Size(52, 15);
        label5.TabIndex = 29;
        label5.Text = "Random";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(12, 351);
        label6.Name = "label6";
        label6.Size = new Size(52, 15);
        label6.TabIndex = 32;
        label6.Text = "Random";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(10, 242);
        label7.Name = "label7";
        label7.Size = new Size(54, 15);
        label7.TabIndex = 31;
        label7.Text = "Gaussian";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(25, 133);
        label8.Name = "label8";
        label8.Size = new Size(39, 15);
        label8.TabIndex = 30;
        label8.Text = "Xavier";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(641, 65);
        label9.Name = "label9";
        label9.Size = new Size(65, 15);
        label9.TabIndex = 35;
        label9.Text = "Step Decay";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(403, 65);
        label10.Name = "label10";
        label10.Size = new Size(48, 15);
        label10.TabIndex = 34;
        label10.Text = "Cyclical";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(171, 65);
        label11.Name = "label11";
        label11.Size = new Size(36, 15);
        label11.TabIndex = 33;
        label11.Text = "None";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Location = new Point(865, 65);
        label12.Name = "label12";
        label12.Size = new Size(100, 15);
        label12.TabIndex = 36;
        label12.Text = "Exponential Delay";
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.Location = new Point(1108, 65);
        label13.Name = "label13";
        label13.Size = new Size(107, 15);
        label13.TabIndex = 43;
        label13.Text = "Reduce On Plateau";
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.Location = new Point(1351, 65);
        label14.Name = "label14";
        label14.Size = new Size(100, 15);
        label14.TabIndex = 50;
        label14.Text = "Cosine Annealing";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.Location = new Point(1351, 425);
        label15.Name = "label15";
        label15.Size = new Size(100, 15);
        label15.TabIndex = 56;
        label15.Text = "Cosine Annealing";
        // 
        // label16
        // 
        label16.AutoSize = true;
        label16.Location = new Point(1108, 425);
        label16.Name = "label16";
        label16.Size = new Size(107, 15);
        label16.TabIndex = 55;
        label16.Text = "Reduce On Plateau";
        // 
        // label17
        // 
        label17.AutoSize = true;
        label17.Location = new Point(865, 425);
        label17.Name = "label17";
        label17.Size = new Size(100, 15);
        label17.TabIndex = 54;
        label17.Text = "Exponential Delay";
        // 
        // label18
        // 
        label18.AutoSize = true;
        label18.Location = new Point(641, 425);
        label18.Name = "label18";
        label18.Size = new Size(65, 15);
        label18.TabIndex = 53;
        label18.Text = "Step Decay";
        // 
        // label19
        // 
        label19.AutoSize = true;
        label19.Location = new Point(403, 425);
        label19.Name = "label19";
        label19.Size = new Size(48, 15);
        label19.TabIndex = 52;
        label19.Text = "Cyclical";
        // 
        // label20
        // 
        label20.AutoSize = true;
        label20.Location = new Point(171, 425);
        label20.Name = "label20";
        label20.Size = new Size(36, 15);
        label20.TabIndex = 51;
        label20.Text = "None";
        // 
        // label21
        // 
        label21.AutoSize = true;
        label21.Location = new Point(6, 15);
        label21.Name = "label21";
        label21.Size = new Size(91, 15);
        label21.TabIndex = 57;
        label21.Text = "Network Layers:";
        // 
        // textBoxNetworkLayers
        // 
        textBoxNetworkLayers.Location = new Point(100, 11);
        textBoxNetworkLayers.MaxLength = 30;
        textBoxNetworkLayers.Name = "textBoxNetworkLayers";
        textBoxNetworkLayers.Size = new Size(100, 23);
        textBoxNetworkLayers.TabIndex = 58;
        textBoxNetworkLayers.Text = "5,5,5";
        // 
        // label22
        // 
        label22.AutoSize = true;
        label22.Location = new Point(217, 15);
        label22.Name = "label22";
        label22.Size = new Size(83, 15);
        label22.TabIndex = 59;
        label22.Text = "Random Seed:";
        // 
        // textBoxSeed
        // 
        textBoxSeed.Location = new Point(303, 11);
        textBoxSeed.MaxLength = 8;
        textBoxSeed.Name = "textBoxSeed";
        textBoxSeed.Size = new Size(100, 23);
        textBoxSeed.TabIndex = 60;
        textBoxSeed.Text = "123";
        // 
        // buttonApply
        // 
        buttonApply.DialogResult = DialogResult.OK;
        buttonApply.Location = new Point(599, 11);
        buttonApply.Name = "buttonApply";
        buttonApply.Size = new Size(75, 23);
        buttonApply.TabIndex = 61;
        buttonApply.Text = "Apply";
        toolTip1.SetToolTip(buttonApply, "Creates network using seed, and runs for required epochs (stopping at 0.8F or 5000).");
        buttonApply.UseVisualStyleBackColor = true;
        buttonApply.Click += ButtonApply_Click;
        // 
        // textBoxEpochLimit
        // 
        textBoxEpochLimit.Location = new Point(478, 11);
        textBoxEpochLimit.MaxLength = 8;
        textBoxEpochLimit.Name = "textBoxEpochLimit";
        textBoxEpochLimit.Size = new Size(100, 23);
        textBoxEpochLimit.TabIndex = 63;
        textBoxEpochLimit.Text = "5000";
        // 
        // label23
        // 
        label23.AutoSize = true;
        label23.Location = new Point(425, 15);
        label23.Name = "label23";
        label23.Size = new Size(48, 15);
        label23.TabIndex = 62;
        label23.Text = "Epochs:";
        // 
        // buttonBenchmark
        // 
        buttonBenchmark.DialogResult = DialogResult.OK;
        buttonBenchmark.Location = new Point(1445, 11);
        buttonBenchmark.Name = "buttonBenchmark";
        buttonBenchmark.Size = new Size(75, 23);
        buttonBenchmark.TabIndex = 64;
        buttonBenchmark.Text = "Benchmark";
        toolTip1.SetToolTip(buttonBenchmark, "Runs repeatedly for a different seed, tracking performance. Pressing stop will finish after current run.");
        buttonBenchmark.UseVisualStyleBackColor = true;
        buttonBenchmark.Click += ButtonBenchmark_Click;
        // 
        // labelRun
        // 
        labelRun.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        labelRun.Location = new Point(1283, 15);
        labelRun.Name = "labelRun";
        labelRun.Size = new Size(144, 15);
        labelRun.TabIndex = 65;
        labelRun.TextAlign = ContentAlignment.MiddleRight;
        // 
        // FormLinearRegression
        // 
        AcceptButton = buttonApply;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1531, 805);
        Controls.Add(labelRun);
        Controls.Add(buttonBenchmark);
        Controls.Add(textBoxEpochLimit);
        Controls.Add(label23);
        Controls.Add(mlTemperatureGraph1);
        Controls.Add(mlTemperatureGraph2);
        Controls.Add(mlTemperatureGraph3);
        Controls.Add(mlTemperatureGraph4);
        Controls.Add(mlTemperatureGraph5);
        Controls.Add(mlTemperatureGraph6);
        Controls.Add(mlTemperatureGraph7);
        Controls.Add(mlTemperatureGraph8);
        Controls.Add(mlTemperatureGraph9);
        Controls.Add(mlTemperatureGraph10);
        Controls.Add(mlTemperatureGraph11);
        Controls.Add(mlTemperatureGraph12);
        Controls.Add(mlTemperatureGraph13);
        Controls.Add(mlTemperatureGraph14);
        Controls.Add(mlTemperatureGraph15);
        Controls.Add(mlTemperatureGraph16);
        Controls.Add(mlTemperatureGraph17);
        Controls.Add(mlTemperatureGraph18);
        Controls.Add(mlTemperatureGraph19);
        Controls.Add(mlTemperatureGraph20);
        Controls.Add(mlTemperatureGraph21);
        Controls.Add(mlTemperatureGraph22);
        Controls.Add(mlTemperatureGraph23);
        Controls.Add(mlTemperatureGraph24);
        Controls.Add(mlTemperatureGraph25);
        Controls.Add(mlTemperatureGraph26);
        Controls.Add(mlTemperatureGraph27);
        Controls.Add(mlTemperatureGraph28);
        Controls.Add(mlTemperatureGraph29);
        Controls.Add(mlTemperatureGraph30);
        Controls.Add(mlTemperatureGraph31);
        Controls.Add(mlTemperatureGraph32);
        Controls.Add(mlTemperatureGraph33);
        Controls.Add(mlTemperatureGraph34);
        Controls.Add(mlTemperatureGraph35);
        Controls.Add(mlTemperatureGraph36);
        Controls.Add(buttonApply);
        Controls.Add(textBoxSeed);
        Controls.Add(label22);
        Controls.Add(textBoxNetworkLayers);
        Controls.Add(label21);
        Controls.Add(label15);
        Controls.Add(label16);
        Controls.Add(label17);
        Controls.Add(label18);
        Controls.Add(label19);
        Controls.Add(label20);
        Controls.Add(label14);
        Controls.Add(label13);
        Controls.Add(label12);
        Controls.Add(label9);
        Controls.Add(label10);
        Controls.Add(label11);
        Controls.Add(label6);
        Controls.Add(label7);
        Controls.Add(label8);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormLinearRegression";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Kyiv, Ukraine - 2012 Temperatures";
        FormClosing += FormLinearRegression_FormClosing;
        Load += FormLinearRegression_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private MLTemperatureGraph mlTemperatureGraph15;
    private MLTemperatureGraph mlTemperatureGraph16;
    private MLTemperatureGraph mlTemperatureGraph1;
    private MLTemperatureGraph mlTemperatureGraph2;
    private MLTemperatureGraph mlTemperatureGraph3;
    private MLTemperatureGraph mlTemperatureGraph4;
    private MLTemperatureGraph mlTemperatureGraph5;
    private MLTemperatureGraph mlTemperatureGraph6;
    private MLTemperatureGraph mlTemperatureGraph7;
    private MLTemperatureGraph mlTemperatureGraph8;
    private MLTemperatureGraph mlTemperatureGraph9;
    private MLTemperatureGraph mlTemperatureGraph10;
    private MLTemperatureGraph mlTemperatureGraph11;
    private MLTemperatureGraph mlTemperatureGraph12;
    private MLTemperatureGraph mlTemperatureGraph13;
    private MLTemperatureGraph mlTemperatureGraph14;
    private MLTemperatureGraph mlTemperatureGraph17;
    private MLTemperatureGraph mlTemperatureGraph18;
    private MLTemperatureGraph mlTemperatureGraph19;
    private MLTemperatureGraph mlTemperatureGraph20;
    private MLTemperatureGraph mlTemperatureGraph21;
    private MLTemperatureGraph mlTemperatureGraph22;
    private MLTemperatureGraph mlTemperatureGraph23;
    private MLTemperatureGraph mlTemperatureGraph24;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label label8;
    private Label label9;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private MLTemperatureGraph mlTemperatureGraph25;
    private MLTemperatureGraph mlTemperatureGraph26;
    private MLTemperatureGraph mlTemperatureGraph27;
    private MLTemperatureGraph mlTemperatureGraph28;
    private MLTemperatureGraph mlTemperatureGraph29;
    private MLTemperatureGraph mlTemperatureGraph30;
    private Label label14;
    private MLTemperatureGraph mlTemperatureGraph31;
    private MLTemperatureGraph mlTemperatureGraph32;
    private MLTemperatureGraph mlTemperatureGraph33;
    private MLTemperatureGraph mlTemperatureGraph34;
    private MLTemperatureGraph mlTemperatureGraph35;
    private MLTemperatureGraph mlTemperatureGraph36;
    private Label label15;
    private Label label16;
    private Label label17;
    private Label label18;
    private Label label19;
    private Label label20;
    private Label label21;
    private TextBox textBoxNetworkLayers;
    private Label label22;
    private TextBox textBoxSeed;
    private Button buttonApply;
    private TextBox textBoxEpochLimit;
    private Label label23;
    private Button buttonBenchmark;
    private Label labelRun;
    private ToolTip toolTip1;
}