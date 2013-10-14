namespace TrackColorForm
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
            this.SBmin = new System.Windows.Forms.HScrollBar();
            this.SBmax = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Nmin = new System.Windows.Forms.NumericUpDown();
            this.Nmax = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.src)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.output)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmax)).BeginInit();
            this.SuspendLayout();
            // 
            // src
            // 
            this.src.Location = new System.Drawing.Point(12, 3);
            this.src.Name = "src";
            this.src.Size = new System.Drawing.Size(332, 361);
            this.src.TabIndex = 0;
            this.src.TabStop = false;
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(363, 12);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(327, 352);
            this.output.TabIndex = 1;
            this.output.TabStop = false;
            // 
            // SBmin
            // 
            this.SBmin.Location = new System.Drawing.Point(144, 376);
            this.SBmin.Maximum = 360;
            this.SBmin.Name = "SBmin";
            this.SBmin.Size = new System.Drawing.Size(168, 20);
            this.SBmin.TabIndex = 2;
            this.SBmin.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SBmin_Scroll);
            // 
            // SBmax
            // 
            this.SBmax.Location = new System.Drawing.Point(144, 405);
            this.SBmax.Maximum = 360;
            this.SBmax.Name = "SBmax";
            this.SBmax.Size = new System.Drawing.Size(168, 17);
            this.SBmax.TabIndex = 3;
            this.SBmax.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SBmax_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Min";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Max";
            // 
            // Nmin
            // 
            this.Nmin.Location = new System.Drawing.Point(315, 376);
            this.Nmin.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Nmin.Name = "Nmin";
            this.Nmin.Size = new System.Drawing.Size(53, 20);
            this.Nmin.TabIndex = 6;
            this.Nmin.ValueChanged += new System.EventHandler(this.Nmin_ValueChanged);
            // 
            // Nmax
            // 
            this.Nmax.Location = new System.Drawing.Point(315, 402);
            this.Nmax.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Nmax.Name = "Nmax";
            this.Nmax.Size = new System.Drawing.Size(53, 20);
            this.Nmax.TabIndex = 7;
            this.Nmax.ValueChanged += new System.EventHandler(this.Nmax_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 441);
            this.Controls.Add(this.Nmax);
            this.Controls.Add(this.Nmin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SBmax);
            this.Controls.Add(this.SBmin);
            this.Controls.Add(this.output);
            this.Controls.Add(this.src);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.src)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.output)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox src;
        private System.Windows.Forms.PictureBox output;
        private System.Windows.Forms.HScrollBar SBmin;
        private System.Windows.Forms.HScrollBar SBmax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Nmin;
        private System.Windows.Forms.NumericUpDown Nmax;
    }
}