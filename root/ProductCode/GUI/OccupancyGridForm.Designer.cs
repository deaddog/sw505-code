using System.Drawing;
namespace SystemInterface.GUI
{
    partial class OccupancyGridForm
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
            this.checkBoxShowBorders = new System.Windows.Forms.CheckBox();
            this.checkBoxShowProbabilities = new System.Windows.Forms.CheckBox();
            this.checkBoxHideUnexplored = new System.Windows.Forms.CheckBox();
            this.checkBoxShowRulers = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridControl = new SystemInterface.GUI.Controls.OccupancyGridControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxShowBorders
            // 
            this.checkBoxShowBorders.AutoSize = true;
            this.checkBoxShowBorders.Location = new System.Drawing.Point(5, 18);
            this.checkBoxShowBorders.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxShowBorders.Name = "checkBoxShowBorders";
            this.checkBoxShowBorders.Size = new System.Drawing.Size(91, 17);
            this.checkBoxShowBorders.TabIndex = 4;
            this.checkBoxShowBorders.Text = "Show borders";
            this.checkBoxShowBorders.UseVisualStyleBackColor = true;
            this.checkBoxShowBorders.CheckedChanged += new System.EventHandler(this.checkBoxShowBorders_CheckedChanged);
            // 
            // checkBoxShowProbabilities
            // 
            this.checkBoxShowProbabilities.AutoSize = true;
            this.checkBoxShowProbabilities.Location = new System.Drawing.Point(5, 60);
            this.checkBoxShowProbabilities.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxShowProbabilities.Name = "checkBoxShowProbabilities";
            this.checkBoxShowProbabilities.Size = new System.Drawing.Size(111, 17);
            this.checkBoxShowProbabilities.TabIndex = 6;
            this.checkBoxShowProbabilities.Text = "Show probabilities";
            this.checkBoxShowProbabilities.UseVisualStyleBackColor = true;
            this.checkBoxShowProbabilities.CheckedChanged += new System.EventHandler(this.checkBoxShowProbabilities_CheckedChanged);
            // 
            // checkBoxHideUnexplored
            // 
            this.checkBoxHideUnexplored.AutoSize = true;
            this.checkBoxHideUnexplored.Checked = true;
            this.checkBoxHideUnexplored.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideUnexplored.Enabled = false;
            this.checkBoxHideUnexplored.Location = new System.Drawing.Point(5, 81);
            this.checkBoxHideUnexplored.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxHideUnexplored.Name = "checkBoxHideUnexplored";
            this.checkBoxHideUnexplored.Size = new System.Drawing.Size(103, 17);
            this.checkBoxHideUnexplored.TabIndex = 7;
            this.checkBoxHideUnexplored.Text = "Hide unexplored";
            this.checkBoxHideUnexplored.UseVisualStyleBackColor = true;
            this.checkBoxHideUnexplored.CheckedChanged += new System.EventHandler(this.checkBoxHideUnexplored_CheckedChanged);
            // 
            // checkBoxShowRulers
            // 
            this.checkBoxShowRulers.AutoSize = true;
            this.checkBoxShowRulers.Checked = true;
            this.checkBoxShowRulers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowRulers.Location = new System.Drawing.Point(5, 39);
            this.checkBoxShowRulers.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxShowRulers.Name = "checkBoxShowRulers";
            this.checkBoxShowRulers.Size = new System.Drawing.Size(81, 17);
            this.checkBoxShowRulers.TabIndex = 5;
            this.checkBoxShowRulers.Text = "Show rulers";
            this.checkBoxShowRulers.UseVisualStyleBackColor = true;
            this.checkBoxShowRulers.CheckedChanged += new System.EventHandler(this.checkBoxShowRulers_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxShowBorders);
            this.groupBox1.Controls.Add(this.checkBoxShowRulers);
            this.groupBox1.Controls.Add(this.checkBoxShowProbabilities);
            this.groupBox1.Controls.Add(this.checkBoxHideUnexplored);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 103);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gridControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl.GridHideUnexplored = true;
            this.gridControl.GridShowRuler = true;
            this.gridControl.Location = new System.Drawing.Point(142, 15);
            this.gridControl.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl.Name = "gridControl";
            this.gridControl.Padding = new System.Windows.Forms.Padding(25);
            this.gridControl.Size = new System.Drawing.Size(712, 534);
            this.gridControl.TabIndex = 0;
            this.gridControl.TabStop = false;
            // 
            // OccupancyGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 564);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gridControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OccupancyGridForm";
            this.Padding = new System.Windows.Forms.Padding(13);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Occupancy Grid";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SystemInterface.GUI.Controls.OccupancyGridControl gridControl;
        private System.Windows.Forms.CheckBox checkBoxShowBorders;
        private System.Windows.Forms.CheckBox checkBoxShowProbabilities;
        private System.Windows.Forms.CheckBox checkBoxHideUnexplored;
        private System.Windows.Forms.CheckBox checkBoxShowRulers;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

