namespace GridViewer
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pointsLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gridControl = new GridViewer.OccupancyGridControl();
            this.tickLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(12, 383);
            this.trackBar1.Maximum = -1;
            this.trackBar1.Minimum = -1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(673, 32);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = -1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // pointsLabel
            // 
            this.pointsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pointsLabel.Location = new System.Drawing.Point(691, 38);
            this.pointsLabel.Name = "pointsLabel";
            this.pointsLabel.Size = new System.Drawing.Size(123, 13);
            this.pointsLabel.TabIndex = 3;
            this.pointsLabel.Text = "Points: {0}";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(691, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Update Perfect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridControl
            // 
            this.gridControl.AllowDrop = true;
            this.gridControl.AreaHeight = 231F;
            this.gridControl.AreaWidth = 308F;
            this.gridControl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gridControl.Image = null;
            this.gridControl.Location = new System.Drawing.Point(12, 12);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(673, 365);
            this.gridControl.TabIndex = 0;
            this.gridControl.Text = "occupancyGridControl1";
            this.gridControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControl_DragDrop);
            this.gridControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.gridControl_DragEnter);
            // 
            // tickLabel
            // 
            this.tickLabel.AutoSize = true;
            this.tickLabel.Location = new System.Drawing.Point(691, 392);
            this.tickLabel.Name = "tickLabel";
            this.tickLabel.Size = new System.Drawing.Size(48, 13);
            this.tickLabel.TabIndex = 5;
            this.tickLabel.Text = "Tick: {0}";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 427);
            this.Controls.Add(this.tickLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pointsLabel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.gridControl);
            this.Name = "Form1";
            this.Text = "Evaluator form";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OccupancyGridControl gridControl;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label pointsLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label tickLabel;
    }
}

