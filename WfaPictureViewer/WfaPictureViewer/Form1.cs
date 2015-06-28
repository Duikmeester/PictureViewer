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
using System.Runtime.InteropServices; // For 'Marshal.Copy'
using System.IO; // For 'MemoryStream'

namespace WfaPictureViewer
{
    /*
    * CLASS DECLARATION, "Form" is the base class and "Form1" is the derived class. The variables cannot be initialised here, only declared
    */
    public partial class Form1 : Form
    {
        string[] inputFileText = System.IO.File.ReadAllLines(@"..\..\input.txt"); // Initialise an array of each line of the input file
        float curCorrectRatio;
        float picBoxRatio;
        Color defaultBG;
        Bitmap originalImg; // The image when loaded, ARGB
        Bitmap currentImg; // The image being displayed, ARGB
        ImageFormat originalImgFormat;
        bool multipleImgLoaded;
        List<Bitmap> loadedImgs;
        int currentImgListIndex;

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
            this.MinimumSize = new Size(200, 100);
            // Bool initialisors
            chkAspectLock.Enabled = false;
            // BG Colour stuff
            defaultBG = BackColor;
            menuResetBGColour.Enabled = false;
            // Run the initial text and Option updates
            UpdateImgOptions();
            UpdateText();

            loadedImgs = new List<Bitmap>();
            //menuBrightnessContrast.Enabled = false;
            menuKanyeQuest.Enabled = false;

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

        /*
         * EVENT HANDLERS
         *///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        private void MenuLoadImage_Click(object sender, EventArgs e)
        {
            // only opens if the V user clicks OK
            if (openPictureDialog.ShowDialog() == DialogResult.OK)
            {   //First, load the image in to a bitmap, using the filename from the dialog
                Bitmap loadedImg = new Bitmap(openPictureDialog.FileName);
                // Passing the new image's name to the PictureBox1 Tag field
                pictureBox1.Tag = openPictureDialog.SafeFileName;
                originalImg = loadedImg;
                // GetArgbVer of the image, and pass it to UpdatePicBox
                UpdatePicBox(GetArgbVer(originalImg));
                // Format to avoid unnessecary conversion later 
                originalImgFormat = originalImg.RawFormat;
            }
        }

        private void menuAddAnother_Click(object sender, EventArgs e)
        {
            /*// only opens if the V user clicks OK
            if (openPictureDialog.ShowDialog() == DialogResult.OK)
            {   //First, load the image in to a bitmap, using the filename from the dialog
                Bitmap loadedImg = new Bitmap(openPictureDialog.FileName);
                // Passing the new image's name to the PictureBox1 Tag field
                pictureBox1.Tag = openPictureDialog.SafeFileName;
                originalImg = loadedImg;
                // GetArgbVer of the image, and pass it to UpdatePicBox
                UpdatePicBox(GetArgbVer(originalImg));
                //       format to avoid unnessecary conversion later 
                originalImgFormat = originalImg.RawFormat;
            }*/
        }

        private void ListManagerAdd()
        {

        }

        private void removeCurrentImg()
        {
            loadedImgs[currentImgListIndex] = null;
            StepThroughImgList(1);
        }

        // Update the picbox with a passed image, ususally straight from the Load event ^
        private void UpdatePicBox(Bitmap img)
        {
            // Assign passed img to current and picbox
            pictureBox1.Image = currentImg = img;

            curCorrectRatio = (float)pictureBox1.Image.Width / (float)pictureBox1.Image.Height;
            UpdateImgOptions();

            // Updating the display info
            UpdatePicboxInfo();
            UpdateText();

            if (chkAutoscaleLoad.Checked)
            {
                menuFitWindow.PerformClick();
            }
        }

        public void StepThroughImgList (int numSteps)
        {
            // if (there's something to move to)
            // change currentimage and picurebox image
            // currentlistindex + numSteps

        }

        private void MenuSaveImage_Click(object sender, EventArgs e)
        {
            // Creating an instance of the dialog to hold 
            using(SaveFileDialog dlgSaveImg = new SaveFileDialog())
            {
                dlgSaveImg.FileName = pictureBox1.Tag.ToString();
                dlgSaveImg.InitialDirectory = "C:/Desktop";
                dlgSaveImg.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";
                dlgSaveImg.Title = "Save Your Image";
            
                // Only initiate save if OK is received
                if (dlgSaveImg.ShowDialog() == DialogResult.OK)
                {
                    SaveImage(dlgSaveImg, currentImg);
                }
            }
        }

        private void SaveImage(SaveFileDialog dlg, Image img)
        {
            // Create a MemoryStream that will be minimally scoped
            using (MemoryStream memStream = new MemoryStream())
            {
                // Save the image to the memorystream in it's native format
                img.Save(memStream, originalImgFormat);

                // Creating an Image that can actually be saved - Should probably make everything up to this point a method
                // Should also incorporate some kind of using statement to close off the MemoryStream
                Image imgToSave = Image.FromStream(memStream);

                // FilterIndex appears to record which filetype is selected
                switch (dlg.FilterIndex)
                {
                    case 1:
                        imgToSave.Save(dlg.FileName, ImageFormat.Jpeg);
                        break;
                    case 2:
                        imgToSave.Save(dlg.FileName, ImageFormat.Bmp);
                        break;
                    case 3:
                        imgToSave.Save(dlg.FileName, ImageFormat.Png);
                        break;
                    case 4:
                        imgToSave.Save(dlg.FileName, ImageFormat.Tiff);
                        break;
                }
            } dlg.Dispose();
        }

        private void MenuClearImage_Click(object sender, EventArgs e)
        {
            // Clears any image that might be in the pictureBox, and if there isn't any being displayed, opens a messagebox
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = currentImg = null;
                pictureBox1.Tag = null;
                UpdateText();
                originalImg = null;
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

        private void menuClose_Click(object sender, EventArgs e)
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

        // Adjust the Brightness (and eventually Contrast) 
        private void MenuTransparency(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // The byte value is necessary for the image adjustments
                byte amount = 0;
                // Creating the Form that will be the dialog box
                Brightness dlgBright = new Brightness();

                // Result is saved before check, so the result can be checked in more than one bool statement
                DialogResult dlgResult =  dlgBright.ShowDialog();

                if (dlgResult == DialogResult.OK)
                {
                    // This method allows the data to be accessed without being public
                    amount =  dlgBright.getAmount();
                    pictureBox1.Image = currentImg = ApplyTransparency(currentImg, amount);
                }
                else if(dlgResult == DialogResult.Cancel)
                {
                    // Nowt
                }
                else
                {
                    MessageBox.Show("Error");
                }

                dlgBright.Dispose();
            }
        }

        private void menuGrayscale_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = currentImg = ApplyGrayscale(currentImg);
            }
        }

        private void menuSepia_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = currentImg = ApplySepia(currentImg);
            }
        }        

        private void menuExportChannels_Click(object sender, EventArgs e)
        {
            using (Channels dlgChannels = new Channels()) 
            {
                if (dlgChannels.ShowDialog() == DialogResult.OK)
                {
                    if (dlgChannels.colourChannel == "R" || dlgChannels.colourChannel == "G" || dlgChannels.colourChannel == "B" || dlgChannels.colourChannel == "A")
                    {
                        ExportChannel(dlgChannels.colourChannel, currentImg);
                    }
                    else if (dlgChannels.colourChannel == "All")
                    {
                        // Runs the method once for each channel
                        ExportChannel("R", currentImg);
                        ExportChannel("G", currentImg);
                        ExportChannel("B", currentImg);
                        ExportChannel("A", currentImg);
                    }
                    else if (dlgChannels.colourChannel == "AllBW")
                    {
                        ExportChannel("R", currentImg);
                        ExportChannel("G", currentImg);
                        ExportChannel("B", currentImg);
                        ExportChannel("ABW", currentImg);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred when registering choice of colour channel.");
                    }
                }
            }
        }

        private void menuTest_Click(object sender, EventArgs e)
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

        private void menuResetAdjustments_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                UpdatePicBox(originalImg);
            }
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

        /*
         * METHODS
         *///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // A method for converting an image in to 
        public byte[] ImgToByteArray (Image imgIn)
        {
            MemoryStream ms = new MemoryStream();
            MessageBox.Show(imgIn.ToString());
            // The image is saved to the MemoryStream in it's native format
            imgIn.Save(ms, imgIn.RawFormat);

            // The MemoryStream is converted to a byte array and returned
            return ms.ToArray();
        }

        public byte[] getByteArray (Image imgIn)
        {
            // Getting a correctly formatted image from GetArgbVer, This might not be necessary depending on how the picturebox stores the image, will check afterwards. 
            Bitmap updatedImg = GetArgbVer(imgIn);

            // Using BitmapData, the Lockbits method can be used to extract the image's pixel pixelData
            // Lockbits 'locks' a bitmap in to memory
            BitmapData pixelData = updatedImg.LockBits(new Rectangle(0, 0, imgIn.Width, imgIn.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // From here on, whenever the pointer is changed, it is changing the data stored at the pointer address

            // A pointer directed at the location of the first pixel read by LockBits. I believe this accesses the B, G, R, A info, as opposed to the pixels themselves
            IntPtr pixelDataPointer = pixelData.Scan0;

            MessageBox.Show(pixelDataPointer.ToString());
            // Here, an array of all the bytes that make up the pixles is created, The stride is the width of the array when also accounting for the extra buffering area.
            byte[] pixelByteArray = new byte[pixelData.Stride * pixelData.Height];

            Marshal.Copy(pixelDataPointer, pixelByteArray, 0, pixelByteArray.Length);

            return pixelByteArray;
            
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
            else if (this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized)
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
                else if (curCorrectRatio > tempPicBoxRatio)
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
 
        // Update options that require an image to be loaded.
        private void UpdateImgOptions()
        {
            // Enable if image is currently loaded
            if (pictureBox1.Image != null)
            {
                menuClearImage.Enabled = true;
                menuCopyImage.Enabled = true;
                menuTransparency.Enabled = true;
                menuGrayscale.Enabled = true;
                menuSepia.Enabled = true;
                menuSaveImage.Enabled = true;
                menuExportChannels.Enabled = true;
                menuResetAdjustments.Enabled = true;
                menuAddAnother.Enabled = true;

                // Activate or deactivate stretching-specific menu items depending on whether stretching is enabled
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
                menuTransparency.Enabled = false;
                menuGrayscale.Enabled = false;
                menuSepia.Enabled = false;
                menuSaveImage.Enabled = false;
                menuExportChannels.Enabled = false;
                menuResetAdjustments.Enabled = false;
                menuAddAnother.Enabled = false;
            }
        }

        // Converts an image into 32bit ARGB format for editing
        public Bitmap GetArgbVer(Image sourceImg)
        {
            // Here, the Bitmap is created, matching the size of the source image
            Bitmap newBmp = new Bitmap (sourceImg.Width, sourceImg.Height,  PixelFormat.Format32bppArgb);

            // DISCLAIMER: I dont fully understand the following code
            // 'Graphics.FromImage' creates a Graphics object that is associated with a specified Image object.
            // The image still hasn't been drawn here yet, gfx is now created and associated with newBmp (which is still just a container)
            using(Graphics gfx = Graphics.FromImage(newBmp))
            {
                // Here, the DrawImage function is able to draw based on an existing image. 
                gfx.DrawImage(sourceImg, new Rectangle(0, 0, newBmp.Width, newBmp.Height), new Rectangle(0, 0, newBmp.Width, newBmp.Height), GraphicsUnit.Pixel);
                gfx.Flush();
            }
            return newBmp;
        }

        // Apply transparency to the supplied image, defaulting the value to 100
        public Bitmap ApplyTransparency(Bitmap sourceImg, byte newAlphaAmount = 100)
        {
            /*// Getting a correctly formatted image from GetArgbVer, This might not be necessary depending on how the picturebox stores the image, will check afterwards. 
            Bitmap updatedImg = GetArgbVer(sourceImg);*/

            // Using BitmapData, the Lockbits method can be used to extract the image's pixel pixelData
            // Lockbits 'locks' a bitmap in to memory
            BitmapData pixelData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
           
            // From here on, whenever the pointer is changed, it is changing the data stored at the pointer address

            // A pointer directed at the location of the first pixel read by LockBits. I believe this accesses the B, G, R, A info, as opposed to the pixels themselves
            IntPtr pixelDataPointer = pixelData.Scan0;

            MessageBox.Show(pixelDataPointer.ToString());
            // Here, an array of all the bytes that make up the pixles is created, The stride is the width of the array when also accounting for the extra buffering area.
            byte[] pixelByteArray = new byte[pixelData.Stride * pixelData.Height];

            // Now the Marshal.Copy function copies pixel pixelData from pointer > byte array, preparing it for editing
            Marshal.Copy(pixelDataPointer, pixelByteArray, 0, pixelByteArray.Length);

            for (int i = 3; i < pixelByteArray.Length ; i += 4)
            {
                pixelByteArray[i] = newAlphaAmount;
            }

            // Copy the byte pixelData back to the pointer, noting that the formatting of the 0 moves to follow the array
            Marshal.Copy(pixelByteArray, 0, pixelDataPointer, pixelByteArray.Length);

            // The data does not have to be passed to pixelData, because the pointer address was pointing to the data all along.

            // The new edited pixels are passed back to the image
            sourceImg.UnlockBits(pixelData);

            pixelByteArray = null;
            pixelData = null;

            return sourceImg;
        }

        // Convert the image to Grayscale
        public Bitmap ApplyGrayscale(Bitmap sourceImg)
        {
            /*// Get a usable Bitmpa from the source image
            Bitmap updatedImg = GetArgbVer(sourceImg);*/

            // Get the bit data from the image and draw it in to imgData
            BitmapData imgData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // A pointer is directed to the address of the first piece of data, this is now an int that holds the ARGB data of a given pixel (I think). A printf returns an int
            IntPtr dataPointer = imgData.Scan0;

            // An EMPTY array that will hold all of the data that makes up the image
            byte[] pixelByteBuffer = new byte[imgData.Stride * imgData.Height];

            // Copy data from the pointer to the buffer
            Marshal.Copy(dataPointer, pixelByteBuffer, 0, pixelByteBuffer.Length);

            Grayscale dlgGrayscale = new Grayscale();
            DialogResult dlgResult;
            dlgResult =  dlgGrayscale.ShowDialog();

            // This will be used to hold the sum of the RGB values, and will be applied to each in turn
            float rgb;

            // The 'Luminosity' button is set to "OK".
            if (dlgResult == DialogResult.OK)
            {
                for (int i = 0; i < pixelByteBuffer.Length; i += 4)
                {
                    // 'Luminosity' method, places more priority on the green, as our eyes are more sensetive to that part of the spectrum
                    rgb = pixelByteBuffer[i] * 0.07f;
                    rgb += pixelByteBuffer[i + 1] * 0.72f;
                    rgb += pixelByteBuffer[i + 2] * 0.21f;

                    // The'amount of colour' value is given to each element. This syntax means the rgb value only needs to be cast once
                    pixelByteBuffer[i + 2] = pixelByteBuffer[i + 1] = pixelByteBuffer[i] = (byte)rgb;
                }
            }
            // The 'Average' button is set to "Yes".
            else if (dlgResult == DialogResult.Yes)
            {
                for (int i = 0; i < pixelByteBuffer.Length; i += 4)
                {
                    // "Average method", the sum of the partial B, G, and R values gives an average 'amount of colour' value
                    rgb = (pixelByteBuffer[i] + pixelByteBuffer[i + 1] + pixelByteBuffer[i + 2]) / 3;

                    // The'amount of colour' value is given to each element. This syntax means the rgb value only needs to be cast once
                    pixelByteBuffer[i + 2] = pixelByteBuffer[i + 1] = pixelByteBuffer[i] = (byte)rgb;
                }
            }
            else
            {
                // Nowt.
            }

            // Copy the data back to the pointer
            Marshal.Copy(pixelByteBuffer, 0, dataPointer, pixelByteBuffer.Length);
            
            sourceImg.UnlockBits(imgData);

            pixelByteBuffer = null;
            imgData = null;

            return sourceImg;
        }

        public Bitmap ApplySepia(Bitmap sourceImg)
        {
            /*// Get a Bitmap formatted version of the source image
            Bitmap convertedImg = GetArgbVer(sourceImg);*/

            // Lock the pixel data from the source image into imgData, a BitmapData Type container. 
            BitmapData imgData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr dataPointer = imgData.Scan0; 

            byte[] imgBuffer = new byte[imgData.Stride * imgData.Height];

            // give data to buffer
            Marshal.Copy(dataPointer, imgBuffer, 0, imgBuffer.Length);

            float B;
            float G;
            float R;

            // Apply Sepia filtering
            for (int i = 0; i < imgBuffer.Length; i += 4)
            {

                // Original RGB values are saved because they are used to construct the new ones and need to stay unchanged.
                B = 0;
                G = 0;
                R = 0;

                B = (imgBuffer[i] * 0.13f + imgBuffer[i + 1] * 0.53f + imgBuffer[i + 2] * 0.27f);

                G = (imgBuffer[i] * 0.16f + imgBuffer[i + 1] * 0.68f + imgBuffer[i + 2] * 0.34f);

                R = (imgBuffer[i] * 0.18f + imgBuffer[i + 1] * 0.76f + imgBuffer[i + 2] * 0.39f);

                if (B > 255)
                    B = 255;

                if (G > 255)
                    G = 255;

                if (R > 255)
                    R = 255;

                imgBuffer[i] = (byte)B;

                imgBuffer[i + 1] = (byte)G;

                imgBuffer[i + 2] = (byte)R;

            }

            // give back to pointer
            Marshal.Copy(imgBuffer, 0, dataPointer, imgBuffer.Length);

            // give to bitmap type image for return
            sourceImg.UnlockBits(imgData);

            imgBuffer = null;
            imgData = null;

            return sourceImg;
        }

        public void ExportChannel(string channel, Bitmap img)
        {
            // Importantly, a completely new Bitmap has to be created from the passed image, to avoid the pointer (I think) editing things like currentImg
            Bitmap channelImg = new Bitmap(img);
            BitmapData imgData = channelImg.LockBits(new Rectangle(0, 0, channelImg.Width, channelImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr dataPointer = imgData.Scan0;

            byte[] imgBuffer = new byte [imgData.Stride * imgData.Height];

            Marshal.Copy(dataPointer, imgBuffer, 0, imgBuffer.Length);

            // An array to hold the offset ints for the channels that will be changed. 
            // B = 0 G = 1 R = 2 A = 3
            int[] bytesToChange = new int[3];
            byte value = 0;

            switch (channel)
            {
                case "R":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;
                    bytesToChange[2] = 1; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "G":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 2;
                    bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "B":
                    bytesToChange[0] = 1;
                    bytesToChange[1] = 2;
                    bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "A":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;
                    bytesToChange[2] = 2;
                    value = 255;
                    break;
                case "ABW":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;
                    bytesToChange[2] = 2;
                    value = 255;
                    break;
            }

            for (int i = 0; i < imgBuffer.Length; i += 4)
            {
                // If we're exporting the B/W alpha image
                if (channel == "ABW")
                {
                    // The new value for R, G & B is equal to the alpha channel
                    value = imgBuffer[i + 3];
                }
                imgBuffer[i + bytesToChange[0]] = value;
                imgBuffer[i + bytesToChange[1]] = value;
                imgBuffer[i + bytesToChange[2]] = value;
            }

            Marshal.Copy(imgBuffer, 0, dataPointer, imgBuffer.Length);

            channelImg.UnlockBits(imgData);

            

            // Creating an instance of the dialog to hold 
            using (SaveFileDialog dlgSaveChannel = new SaveFileDialog())
            {
                // Converging the alphas for the puspose of savedialog creation
                if (channel == "ABW")
                    channel = "A";

                dlgSaveChannel.FileName = (Path.GetFileNameWithoutExtension(pictureBox1.Tag.ToString()) + "_" + channel);
                dlgSaveChannel.InitialDirectory = "C:/Desktop";
                dlgSaveChannel.Title = "Save (" + channel + ") Image Channel";

                if (channel == "A")
                {
                    dlgSaveChannel.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";
                }
                else
                {
                    dlgSaveChannel.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";
                }


                if (dlgSaveChannel.ShowDialog() == DialogResult.OK)
                {
                    SaveImage(dlgSaveChannel, channelImg);
                }
                else
                {
                    // Saving failed
                }
            }
            channel = null;
            imgBuffer = null;
            imgData = null;
        }

        public void TestFunction(int x)
        {
            MessageBox.Show(pictureBox1.Image.ToString());
            //MessageBox.Show(x.ToString());
            //chkStretch.Checked = !chkStretch.Checked;
        }

        public void TestFunction2(string whatKanyeGot)
        {
            
        }
    }
}
