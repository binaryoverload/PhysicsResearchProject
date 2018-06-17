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
                if (Double.Parse((sender as ToolStripTextBox).Text) <= 0)
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
                if (Double.Parse((sender as ToolStripTextBox).Text) <= 0)
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
                if (Double.Parse((sender as ToolStripTextBox).Text) <= 0)
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
