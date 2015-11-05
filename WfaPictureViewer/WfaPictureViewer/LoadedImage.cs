using System;
using System.IO; // Path
using System.Drawing; // Bitmap
using System.Drawing.Imaging; // Imageformat
using System.Windows.Forms; //PictureBox

namespace WfaPictureViewer
{
    public class LoadedImage
    {
        private Bitmap originalVer { get; set; }
        private Bitmap currentVer { get; set; }
        private Bitmap previousVer { get; set; }
        private PictureBox thumbnail { get; set; }
        private Label lblThumb { get; set; }
        private string name { get; set; }
        private string defaultDir { get; set; }
        private ImageFormat originalFormat { get; set; }
        private ImageFormat tmpExportFormat { get; set; }
        private double correctRatio { get; set; }

        // CONSTRUCTOR
        public LoadedImage(string path, Bitmap baseImage, PicViewer sender, int index)
        {
            originalVer = currentVer = baseImage; // baseImage already converted to ARGB

            name = Path.GetFileNameWithoutExtension(path);
            originalFormat = baseImage.RawFormat;
            defaultDir = Path.GetDirectoryName(path);
            correctRatio = (float)currentVer.Width / (float)currentVer.Height;

            UpdateCurrentThumbnail();

            lblThumb = new Label();
            // Docking the label over the thumbnail
            thumbnail.Controls.Add(lblThumb);
            lblThumb.Dock = DockStyle.Left;
            lblThumb.Size = new Size(15, 15);
            lblThumb.Font = new Font(lblThumb.Font, FontStyle.Bold);
            lblThumb.ForeColor = Color.White;
            lblThumb.TextAlign = ContentAlignment.TopLeft;
            lblThumb.BackColor = Color.FromArgb(50, 0, 0, 0);

            // Names are given the index as a string, they will eventually be converted back to ints and used as indexing definitions.
            thumbnail.Name = lblThumb.Name = index.ToString();

            // Add eventhandler references to clicks
            thumbnail.Click += sender.picBox_Click;
            lblThumb.Click += sender.picBox_Click;
        }

        // Return one of the three bitmap properties.
        // c = currentVer, o = originalVer, p = previousVer
        public Bitmap GetBitmap(string ver)
        {
            switch (ver)
            {
                case "o":
                    {
                        return originalVer;
                    }
                case "c":
                    {
                        return currentVer;
                    }
                case "p":
                    {
                        return previousVer;
                    }
                default:
                    {
                        MessageBox.Show("An invalid Bitmap type was defined, please input o, c or p.");
                        Environment.Exit(20);
                        return null;
                    }
            }
        }

        public void UpdateBitmap(string ver, Bitmap img)
        {
            // Can be used for validating passed image
            if (true)
            {
                switch (ver)
                {
                    case "o":
                        {
                            originalVer = img;
                        }
                        break;
                    case "c":
                        {
                            currentVer = img;
                        }
                        break;
                    case "p":
                        {
                            previousVer = img;
                        }
                        break;
                    default:
                        {
                            MessageBox.Show("An invalid Bitmap type was defined, please input o, c or p.");
                            Environment.Exit(21);
                        }
                        break;
                }
            }
        }

        public PictureBox GetThumbnail()
        {
            if (thumbnail != null)
                return thumbnail;
            else
                MessageBox.Show("Thumbail {0} not found", name);
                return null;
        }

        public void UpdateCurrentThumbnail() // Uses current Image to create a new thumbnail
        {
            thumbnail = new PictureBox();
            // New image height given that thumbnail width is 100: original height / original width x 100 = new height
            double newHeight = (double)originalVer.Height / (double)originalVer.Width * 100;
            // Rounding to the nearest int
            int newHeightRounded = (int)Math.Round(newHeight);
            // Assigning new height calculations ot thumbnail
            thumbnail.Image = new Bitmap(currentVer, new Size(100, newHeightRounded));
            thumbnail.Size = new Size(thumbnail.Image.Size.Width, thumbnail.Image.Size.Height);
            thumbnail.BorderStyle = BorderStyle.Fixed3D;
            thumbnail.Dock = DockStyle.Top;
            thumbnail.SizeMode = PictureBoxSizeMode.AutoSize;            
        }

        // This should probably grow into a more far reaching state system, with multiple levels of undos, an array of states with undo/redo?
        public void CreatePreviousVer()
        {
            previousVer = currentVer;
        }

        public string GetName()
        {
            if (name != null)
            return name;
            else
            {
                return null;
            }                
        }

        public double GetCorrectRatio()
        {
            return correctRatio;
        }

        public ImageFormat GetOriginalFormat()
        {
            return originalFormat;
        }

        public ImageFormat GetExportFormat()
        {
            return tmpExportFormat;
        }

        public void UpdateExportFormat(ImageFormat frmt)
        {
            tmpExportFormat = frmt;
        }

        public string GetDefaultDir()
        {
            return defaultDir;
        }

        public string GetLblThumbText()
        {
            return lblThumb.Text;
        }

        public void UpdateLblThumbText(string txt)
        {
            lblThumb.Text = txt;
        }

        public void UpdateLblThumbName(string txt)
        {
            lblThumb.Name = txt;
        }
    }
}
