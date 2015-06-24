namespace WfaPictureViewer
{
    partial class Grayscale
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
            this.btnLuminosity = new System.Windows.Forms.Button();
            this.btnAverage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLuminosity
            // 
            this.btnLuminosity.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLuminosity.Location = new System.Drawing.Point(59, 39);
            this.btnLuminosity.Name = "btnLuminosity";
            this.btnLuminosity.Size = new System.Drawing.Size(75, 23);
            this.btnLuminosity.TabIndex = 2;
            this.btnLuminosity.Text = "Luminosity";
            this.btnLuminosity.UseVisualStyleBackColor = true;
            this.btnLuminosity.Click += new System.EventHandler(this.btnLuminosity_Click);
            // 
            // btnAverage
            // 
            this.btnAverage.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAverage.Location = new System.Drawing.Point(140, 39);
            this.btnAverage.Name = "btnAverage";
            this.btnAverage.Size = new System.Drawing.Size(75, 23);
            this.btnAverage.TabIndex = 3;
            this.btnAverage.Text = "Average";
            this.btnAverage.UseVisualStyleBackColor = true;
            this.btnAverage.Click += new System.EventHandler(this.btnAverage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Please select a conversion algorithm:";
            // 
            // Grayscale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 87);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAverage);
            this.Controls.Add(this.btnLuminosity);
            this.Name = "Grayscale";
            this.Text = "Grayscale";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLuminosity;
        private System.Windows.Forms.Button btnAverage;
        private System.Windows.Forms.Label label1;
    }
}