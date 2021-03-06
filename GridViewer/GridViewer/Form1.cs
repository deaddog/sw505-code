﻿using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemInterface.GUI.Controls;

namespace GridViewer
{
    public partial class Form1 : Form
    {
        private Size diffSize;
        private OccupancyGrid[] grids = new OccupancyGrid[0];
        private OccupancyGrid defaultGrid;

        private StateGrid perfect = new StateGrid(30, 23);

        private string tickText;
        private string pointText;

        public static string ExecutingDirectory
        {
            get
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Uri uri = new Uri(assembly.CodeBase);
                return Path.GetDirectoryName(uri.LocalPath);
            }
        }

        private readonly string perfectPath;

        public Form1()
        {
            InitializeComponent();

            diffSize = new Size(this.Width - gridControl.Width, this.Height - gridControl.Height);
            gridControl.Resize += gridControl_Resize;

            defaultGrid = new OccupancyGrid(30, 23, 10, -150, -115);
            gridControl.Grid = defaultGrid;
            gridControl.Image = Properties.Resources.emptyGrid;

            tickText = tickLabel.Text;
            pointText = pointsLabel.Text;

            this.perfectPath = ExecutingDirectory + "\\..\\..\\perfectgrid";
            this.perfect = StateGrid.Load(SystemInterface.GUI.Controls.GridLogger.LoadGrids(this.perfectPath)[0], CalcState);

            tickLabel.Text = string.Format(tickText, 0);
            UpdatePoints();
        }
        private void gridControl_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            else
                this.Size = new Size(gridControl.Width + diffSize.Width, gridControl.Height + diffSize.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SetPerfectGridForm form = new SetPerfectGridForm())
            {
                form.ShowDialog();
                this.perfect = StateGrid.Load(SystemInterface.GUI.Controls.GridLogger.LoadGrids(this.perfectPath)[0], CalcState);
                UpdatePoints();
            }
        }

        private void gridControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 && files[0].EndsWith(".log"))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void gridControl_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            this.grids = GridLogger.LoadGrids(file);

            this.trackBar1.Value = -1;
            this.trackBar1.Maximum = grids.Length - 1;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            tickLabel.Text = string.Format(tickText, trackBar1.Value + 1);
            UpdatePoints();

            if (trackBar1.Value == -1)
                gridControl.Grid = defaultGrid;
            else
                gridControl.Grid = grids[trackBar1.Value];
        }

        private void UpdatePoints()
        {
            int points;
            if (trackBar1.Value == -1)
                points = CalculatePoints(defaultGrid);
            else
                points = CalculatePoints(grids[trackBar1.Value]);

            pointsLabel.Text = string.Format(pointText, points);
        }

        private int CalculatePoints(OccupancyGrid grid)
        {
            StateGrid stateGrid = StateGrid.Load(grid, CalcState);

            int p = 0;

            for (int x = 0; x < grid.Columns; x++)
                for (int y = 0; y < grid.Rows; y++)
                    if (perfect[x, y] != CellState.Unknown)
                    {
                        if (perfect[x, y] == stateGrid[x, y]) p++;
                        else p--;
                    }

            return p;
        }
        private CellState CalcState(double probability)
        {
            if (probability < 0.5)
                return CellState.Free;
            else if (probability > 0.5)
                return CellState.Occupied;
            else
                return CellState.Unknown;
        }
    }
}
