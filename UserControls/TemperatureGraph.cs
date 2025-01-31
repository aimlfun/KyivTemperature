using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Temperature;

/// <summary>
/// A graph of temperatures. The temperatures are plotted as green bars with a cross in the middle.
/// The purpose of it is to provide a visual representation of the known temperatures. It can be subclassed 
/// to provide a graph of the neural network's output, which can then be compared to the known temperatures.
/// 
/// DEFAULT: Kyiv 2012 temperatures from https://figshare.com/articles/dataset/temperature_csv/3171766?file=4938964
/// </summary>
public partial class TemperatureGraph : UserControl
{
    /// <summary>
    /// The temperatures to be plotted.
    /// </summary>
    private double[] _temperatures = [23.48, 12.30, 34.15, 52.96, 64.58, 67.64, 73.66, 68.59, 60.47, 49.20, 39.55, 22.39];

    /// <summary>
    /// Height of the graph (primarily tracked for the sake of bitmap).
    /// </summary>
    protected int _height;

    /// <summary>
    /// Width of the graph (primarily tracked for the sake of bitmap).
    /// </summary>
    protected int _width;

    /// <summary>
    /// Monthly temperatures x,y plotted. Used for the neural network to learn
    /// relationship of day to temperature.
    /// </summary>
    private readonly List<PointF> _knownTemperaturesGraphPoints = [];

    /// <summary>
    /// List of known temperatures graph points.
    /// i.e. the points where the known temperatures are plotted on the graph.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public List<PointF> KnownTemperaturesGraphPoints => _knownTemperaturesGraphPoints;

    /// <summary>
    /// Brush for drawing the bars representing known temperatures.
    /// </summary>
    private SolidBrush _brushToPlotBars = new(Color.FromArgb(30, 0, 255, 0));

    /// <summary>
    /// The pen to plot the cross at the exact temperature point.
    /// </summary>
    private Pen _penToPlotCross = Pens.Lime;

    /// <summary>
    /// The pen to plot the outline of the bars.
    /// </summary>
    private Pen _penToPlotBarOutline = Pens.Green;

    /// <summary>
    /// Width of a bar for a month.
    /// </summary>
    protected float _barWidthForAMonth;

    /// <summary>
    /// Width of a bar for a month.
    /// </summary>
    public float BarWidthForAMonth => _barWidthForAMonth;

    /// <summary>
    /// Bitmap for the base graph. When we generate the graph, we draw the known temperatures on this bitmap.
    /// It can then be used behind the neural network's output to compare the two.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // required to stop designer serialising the bitmap
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public Bitmap? BitmapBaseGraph { get; private set; }

    #region DESIGNER VISIBLE PROPERTIES
    /// <summary>
    /// 
    /// </summary>
    [Description("Kyiv 2012 monthly temperatures to be plotted / learnt."), Category("Graph")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double[] Temperatures
    {
        get
        {
            return _temperatures;
        }
        set
        {
            _temperatures = value;

            DrawGraph();
        }
    }

    /// <summary>
    /// Pen for drawing an outline around temperatures.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Pen BarOutline
    {
        get
        {
            return _penToPlotBarOutline;
        }

        set
        {
            _penToPlotBarOutline = value;
            DrawGraph();
        }
    }

    /// <summary>
    /// Pen for drawing a cross at the exact temperature point.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Pen CrossPen
    {
        get
        {
            return _penToPlotCross;
        }

        set
        {
            _penToPlotCross = value;
            DrawGraph();
        }
    }

    /// <summary>
    /// Brush for drawing known temperatures.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SolidBrush BarBrush
    {
        get
        {
            return _brushToPlotBars;
        }

        set
        {
            _brushToPlotBars = value;
            DrawGraph();
        }
    }

    #endregion

    /// <summary>
    /// Constructor.
    /// </summary>
    public TemperatureGraph() : base()
    {
        InitializeComponent();
    }

    /// <summary>
    /// We override the OnLoad event to draw the graph. We can't do this in the constructor because inheritence breaks. Even with methods called in a logical method,
    /// it results in bizarre errors. 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        DrawGraph();
    }

    /// <summary>
    /// Draws the graph containing the known temperatures.
    /// 
    /// The graph is rendered to a bitmap, which is then displayed on the control.
    /// It is comprised of green bars (from floor to vertical point) with a cross in the middle indicating exact point.
    /// 
    /// Remember, we are given average monthly temperatures, so we cannot draw a line between them. Therefore we use
    /// a barchart to represent the known temperatures. When the ML model is trained, it will learn the relationship
    /// and be able to predict the temperature for any given day, thus providing a curve.
    /// </summary>
    /// <returns></returns>
    private void DrawGraph()
    {
        _height = pictureBoxGraph.Height;
        _width = pictureBoxGraph.Width;
        _barWidthForAMonth = (float)Math.Floor((_width - 12) / 12f); // 12 months + 0.5 each side for padding

        if (_height < 12 || _width < 12) return; // don't draw if too small

        BitmapBaseGraph = new(_width, _height);

        using Graphics graphics = Graphics.FromImage(BitmapBaseGraph);

        graphics.Clear(Color.Black);
        graphics.SmoothingMode = SmoothingMode.HighQuality; // no jaggies

        GenerateTemperatureGreenCrosses();

        PlotKnownTemperatureReadings(graphics);

        graphics.Flush();

        // This paints our graph to the user control by injecting the bitmap into the PictureBox.
        // We clone the image, because it will also be the background for the neural network's output.
        UpdateGraph(new Bitmap(BitmapBaseGraph)); // this is a "clone" of the BitmapBaseGraph because the images are disposed during the UpdateGraph call.
    }

    /// <summary>
    /// Updates the graph with a new image.
    /// </summary>
    /// <param name="bitmap"></param>
    public void UpdateGraph(Bitmap bitmap)
    {
        pictureBoxGraph.Image?.Dispose();
        pictureBoxGraph.Image = bitmap;
    }

    /// <summary>
    /// Enable post-render drawing such as ranks.
    /// </summary>
    /// <returns></returns>
    public Bitmap GetGraphImage()
    {
        return pictureBoxGraph.Image == null ? throw new NullReferenceException("Graph image is null.") : new(pictureBoxGraph.Image);
    }

    /// <summary>
    /// Updates the label (above the graph) with the given text.
    /// </summary>
    /// <param name="text"></param>
    public void UpdateLabel(string text)
    {
        labelGraphTitle.Text = text;
    }

    /// <summary>
    /// Generates a set of of green crosses representing the known temperatures, that are plotted
    /// and the neural network will learn from.
    /// </summary>
    private void GenerateTemperatureGreenCrosses()
    {
        // get max temperature to determine the scale required
        double max = 0;

        foreach (double d in _temperatures) max = Math.Max(max, d);

        double scale = (_height - 5f) / max;

        _knownTemperaturesGraphPoints.Clear();

        float x = (float)Math.Round(_barWidthForAMonth / 2 + 6);

        // construct a list of temperature points
        for (int idx = 0; idx < _temperatures.Length; idx++)
        {
            float y = (int)Math.Round(_temperatures[idx] * scale);

            // for ML, we need to know the x,y of the known temperatures it is learning from
            // we will also plot these as green bars with a cross in the middle
            _knownTemperaturesGraphPoints.Add(new PointF((int)Math.Round(x), (int)y));

            // move to middle of the bar for the next month
            x += _barWidthForAMonth;
        }
    }

    /// <summary>
    /// Plots the known temperature readings, as green bars with a cross in the middle indicating exact point.
    /// </summary>
    /// <param name="graphics"></param>
    private void PlotKnownTemperatureReadings(Graphics graphics)
    {
        float halfBarWidth = _barWidthForAMonth / 2;

        foreach (PointF pointOnGraph in _knownTemperaturesGraphPoints)
        {
            float y = _height - pointOnGraph.Y; // we need this for top of bar, and to draw the cross

            RectangleF r = new(pointOnGraph.X - halfBarWidth, y, _barWidthForAMonth, pointOnGraph.Y);

            //
            //    _x_    <- this is the "x"
            //    |  |
            //    |  |. .
            // . .|  |  .
            // .  |  |  .
            //     ^^ this is the rectangle
            graphics.FillRectangle(_brushToPlotBars, r);
            graphics.DrawRectangle(_penToPlotBarOutline, r);

            DrawX(graphics, new PointF(pointOnGraph.X, y));
        }
    }

    /// <summary>
    /// Draws an "x" at the given position.
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="positionOfX"></param>
    private void DrawX(Graphics graphics, PointF positionOfX)
    {
        if (positionOfX.Y < 0) return; // don't draw if off the top of the graph

        // x marks the spot
        graphics.DrawLine(_penToPlotCross, positionOfX.X - 2, positionOfX.Y - 2, positionOfX.X + 2, positionOfX.Y + 2);
        graphics.DrawLine(_penToPlotCross, positionOfX.X - 2, positionOfX.Y + 2, positionOfX.X + 2, positionOfX.Y - 2);
    }

    /// <summary>
    /// Handles the resize event.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);

        DrawGraph(); // recompute the graph
    }
}