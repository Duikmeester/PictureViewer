using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfaPictureViewer
{
    public partial class Brightness : Form
    {
        byte amountBrightness;

        public Brightness()
        {
            InitializeComponent();
        }

        private void numInput_ValueChanged(object sender, EventArgs e)
        {
            if (numInput.Value > 255)
                numInput.Value = 255;
            else if (numInput.Value < 0)
                numInput.Value = 0;

            // Update with each change, in case the OK button fails
            amountBrightness = (byte)numInput.Value;
        }

        // Return the value without it needing to be public
        public byte getAmount()
        {
            return amountBrightness;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Final update of value
            amountBrightness = (byte)numInput.Value;
        }
    }
}
