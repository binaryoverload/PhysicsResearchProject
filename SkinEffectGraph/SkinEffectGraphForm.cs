using System;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.WinForms;

namespace SkinEffectGraph
{
    public partial class SkinEffectGraphForm : Form
    {

        public double InitalCurrentDensity;
        public double SkinDepth;
        public double WireThickness;
        public LiveCharts.Wpf.AxisSection SkinDepthAxisSection;
        public LiveCharts.Wpf.AxisSection NegligibleAxisSection;
        public LiveCharts.Wpf.AxisSection MinimalAxisSection;
        public double Increments;

        private readonly Events _events;

        public SkinEffectGraphForm()
        {
            InitializeComponent();

            _events = new Events(this);

            ToolStrip toolStrip = new ToolStrip
            {
                Name = "toolStrip",
                Dock = DockStyle.Top
            };

            ToolStripLabel initialCurrentDensityLabel = new ToolStripLabel("Inital Current Density: ");
            ToolStripTextBox initialCurrentDensityTxt = new ToolStripTextBox
            {
                Name = "initialCurrentDensityTxt",
                Text = "100"
            };
            initialCurrentDensityTxt.KeyPress += Numberonly_txtbox;
            initialCurrentDensityTxt.TextChanged += _events.InitialCurrentDensityTxt_TextChanged;
            initialCurrentDensityTxt.LostFocus += _events.InitialCurrentDensityTxt_LostFocus;

            ToolStripLabel skinDepthLabel = new ToolStripLabel("Skin Depth: ");
            ToolStripTextBox skinDepthTxt = new ToolStripTextBox
            {
                Name = "skinDepthTxt",
                Text = "0.1"
            };
            skinDepthTxt.KeyPress += Numberonly_txtbox;
            skinDepthTxt.TextChanged += _events.SkinDepthTxt_TextChanged;
            skinDepthTxt.LostFocus += _events.SkinDepthTxt_LostFocus;


            ToolStripLabel wireThicknessLabel = new ToolStripLabel("Wire Thickness: ");
            ToolStripTextBox wireThicknessTxt = new ToolStripTextBox
            {
                Name = "wireThicknessTxt",
                Text = "2"
            };
            wireThicknessTxt.KeyPress += Numberonly_txtbox;
            wireThicknessTxt.TextChanged += _events.WireThicknessTxt_TextChanged;
            wireThicknessTxt.LostFocus += _events.WireThicknessTxt_LostFocus;

            ToolStripLabel pointCountLabel = new ToolStripLabel("Point Count: ");
            ToolStripNumberControl pointCountUpDown = new ToolStripNumberControl();
            pointCountUpDown.NumericUpDownControl.Value = 100;
            pointCountUpDown.NumericUpDownControl.Minimum = 10;
            pointCountUpDown.NumericUpDownControl.Maximum = 500;
            pointCountUpDown.ValueChanged += _events.Increments_ValueChanged;

            toolStrip.Items.Add(initialCurrentDensityLabel);
            toolStrip.Items.Add(initialCurrentDensityTxt);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(skinDepthLabel);
            toolStrip.Items.Add(skinDepthTxt);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(wireThicknessLabel);
            toolStrip.Items.Add(wireThicknessTxt);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(pointCountLabel);
            toolStrip.Items.Add(pointCountUpDown);

            InitalCurrentDensity = Double.Parse(initialCurrentDensityTxt.Text);
            SkinDepth = Double.Parse(skinDepthTxt.Text);
            WireThickness = Double.Parse(wireThicknessTxt.Text);
            Increments = Convert.ToDouble(pointCountUpDown.NumericUpDownControl.Value);

            CartesianChart cartesianChart = new CartesianChart
            {
                Name = "chart",
                Dock = DockStyle.Fill,
                LegendLocation = LegendLocation.None,
                Series = new SeriesCollection
                {
                    new LiveCharts.Wpf.LineSeries
                    {
                        Title = "Current Density",
                        Values = new ChartValues<ObservablePoint>(),
                        PointGeometry = LiveCharts.Wpf.DefaultGeometries.Cross,
                        PointGeometrySize = 5,
                        StrokeThickness = 2,
                        Stroke = Brushes.Black,
                        Fill = Brushes.Transparent
                    }
                }
            };


            cartesianChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Depth",
                MinValue = 0,
                Foreground = Brushes.Black,
                FontSize = 12
            });

            cartesianChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Current Density",
                MinValue = 0,
                Foreground = Brushes.Black,
                FontSize = 12
            });

            Controls.Add(cartesianChart);
            Controls.Add(toolStrip);

            UpdateData();

        }

        public void UpdateData()
        {
            CartesianChart chart = (Controls["chart"] as CartesianChart);
            InitialiseChart();
            for (double depth = 0; depth <= WireThickness / 2; depth += (WireThickness / 2 / Increments))
            {
                chart?.Series[0].Values.Add(new ObservablePoint(depth, CalculateCurrentDensity(InitalCurrentDensity, depth, SkinDepth)));
                if (CalculateCurrentDensity(InitalCurrentDensity, depth, SkinDepth) <= InitalCurrentDensity * 0.001 && NegligibleAxisSection.Value == 0)
                {
                    NegligibleAxisSection.Value = depth;
                    NegligibleAxisSection.Visibility = 0;
                }
                if (CalculateCurrentDensity(InitalCurrentDensity, depth, SkinDepth) <= InitalCurrentDensity * 0.01 && MinimalAxisSection.Value == 0)
                {
                    MinimalAxisSection.Value = depth;
                    MinimalAxisSection.Visibility = Visibility.Visible;
                }
            }

            SkinDepthAxisSection.Value = InitalCurrentDensity * Math.Pow(Math.E, -1);
            SkinDepthAxisSection.Visibility = Visibility.Visible;

            if (chart != null) chart.AxisY[0].MaxValue = InitalCurrentDensity;
        }

        public void InitialiseChart()
        {
            CartesianChart chart = (Controls["chart"] as CartesianChart);
            chart?.Series[0].Values.Clear();
            if (NegligibleAxisSection == null)
            {
                NegligibleAxisSection = new LiveCharts.Wpf.AxisSection()
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1,
                };
                chart.AxisX[0].Sections.Add(NegligibleAxisSection);
            }
            NegligibleAxisSection.Value = 0;
            NegligibleAxisSection.Visibility = Visibility.Hidden;
            if (MinimalAxisSection == null)
            {
                MinimalAxisSection = new LiveCharts.Wpf.AxisSection()
                {
                    Stroke = Brushes.Green,
                    StrokeThickness = 1,
                };
                chart.AxisX[0].Sections.Add(MinimalAxisSection);
            }
            MinimalAxisSection.Value = 0;
            MinimalAxisSection.Visibility = Visibility.Hidden;
            if (SkinDepthAxisSection == null)
            {
                SkinDepthAxisSection = new LiveCharts.Wpf.AxisSection()
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = 1,
                };
                chart.AxisY[0].Sections.Add(SkinDepthAxisSection);
            }
            SkinDepthAxisSection.Value = 0;
            SkinDepthAxisSection.Visibility = Visibility.Hidden;
        }

        private void Numberonly_txtbox(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && (((ToolStripTextBox) sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private double CalculateCurrentDensity(double initialCurrent, double depth, double skinDepth)
        {
            return initialCurrent * Math.Pow(Math.E, -depth / skinDepth);
        }

    }
}
