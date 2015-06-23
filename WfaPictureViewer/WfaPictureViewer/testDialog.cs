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
    public partial class testDialog : Form
    
    {
        // Declaring the Form1 Type variable, calling it 'mainForm'
        Form1 mainForm;

        // Default constructor
        public testDialog()
        {
            InitializeComponent();
        }

        // Constructer takes the Form1 Type as an argument
        public testDialog(Form1 mainFormPass)
        {
            // The outer-scoped (class) variable is being assigned the value of the passed Form information
            mainForm = mainFormPass;
            InitializeComponent();

            // Routing control through a single-service public function, to avoid mess
            mainForm.TestFunction(123);            
        }

        private void testDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
