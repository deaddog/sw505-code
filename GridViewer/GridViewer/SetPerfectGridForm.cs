using CommonLib.DTOs;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GridViewer
{
    public partial class SetPerfectGridForm : Form
    {
        private StateGrid grid;
        private Size diffSize;

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

        public SetPerfectGridForm()
        {
            InitializeComponent();

            diffSize = new Size(this.Width - gridControl.Width, this.Height - gridControl.Height);
            gridControl.Resize += gridControl_Resize;

            gridControl.Image = Properties.Resources.emptyGrid;

            this.perfectPath = ExecutingDirectory + "\\..\\..\\perfectgrid";
            this.grid = StateGrid.Load(SystemInterface.GUI.Controls.GridLogger.LoadGrids(this.perfectPath)[0], CalcState);
            this.gridControl.Grid = StateGrid.BuildGrid(grid, 10, -150, -115, CalcProbability);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            FileInfo file = new FileInfo(perfectPath);
            if (file.Exists)
                file.Delete();

            SystemInterface.GUI.Controls.GridLogger logger = new SystemInterface.GUI.Controls.GridLogger(perfectPath);
            logger.Log(gridControl.Grid);
        }
        private void gridControl_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            else
                this.Size = new Size(gridControl.Width + diffSize.Width, gridControl.Height + diffSize.Height);
        }

        private CellState settingState = CellState.Unknown;

        private void gridControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                CellIndex index = gridControl.GetCellIndex(e.Location);
                settingState = grid[index.X, index.Y] == CellState.Free ? CellState.Unknown : CellState.Free;
                UpdateCell(index);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CellIndex index = gridControl.GetCellIndex(e.Location);
                settingState = grid[index.X, index.Y] == CellState.Occupied ? CellState.Unknown : CellState.Occupied;
                UpdateCell(index);
            }
        }
        private void gridControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CellIndex index = gridControl.GetCellIndex(e.Location);
                UpdateCell(index);
            }
        }

        private void UpdateCell(CellIndex index)
        {
            CellState oldstate = grid[index.X, index.Y];
            if (settingState == oldstate)
                return;

            grid[index.X, index.Y] = settingState;

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
