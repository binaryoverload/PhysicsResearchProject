using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class SkinEffectForm : Form
    {

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        double initalCurrentDensity;
        double skinDepth;
        double wireThickness;

        public SkinEffectForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].CursorX.LineColor = Color.Aqua;
            chart1.MouseMove += new MouseEventHandler(chart1_MouseMove);
            currentDensityTxtbox.KeyPress += new KeyPressEventHandler(numberonly_txtbox);
            skinDepthTxtbox.KeyPress += new KeyPressEventHandler(numberonly_txtbox);
            wireThicknessTxtbox.KeyPress += new KeyPressEventHandler(numberonly_txtbox);


            initalCurrentDensity = Double.Parse(currentDensityTxtbox.Text);
            skinDepth = Double.Parse(skinDepthTxtbox.Text);
            wireThickness = Double.Parse(wireThicknessTxtbox.Text);

            updateGraph();
            adjustSizes();
        }


        private double calculateCurrentDensity(double initialCurrent, double depth, double skinDepth)
        {
            return initialCurrent * Math.Pow(Math.E, -depth / skinDepth);
        }

        private void updateGraph()
        {
            chart1.Series[0].Points.Clear();
            for (double depth = 0; depth <= wireThickness / 2; depth += (wireThickness / 200))
            {
                chart1.Series[0].Points.AddXY(depth, calculateCurrentDensity(initalCurrentDensity, depth, skinDepth));
          
            }
            chart1.ChartAreas[0].CursorY.Position = initalCurrentDensity * Math.Pow(Math.E, -1);
            chart1.ChartAreas[0].AxisY.Maximum = initalCurrentDensity;
        }

        private void numberonly_txtbox(object sender, KeyPressEventArgs e)
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

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart1.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around the point)
                        if (Math.Abs(pos.X - pointXPixel) < 50 &&
                            Math.Abs(pos.Y - pointYPixel) < 50)
                        {
                            tooltip.Show("X=" + prop.XValue + ", Y=" + prop.YValues[0], this.chart1,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        private void currentDensityTxtbox_TextChanged(object sender, EventArgs e)
        {
            initalCurrentDensity = Double.Parse((sender as ToolStripTextBox).Text == "" ? "0" : (sender as ToolStripTextBox).Text);
            updateGraph();
        }

        private void skinDepthTxtbox_TextChanged(object sender, EventArgs e)
        {
            skinDepth = Double.Parse((sender as ToolStripTextBox).Text == "" ? "0" : (sender as ToolStripTextBox).Text);
            updateGraph();
        }

        private void wireThicknessTxtbox_TextChanged(object sender, EventArgs e)
        {
            wireThickness = Double.Parse((sender as ToolStripTextBox).Text == "" ? "0" : (sender as ToolStripTextBox).Text);
            updateGraph();
        }

        private void SkinEffectForm_Resize(object sender, EventArgs e)
        {
            adjustSizes();
        }

        private void adjustSizes()
        {
            chart1.Height = ClientSize.Height - 25;
        }
    }
}
