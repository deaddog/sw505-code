﻿using CommonLib.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridViewer
{
    public partial class SetPerfectGridForm : Form
    {
        private StateGrid grid;
        private Size diffSize;

        public SetPerfectGridForm()
        {
            InitializeComponent();

            diffSize = new Size(this.Width - gridControl.Width, this.Height - gridControl.Height);
            gridControl.Resize += gridControl_Resize;

            gridControl.Grid = new OccupancyGrid(30, 23, 10, -150, -115);
            gridControl.Image = Properties.Resources.emptyGrid;

            this.grid = new StateGrid(30, 23);
        }
        private void gridControl_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            else
                this.Size = new Size(gridControl.Width + diffSize.Width, gridControl.Height + diffSize.Height);
        }

        private void gridControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                CellIndex index = gridControl.GetCellIndex(e.Location);
                UpdateCell(index, CellState.Free);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CellIndex index = gridControl.GetCellIndex(e.Location);
                UpdateCell(index, CellState.Occupied);
            }
        }
        private void gridControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                CellIndex index = gridControl.GetCellIndex(e.Location);
                UpdateCell(index, CellState.Free);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CellIndex index = gridControl.GetCellIndex(e.Location);
                UpdateCell(index, CellState.Occupied);
            }
        }

        private void UpdateCell(CellIndex index, CellState state)
        {
            CellState oldstate = grid[index.X, index.Y];
            if (state == oldstate)
                return;

            grid[index.X, index.Y] = state;

            gridControl.Grid = StateGrid.BuildGrid(grid,
                gridControl.Grid.CellSize, gridControl.Grid.X, gridControl.Grid.Y, CalcProbability);
        }

        private double CalcProbability(CellState state)
        {
            switch (state)
            {
                case CellState.Free: return 0;
                case CellState.Occupied: return 1;
                case CellState.Unknown: return 0.5;
                default: throw new ApplicationException("Invalid state");
            }
        }
    }
}
