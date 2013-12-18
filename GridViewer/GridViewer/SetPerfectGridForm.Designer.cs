namespace GridViewer
{
    partial class SetPerfectGridForm
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
            this.gridControl = new GridViewer.OccupancyGridControl();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.AreaHeight = 231F;
            this.gridControl.AreaWidth = 308F;
            this.gridControl.Image = null;
            this.gridControl.Location = new System.Drawing.Point(12, 12);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(632, 375);
            this.gridControl.TabIndex = 0;
            this.gridControl.Text = "occupancyGridControl1";
            this.gridControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridControl_MouseDown);
            this.gridControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridControl_MouseMove);
            // 
            // SetPerfectGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 399);
            this.Controls.Add(this.gridControl);
            this.Name = "SetPerfectGridForm";
            this.Text = "SetPerfectGridForm";
            this.ResumeLayout(false);

        }

        #endregion

        private OccupancyGridControl gridControl;
    }
}