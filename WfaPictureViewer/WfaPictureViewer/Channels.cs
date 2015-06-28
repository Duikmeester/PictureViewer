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
    public partial class Channels : Form
    {
        bool alphaBW;

        public string colourChannel;

        public Channels()
        {
            InitializeComponent();
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            colourChannel = "R";
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            colourChannel = "G";
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            colourChannel = "B";
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            if (alphaBW == false)
            {
                colourChannel = "A";
            }
            else
            {
                colourChannel = "ABW";
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            if (alphaBW == false)
            {
                colourChannel = "All";
            }
            else if (alphaBW == true)
            {
                colourChannel = "AllBW";
            }
        }

        private void chkAlphaBW_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlphaBW.Checked == true)
            {
                alphaBW = true;
            }
            else if (chkAlphaBW.Checked == false)
            {
                alphaBW = false;
            }
        }
    }
}
