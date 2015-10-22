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
    public partial class BatchSettings : Form
    {
        // Attributes created for public accessing
        public bool export;
        public bool bypass;
        public bool rename;
        public int fileType;
        public string fileTypeString;
        public string fileName;


        public BatchSettings()
        {
            InitializeComponent();
            // Stops manual editing of the combo box, meaning that somehting HAS to be selected
            comboFileType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFileType.SelectedIndex = 0;
            UpdateOptions();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void chkExport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExport.Checked)
                export = true;
            else
                export = false;

            UpdateOptions();
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBypass.Checked)
                bypass = true;
            else
                bypass = false;

            UpdateOptions();
        }

        private void chkFileName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFileName.Checked == true)
            {
                rename = true;
            }
            else
                rename = false;
            UpdateOptions();
        }

        private void comboFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
        }

        private void UpdateOptions()
        {
            if (chkExport.Checked)
                export = true;
            else
                export = false;

            if (export)
            {
                chkBypass.Enabled = true;

                if (chkBypass.Checked)
                    bypass = true;
                else
                    bypass = false;

                if (bypass)
                {
                    comboFileType.Enabled = true;
                    chkFileName.Enabled = true;
                    lblFileType.Enabled = true;

                    if (rename == true)
                    {
                        comboFileType.Enabled = false;
                        btnGetPath.Enabled = true;
                        lblFileName.Enabled = true;
                        if (string.IsNullOrEmpty(txtFileName.Text))
                        {
                            txtFileName.TextAlign = HorizontalAlignment.Right;
                            txtFileName.Text = ">>>";
                            btnGetPath.PerformClick();
                        }
                    }
                    else
                    {
                        btnGetPath.Enabled = false;
                        lblFileName.Enabled = false;
                    }
                }
                else
                {
                    comboFileType.Enabled = false;
                    chkFileName.Enabled = false;
                    txtFileName.Enabled = false;
                    lblFileType.Enabled = false;
                    lblFileName.Enabled = false;
                }
            }
            else
            {
                chkBypass.Enabled = false;

                comboFileType.Enabled = false;
                chkFileName.Enabled = false;
                txtFileName.Enabled = false;
                lblFileType.Enabled = false;
                lblFileName.Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void BatchSettings_Load(object sender, EventArgs e)
        {

        }

        private void btnGetPath_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlgGetPath = new SaveFileDialog())
            {
                dlgGetPath.InitialDirectory = "C:/Desktop";
                dlgGetPath.Title = "Select File Name and Path";
                dlgGetPath.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";

                if (dlgGetPath.ShowDialog() == DialogResult.OK)
                {
                    fileName = dlgGetPath.FileName;
                    fileType = dlgGetPath.FilterIndex;
                    txtFileName.Text = dlgGetPath.FileName;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (bypass)
            {
                if (!rename)
                {
                    fileType = comboFileType.SelectedIndex;
                    fileTypeString = comboFileType.Text;
                }
                else
                    // unchanged, leave it as the dlg assigned it 

                    if (fileName != null)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid file name & path using the [...] button.");
                    }
            }
            else
            {
                int i;
            }
        }
    }
}
