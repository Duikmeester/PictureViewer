namespace WfaPictureViewer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPicInfo = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAutoscaleLoad = new System.Windows.Forms.CheckBox();
            this.chkStretch = new System.Windows.Forms.CheckBox();
            this.chkAspectLock = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNavigateRight = new System.Windows.Forms.Button();
            this.lblPicNotifier = new System.Windows.Forms.Label();
            this.btnNavigateLeft = new System.Windows.Forms.Button();
            this.openPictureDialog = new System.Windows.Forms.OpenFileDialog();
            this.colourDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddAnother = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBatch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBatchChannels = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBatchGrayscale = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBatchTransparency = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBatchSepia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuBatchResetAdjustments = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExportChannels = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGrayscale = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTransparency = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSepia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuResetAdjustments = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFitWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuResetStretching = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangeBG = new System.Windows.Forms.ToolStripMenuItem();
            this.menuResetBGColour = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDisplayBGInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.forEachTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPicInfo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(591, 284);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 5);
            this.pictureBox1.Size = new System.Drawing.Size(460, 278);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblPicInfo
            // 
            this.lblPicInfo.AutoSize = true;
            this.lblPicInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPicInfo.Location = new System.Drawing.Point(469, 0);
            this.lblPicInfo.Name = "lblPicInfo";
            this.tableLayoutPanel1.SetRowSpan(this.lblPicInfo, 2);
            this.lblPicInfo.Size = new System.Drawing.Size(119, 149);
            this.lblPicInfo.TabIndex = 4;
            this.lblPicInfo.Text = " ";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkAutoscaleLoad);
            this.flowLayoutPanel2.Controls.Add(this.chkStretch);
            this.flowLayoutPanel2.Controls.Add(this.chkAspectLock);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(469, 187);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(119, 94);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // chkAutoscaleLoad
            // 
            this.chkAutoscaleLoad.AutoSize = true;
            this.chkAutoscaleLoad.Checked = true;
            this.chkAutoscaleLoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoscaleLoad.Location = new System.Drawing.Point(3, 74);
            this.chkAutoscaleLoad.Name = "chkAutoscaleLoad";
            this.chkAutoscaleLoad.Size = new System.Drawing.Size(117, 17);
            this.chkAutoscaleLoad.TabIndex = 0;
            this.chkAutoscaleLoad.Text = "Autoscale on load?";
            this.chkAutoscaleLoad.UseVisualStyleBackColor = true;
            // 
            // chkStretch
            // 
            this.chkStretch.AutoSize = true;
            this.chkStretch.Location = new System.Drawing.Point(3, 51);
            this.chkStretch.Name = "chkStretch";
            this.chkStretch.Size = new System.Drawing.Size(60, 17);
            this.chkStretch.TabIndex = 1;
            this.chkStretch.Text = "Stretch";
            this.chkStretch.UseVisualStyleBackColor = true;
            this.chkStretch.CheckedChanged += new System.EventHandler(this.chkStretch_CheckedChanged);
            // 
            // chkAspectLock
            // 
            this.chkAspectLock.AutoSize = true;
            this.chkAspectLock.Location = new System.Drawing.Point(3, 28);
            this.chkAspectLock.Name = "chkAspectLock";
            this.chkAspectLock.Size = new System.Drawing.Size(114, 17);
            this.chkAspectLock.TabIndex = 2;
            this.chkAspectLock.Text = "Lock Aspect Ratio";
            this.chkAspectLock.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNavigateRight);
            this.panel1.Controls.Add(this.lblPicNotifier);
            this.panel1.Controls.Add(this.btnNavigateLeft);
            this.panel1.Location = new System.Drawing.Point(469, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 29);
            this.panel1.TabIndex = 7;
            // 
            // btnNavigateRight
            // 
            this.btnNavigateRight.Location = new System.Drawing.Point(86, 3);
            this.btnNavigateRight.Name = "btnNavigateRight";
            this.btnNavigateRight.Size = new System.Drawing.Size(30, 23);
            this.btnNavigateRight.TabIndex = 1;
            this.btnNavigateRight.Text = ">";
            this.btnNavigateRight.UseVisualStyleBackColor = true;
            this.btnNavigateRight.Click += new System.EventHandler(this.btnNavigateRight_Click);
            // 
            // lblPicNotifier
            // 
            this.lblPicNotifier.AutoSize = true;
            this.lblPicNotifier.Location = new System.Drawing.Point(41, 8);
            this.lblPicNotifier.Name = "lblPicNotifier";
            this.lblPicNotifier.Size = new System.Drawing.Size(35, 13);
            this.lblPicNotifier.TabIndex = 5;
            this.lblPicNotifier.Text = "label1";
            // 
            // btnNavigateLeft
            // 
            this.btnNavigateLeft.Location = new System.Drawing.Point(4, 3);
            this.btnNavigateLeft.Name = "btnNavigateLeft";
            this.btnNavigateLeft.Size = new System.Drawing.Size(31, 23);
            this.btnNavigateLeft.TabIndex = 0;
            this.btnNavigateLeft.Text = "<";
            this.btnNavigateLeft.UseVisualStyleBackColor = true;
            this.btnNavigateLeft.Click += new System.EventHandler(this.btnNavigateLeft_Click);
            // 
            // openPictureDialog
            // 
            this.openPictureDialog.FileName = "openFileDialog1";
            this.openPictureDialog.Filter = "All files (*.*)|*.*|JPEG Files (*.jpg)|*.jpg|PNG Filez (*.png)|*.png|BMP Files (*" +
    ".bmp)|(*.bmp)";
            this.openPictureDialog.Title = "Select a picture file";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuImage,
            this.menuWindow,
            this.forEachTestToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(591, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLoadImage,
            this.menuAddAnother,
            this.menuClearImage,
            this.menuCopyImage,
            this.menuSaveImage,
            this.toolStripSeparator1,
            this.menuClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuLoadImage
            // 
            this.menuLoadImage.Name = "menuLoadImage";
            this.menuLoadImage.Size = new System.Drawing.Size(178, 22);
            this.menuLoadImage.Text = "Load Image";
            this.menuLoadImage.Click += new System.EventHandler(this.MenuLoadImage_Click);
            // 
            // menuAddAnother
            // 
            this.menuAddAnother.Name = "menuAddAnother";
            this.menuAddAnother.Size = new System.Drawing.Size(178, 22);
            this.menuAddAnother.Text = "Add Another Image";
            this.menuAddAnother.Click += new System.EventHandler(this.menuAddAnother_Click);
            // 
            // menuClearImage
            // 
            this.menuClearImage.Name = "menuClearImage";
            this.menuClearImage.Size = new System.Drawing.Size(178, 22);
            this.menuClearImage.Text = "Clear Image";
            this.menuClearImage.Click += new System.EventHandler(this.MenuClearImage_Click);
            // 
            // menuCopyImage
            // 
            this.menuCopyImage.Name = "menuCopyImage";
            this.menuCopyImage.Size = new System.Drawing.Size(178, 22);
            this.menuCopyImage.Text = "Copy";
            this.menuCopyImage.Click += new System.EventHandler(this.MenuCopyImage_Click);
            // 
            // menuSaveImage
            // 
            this.menuSaveImage.Name = "menuSaveImage";
            this.menuSaveImage.Size = new System.Drawing.Size(178, 22);
            this.menuSaveImage.Text = "Save ";
            this.menuSaveImage.Click += new System.EventHandler(this.MenuSaveImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(178, 22);
            this.menuClose.Text = "Close";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // menuImage
            // 
            this.menuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBatch,
            this.toolStripSeparator4,
            this.menuExportChannels,
            this.menuGrayscale,
            this.menuTransparency,
            this.menuSepia,
            this.toolStripSeparator3,
            this.menuResetAdjustments,
            this.menuFitWindow,
            this.menuResetStretching});
            this.menuImage.Name = "menuImage";
            this.menuImage.Size = new System.Drawing.Size(52, 20);
            this.menuImage.Text = "Image";
            // 
            // menuBatch
            // 
            this.menuBatch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBatchChannels,
            this.menuBatchGrayscale,
            this.menuBatchTransparency,
            this.menuBatchSepia,
            this.toolStripSeparator5,
            this.menuBatchResetAdjustments});
            this.menuBatch.Name = "menuBatch";
            this.menuBatch.Size = new System.Drawing.Size(208, 22);
            this.menuBatch.Text = "Batch";
            // 
            // menuBatchChannels
            // 
            this.menuBatchChannels.Name = "menuBatchChannels";
            this.menuBatchChannels.Size = new System.Drawing.Size(172, 22);
            this.menuBatchChannels.Text = "Export Channels";
            this.menuBatchChannels.Click += new System.EventHandler(this.menuBatchChannels_Click);
            // 
            // menuBatchGrayscale
            // 
            this.menuBatchGrayscale.Name = "menuBatchGrayscale";
            this.menuBatchGrayscale.Size = new System.Drawing.Size(172, 22);
            this.menuBatchGrayscale.Text = "Apply Grayscale";
            this.menuBatchGrayscale.Click += new System.EventHandler(this.menuBatchGrayscale_Click);
            // 
            // menuBatchTransparency
            // 
            this.menuBatchTransparency.Name = "menuBatchTransparency";
            this.menuBatchTransparency.Size = new System.Drawing.Size(172, 22);
            this.menuBatchTransparency.Text = "Transparency";
            this.menuBatchTransparency.Click += new System.EventHandler(this.menuBatchTransparency_Click);
            // 
            // menuBatchSepia
            // 
            this.menuBatchSepia.Name = "menuBatchSepia";
            this.menuBatchSepia.Size = new System.Drawing.Size(172, 22);
            this.menuBatchSepia.Text = "Apply Sepia";
            this.menuBatchSepia.Click += new System.EventHandler(this.menuBatchSepia_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
            // 
            // menuBatchResetAdjustments
            // 
            this.menuBatchResetAdjustments.Name = "menuBatchResetAdjustments";
            this.menuBatchResetAdjustments.Size = new System.Drawing.Size(172, 22);
            this.menuBatchResetAdjustments.Text = "Reset Adjustments";
            this.menuBatchResetAdjustments.Click += new System.EventHandler(this.menuBatchResetAdjustments_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(205, 6);
            // 
            // menuExportChannels
            // 
            this.menuExportChannels.Name = "menuExportChannels";
            this.menuExportChannels.Size = new System.Drawing.Size(208, 22);
            this.menuExportChannels.Text = "Export Channels";
            this.menuExportChannels.Click += new System.EventHandler(this.menuExportChannels_Click);
            // 
            // menuGrayscale
            // 
            this.menuGrayscale.Name = "menuGrayscale";
            this.menuGrayscale.Size = new System.Drawing.Size(208, 22);
            this.menuGrayscale.Text = "Apply Grayscale";
            this.menuGrayscale.Click += new System.EventHandler(this.menuGrayscale_Click);
            // 
            // menuTransparency
            // 
            this.menuTransparency.Name = "menuTransparency";
            this.menuTransparency.Size = new System.Drawing.Size(208, 22);
            this.menuTransparency.Text = "Transparency";
            this.menuTransparency.Click += new System.EventHandler(this.MenuTransparency);
            // 
            // menuSepia
            // 
            this.menuSepia.Name = "menuSepia";
            this.menuSepia.Size = new System.Drawing.Size(208, 22);
            this.menuSepia.Text = "Apply Sepia";
            this.menuSepia.Click += new System.EventHandler(this.menuSepia_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
            // 
            // menuResetAdjustments
            // 
            this.menuResetAdjustments.Name = "menuResetAdjustments";
            this.menuResetAdjustments.Size = new System.Drawing.Size(208, 22);
            this.menuResetAdjustments.Text = "Reset Image Adjustments";
            this.menuResetAdjustments.Click += new System.EventHandler(this.menuResetAdjustments_Click);
            // 
            // menuFitWindow
            // 
            this.menuFitWindow.Name = "menuFitWindow";
            this.menuFitWindow.Size = new System.Drawing.Size(208, 22);
            this.menuFitWindow.Text = "Fit Window to Image";
            this.menuFitWindow.Click += new System.EventHandler(this.menuFitWindow_Click);
            // 
            // menuResetStretching
            // 
            this.menuResetStretching.Name = "menuResetStretching";
            this.menuResetStretching.Size = new System.Drawing.Size(208, 22);
            this.menuResetStretching.Text = "Reset Stretching";
            this.menuResetStretching.Click += new System.EventHandler(this.MenuResetStretching_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuChangeBG,
            this.menuResetBGColour,
            this.menuDisplayBGInfo});
            this.menuWindow.Name = "menuWindow";
            this.menuWindow.Size = new System.Drawing.Size(63, 20);
            this.menuWindow.Text = "Window";
            // 
            // menuChangeBG
            // 
            this.menuChangeBG.Name = "menuChangeBG";
            this.menuChangeBG.Size = new System.Drawing.Size(193, 22);
            this.menuChangeBG.Text = "Change BG Colour";
            this.menuChangeBG.Click += new System.EventHandler(this.MenuChangeBG_Click);
            // 
            // menuResetBGColour
            // 
            this.menuResetBGColour.Name = "menuResetBGColour";
            this.menuResetBGColour.Size = new System.Drawing.Size(193, 22);
            this.menuResetBGColour.Text = "Reset BG Colour";
            this.menuResetBGColour.Click += new System.EventHandler(this.MenuResetBGColour_Click);
            // 
            // menuDisplayBGInfo
            // 
            this.menuDisplayBGInfo.Name = "menuDisplayBGInfo";
            this.menuDisplayBGInfo.Size = new System.Drawing.Size(193, 22);
            this.menuDisplayBGInfo.Text = "Display BG Colour Info";
            this.menuDisplayBGInfo.Click += new System.EventHandler(this.MenuDisplayBGInfo_Click);
            // 
            // forEachTestToolStripMenuItem
            // 
            this.forEachTestToolStripMenuItem.Name = "forEachTestToolStripMenuItem";
            this.forEachTestToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.forEachTestToolStripMenuItem.Text = "ForEach Test";
            this.forEachTestToolStripMenuItem.Click += new System.EventHandler(this.forEachTestToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 308);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Picture Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_PostResize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkStretch;
        private System.Windows.Forms.OpenFileDialog openPictureDialog;
        private System.Windows.Forms.ColorDialog colourDialog1;
        private System.Windows.Forms.Label lblPicInfo;
        private System.Windows.Forms.Label lblPicNotifier;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkAutoscaleLoad;
        private System.Windows.Forms.CheckBox chkAspectLock;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuLoadImage;
        private System.Windows.Forms.ToolStripMenuItem menuSaveImage;
        private System.Windows.Forms.ToolStripMenuItem menuClearImage;
        private System.Windows.Forms.ToolStripMenuItem menuCopyImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripMenuItem menuImage;
        private System.Windows.Forms.ToolStripMenuItem menuFitWindow;
        private System.Windows.Forms.ToolStripMenuItem menuWindow;
        private System.Windows.Forms.ToolStripMenuItem menuChangeBG;
        private System.Windows.Forms.ToolStripMenuItem menuResetStretching;
        private System.Windows.Forms.ToolStripMenuItem menuTransparency;
        private System.Windows.Forms.ToolStripMenuItem menuResetBGColour;
        private System.Windows.Forms.ToolStripMenuItem menuDisplayBGInfo;
        private System.Windows.Forms.ToolStripMenuItem menuGrayscale;
        private System.Windows.Forms.ToolStripMenuItem menuSepia;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem menuResetAdjustments;
        private System.Windows.Forms.ToolStripMenuItem menuExportChannels;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuAddAnother;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNavigateRight;
        private System.Windows.Forms.Button btnNavigateLeft;
        private System.Windows.Forms.ToolStripMenuItem forEachTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuBatch;
        private System.Windows.Forms.ToolStripMenuItem menuBatchChannels;
        private System.Windows.Forms.ToolStripMenuItem menuBatchGrayscale;
        private System.Windows.Forms.ToolStripMenuItem menuBatchTransparency;
        private System.Windows.Forms.ToolStripMenuItem menuBatchSepia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuBatchResetAdjustments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}

