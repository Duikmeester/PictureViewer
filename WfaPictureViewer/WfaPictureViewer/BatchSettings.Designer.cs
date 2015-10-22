namespace WfaPictureViewer
{
    partial class BatchSettings
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
            this.components = new System.ComponentModel.Container();
            this.chkBypass = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.chkFileName = new System.Windows.Forms.CheckBox();
            this.comboFileType = new System.Windows.Forms.ComboBox();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.chkExport = new System.Windows.Forms.CheckBox();
            this.btnGetPath = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkBypass
            // 
            this.chkBypass.AutoSize = true;
            this.chkBypass.Checked = true;
            this.chkBypass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBypass.Location = new System.Drawing.Point(12, 33);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(127, 17);
            this.chkBypass.TabIndex = 0;
            this.chkBypass.Text = "Bypass Save Dialog?";
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(176, 154);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Continue";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Enabled = false;
            this.txtFileName.Location = new System.Drawing.Point(15, 125);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(210, 20);
            this.txtFileName.TabIndex = 2;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // chkFileName
            // 
            this.chkFileName.AutoSize = true;
            this.chkFileName.Checked = true;
            this.chkFileName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFileName.Enabled = false;
            this.chkFileName.Location = new System.Drawing.Point(12, 83);
            this.chkFileName.Name = "chkFileName";
            this.chkFileName.Size = new System.Drawing.Size(159, 17);
            this.chkFileName.TabIndex = 3;
            this.chkFileName.Text = "Provide new filename/path?";
            this.chkFileName.UseVisualStyleBackColor = true;
            this.chkFileName.CheckedChanged += new System.EventHandler(this.chkFileName_CheckedChanged);
            // 
            // comboFileType
            // 
            this.comboFileType.FormattingEnabled = true;
            this.comboFileType.Items.AddRange(new object[] {
            ".Jpg",
            ".Bmp",
            ".Png",
            ".Tiff"});
            this.comboFileType.Location = new System.Drawing.Point(75, 56);
            this.comboFileType.Name = "comboFileType";
            this.comboFileType.Size = new System.Drawing.Size(64, 21);
            this.comboFileType.TabIndex = 4;
            this.comboFileType.SelectedIndexChanged += new System.EventHandler(this.comboFileType_SelectedIndexChanged);
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Location = new System.Drawing.Point(12, 59);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(50, 13);
            this.lblFileType.TabIndex = 5;
            this.lblFileType.Text = "FileType:";
            this.lblFileType.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Enabled = false;
            this.lblFileName.Location = new System.Drawing.Point(12, 109);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(84, 13);
            this.lblFileName.TabIndex = 6;
            this.lblFileName.Text = "File Name/Path:";
            // 
            // chkExport
            // 
            this.chkExport.AutoSize = true;
            this.chkExport.Checked = true;
            this.chkExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExport.Location = new System.Drawing.Point(12, 10);
            this.chkExport.Name = "chkExport";
            this.chkExport.Size = new System.Drawing.Size(98, 17);
            this.chkExport.TabIndex = 7;
            this.chkExport.Text = "Export to Files?";
            this.chkExport.UseVisualStyleBackColor = true;
            this.chkExport.CheckedChanged += new System.EventHandler(this.chkExport_CheckedChanged);
            // 
            // btnGetPath
            // 
            this.btnGetPath.Enabled = false;
            this.btnGetPath.Location = new System.Drawing.Point(223, 124);
            this.btnGetPath.Name = "btnGetPath";
            this.btnGetPath.Size = new System.Drawing.Size(28, 22);
            this.btnGetPath.TabIndex = 8;
            this.btnGetPath.Text = "...";
            this.btnGetPath.UseVisualStyleBackColor = true;
            this.btnGetPath.Click += new System.EventHandler(this.btnGetPath_Click);
            // 
            // BatchSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 184);
            this.Controls.Add(this.btnGetPath);
            this.Controls.Add(this.chkExport);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblFileType);
            this.Controls.Add(this.comboFileType);
            this.Controls.Add(this.chkFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkBypass);
            this.Name = "BatchSettings";
            this.Text = "BatchSettings";
            this.Load += new System.EventHandler(this.BatchSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBypass;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.CheckBox chkFileName;
        private System.Windows.Forms.ComboBox comboFileType;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.CheckBox chkExport;
        private System.Windows.Forms.Button btnGetPath;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}