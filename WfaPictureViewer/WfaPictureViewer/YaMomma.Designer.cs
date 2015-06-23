namespace WfaPictureViewer
{
    partial class YaMomma
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
            this.btnKanye = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWhatTake = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnKanye
            // 
            this.btnKanye.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnKanye.Location = new System.Drawing.Point(51, 99);
            this.btnKanye.Name = "btnKanye";
            this.btnKanye.Size = new System.Drawing.Size(174, 23);
            this.btnKanye.TabIndex = 0;
            this.btnKanye.Text = "Go on a Kanye Quest";
            this.btnKanye.UseVisualStyleBackColor = true;
            this.btnKanye.Click += new System.EventHandler(this.btnKanye_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "What will you take with you?";
            // 
            // txtWhatTake
            // 
            this.txtWhatTake.Location = new System.Drawing.Point(12, 62);
            this.txtWhatTake.Name = "txtWhatTake";
            this.txtWhatTake.Size = new System.Drawing.Size(260, 20);
            this.txtWhatTake.TabIndex = 2;
            // 
            // YaMomma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 152);
            this.Controls.Add(this.txtWhatTake);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnKanye);
            this.Name = "YaMomma";
            this.Text = "YaMomma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKanye;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWhatTake;
    }
}