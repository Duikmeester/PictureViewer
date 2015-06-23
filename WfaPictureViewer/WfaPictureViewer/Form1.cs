using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging; // For 'PixelFormat' and others, redrawing Bitmaps
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading; // For 'Thread.Sleep'

namespace WfaPictureViewer
{
    /*
    * THE VARIABLES CAN NOT BE INITIALISED IN THIS, THEY CAN ONLY BE DECLARED HERE.
    * QUESTION, Are they prototypes?
    */
    // CLASS DECLARATION, "Form" is the base class and "Form1" is the derived class
    public partial class Form1 : Form
    {
        //Image currentImage; // Container for image that will be displayed
        //int len; // Used for loops, will contain the size of the array
        //int[] curSize;
        string[] inputFileText = System.IO.File.ReadAllLines(@"..\..\input.txt"); // Initialise an array of each line of the input file
        float curCorrectRatio;
        float picBoxRatio;
        bool imgIsLoaded;
        Color defaultBG;
        Bitmap originalImage;

        // Form declarations, to be used as dialogs. The actual Form name is used as the Type, and it's given a name.
        testDialog dlgTest;
        YaMomma dlgYaMomma;

        /* 
         * HERE, THE VARIABLES ARE INITIALISED, ALONG WITH THE FORM ITSELF, AND SOME OTHER STUFFS
         */
        //Constructor - Does not have a return type and shares a name with the class
        public Form1()
        {
            InitializeComponent();

            // Allow the form to process key inputs
            this.KeyPreview = true; 
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPressReg);

            // "Size" is a struct, so you can't simply declare this.MinimumSize.Size = x,y
            this.MinimumSize = new Size(580, 100);
            // Bool initialisors
            chkAspectLock.Enabled = false;
            imgIsLoaded = false;
            // BG Colour stuff
            defaultBG = BackColor;
            menuResetBGColour.Enabled = false;
            // Run the initial text and Option updates
            UpdateImgOptions();
            UpdateText();

            menuBrightnessContrast.Enabled = false;
            menuKanyeQuest.Enabled = false;

            // In C#, the square brackets go after the type declaration, not the name
            //int[] curSize = {1,1};
            /*// Displaying each line of text in the input file in the Output console
            len = inputFileText.Length;
            for (int i = 0; i < len; i++)
            {
                System.Diagnostics.Debug.WriteLine("Line number " + i + " in the array is: " + inputFileText[i]);
            }*/

        }

        void Form1_KeyPressReg(object sender, KeyPressEventArgs key)
        {
            //MessageBox.Show(key.KeyChar.ToString());
            if (key.KeyChar.ToString() == "f")
            {
                CycleMaximised();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {                                                         
        }
         
        // Known as an "Event Handler" becuase they are called when an event occurs in the program
        private void chkStretch_CheckedChanged(object sender, EventArgs e)
        {
            // Finds out if the box is/isn't checked after a click, and defines image sizing based on that
            if (chkStretch.Checked)
            {
                // The picture box size mode is given the 'stretchimage' property
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                // Make the font Bold, mimicing the current font's style, but making it bold
                chkStretch.Font = new Font(chkStretch.Font, FontStyle.Bold);
                UpdatePicboxInfo();

                //Update though UpdateImgOptions(), because menu items should only be activated if there is actually an image loaded
                UpdateImgOptions();
            }
            else if (chkStretch.Checked == false)
            {
                // The picture box size mode is assigned the regular image scaling property
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                // Make the font Bold, mimicing the current font's style, but making it not bold
                chkStretch.Font = new Font(chkStretch.Font, FontStyle.Regular);
                UpdatePicboxInfo();

                //Update though UpdateImgOptions(), because menu items should only be activated if there is actually an image loaded
                UpdateImgOptions();
            }
        }

        private void UpdatePicboxInfo()
    {
        if (pictureBox1.Image != null)
        {
            // Writing the file info to the label
            lblPicInfo.Text = ("File Name: " + pictureBox1.Tag + Environment.NewLine + "H: " + pictureBox1.Image.Height + Environment.NewLine + "W: " + pictureBox1.Image.Width + Environment.NewLine + "Aspect Ratio: " + GetPicBoxRatio() + Environment.NewLine + "Stretching: " + GetRatioDistortion());
        }
    }       

        // Method is called whenever upon changing the state of the picturebox. 
        private void UpdateText()
        {
            if (pictureBox1.Image != null)
            {
                // Take the first line of the input text file
                lblPicNotifier.Text = inputFileText[0];
            }
            else
            {
                // Take the second line of the input text file
                lblPicNotifier.Text = inputFileText[1];
            }
        }

        private void CycleMaximised()
        {
            // If the form is not maximised
            if (this.FormBorderStyle == FormBorderStyle.Sizable && this.WindowState == FormWindowState.Normal)
            {
                // Maximise
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            // else if the form IS maximised
            else if(this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized)
            {
                // Un-maximise (?)
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                MessageBox.Show("Fullscreening error");
                this.Close();
            }
        }

        private void Form1_PostResize(object sender, EventArgs e)
        {
            UpdatePicboxInfo();
        }

        // Returns the aspect ratio of the image currently loaded in the picBox
        private float GetPicBoxRatio()
        {
            // If there is actually an image loaded
            if (pictureBox1.Image != null)
            {
                // Find out if the stretching is turned on
                if (chkStretch.Checked == false)
                {
                    // The ratio is the width of the image divided by the height
                    picBoxRatio = (float)pictureBox1.Image.PhysicalDimension.Width / (float)pictureBox1.Image.PhysicalDimension.Height;
                    return picBoxRatio;
                }
                // Or if the stretch checkbox IS checked, instead get the values of the pictureBox itself, as the iamge will match it
                else 
                {
                    picBoxRatio = (float)pictureBox1.Width / (float)pictureBox1.Height;
                    return picBoxRatio;
                }
            }
            else
            {
                return 0.0f;
            }
        }

        // Compare the current stretched image's aspect ratio against it's original aspect ratio
        private string GetRatioDistortion()
        {
            // Only run the comparison if image stretching is enabled
            if (chkStretch.Checked)
            {
                // To avoid calling the function multiple times
                float tempPicBoxRatio = GetPicBoxRatio();
                float distortion;

                // if the images 'correct' ratio (e.g. 1.43322) is less than current 
                if (curCorrectRatio < tempPicBoxRatio)
                {
                    Console.WriteLine("Correct < Current" + Environment.NewLine + "Correct: " + curCorrectRatio + "Current: " + tempPicBoxRatio);
                    distortion = curCorrectRatio / tempPicBoxRatio - 1;
                    return (distortion.ToString("0.000"));
                }
                else if(curCorrectRatio > tempPicBoxRatio)
                {
                    Console.WriteLine("Correct > Current" + Environment.NewLine + "Correct: " + curCorrectRatio + "Current: " + tempPicBoxRatio);
                    distortion = curCorrectRatio / tempPicBoxRatio - 1;
                    return (distortion.ToString("0.000"));
                }
                else if (curCorrectRatio == tempPicBoxRatio)
                {
                    return "Aspect ratio accurate!";
                }
                else
                {
                    return "Error";
                }
            }
            else
            {
                return "Aspect ratio accurate!";
            }
        }

        private void MenuLoadImage_Click(object sender, EventArgs e)
        {
            // this -------------V part actually opens the dialog, it is held within the if() statement. 
            // only opens if the V user clicks OK
            if (openPictureDialog.ShowDialog() == DialogResult.OK)
            {   //First, load the image in to a bitmap, using the filename from the dialog
                Bitmap loadedImage = new Bitmap(openPictureDialog.FileName);
                // Then, load an ARGB version in to the picturebox, also save original image to avoid loading multiple times
                pictureBox1.Image = originalImage = GetArgbVer(loadedImage);
                // Passing the new image's name to the PictureBox1 Tag field
                pictureBox1.Tag = openPictureDialog.SafeFileName;

                curCorrectRatio = (float)pictureBox1.Image.Width / (float)pictureBox1.Image.Height;
                imgIsLoaded = true;
                UpdateImgOptions();
                
                // Updating the display info
                UpdatePicboxInfo();
                UpdateText();

                if (chkAutoscaleLoad.Checked)
                {
                    menuFitWindow.PerformClick();
                }
            }
        }

        private void MenuClearImage_Click(object sender, EventArgs e)
        {
            // Clears any image that might be in the pictureBox, and if there isn't any being displayed, opens a messagebox
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = null;
                pictureBox1.Tag = null;
                UpdateText();
                imgIsLoaded = false;
                originalImage = null;
                UpdateImgOptions();
            }
            else
            {
                MessageBox.Show("No image currently being displayed");
            }
        }

        private void MenuCopyImage_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            MessageBox.Show("Image copied to clipboard");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this (a component that represents the program being run from) runs the 'Close' function
            this.Close();
        }

        private void menuFitWindow_Click(object sender, EventArgs e)
        {
            // Getting Image's original size
            int picWidth = pictureBox1.Image.Width;
            int picHeight = pictureBox1.Image.Height;
            this.Size = new Size(picWidth + 149, picHeight + 72);
        }

        private void MenuChangeBG_Click(object sender, EventArgs e)
        {
            // Runs the colour dialog in the if(), and if the user selects OK
            if (colourDialog1.ShowDialog() == DialogResult.OK)
            {
                // Change background colour to whatever the color dialog value was
                BackColor = colourDialog1.Color;
            }
            menuResetBGColour.Enabled = true;
        }
        
        private void MenuResetStretching_Click(object sender, EventArgs e)
        {
            // Getting Image's original size
            int picWidth = pictureBox1.Image.Width;
            int picHeight = pictureBox1.Image.Height;
            this.Size = new Size(picWidth + 149, picHeight + 72);
        }

        private void MenuBrightnessContrast_Click(object sender, EventArgs e)
        {
            // Initialising the new form, using the Form name as the Type. Becuase it is a struct, the 'new' keyword is required.
            // There is a constructor in the testDialog class that takes a Form1 Type, Form1 or "this" is being passed in to that constructor.
            testDialog dlgTest = new testDialog(this);
            
            // "DialogResult" Is a property that is assigned in the designer. 
            if (dlgTest.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show("You clicked OK");
            }
            else
            {
                Console.WriteLine("Cancelled");
            }
            dlgTest.Dispose();
        }

        private void MenuKanyeQuest_Click(object sender, EventArgs e)
        {
            YaMomma dlgYaMomma = new YaMomma(this);

            // If succesful, close the dlgBox
            if (dlgYaMomma.ShowDialog(this) == DialogResult.Yes)
            {
                dlgYaMomma.Close();
                dlgYaMomma.Dispose();
            }
        }

        private void MenuResetBGColour_Click(object sender, EventArgs e)
        {
            Color tempBGColor = Color.FromArgb(defaultBG.A, defaultBG.R, defaultBG.G, defaultBG.B);
            BackColor = tempBGColor;
            menuResetBGColour.Enabled = false;
        }

        private void MenuDisplayBGInfo_Click(object sender, EventArgs e)
        {
            // Now, a single variable can be changed, as opposed to many. Should consider creating a function that takes a passed value
            Color colourToTest = BackColor;
            // Feeding argb values to an integer
            int argbInt = colourToTest.ToArgb();

            MessageBox.Show
                ("Name: " + colourToTest.Name + 
                Environment.NewLine + "A: " + colourToTest.A +  " R: " + colourToTest.R + " G: " + colourToTest.G + " B: " + colourToTest.B +
                Environment.NewLine + "Brightness: " + colourToTest.GetBrightness() + 
                Environment.NewLine + "Hash Code: " + colourToTest.GetHashCode() +
                Environment.NewLine + "Hue: " + colourToTest.GetHue() + 
                Environment.NewLine + "Saturation: " + colourToTest.GetSaturation() +
                Environment.NewLine + "Int value: " + argbInt.ToString()
                );

            // Because 'Color' is a struct, you cannot assign it 'null', instead the 'Empty' value is assigned.
            colourToTest = Color.Empty;
        }
 
        // Update options that require an image to be loaded.
        private void UpdateImgOptions()
        {
            // Enable if image is currently loaded
            if (imgIsLoaded)
            {
                menuClearImage.Enabled = true;
                menuCopyImage.Enabled = true;
                //menuFitWindow.Enabled = true;
                //menuResetStretching.Enabled = true;

                // Activate or deactivate menu items depending on whether stretching is enabled
                if (chkStretch.Checked == true)
                {
                    menuResetStretching.Enabled = true;
                    menuFitWindow.Enabled = false;
                }
                else if (chkStretch.Checked == false)
                {
                    menuResetStretching.Enabled = false;
                    menuFitWindow.Enabled = true;
                }
            }
            // Disable if image is not currently loaded
            else
            {
                menuClearImage.Enabled = false;
                menuCopyImage.Enabled = false;
                menuFitWindow.Enabled = false;
                menuResetStretching.Enabled = false;
            }
        }

        // Converts an image into 32bit ARGB format for editing
        public Bitmap GetArgbVer(Image sourceImg)
        {
            // Here, the variable is created, matching the size of the source image
            Bitmap newBmp = new Bitmap (sourceImg.Width, sourceImg.Height,  PixelFormat.Format32bppArgb);

            // DISCLAIMER: I dont fully understand the following code
            // 'Graphics.FromImage' creates a Graphics object that is associated with a specified Image object.
            // The image still hasn't been drawn here yet, gfx is now created and associated with newBmp (which is still just a container)
            using(Graphics gfx = Graphics.FromImage(newBmp))
            {
                // I can see the source image and two rectangles being drawn to the same size as the source image. 
                gfx.DrawImage(sourceImg, new Rectangle(0, 0, newBmp.Width, newBmp.Height), new Rectangle(0, 0, newBmp.Width, newBmp.Height), GraphicsUnit.Pixel);
                gfx.Flush();
            }
            return newBmp;
        }

        public void TestFunction(int x)
        {
            MessageBox.Show(x.ToString());
            chkStretch.Checked = !chkStretch.Checked;
        }

        public void TestFunction2(string whatKanyeGot)
        {
            MessageBox.Show("Kanye be takin: " + whatKanyeGot);
        }

    }
}
