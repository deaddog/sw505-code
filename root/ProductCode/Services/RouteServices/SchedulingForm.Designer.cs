namespace Services.RouteServices
{
    partial class SchedulingForm
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
            this.occupancyGridControl1 = new Services.RouteServices.OccupancyGridControl();
            this.SuspendLayout();
            // 
            // occupancyGridControl1
            // 
            this.occupancyGridControl1.AreaHeight = 1F;
            this.occupancyGridControl1.Location = new System.Drawing.Point(12, 12);
            this.occupancyGridControl1.Name = "occupancyGridControl1";
            this.occupancyGridControl1.Size = new System.Drawing.Size(572, 409);
            this.occupancyGridControl1.TabIndex = 0;
            this.occupancyGridControl1.Text = "occupancyGridControl1";
            // 
            // SchedulingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 433);
            this.Controls.Add(this.occupancyGridControl1);
            this.Name = "SchedulingForm";
            this.Text = "SchedulingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private OccupancyGridControl occupancyGridControl1;
    }
}