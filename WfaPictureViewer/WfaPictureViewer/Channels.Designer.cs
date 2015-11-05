namespace WfaPictureViewer
{
    partial class Channels
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
            this.btnR = new System.Windows.Forms.Button();
            this.btnG = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.btnAll = new System.Windows.Forms.Button();
            this.chkAlphaBW = new System.Windows.Forms.CheckBox();
            this.comboFileType = new System.Windows.Forms.ComboBox();
            this.chkBypass = new System.Windows.Forms.CheckBox();
            this.lblFileType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnR
            // 
            this.btnR.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnR.Location = new System.Drawing.Point(90, 77);
            this.btnR.Name = "btnR";
            this.btnR.Size = new System.Drawing.Size(75, 23);
            this.btnR.TabIndex = 0;
            this.btnR.Text = "R";
            this.btnR.UseVisualStyleBackColor = true;
            this.btnR.Click += new System.EventHandler(this.btnR_Click);
            // 
            // btnG
            // 
            this.btnG.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnG.Location = new System.Drawing.Point(171, 77);
            this.btnG.Name = "btnG";
            this.btnG.Size = new System.Drawing.Size(75, 23);
            this.btnG.TabIndex = 1;
            this.btnG.Text = "G";
            this.btnG.UseVisualStyleBackColor = true;
            this.btnG.Click += new System.EventHandler(this.btnG_Click);
            // 
            // btnB
            // 
            this.btnB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnB.Location = new System.Drawing.Point(252, 77);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(75, 23);
            this.btnB.TabIndex = 2;
            this.btnB.Text = "B";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // btnA
            // 
            this.btnA.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnA.Location = new System.Drawing.Point(9, 77);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(75, 23);
            this.btnA.TabIndex = 3;
            this.btnA.Text = "Alpha";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(6, 61);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(153, 13);
            this.lblInstructions.TabIndex = 4;
            this.lblInstructions.Text = "Select the channel/s to export:";
            // 
            // btnAll
            // 
            this.btnAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAll.Location = new System.Drawing.Point(171, 106);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(156, 46);
            this.btnAll.TabIndex = 5;
            this.btnAll.Text = "All (R,G,B,A)";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // chkAlphaBW
            // 
            this.chkAlphaBW.AutoSize = true;
            this.chkAlphaBW.Location = new System.Drawing.Point(9, 106);
            this.chkAlphaBW.Name = "chkAlphaBW";
            this.chkAlphaBW.Size = new System.Drawing.Size(130, 17);
            this.chkAlphaBW.TabIndex = 6;
            this.chkAlphaBW.Text = "Alpha as Black/White";
            this.chkAlphaBW.UseVisualStyleBackColor = true;
            this.chkAlphaBW.CheckedChanged += new System.EventHandler(this.chkAlphaBW_CheckedChanged);
            // 
            // comboFileType
            // 
            this.comboFileType.FormattingEnabled = true;
            this.comboFileType.Items.AddRange(new object[] {
            ".jpg",
            ".bmp",
            ".png",
            ".tiff"});
            this.comboFileType.Location = new System.Drawing.Point(90, 35);
            this.comboFileType.Name = "comboFileType";
            this.comboFileType.Size = new System.Drawing.Size(75, 21);
            this.comboFileType.TabIndex = 10;
            this.comboFileType.SelectedIndexChanged += new System.EventHandler(this.comboFileType_SelectedIndexChanged);
            // 
            // chkBypass
            // 
            this.chkBypass.AutoSize = true;
            this.chkBypass.Checked = true;
            this.chkBypass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBypass.Location = new System.Drawing.Point(9, 12);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(233, 17);
            this.chkBypass.TabIndex = 7;
            this.chkBypass.Text = "Bypass Save Dialog? (Use current filename)";
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Location = new System.Drawing.Point(7, 38);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(50, 13);
            this.lblFileType.TabIndex = 11;
            this.lblFileType.Text = "FileType:";
            // 
            // Channels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 160);
            this.Controls.Add(this.lblFileType);
            this.Controls.Add(this.comboFileType);
            this.Controls.Add(this.chkBypass);
            this.Controls.Add(this.chkAlphaBW);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnA);
            this.Controls.Add(this.btnB);
            this.Controls.Add(this.btnG);
            this.Controls.Add(this.btnR);
            this.Name = "Channels";
            this.Text = "Channels";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnR;
        private System.Windows.Forms.Button btnG;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.CheckBox chkAlphaBW;
        private System.Windows.Forms.ComboBox comboFileType;
        private System.Windows.Forms.CheckBox chkBypass;
        private System.Windows.Forms.Label lblFileType;
    }
}