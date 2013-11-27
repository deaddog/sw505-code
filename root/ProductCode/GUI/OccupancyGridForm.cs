using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using CommonLib.DTOs;

namespace SystemInterface.GUI
{
    public partial class OccupancyGridForm : Form
    {
        public OccupancyGridForm()
        {
            InitializeComponent();
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            submitForm();
        }

        private void checkBoxShowBorders_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowBorders.Checked)
                occupancyGridControl1.GridShowBorders = true;
            else
                occupancyGridControl1.GridShowBorders = false;
        }

        private void checkBoxShowProbabilities_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowProbabilities.Checked)
                occupancyGridControl1.GridShowProbabilities = true;
            else
                occupancyGridControl1.GridShowProbabilities = false;

            validateCheckBoxes();
        }

        private void comboBoxColumns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && validateForm())
                submitForm();
        }

        private void comboBoxColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateForm();
        }

        private void comboBoxProbability_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && validateForm())
                submitForm();
        }

        private void comboBoxProbability_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateForm();
        }

        private void comboBoxRows_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && validateForm())
                submitForm();
        }

        private void comboBoxRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OccupancyGrid grid = new OccupancyGrid(10, 10);
            occupancyGridControl1.Grid = grid;

            #region Initialize comboboxes
            foreach (int item in PopulateCombobox(1, grid.Rows))
            {
                comboBoxRows.Items.Add(item);
            }

            foreach (int item in PopulateCombobox(1, grid.Columns))
            {
                comboBoxColumns.Items.Add(item);
            }

            foreach (int item in PopulateCombobox(0, 100))
            {
                comboBoxProbability.Items.Add(Math.Round(item * 0.01, 2));
            }
            #endregion

            validateForm();
            validateCheckBoxes();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            occupancyGridControl1.Invalidate();
        }

        private IEnumerable<int> PopulateCombobox(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                yield return i;
            }
        }
        private void submitForm()
        {
            if (validateForm())
            {
                //Update probability for [comboBoxRows, comboBoxColumns] to comboBoxProbability
            }
        }

        private void validateCheckBoxes()
        {
            if (checkBoxShowProbabilities.Checked)
                checkBoxHideUnexplored.Enabled = true;
            else
                checkBoxHideUnexplored.Enabled = false;

            if (checkBoxHideUnexplored.Checked)
                occupancyGridControl1.GridHideUnexplored = true;
            else
                occupancyGridControl1.GridHideUnexplored = false;

            if (checkBoxShowRulers.Checked)
                occupancyGridControl1.GridShowRuler = true;
            else
                occupancyGridControl1.GridShowRuler = false;
        }

        private bool validateForm()
        {
            if ((comboBoxRows.SelectedIndex == -1
                || comboBoxColumns.SelectedIndex == -1
                || comboBoxProbability.SelectedIndex == -1))
            {
                buttonSet.Enabled = false;
                return false;
            }
            else
            {
                buttonSet.Enabled = true;
                return true;
            }
        }

        private void checkBoxHideUnexplored_CheckedChanged(object sender, EventArgs e)
        {
            validateCheckBoxes();
        }

        private void checkBoxShowRulers_CheckedChanged(object sender, EventArgs e)
        {
            validateCheckBoxes();
        }

        private void occupancyGridControl1_Resize(object sender, EventArgs e)
        {

        }
    }
}
