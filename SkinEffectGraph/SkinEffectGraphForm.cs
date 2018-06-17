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

        public double initalCurrentDensity;
        public double skinDepth;
        public double wireThickness;
        public LiveCharts.Wpf.AxisSection skinDepthAxisSection;
        public LiveCharts.Wpf.AxisSection negligibleAxisSection;
        public LiveCharts.Wpf.AxisSection minimalAxisSection;
        public double increments;

        private Events events;

        public SkinEffectGraphForm()
        {
            InitializeComponent();

            events = new Events(this);

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
            initialCurrentDensityTxt.TextChanged += events.InitialCurrentDensityTxt_TextChanged;
            initialCurrentDensityTxt.LostFocus += events.InitialCurrentDensityTxt_LostFocus;

            ToolStripLabel skinDepthLabel = new ToolStripLabel("Skin Depth: ");
            ToolStripTextBox skinDepthTxt = new ToolStripTextBox
            {
                Name = "skinDepthTxt",
                Text = "0.1"
            };
            skinDepthTxt.KeyPress += Numberonly_txtbox;
            skinDepthTxt.TextChanged += events.SkinDepthTxt_TextChanged;
            skinDepthTxt.LostFocus += events.SkinDepthTxt_LostFocus;


            ToolStripLabel wireThicknessLabel = new ToolStripLabel("Wire Thickness: ");
            ToolStripTextBox wireThicknessTxt = new ToolStripTextBox
            {
                Name = "wireThicknessTxt",
                Text = "2"
            };
            wireThicknessTxt.KeyPress += Numberonly_txtbox;
            wireThicknessTxt.TextChanged += events.WireThicknessTxt_TextChanged;
            wireThicknessTxt.LostFocus += events.WireThicknessTxt_LostFocus;

            ToolStripLabel pointCountLabel = new ToolStripLabel("Point Count: ");
            ToolStripNumberControl pointCountUpDown = new ToolStripNumberControl();
            pointCountUpDown.NumericUpDownControl.Value = 100;
            pointCountUpDown.NumericUpDownControl.Minimum = 10;
            pointCountUpDown.NumericUpDownControl.Maximum = 500;
            pointCountUpDown.ValueChanged += events.Increments_ValueChanged;

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

            initalCurrentDensity = Double.Parse(initialCurrentDensityTxt.Text);
            skinDepth = Double.Parse(skinDepthTxt.Text);
            wireThickness = Double.Parse(wireThicknessTxt.Text);
            increments = Convert.ToDouble(pointCountUpDown.NumericUpDownControl.Value);

            CartesianChart cartesianChart = new CartesianChart
            {
                Name = "chart",
                Dock = DockStyle.Fill,
                LegendLocation = LegendLocation.None
            };

            cartesianChart.Series = new SeriesCollection
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

            this.Controls.Add(cartesianChart);
            this.Controls.Add(toolStrip);

            UpdateData();

        }

        public void UpdateData()
        {
            CartesianChart chart = (this.Controls["chart"] as CartesianChart);
            InitialiseChart();
            for (double depth = 0; depth <= wireThickness / 2; depth += (wireThickness / 2 / increments))
            {
                chart.Series[0].Values.Add(new ObservablePoint(depth, CalculateCurrentDensity(initalCurrentDensity, depth, skinDepth)));
                if (CalculateCurrentDensity(initalCurrentDensity, depth, skinDepth) <= initalCurrentDensity * 0.001 && negligibleAxisSection.Value == 0)
                {
                    negligibleAxisSection.Value = depth;
                    negligibleAxisSection.Visibility = 0;
                }
                if (CalculateCurrentDensity(initalCurrentDensity, depth, skinDepth) <= initalCurrentDensity * 0.01 && minimalAxisSection.Value == 0)
                {
                    minimalAxisSection.Value = depth;
                    minimalAxisSection.Visibility = Visibility.Visible;
                }
            }

            skinDepthAxisSection.Value = initalCurrentDensity * Math.Pow(Math.E, -1);
            skinDepthAxisSection.Visibility = Visibility.Visible;

            chart.AxisY[0].MaxValue = initalCurrentDensity;
        }

        public void InitialiseChart()
        {
            CartesianChart chart = (this.Controls["chart"] as CartesianChart);
            chart.Series[0].Values.Clear();
            if (negligibleAxisSection == null)
            {
                negligibleAxisSection = new LiveCharts.Wpf.AxisSection()
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1,
                };
                chart.AxisX[0].Sections.Add(negligibleAxisSection);
            }
            negligibleAxisSection.Value = 0;
            negligibleAxisSection.Visibility = Visibility.Hidden;
            if (minimalAxisSection == null)
            {
                minimalAxisSection = new LiveCharts.Wpf.AxisSection()
                {
                    Stroke = Brushes.Green,
                    StrokeThickness = 1,
                };
                chart.AxisX[0].Sections.Add(minimalAxisSection);
            }
            minimalAxisSection.Value = 0;
            minimalAxisSection.Visibility = Visibility.Hidden;
            if (skinDepthAxisSection == null)
            {
                skinDepthAxisSection = new LiveCharts.Wpf.AxisSection()
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = 1,
                };
                chart.AxisY[0].Sections.Add(skinDepthAxisSection);
            }
            skinDepthAxisSection.Value = 0;
            skinDepthAxisSection.Visibility = Visibility.Hidden;
        }

        private void Numberonly_txtbox(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as ToolStripTextBox).Text.IndexOf('.') > -1))
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
