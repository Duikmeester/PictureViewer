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
    public partial class YaMomma : Form
    {
        Form1 mainForm;

        public YaMomma()
        {
            InitializeComponent();
        }

        public YaMomma(Form1 formSender)
        {
            mainForm = formSender;
            InitializeComponent();
        }

        private void btnKanye_Click(object sender, EventArgs e)
        {
            if (txtWhatTake.Text != "")
            {
                // Instead of making txtWhatTake.Text public, it is passed to a public function inside Form1. 
                mainForm.TestFunction2(txtWhatTake.Text);
            }
            else
            {
                MessageBox.Show("Kanye gotta have something.");
            }
        }
    }
}
