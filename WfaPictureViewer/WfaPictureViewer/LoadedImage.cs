using System;

namespace WfaPictureViewer
{
    class LoadedImage
    {
        public Bitmap originalVer { get; set; }
        public Bitmap currentVer { get; set; }
        public PictureBox thumbnail { get; set; }
        public Label lblThumb { get; set; }
        public string name { get; set; }
        public string defaultDir { get; set; }
        public ImageFormat originalFormat { get; set; }
        public ImageFormat tmpExportFormat { get; set; }
        public float correctRatio { get; set; }
    }
}
