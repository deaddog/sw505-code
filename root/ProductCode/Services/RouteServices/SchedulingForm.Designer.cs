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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.labelGuide = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelMouse = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.occupancyGridControl1 = new Services.RouteServices.OccupancyGridControl();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(42, 67);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 2;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelGuide
            // 
            this.labelGuide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGuide.Location = new System.Drawing.Point(3, 16);
            this.labelGuide.Name = "labelGuide";
            this.labelGuide.Size = new System.Drawing.Size(564, 30);
            this.labelGuide.TabIndex = 3;
            this.labelGuide.Text = "Venstre klik tilføjer nye punkter. Ved dobbelt klik oprettes en ny route. Det sen" +
    "este punkt kan fjernes ved at højre klikke.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelGuide);
            this.groupBox1.Location = new System.Drawing.Point(150, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 49);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vejledning";
            // 
            // labelMouse
            // 
            this.labelMouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMouse.Location = new System.Drawing.Point(3, 16);
            this.labelMouse.Name = "labelMouse";
            this.labelMouse.Size = new System.Drawing.Size(126, 30);
            this.labelMouse.TabIndex = 5;
            this.labelMouse.Text = "150.0 ; 150.0";
            this.labelMouse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 96);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(132, 350);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelMouse);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 49);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Musens placering";
            // 
            // occupancyGridControl1
            // 
            this.occupancyGridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.occupancyGridControl1.AreaHeight = 231F;
            this.occupancyGridControl1.AreaWidth = 308F;
            this.occupancyGridControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.occupancyGridControl1.GridShowRuler = true;
            this.occupancyGridControl1.Location = new System.Drawing.Point(150, 67);
            this.occupancyGridControl1.Name = "occupancyGridControl1";
            this.occupancyGridControl1.Padding = new System.Windows.Forms.Padding(25, 25, 0, 0);
            this.occupancyGridControl1.Size = new System.Drawing.Size(570, 379);
            this.occupancyGridControl1.TabIndex = 0;
            this.occupancyGridControl1.Text = "occupancyGridControl1";
            this.occupancyGridControl1.UpdatePoint += new System.EventHandler(this.occupancyGridControl1_UpdatePoint);
            // 
            // SchedulingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 460);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.occupancyGridControl1);
            this.Name = "SchedulingForm";
            this.Text = "SchedulingForm";
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OccupancyGridControl occupancyGridControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Label labelGuide;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelMouse;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}