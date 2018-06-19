using System;
using System.Windows.Forms;

namespace SkinEffectGraph
{
    class Events
    {

        private readonly SkinEffectGraphForm _mainForm;

        public Events(SkinEffectGraphForm form)
        {
            _mainForm = form;
        }

        public void Increments_ValueChanged(object sender, EventArgs e)
        {
            _mainForm.Increments = Convert.ToDouble(((ToolStripNumberControl) sender).NumericUpDownControl.Value);
            _mainForm.UpdateData();
        }

        public void WireThicknessTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripTextBox) sender).Text != "")
                {
                    _mainForm.WireThickness = Double.Parse(((ToolStripTextBox) sender).Text);
                    if (!(_mainForm.WireThickness <= 0))
                    {
                        _mainForm.UpdateData();
                    }
                    if (_mainForm.WireThickness > 1000000)
                    {
                        MessageBox.Show("That wire is too wide for me!", "Skin Effect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ((ToolStripTextBox) sender).Text = "2";
                    }
                }
            }
            catch (Exception)
            {
                ((ToolStripTextBox) sender).Text = "2";
            }
        }

        public void SkinDepthTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripTextBox) sender).Text != "")
                {
                    _mainForm.SkinDepth = Double.Parse(((ToolStripTextBox) sender).Text);
                    if (!(_mainForm.SkinDepth <= 0))
                    {
                        _mainForm.UpdateData();
                    }
                    if (_mainForm.SkinDepth > 1000)
                    {
                        MessageBox.Show("Are you sure you've done your maths right? This is a biggg skin depth!", "Skin Effect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ((ToolStripTextBox) sender).Text = "0.1";
                    }
                }
            }
            catch (Exception)
            {
                ((ToolStripTextBox) sender).Text = "0.1";
            }
        }

        public void InitialCurrentDensityTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripTextBox) sender).Text != "")
                {
                    _mainForm.InitalCurrentDensity = Double.Parse(((ToolStripTextBox) sender).Text);
                    if (!(_mainForm.InitalCurrentDensity <= 0))
                    {
                        _mainForm.UpdateData();
                    }
                    if (_mainForm.InitalCurrentDensity > 1000000)
                    {
                        MessageBox.Show("A current density bigger than 1x10^6? You're getting too big for your boots!", "Skin Effect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ((ToolStripTextBox) sender).Text = "100";
                    }
                }
            }
            catch (Exception)
            {
                ((ToolStripTextBox) sender).Text = "100";
            }
        }

        public void InitialCurrentDensityTxt_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse(((ToolStripTextBox) sender).Text) <= 0 || Double.Parse(((ToolStripTextBox) sender).Text) >= 1000000)
                {
                    ((ToolStripTextBox) sender).Text = "100";
                }
            }
            catch (Exception)
            {
                ((ToolStripTextBox) sender).Text = "100";
            }
        }

        public void SkinDepthTxt_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse(((ToolStripTextBox) sender).Text) <= 0 || Double.Parse(((ToolStripTextBox) sender).Text) >= 1000)
                {
                    ((ToolStripTextBox) sender).Text = "0.1";
                }
            }
            catch (Exception)
            {
                ((ToolStripTextBox) sender).Text = "0.1";
            }
        }

        public void WireThicknessTxt_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse(((ToolStripTextBox) sender).Text) <= 0 || Double.Parse(((ToolStripTextBox) sender).Text) >= 1000000)
                {
                    ((ToolStripTextBox) sender).Text = "2";
                }
            }
            catch (Exception)
            {
                ((ToolStripTextBox) sender).Text = "2";
            }
        }



    }
}
