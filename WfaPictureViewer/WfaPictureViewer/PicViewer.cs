using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging; // For 'PixelFormat' and others, redrawing Bitmaps
using System.Linq;
using System.Windows.Forms;
using System.Threading; // For 'Thread.Sleep'
using System.Runtime.InteropServices; // For 'Marshal.Copy'
using System.IO; // For 'MemoryStream'

namespace WfaPictureViewer
{
    // CLASS DECLARATION, "Form" is the base class and "PicViewer" is the derived class. The variables cannot be initialised here, only declared
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class PicViewer : Form
    {
        string[] inputFileText = System.IO.File.ReadAllLines(@"..\..\input.txt"); // Initialise an array of each line of the input file
        float picBoxRatio;
        Color defaultBG;
        List<LoadedImage> listLoadedImg;
        int curImgIndex, curGalleryHeight; // The height of the flowGallery
        bool pnlGalleryHidden;

        //CONSTRUCTOR - Does not have a return type and shares a name with the class
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public PicViewer()
        {
            InitializeComponent();

            // Allow the form to process key inputs
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

            // "Size" is a struct, so you can't simply declare this.MinimumSize.Size = x,y
            this.MinimumSize = new Size(400, 200);
            // Bool initialisors
            chkAspectLock.Enabled = false;
            // BG Colour stuff
            defaultBG = BackColor;
            menuResetBGColour.Enabled = false;
            // Run the initial text and Option updates
            UpdateImgOptions();
            UpdateText();

            // Gallery initialisation
            pnlGalleryHidden = false;
            // Don't want the panel that holds the flowgallery to scroll, only want the gallery itself to scroll
            pnlGallery.AutoScrollMinSize = new Size(1, 1);
            listLoadedImg = new List<LoadedImage>();
            UpdateGallery();
        }

        // METHODS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Creating a LoadedImage object, most of which is handled by it's constructor.
        private void CreateLoadedImg(string path)
        {
            //First, create a Bitmap with the filepath, and convert it to Argb.
            Bitmap baseImage = GetArgbVer(new Bitmap(path));

            // Creating a LoadedImage object with, passing filepath and Bitmap to constructor
            LoadedImage img = new LoadedImage(path, baseImage, this, curImgIndex); 

            // Add the new object to the list
            listLoadedImg.Add(img);
            curImgIndex++;
        }

        private void removeCurrentImg()
        {
            listLoadedImg[curImgIndex] = null;
            StepThroughImgList(1);
        }

        // Update the picbox with a passed image, ususally straight from the Load event ^
        private void UpdatePicbox(LoadedImage img)
        {
            // Assign current img to current and picbox
            picBoxMain.Image = img.GetBitmap("c");

            // Updating the display info
            UpdatePicboxInfoAndSizeMode();
            UpdateText();
            UpdateImgOptions();

            pnlPicBox.AutoScrollMinSize = new Size(img.GetBitmap("c").Width - 10, img.GetBitmap("c").Height - 10);

            if (chkAutoscaleLoad.Checked)
                menuFitWindow.PerformClick();
        }

        private void UpdateGallery()
        {// An array of the columnstyles, used below to define width of a specific column
            TableLayoutColumnStyleCollection styleCollection = tableLayoutPanel1.ColumnStyles;

            if (pnlGalleryHidden == false)
            {
                // Empty all thumbnails from gallery
                flowGallery.Controls.Clear();
                // First, update each thumbnail so they match the actual current versions of the image, if there's an image loaded
                if (listLoadedImg != null && listLoadedImg.Count() != 0)
                {
                    curGalleryHeight = 0;
                    for (int i = 0; i < listLoadedImg.Count(); i++)
                    {
                        // Increase the height of the gallery to fit each thumbnail.
                        curGalleryHeight += listLoadedImg[i].GetThumbnail().Height;

                        // Update label with semantic numbering
                        listLoadedImg[i].UpdateLblThumbText((i + 1).ToString());
                        // Update .Names to i, which matches their imgIndex, and will be used when clicked on to dictate the image that gets loaded
                        listLoadedImg[i].UpdateLblThumbName(i.ToString());
                        listLoadedImg[i].GetThumbnail().Name = i.ToString();

                        flowGallery.Controls.Add(listLoadedImg[i].GetThumbnail());

                        UpdateGallerySelection();
                    }
                }
                // Column, panel and flowgallery all have size set, curGalleryHeight is used to define when scrollbars appear
                // 137 is 100 image width + fixed3d padding + scrollbar width
                styleCollection[1].Width = flowGallery.Width = pnlGallery.Width = 137;

                flowGallery.AutoScrollMinSize = new Size(1, curGalleryHeight);
            }
            else
            {
                flowGallery.Controls.Clear();
                styleCollection[1].Width = flowGallery.Width = pnlGallery.Width = 0;
                flowGallery.AutoScrollMinSize = new Size(1, curGalleryHeight);
            }
        }

        public void UpdateGallerySelection()
        {
            for (int i = 0; i < listLoadedImg.Count(); i++)
            {
                // Aesthetic changing of borderstyle based on image selection.
                if (i == curImgIndex)
                    listLoadedImg[i].GetThumbnail().BorderStyle = BorderStyle.Fixed3D;
                else if (listLoadedImg[i].GetThumbnail().BorderStyle != BorderStyle.None)
                    listLoadedImg[i].GetThumbnail().BorderStyle = BorderStyle.None;
            }
        }

        

        public void StepThroughImgList(int numSteps)
        {
            // If the desired destination is not null
            if ((curImgIndex + numSteps) >= 0 && (curImgIndex + numSteps) <= (listLoadedImg.Count - 1))
            {
                curImgIndex += numSteps;
                UpdatePicbox(listLoadedImg[curImgIndex]);
                UpdateGallerySelection();
            }
        }

        public void ExportChannel(string channel, Bitmap img, bool bypass)
        {
            // Importantly, a completely new Bitmap has to be created from the passed image, to avoid the pointer (I think) editing things like curImg
            Bitmap channelImg = new Bitmap(img);
            BitmapData imgData = channelImg.LockBits(new Rectangle(0, 0, channelImg.Width, channelImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr dataPointer = imgData.Scan0;
            byte[] imgBuffer = new byte[imgData.Stride * imgData.Height];
            Marshal.Copy(dataPointer, imgBuffer, 0, imgBuffer.Length);

            // An array to hold the offset ints that represent the channels to be changed. 
            // B = 0 G = 1 R = 2 A = 3
            int[] bytesToChange = new int[2]; // initialised with 3 indicies, as R,G & B cases only require two channel edits, a third is added later only if necessary.
            byte value = 0;

            switch (channel)
            {
                case "R":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;                    //bytesToChange[2] = 1; // Duped to avoid editing the alpha - Maybe look at creating the array inside here to make less wasteful? 
                    value = 0;
                    break;
                case "G":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 2;                    //bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "B":
                    bytesToChange[0] = 1;
                    bytesToChange[1] = 2;                    //bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "A": case "ABW":
                    bytesToChange = new int[3];
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;
                    bytesToChange[2] = 2;
                    value = 255;
                    break;
            }

            for (int i = 0; i < imgBuffer.Length; i += 4)
            {
                // If we're exporting the B/W alpha image, the new value for R, G & B is equal to the alpha channel
                if (channel == "ABW")
                    value = imgBuffer[i + 3];
                // For each byte that is to be changed (as offset of current buffer pixel position), change to value
                for (int j = 0; j < bytesToChange.Length;  j++)
                {
                    imgBuffer[i + bytesToChange[j]] = value;
                }
            }
            Marshal.Copy(imgBuffer, 0, dataPointer, imgBuffer.Length);
            channelImg.UnlockBits(imgData);

            // Creating a minimally scoped instance of the dialog 
            using (SaveFileDialog dlgSaveChannel = new SaveFileDialog())
            {
                // Converging the alphas for the puspose of savedialog creation
                if (channel == "ABW")
                    channel = "A";

                // Preparing default state for dlg
                dlgSaveChannel.FileName = Path.GetFileNameWithoutExtension(listLoadedImg[curImgIndex].GetName()) + "_" + channel;
                dlgSaveChannel.InitialDirectory = "C://Desktop";
                dlgSaveChannel.Title = "Save (" + channel + ") Image Channel";
                dlgSaveChannel.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";

                if (bypass == false)
                {
                    if (dlgSaveChannel.ShowDialog() == DialogResult.OK)
                        SaveImage(dlgSaveChannel, channelImg);
                }
                else
                {
                    // Need to get type from batch dialog instead
                    string path = listLoadedImg[curImgIndex].GetDefaultDir() + "\\" + Path.GetFileNameWithoutExtension(listLoadedImg[curImgIndex].GetName()) + "_" + channel + "." + listLoadedImg[curImgIndex].GetExportFormat().ToString();
                    channelImg.Save(path, listLoadedImg[curImgIndex].GetExportFormat());
                }
            }
        }

        // Utilises the Batch dialog window to export, rename, apply file types to files AFTER processing
        private void BatchFileProcess()
        {
            using (BatchSettings dlgBatch = new BatchSettings())
            {
                if (dlgBatch.ShowDialog() == DialogResult.OK)
                {
                    // If exporting is not turned on
                    if (dlgBatch.export == false)
                    {
                        // nowt
                    }
                    // If it IS turned on
                    else
                    {
                        // If isBypassing dialog is not activated
                        if (dlgBatch.bypass == false)
                        {
                            foreach (LoadedImage img in listLoadedImg)
                            {
                                using (SaveFileDialog dlgSave = new SaveFileDialog())
                                {
                                    dlgSave.FileName = img.GetName();
                                    dlgSave.InitialDirectory = "C:/Desktop";
                                    dlgSave.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";
                                    dlgSave.Title = "Save Your Image";

                                    if (dlgSave.ShowDialog() == DialogResult.OK)
                                    {
                                        SaveImage(dlgSave, img.GetBitmap("c"));
                                    }
                                }
                            }
                        }
                        // if it IS turned on
                        else
                        {
                            string filename = null;
                            ImageFormat format = null;

                            // Applying the selected filetype from the dlgBatch using an index. THIS REQUIRES THAT THE INDICIES MATCH the cases below
                            switch (dlgBatch.fileType)
                            {
                                case 0:
                                    format = ImageFormat.Jpeg;
                                    break;
                                case 1:
                                    format = ImageFormat.Bmp;
                                    break;
                                case 2:
                                    format = ImageFormat.Png;
                                    break;
                                case 3:
                                    format = ImageFormat.Tiff;
                                    break;
                            }

                            for (int i = 0; i < listLoadedImg.Count(); i++)
                            {
                                if (dlgBatch.rename == true)
                                {
                                    string path = Path.GetDirectoryName(dlgBatch.fileName) + "\\" + Path.GetFileNameWithoutExtension(dlgBatch.fileName) + "_grayscale_" + i + Path.GetExtension(dlgBatch.fileName);
                                    filename = path;
                                }
                                else
                                    filename = (listLoadedImg[curImgIndex].GetDefaultDir() + "\\" + listLoadedImg[i].GetName() + "_Grayscale" + dlgBatch.fileTypeString);

                                listLoadedImg[i].GetBitmap("c").Save(filename, format);
                            }
                        }
                    }
                }
            }
        }

        // Combined method that updates the picbox label info display and the sizemode of the image
        private void UpdatePicboxInfoAndSizeMode()
        {
            // Writing the file info to the label
            if (picBoxMain.Image != null)
            {
                lblPicInfo.Text = ("File Name: " + listLoadedImg[curImgIndex].GetName() + Environment.NewLine + "H: " + picBoxMain.Image.Height + Environment.NewLine + "W: " + picBoxMain.Image.Width + Environment.NewLine + "Aspect Ratio: " + GetPicBoxRatio() + Environment.NewLine + "Stretching: " + GetRatioDistortion());
                // Note: "AutoSize" allows the use of the scroll bars
                if (chkStretch.Checked == true)
                    picBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
                else if (picBoxMain.Size.Width < picBoxMain.Image.Width || picBoxMain.Size.Height < picBoxMain.Image.Height)
                    picBoxMain.SizeMode = PictureBoxSizeMode.AutoSize;
                else // includes if picBoxMain > PicBoxMain.Image
                    picBoxMain.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
                lblPicInfo.Text = null;
        }

        // Method is called whenever upon changing the state of the picturebox. 
        private void UpdateText()
        {
            if (picBoxMain.Image != null)
                lblPicNotifier.Text = "Img " + (curImgIndex + 1) + "/" + listLoadedImg.Count;
            else
                lblPicNotifier.Text = "Img 0/0";
        }

        private void CycleMaximised()
        {
            // If the form is not maximised
            if (this.FormBorderStyle == FormBorderStyle.Sizable && this.WindowState == FormWindowState.Normal)
            {
                // Maximise
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            // else if the form IS maximised
            else if (this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized)
            {
                // Un-maximise
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                MessageBox.Show("Fullscreening error");
                this.Close();
            }
        }

        // After resizing
        private void Form1_PostResize(object sender, EventArgs e)
        {
            UpdatePicboxInfoAndSizeMode();
        }

        // Returns the aspect ratio of the image currently loaded in the picBox
        private float GetPicBoxRatio()
        {
            // If there is actually an image loaded
            if (picBoxMain.Image != null)
            {
                // Find out if the stretching is turned on
                if (chkStretch.Checked == false)
                {
                    // The ratio is the width of the image divided by the height
                    picBoxRatio = (float)picBoxMain.Image.PhysicalDimension.Width / (float)picBoxMain.Image.PhysicalDimension.Height;
                    return picBoxRatio;
                }
                // Or if the stretch checkbox IS checked, instead get the values of the pictureBox itself, as the iamge will match it
                else
                {
                    picBoxRatio = (float)picBoxMain.Width / (float)picBoxMain.Height;
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
                double tempPicBoxRatio = GetPicBoxRatio();
                double distortion;
                double tmpCorrectRatio = listLoadedImg[curImgIndex].GetCorrectRatio();

                // if the images 'correct' ratio (e.g. 1.43322) is less than current 
                if (tmpCorrectRatio < tempPicBoxRatio)
                {
                    Console.WriteLine("Correct < Current" + Environment.NewLine + "Correct: " + tmpCorrectRatio + "Current: " + tempPicBoxRatio);
                    distortion = tmpCorrectRatio / tempPicBoxRatio - 1;
                    return (distortion.ToString("0.000"));
                }
                else if (tmpCorrectRatio > tempPicBoxRatio)
                {
                    Console.WriteLine("Correct > Current" + Environment.NewLine + "Correct: " + tmpCorrectRatio + "Current: " + tempPicBoxRatio);
                    distortion = tmpCorrectRatio / tempPicBoxRatio - 1;
                    return (distortion.ToString("0.000"));
                }
                else if (tmpCorrectRatio == tempPicBoxRatio)
                    return "Aspect ratio accurate!";

                else
                    return "Error";
            }
            else
                return "Aspect ratio accurate!";
        }

        // Update options that require an image to be loaded.
        private void UpdateImgOptions()
        {
            // Enable if image is currently loaded
            if (picBoxMain.Image != null)
            {
                menuClearImage.Enabled =
                menuCopyImage.Enabled =
                menuTransparency.Enabled =
                menuGrayscale.Enabled =
                menuSepia.Enabled =
                menuSaveImage.Enabled =
                menuChannels.Enabled =
                menuResetAdjustments.Enabled = true;

                if (listLoadedImg.Count >= 2)
                {
                    menuBatch.Enabled = true;
                    //MenuHideGallery.Enabled = true;
                }
                else
                {
                    menuBatch.Enabled = false;
                    //MenuHideGallery.Enabled = false;
                }

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

                // If there is something in the list
                if (listLoadedImg.Count > 0)
                {
                    // If the list only has one item
                    if (listLoadedImg.Count == 1)
                    {
                        btnNavigateRight.Enabled = false;
                        btnNavigateLeft.Enabled = false;
                    }
                    // if it has more than one, and user is on the first
                    else if (listLoadedImg.Count > 1 && curImgIndex == 0)
                    {
                        btnNavigateRight.Enabled = true;
                        btnNavigateLeft.Enabled = false;
                    }
                    // if on the last item, and there is more than one
                    else if (curImgIndex == listLoadedImg.Count - 1 && listLoadedImg.Count > 1)
                    {
                        btnNavigateRight.Enabled = false;
                        btnNavigateLeft.Enabled = true;
                    }
                    // Otherwise, acrtivate them both
                    else
                    {
                        btnNavigateLeft.Enabled = true;
                        btnNavigateRight.Enabled = true;
                    }
                }
                // If nothing in list
                else
                {
                    btnNavigateRight.Enabled = false;
                    btnNavigateLeft.Enabled = false;
                }

            }
            // Disable if image is not currently loaded
            else
            {
                menuLoadImage.Enabled = true;
                menuClearImage.Enabled = false;
                menuCopyImage.Enabled = false;
                menuFitWindow.Enabled = false;
                menuResetStretching.Enabled = false;
                menuTransparency.Enabled = false;
                menuGrayscale.Enabled = false;
                menuSepia.Enabled = false;
                menuSaveImage.Enabled = false;
                menuChannels.Enabled = false;
                menuResetAdjustments.Enabled = false;

                btnNavigateLeft.Enabled = false;
                btnNavigateRight.Enabled = false;

                menuBatch.Enabled = false;
            }
        }

        // Converts an image into 32bit ARGB format for editing
        public Bitmap GetArgbVer(Image sourceImg)
        {
            // Here, the Bitmap is created, matching the size of the source image
            Bitmap newBmp = new Bitmap(sourceImg.Width, sourceImg.Height, PixelFormat.Format32bppArgb);

            // DISCLAIMER: I dont fully understand the following code
            // 'Graphics.FromImage' creates a Graphics object that is associated with a specified Image object.
            // The image still hasn't been drawn here yet, gfx is now created and associated with newBmp (which is still just a container)
            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                // Here, the DrawImage function is able to draw based on an existing image. 
                gfx.DrawImage(sourceImg, new Rectangle(0, 0, newBmp.Width, newBmp.Height), new Rectangle(0, 0, newBmp.Width, newBmp.Height), GraphicsUnit.Pixel);
                gfx.Flush();
            }
            return newBmp;
        }

        // Apply transparency to the supplied image, defaulting the value to 100
        public Bitmap ApplyTransparency(Bitmap passedImg, byte newAlphaAmount = 100)
        {
            // Creating a new memory assignment, so the pointer(I think) doesn't chance the originalImg
            Bitmap sourceImg = new Bitmap(passedImg);

            // Using BitmapData, the Lockbits method can be used to extract the image's pixel pixelData
            // Lockbits 'locks' a bitmap in to memory
            BitmapData pixelData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // From here on, whenever the pointer is changed, it is changing the data stored at the pointer address

            // A pointer directed at the location of the first pixel read by LockBits. I believe this accesses the B, G, R, A info, as opposed to the pixels themselves
            IntPtr pixelDataPointer = pixelData.Scan0;

            // Here, an array of all the bytes that make up the pixles is created, The stride is the width of the array when also accounting for the extra buffering area.
            byte[] pixelByteArray = new byte[pixelData.Stride * pixelData.Height];

            // Now the Marshal.Copy function copies pixel pixelData from pointer > byte array, preparing it for editing
            Marshal.Copy(pixelDataPointer, pixelByteArray, 0, pixelByteArray.Length);

            for (int i = 3; i < pixelByteArray.Length; i += 4)
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
        public Bitmap ApplyGrayscale(Bitmap passedImg, string algorithm)
        {
            // Creating a new memory assignment, so the pointer(I think) doesn't chance the originalImg
            Bitmap sourceImg = new Bitmap(passedImg);

            // Get the bit data from the image and draw it in to imgData
            BitmapData imgData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // A pointer is directed to the address of the first piece of data, this is now an int that holds the ARGB data of a given pixel (I think). A printf returns an int
            IntPtr dataPointer = imgData.Scan0;

            // An EMPTY array that will hold all of the data that makes up the image
            byte[] pixelByteBuffer = new byte[imgData.Stride * imgData.Height];

            // Copy data from the pointer to the buffer
            Marshal.Copy(dataPointer, pixelByteBuffer, 0, pixelByteBuffer.Length);

            // This will be used to hold the sum of the RGB values, and will be applied to each in turn
            float rgb;

            if (algorithm == "luminosity")
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
            else if (algorithm == "average")
            {
                for (int i = 0; i < pixelByteBuffer.Length; i += 4)
                {
                    // "Average method", the sum of the partial B, G, and R values gives an average 'amount of colour' value
                    rgb = (pixelByteBuffer[i] + pixelByteBuffer[i + 1] + pixelByteBuffer[i + 2]) / 3;

                    // The'amount of colour' value is given to each element. This syntax means the rgb value only needs to be cast once
                    pixelByteBuffer[i + 2] = pixelByteBuffer[i + 1] = pixelByteBuffer[i] = (byte)rgb;
                }
            }

            // Copy the data back to the pointer
            Marshal.Copy(pixelByteBuffer, 0, dataPointer, pixelByteBuffer.Length);

            sourceImg.UnlockBits(imgData);
            pixelByteBuffer = null;
            imgData = null;
            return sourceImg;
        }

        public Bitmap ApplySepia(Bitmap passedImg)
        {
            // Creating a new memory assignment, so the pointer(I think) doesn't chance the originalImg
            Bitmap sourceImg = new Bitmap(passedImg);

            // Lock the pixel data from the source image into imgData, a BitmapData Type container. 
            BitmapData imgData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr dataPointer = imgData.Scan0;

            byte[] imgBuffer = new byte[imgData.Stride * imgData.Height];

            // give data to buffer
            Marshal.Copy(dataPointer, imgBuffer, 0, imgBuffer.Length);

            float B, G, R;

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

        // EVENT HANDLERS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void Form1_KeyPress(object sender, KeyPressEventArgs key)
        {
            if (key.KeyChar.ToString() == "f")
                CycleMaximised();
            if (key.KeyChar.ToString() == "x")
                StepThroughImgList(1);
            if (key.KeyChar.ToString() == "z")
                StepThroughImgList(-1);
        }

        private void MenuLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlgOpen = new OpenFileDialog())
            {
                dlgOpen.InitialDirectory = "C:/Desktop";
                dlgOpen.Filter = "All Image Files|*.jpg; *.bmp; *.png; *.tiff|Jpeg Image|*.jpg|Bmp Image|*.bmp|Png Image|*.png|Tiff Image|*.tiff";
                dlgOpen.Title = "Select an image to Load";
                dlgOpen.Multiselect = true;

                // only opens if the V user clicks OK
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    // 'Filenames' is a property that holds an array of strings, iterating through the array each can be added to the LoadedImg list
                    foreach (string name in dlgOpen.FileNames)
                    {
                        CreateLoadedImg(name);
                    }
                    curImgIndex = 0; // change to equivalent of total length, or currently displayed??
                    UpdatePicbox(listLoadedImg[curImgIndex]);
                    UpdateGallery();
                }
            }
        }

        private void menuAddAnother_Click(object sender, EventArgs e)
        {
            /* using (OpenFileDialog dlgOpen = new OpenFileDialog())
             {
                 dlgOpen.InitialDirectory = "C:/Desktop";
                 dlgOpen.Filter = "All Image Files|*.jpg; *.bmp; *.png; *.tiff|Jpeg Image|*.jpg|Bmp Image|*.bmp|Png Image|*.png|Tiff Image|*.tiff";
                 dlgOpen.Title = "Select an image to Load";
                 dlgOpen.Multiselect = true;

                 if (listLoadedImg.Count > 0 && dlgOpen.ShowDialog() == DialogResult.OK)
                 {
                     // Every file that is selected in the dlg
                     foreach (string file in dlgOpen.FileNames)
                     {
                         CreateLoadedImg(file, true);
                         UpdatePicbox(listLoadedImg[curImgIndex]);
                         UpdateGallery();
                     }
                 }
             }*/
        }

        // Run when a thumbnail picbox in the gallery is clicked, also applied to the label.
        public void picBox_Click(object sender, EventArgs e)
        {
            // Check what type the sender was (whether the user clicked on the label or the picturebox
            // The index is stored in the object as it's name, and is used to the dictate the curimg index

            if (sender is PictureBox)
                curImgIndex = Int32.Parse(((PictureBox)sender).Name);
            else if (sender is Label)
                curImgIndex = Int32.Parse(((Label)sender).Name);
            UpdatePicbox(listLoadedImg[curImgIndex]);
            UpdateGallerySelection();
        }

        private void btnNavigateLeft_Click(object sender, EventArgs e)
        {
            StepThroughImgList(-1);
        }

        private void btnNavigateRight_Click(object sender, EventArgs e)
        {
            StepThroughImgList(1);
        }

        private void MenuSaveImage_Click(object sender, EventArgs e)
        {
            // Creating an instance of the dialog to hold 
            using (SaveFileDialog dlgSaveImg = new SaveFileDialog())
            {
                dlgSaveImg.FileName = listLoadedImg[curImgIndex].GetName();
                dlgSaveImg.InitialDirectory = "C:/Desktop";
                dlgSaveImg.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";
                dlgSaveImg.Title = "Save Your Image";

                // Only initiate save if OK is received
                if (dlgSaveImg.ShowDialog() == DialogResult.OK)
                    SaveImage(dlgSaveImg, listLoadedImg[curImgIndex].GetBitmap("c"));
            }
        }

        private void SaveImage(SaveFileDialog dlg, Image img)
        {
            // Create a MemoryStream that will be minimally scoped
            using (MemoryStream memStream = new MemoryStream())
            {
                // Save the image to the memorystream in it's native format
                img.Save(memStream, listLoadedImg[curImgIndex].GetOriginalFormat());

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
            if (picBoxMain.Image != null)
            {
                // Resetting the scrollbars
                pnlPicBox.AutoScrollMinSize = new Size(0, 0);

                // Removes the currently displayed item from the list
                listLoadedImg.Remove(listLoadedImg[curImgIndex]);

                if (listLoadedImg.Count == 0)
                {
                    // Updating needs to be done here, because UpdatePicbox wont be called
                    curImgIndex = 0;
                    UpdateText();
                    UpdatePicboxInfoAndSizeMode();
                    UpdateImgOptions();
                    UpdateGallery();
                }
                // If there is still an image loaded
                else
                {
                    // if on first index, list steps forward, load the new first object
                    if (curImgIndex == 0)
                    {
                        UpdatePicbox(listLoadedImg[curImgIndex]);
                    }
                    // If not, adjust index int and refresh with new image
                    else
                    {
                        curImgIndex -= 1;
                        UpdatePicbox(listLoadedImg[curImgIndex]);
                    }
                    UpdateGallery();
                }
            }
            else
                MessageBox.Show("No image currently being displayed");
        }

        private void MenuCopyImage_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(picBoxMain.Image);
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
            int picWidth = picBoxMain.Image.Width;
            int picHeight = picBoxMain.Image.Height;
            int galWidth;

            if (pnlGalleryHidden)
                galWidth = 25;
            else
                galWidth = 100;

            this.Size = new Size(picWidth + galWidth + 149, picHeight + 72);
        }

        private void MenuChangeBG_Click(object sender, EventArgs e)
        {
            // Runs the colour dialog in the if(), and if the user selects OK
            if (colourDialog1.ShowDialog() == DialogResult.OK)
                BackColor = colourDialog1.Color;

            menuResetBGColour.Enabled = true;
        }

        private void MenuResetStretching_Click(object sender, EventArgs e)
        {
            // Getting Image's original size
            int picWidth = picBoxMain.Image.Width;
            int picHeight = picBoxMain.Image.Height;
            this.Size = new Size(picWidth + 149, picHeight + 72);
        }

        // Adjust the Brightness (and eventually Contrast) 
        private void MenuTransparency(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                // The byte value is necessary for the image adjustments
                byte amount = 0;
                // Creating the Form that will be the dialog box
                using (Brightness dlgBright = new Brightness())
                {
                    // Result is saved before check, so the result can be checked in more than one bool statement
                    DialogResult dlgResult = dlgBright.ShowDialog();

                    if (dlgResult == DialogResult.OK)
                    {
                        amount = dlgBright.getAmount();
                        listLoadedImg[curImgIndex].CreatePreviousVer();

                        // tmp created for readability, with transparency is applied separately. The main picBox image is also updated here.
                        Bitmap tmp = ApplyTransparency(listLoadedImg[curImgIndex].GetBitmap("c"), amount);
                        listLoadedImg[curImgIndex].UpdateBitmap("c", tmp);
                        picBoxMain.Image = listLoadedImg[curImgIndex].GetBitmap("c");
                    }
                    else if (dlgResult == DialogResult.Cancel)
                    {
                        // Nothing
                    }
                    else
                        MessageBox.Show("Error");
                }
            }
        }

        private void menuGrayscale_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                Grayscale dlgGrayscale = new Grayscale();
                DialogResult dlgResult;
                dlgResult = dlgGrayscale.ShowDialog();
                listLoadedImg[curImgIndex].CreatePreviousVer();

                // tmp created for readability, will eventually be applied to picBoxMain
                Bitmap tmp;

                // The 'Luminosity' button is set to "OK".
                if (dlgResult == DialogResult.OK)
                {
                    tmp = ApplyGrayscale(listLoadedImg[curImgIndex].GetBitmap("c"), "luminosity");
                    listLoadedImg[curImgIndex].UpdateBitmap("c", tmp);
                    picBoxMain.Image = tmp;
                }
                // The 'Average' button is set to "Yes".
                else if (dlgResult == DialogResult.Yes)
                {
                    tmp = ApplyGrayscale(listLoadedImg[curImgIndex].GetBitmap("c"), "average");
                    listLoadedImg[curImgIndex].UpdateBitmap("c", tmp);
                    picBoxMain.Image = tmp;
                }
                else
                {
                    MessageBox.Show("An error has occured during the grayscale operation.");
                    Environment.Exit(22);
                }
            }
        }

        private void menuSepia_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                listLoadedImg[curImgIndex].CreatePreviousVer();
                Bitmap tmp = ApplySepia(listLoadedImg[curImgIndex].GetBitmap("c"));
                listLoadedImg[curImgIndex].UpdateBitmap("c", tmp);
            }
        }

        private void menuExportChannels_Click(object sender, EventArgs e)
        {
            using (Channels dlgChannels = new Channels())
            {
                if (dlgChannels.ShowDialog() == DialogResult.OK)
                {
                    // Don't bypass dlg, since there's only 1 or 4 images being saved
                    ExportChannelMediator(dlgChannels.colourChannel, false);
                }
            }
        }

        private void ExportChannelMediator(string colourChannel, bool isBypassing)
        {
            if (colourChannel == "R" || colourChannel == "G" || colourChannel == "B" || colourChannel == "A")
                ExportChannel(colourChannel, listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);

            else if (colourChannel == "All")
            {
                // Runs the method once for each channel
                ExportChannel("R", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("G", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("B", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("A", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
            }
            else if (colourChannel == "AllBW")
            {
                ExportChannel("R", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("G", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("B", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("ABW", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
            }
            else
                MessageBox.Show("An error occurred when registering choice of colour channel.");
        }

        private void menuResetAdjustments_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                // Reverting the class' current image to match the original
                listLoadedImg[curImgIndex].CreatePreviousVer();
                listLoadedImg[curImgIndex].UpdateBitmap("c", listLoadedImg[curImgIndex].GetBitmap("o"));
                UpdatePicbox(listLoadedImg[curImgIndex]);
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
                Environment.NewLine + "A: " + colourToTest.A + " R: " + colourToTest.R + " G: " + colourToTest.G + " B: " + colourToTest.B +
                Environment.NewLine + "Brightness: " + colourToTest.GetBrightness() +
                Environment.NewLine + "Hash Code: " + colourToTest.GetHashCode() +
                Environment.NewLine + "Hue: " + colourToTest.GetHue() +
                Environment.NewLine + "Saturation: " + colourToTest.GetSaturation() +
                Environment.NewLine + "Int value: " + argbInt.ToString()
                );

            // Because 'Color' is a struct, you cannot assign it 'null', instead the 'Empty' value is assigned.
            colourToTest = Color.Empty;
        }

        private void menuBatchChannels_Click(object sender, EventArgs e)
        {
            using (Channels dlgChannels = new Channels())
            {
                if (dlgChannels.ShowDialog() == DialogResult.OK)
                {
                    // To return to after iterating
                    int tmp = curImgIndex;
                    curImgIndex = 0;

                    // using for loop instead of foreach because the index is needed to get name, filetype etc. in other methods. 
                    // dlgChannels.Bypass is a bool that dictates whether to bypass dlg
                    for (curImgIndex = 0; curImgIndex < listLoadedImg.Count; curImgIndex++)
                    {
                        UpdatePicbox(listLoadedImg[curImgIndex]);
                        // Assign the export format choice to the class
                        listLoadedImg[curImgIndex].UpdateExportFormat(dlgChannels.fileType);
                        ExportChannelMediator(dlgChannels.colourChannel, dlgChannels.bypass);
                        // Clear the format choice, to avoid polluting future usage
                        listLoadedImg[curImgIndex].UpdateExportFormat(null);
                    }
                    curImgIndex = tmp;
                    UpdatePicbox(listLoadedImg[curImgIndex]);
                }
            }
        }

        private void menuBatchGrayscale_Click(object sender, EventArgs e)
        {
            using (Grayscale dlgGrayscale = new Grayscale())
            {
                if (dlgGrayscale.ShowDialog() != DialogResult.Cancel)
                {
                    string algorithm = null;

                    // "OK" is the result assigned to the luminosity button
                    if (dlgGrayscale.DialogResult == DialogResult.OK)
                    {
                        algorithm = "luminosity";
                    }
                    else if (dlgGrayscale.DialogResult == DialogResult.Yes)
                    {
                        algorithm = "average";
                    }
                    else
                    {
                        // This should never happen
                        dlgGrayscale.Close();
                    }

                    // Make changes initially before deciding on whether to save, etc. 
                    foreach (LoadedImage img in listLoadedImg)
                    {
                        listLoadedImg[curImgIndex].CreatePreviousVer();
                        img.UpdateBitmap("c", ApplyGrayscale(img.GetBitmap("c"), algorithm));
                    }
                    UpdatePicbox(listLoadedImg[curImgIndex]);
                    BatchFileProcess();                    
                }
            }
        }

        private void menuBatchTransparency_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                using (Brightness dlgTrans = new Brightness())
                {
                    if (dlgTrans.ShowDialog() == DialogResult.OK)
                    {
                        byte x = dlgTrans.getAmount();
                        MessageBox.Show(x.ToString());

                        foreach (LoadedImage img in listLoadedImg)
                        {
                            listLoadedImg[curImgIndex].CreatePreviousVer();
                            img.UpdateBitmap("c", ApplyTransparency(img.GetBitmap("c"), x));
                        }
                        UpdatePicbox(listLoadedImg[curImgIndex]);
                        BatchFileProcess();
                    }
                }
            }
            else
            {
                MessageBox.Show("You must first load an image.");
            }
        }

        private void menuBatchSepia_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                foreach (LoadedImage img in listLoadedImg)
                {
                    listLoadedImg[curImgIndex].CreatePreviousVer();
                    img.UpdateBitmap("c", ApplySepia(img.GetBitmap("c")));
                }
                UpdatePicbox(listLoadedImg[curImgIndex]);
                BatchFileProcess();
            }
            else
            {
                MessageBox.Show("You must first load an image.");
            }
        }

        private void menuBatchResetAdjustments_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                foreach (LoadedImage img in listLoadedImg)
                {
                    listLoadedImg[curImgIndex].CreatePreviousVer();
                    img.UpdateBitmap("c", img.GetBitmap("o"));
                }
                UpdatePicbox(listLoadedImg[curImgIndex]);
                BatchFileProcess();
            }
            else
            {
                MessageBox.Show("You must first load an image");
            }
        }

        // Known as an "Event Handler" becuase they are called when an event occurs in the program
        private void chkStretch_CheckedChanged(object sender, EventArgs e)
        {
            // Finds out if the box is/isn't checked after a click, runs the method that updates the sizemode based on chkStretched
            if (chkStretch.Checked)
            {
                // Make the font Bold, mimicing the current font's style, but making it bold
                chkStretch.Font = new Font(chkStretch.Font, FontStyle.Bold);
                UpdatePicboxInfoAndSizeMode();
            }
            else if (chkStretch.Checked == false)
            {
                // Make the font Bold, mimicing the current font's style, but making it not bold
                chkStretch.Font = new Font(chkStretch.Font, FontStyle.Regular);
                UpdatePicboxInfoAndSizeMode();
            }
            UpdateImgOptions();
        }

        private void forEachTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listLoadedImg.Count > 0)
            {
                foreach (LoadedImage chunk in listLoadedImg)
                {
                    MessageBox.Show(chunk.GetName());
                }
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageFormat test = ImageFormat.Jpeg;
            MessageBox.Show(test.ToString());
        }

        private void MenuHideGallery_Click(object sender, EventArgs e)
        {
            pnlGalleryHidden = !pnlGalleryHidden;
            UpdateGallery();
        }        
    }
}
