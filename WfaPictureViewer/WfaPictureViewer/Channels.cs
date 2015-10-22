using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WfaPictureViewer
{
    public partial class Channels : Form
    {
        bool alphaBW;
        public bool bypass;
        public bool rename;
        public int fileTypeIndex;
        public ImageFormat fileType;
        public string fileName;
        public string colourChannel;
        

        public Channels()
        {
            InitializeComponent();
            // Stops manual editing of the combo box, meaning that somehting HAS to be selected
            comboFileType.DropDownStyle = ComboBoxStyle.DropDownList;
            bypass = chkBypass.Checked;
            comboFileType.SelectedIndex = 0;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
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
                btnA.Text = "Alpha (B/W)";
            }
            else if (chkAlphaBW.Checked == false)
            {
                alphaBW = false;
                btnA.Text = "Alpha";
            }
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBypass.Checked)
            {
                bypass = true;
                comboFileType.Enabled = true;
                lblFileType.Enabled = true;
            }
            else
            {
                bypass = false;
                comboFileType.Enabled = false;
                lblFileType.Enabled = false;
            }
        }

        private void comboFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFileType.SelectedIndex == 0)
                fileType = ImageFormat.Jpeg;
            else if (comboFileType.SelectedIndex == 1)
                fileType = ImageFormat.Bmp;
            else if (comboFileType.SelectedIndex == 2)
                fileType = ImageFormat.Png;
            else if (comboFileType.SelectedIndex == 3)
                fileType = ImageFormat.Tiff;
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
