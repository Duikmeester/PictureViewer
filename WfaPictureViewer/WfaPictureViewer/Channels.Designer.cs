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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAll = new System.Windows.Forms.Button();
            this.chkAlphaBW = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnR
            // 
            this.btnR.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnR.Location = new System.Drawing.Point(14, 24);
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
            this.btnG.Location = new System.Drawing.Point(95, 24);
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
            this.btnB.Location = new System.Drawing.Point(176, 24);
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
            this.btnA.Location = new System.Drawing.Point(14, 53);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(75, 23);
            this.btnA.TabIndex = 3;
            this.btnA.Text = "Alpha";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select the channel/s to export:";
            // 
            // btnAll
            // 
            this.btnAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAll.Location = new System.Drawing.Point(176, 78);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 5;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // chkAlphaBW
            // 
            this.chkAlphaBW.AutoSize = true;
            this.chkAlphaBW.Location = new System.Drawing.Point(14, 82);
            this.chkAlphaBW.Name = "chkAlphaBW";
            this.chkAlphaBW.Size = new System.Drawing.Size(130, 17);
            this.chkAlphaBW.TabIndex = 6;
            this.chkAlphaBW.Text = "Alpha as Black/White";
            this.chkAlphaBW.UseVisualStyleBackColor = true;
            this.chkAlphaBW.CheckedChanged += new System.EventHandler(this.chkAlphaBW_CheckedChanged);
            // 
            // Channels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 110);
            this.Controls.Add(this.chkAlphaBW);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.CheckBox chkAlphaBW;
    }
}