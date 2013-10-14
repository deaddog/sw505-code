namespace ObjectFromColor
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
            this.src = new System.Windows.Forms.PictureBox();
            this.output = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.src)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.output)).BeginInit();
            this.SuspendLayout();
            // 
            // src
            // 
            this.src.Location = new System.Drawing.Point(12, 12);
            this.src.Name = "src";
            this.src.Size = new System.Drawing.Size(262, 272);
            this.src.TabIndex = 0;
            this.src.TabStop = false;
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(290, 12);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(262, 272);
            this.output.TabIndex = 1;
            this.output.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 293);
            this.Controls.Add(this.output);
            this.Controls.Add(this.src);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.src)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.output)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox src;
        private System.Windows.Forms.PictureBox output;
    }
}