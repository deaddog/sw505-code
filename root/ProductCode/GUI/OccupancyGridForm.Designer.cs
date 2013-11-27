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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OccupancyGridForm));
            this.labelRow = new System.Windows.Forms.Label();
            this.labelColumn = new System.Windows.Forms.Label();
            this.labelProbability = new System.Windows.Forms.Label();
            this.buttonSet = new System.Windows.Forms.Button();
            this.comboBoxRows = new System.Windows.Forms.ComboBox();
            this.comboBoxColumns = new System.Windows.Forms.ComboBox();
            this.comboBoxProbability = new System.Windows.Forms.ComboBox();
            this.checkBoxShowBorders = new System.Windows.Forms.CheckBox();
            this.checkBoxShowProbabilities = new System.Windows.Forms.CheckBox();
            this.checkBoxHideUnexplored = new System.Windows.Forms.CheckBox();
            this.checkBoxShowRulers = new System.Windows.Forms.CheckBox();
            this.occupancyGridControl1 = new SystemInterface.GUI.Controls.OccupancyGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.occupancyGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRow
            // 
            this.labelRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRow.AutoSize = true;
            this.labelRow.Location = new System.Drawing.Point(1114, 93);
            this.labelRow.Margin = new System.Windows.Forms.Padding(5, 8, 5, 5);
            this.labelRow.Name = "labelRow";
            this.labelRow.Size = new System.Drawing.Size(41, 20);
            this.labelRow.TabIndex = 1;
            this.labelRow.Text = "Row";
            // 
            // labelColumn
            // 
            this.labelColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColumn.AutoSize = true;
            this.labelColumn.Location = new System.Drawing.Point(1114, 28);
            this.labelColumn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 5);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(63, 20);
            this.labelColumn.TabIndex = 2;
            this.labelColumn.Text = "Column";
            // 
            // labelProbability
            // 
            this.labelProbability.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProbability.AutoSize = true;
            this.labelProbability.Location = new System.Drawing.Point(1114, 160);
            this.labelProbability.Margin = new System.Windows.Forms.Padding(5, 8, 5, 5);
            this.labelProbability.Name = "labelProbability";
            this.labelProbability.Size = new System.Drawing.Size(81, 20);
            this.labelProbability.TabIndex = 3;
            this.labelProbability.Text = "Probability";
            // 
            // buttonSet
            // 
            this.buttonSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSet.Location = new System.Drawing.Point(1114, 243);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(152, 36);
            this.buttonSet.TabIndex = 3;
            this.buttonSet.Text = "Set";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // comboBoxRows
            // 
            this.comboBoxRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRows.FormattingEnabled = true;
            this.comboBoxRows.Location = new System.Drawing.Point(1114, 121);
            this.comboBoxRows.Name = "comboBoxRows";
            this.comboBoxRows.Size = new System.Drawing.Size(152, 28);
            this.comboBoxRows.TabIndex = 1;
            this.comboBoxRows.SelectedIndexChanged += new System.EventHandler(this.comboBoxRows_SelectedIndexChanged);
            this.comboBoxRows.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxRows_KeyDown);
            // 
            // comboBoxColumns
            // 
            this.comboBoxColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxColumns.FormattingEnabled = true;
            this.comboBoxColumns.Location = new System.Drawing.Point(1114, 54);
            this.comboBoxColumns.Name = "comboBoxColumns";
            this.comboBoxColumns.Size = new System.Drawing.Size(152, 28);
            this.comboBoxColumns.TabIndex = 0;
            this.comboBoxColumns.SelectedIndexChanged += new System.EventHandler(this.comboBoxColumns_SelectedIndexChanged);
            this.comboBoxColumns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxColumns_KeyDown);
            // 
            // comboBoxProbability
            // 
            this.comboBoxProbability.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProbability.FormattingEnabled = true;
            this.comboBoxProbability.Location = new System.Drawing.Point(1114, 188);
            this.comboBoxProbability.Name = "comboBoxProbability";
            this.comboBoxProbability.Size = new System.Drawing.Size(152, 28);
            this.comboBoxProbability.TabIndex = 2;
            this.comboBoxProbability.SelectedIndexChanged += new System.EventHandler(this.comboBoxProbability_SelectedIndexChanged);
            this.comboBoxProbability.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxProbability_KeyDown);
            // 
            // checkBoxShowBorders
            // 
            this.checkBoxShowBorders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowBorders.AutoSize = true;
            this.checkBoxShowBorders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxShowBorders.Checked = true;
            this.checkBoxShowBorders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowBorders.Location = new System.Drawing.Point(1140, 304);
            this.checkBoxShowBorders.Name = "checkBoxShowBorders";
            this.checkBoxShowBorders.Size = new System.Drawing.Size(126, 24);
            this.checkBoxShowBorders.TabIndex = 4;
            this.checkBoxShowBorders.Text = "Show borders";
            this.checkBoxShowBorders.UseVisualStyleBackColor = true;
            this.checkBoxShowBorders.CheckedChanged += new System.EventHandler(this.checkBoxShowBorders_CheckedChanged);
            // 
            // checkBoxShowProbabilities
            // 
            this.checkBoxShowProbabilities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowProbabilities.AutoSize = true;
            this.checkBoxShowProbabilities.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxShowProbabilities.Location = new System.Drawing.Point(1110, 364);
            this.checkBoxShowProbabilities.Name = "checkBoxShowProbabilities";
            this.checkBoxShowProbabilities.Size = new System.Drawing.Size(156, 24);
            this.checkBoxShowProbabilities.TabIndex = 6;
            this.checkBoxShowProbabilities.Text = "Show probabilities";
            this.checkBoxShowProbabilities.UseVisualStyleBackColor = true;
            this.checkBoxShowProbabilities.CheckedChanged += new System.EventHandler(this.checkBoxShowProbabilities_CheckedChanged);
            // 
            // checkBoxHideUnexplored
            // 
            this.checkBoxHideUnexplored.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxHideUnexplored.AutoSize = true;
            this.checkBoxHideUnexplored.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxHideUnexplored.Checked = true;
            this.checkBoxHideUnexplored.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideUnexplored.Enabled = false;
            this.checkBoxHideUnexplored.Location = new System.Drawing.Point(1123, 394);
            this.checkBoxHideUnexplored.Name = "checkBoxHideUnexplored";
            this.checkBoxHideUnexplored.Size = new System.Drawing.Size(143, 24);
            this.checkBoxHideUnexplored.TabIndex = 7;
            this.checkBoxHideUnexplored.Text = "Hide unexplored";
            this.checkBoxHideUnexplored.UseVisualStyleBackColor = true;
            this.checkBoxHideUnexplored.CheckedChanged += new System.EventHandler(this.checkBoxHideUnexplored_CheckedChanged);
            // 
            // checkBoxShowRulers
            // 
            this.checkBoxShowRulers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowRulers.AutoSize = true;
            this.checkBoxShowRulers.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxShowRulers.Location = new System.Drawing.Point(1155, 334);
            this.checkBoxShowRulers.Name = "checkBoxShowRulers";
            this.checkBoxShowRulers.Size = new System.Drawing.Size(111, 24);
            this.checkBoxShowRulers.TabIndex = 5;
            this.checkBoxShowRulers.Text = "Show rulers";
            this.checkBoxShowRulers.UseVisualStyleBackColor = true;
            this.checkBoxShowRulers.CheckedChanged += new System.EventHandler(this.checkBoxShowRulers_CheckedChanged);
            // 
            // occupancyGridControl1
            // 
            this.occupancyGridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.occupancyGridControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.occupancyGridControl1.GridHideUnexplored = true;
            this.occupancyGridControl1.GridShowRuler = true;
            this.occupancyGridControl1.Image = ((System.Drawing.Image)(resources.GetObject("occupancyGridControl1.Image")));
            this.occupancyGridControl1.Location = new System.Drawing.Point(23, 23);
            this.occupancyGridControl1.Name = "occupancyGridControl1";
            this.occupancyGridControl1.Padding = new System.Windows.Forms.Padding(25);
            this.occupancyGridControl1.Size = new System.Drawing.Size(1063, 804);
            this.occupancyGridControl1.TabIndex = 0;
            this.occupancyGridControl1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 868);
            this.Controls.Add(this.checkBoxShowRulers);
            this.Controls.Add(this.checkBoxHideUnexplored);
            this.Controls.Add(this.checkBoxShowProbabilities);
            this.Controls.Add(this.checkBoxShowBorders);
            this.Controls.Add(this.comboBoxProbability);
            this.Controls.Add(this.comboBoxColumns);
            this.Controls.Add(this.comboBoxRows);
            this.Controls.Add(this.buttonSet);
            this.Controls.Add(this.labelProbability);
            this.Controls.Add(this.labelColumn);
            this.Controls.Add(this.labelRow);
            this.Controls.Add(this.occupancyGridControl1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.occupancyGridControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SystemInterface.GUI.Controls.OccupancyGridControl occupancyGridControl1;
        private System.Windows.Forms.Label labelRow;
        private System.Windows.Forms.Label labelColumn;
        private System.Windows.Forms.Label labelProbability;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.ComboBox comboBoxRows;
        private System.Windows.Forms.ComboBox comboBoxColumns;
        private System.Windows.Forms.ComboBox comboBoxProbability;
        private System.Windows.Forms.CheckBox checkBoxShowBorders;
        private System.Windows.Forms.CheckBox checkBoxShowProbabilities;
        private System.Windows.Forms.CheckBox checkBoxHideUnexplored;
        private System.Windows.Forms.CheckBox checkBoxShowRulers;
    }
}

