using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkinEffectGraph
{
    class Events
    {

        private SkinEffectGraphForm mainForm;

        public Events(SkinEffectGraphForm form)
        {
            this.mainForm = form;
        }

        public void Increments_ValueChanged(object sender, EventArgs e)
        {
            mainForm.increments = Convert.ToDouble((sender as ToolStripNumberControl).NumericUpDownControl.Value);
            mainForm.UpdateData();
        }

        public void WireThicknessTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((sender as ToolStripTextBox).Text == ""))
                {
                    mainForm.wireThickness = Double.Parse((sender as ToolStripTextBox).Text);
                    if (!(mainForm.wireThickness <= 0))
                    {
                        mainForm.UpdateData();
                    }
                    if (mainForm.wireThickness > 1000000)
                    {
                        MessageBox.Show("That wire is too wide for me!", "Skin Effect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        (sender as ToolStripTextBox).Text = "2";
                    }
                }
            }
            catch (Exception)
            {
                (sender as ToolStripTextBox).Text = "2";
            }
        }

        public void SkinDepthTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((sender as ToolStripTextBox).Text == ""))
                {
                    mainForm.skinDepth = Double.Parse((sender as ToolStripTextBox).Text);
                    if (!(mainForm.skinDepth <= 0))
                    {
                        mainForm.UpdateData();
                    }
                    if (mainForm.skinDepth > 1000)
                    {
                        MessageBox.Show("Are you sure you've done your maths right? This is a biggg skin depth!", "Skin Effect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        (sender as ToolStripTextBox).Text = "0.1";
                    }
                }
            }
            catch (Exception)
            {
                (sender as ToolStripTextBox).Text = "0.1";
            }
        }

        public void InitialCurrentDensityTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((sender as ToolStripTextBox).Text == ""))
                {
                    mainForm.initalCurrentDensity = Double.Parse((sender as ToolStripTextBox).Text);
                    if (!(mainForm.initalCurrentDensity <= 0))
                    {
                        mainForm.UpdateData();
                    }
                    if (mainForm.initalCurrentDensity > 1000000)
                    {
                        MessageBox.Show("A current density bigger than 1x10^6? You're getting too big for your boots!", "Skin Effect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        (sender as ToolStripTextBox).Text = "100";
                    }
                }
            }
            catch (Exception)
            {
                (sender as ToolStripTextBox).Text = "100";
            }
        }

        public void InitialCurrentDensityTxt_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse((sender as ToolStripTextBox).Text) <= 0 || Double.Parse((sender as ToolStripTextBox).Text) >= 1000000)
                {
                    (sender as ToolStripTextBox).Text = "100";
                }
            }
            catch (Exception)
            {
                (sender as ToolStripTextBox).Text = "100";
            }
        }

        public void SkinDepthTxt_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse((sender as ToolStripTextBox).Text) <= 0 || Double.Parse((sender as ToolStripTextBox).Text) >= 1000)
                {
                    (sender as ToolStripTextBox).Text = "0.1";
                }
            }
            catch (Exception)
            {
                (sender as ToolStripTextBox).Text = "0.1";
            }
        }

        public void WireThicknessTxt_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse((sender as ToolStripTextBox).Text) <= 0 || Double.Parse((sender as ToolStripTextBox).Text) >= 1000000)
                {
                    (sender as ToolStripTextBox).Text = "2";
                }
            }
            catch (Exception)
            {
                (sender as ToolStripTextBox).Text = "2";
            }
        }



    }
}
