using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SkinEffectGraph
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripNumberControl : ToolStripControlHost
    {

        public ToolStripNumberControl() : base(new NumericUpDown())
        {
            
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged += OnValueChanged;
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged -= OnValueChanged;
        }

        public event EventHandler ValueChanged;

        public NumericUpDown NumericUpDownControl => Control as NumericUpDown;

        public void OnValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
