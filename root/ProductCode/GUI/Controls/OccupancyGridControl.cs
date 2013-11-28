﻿using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SystemInterface.GUI.Controls;
using Data;
using Services.TrackingServices;
using CommonLib.DTOs;

namespace SystemInterface.GUI.Controls
{
    /// <summary>
    /// Represents an occupancy grid drawn on top of a picturebox
    /// </summary>
    public class OccupancyGridControl : PictureBox
    {
        private const int RULER_HEIGHT_WIDTH = 15;
        private const int GRID_TRANSPARANCY = 150;
        // shows unexplored areas more clearly compared to other cells by lowering transparancy
        private const int UNEXPLORED_TRANSPARANC_TO_SUBSTRACT = 25;

        private Services.TrackingServices.CoordinateConverter conv = new CoordinateConverter(640, 480, 308, 231);

        private OccupancyGrid grid;
        /// <summary>
        /// The grid containing the data. Redrawn each time given a new grid.
        /// </summary>
        [Browsable(false)]
        public OccupancyGrid Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                this.Invalidate();
            }
        }

        #region Grid properties

        private bool gridHideUnexplored = false;
        private bool gridShowBorders = true;
        private bool gridShowProbilities = false;
        private bool gridShowRuler = false;

        /// <summary>
        /// When probabilities are shown, hide or show unexplored areas of the grid (probability = 0.5)
        /// </summary>
        [DefaultValue(typeof(bool), "false")]
        public bool GridHideUnexplored
        {
            get { return gridHideUnexplored; }
            set
            {
                gridHideUnexplored = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Draw a border around each cell in the grid
        /// </summary>
        [DefaultValue(typeof(bool), "true")]
        public bool GridShowBorders
        {
            get { return gridShowBorders; }
            set
            {
                gridShowBorders = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Show probabilities of grid cells
        /// </summary>
        [DefaultValue(typeof(bool), "false")]
        public bool GridShowProbabilities
        {
            get { return gridShowProbilities; }
            set
            {
                gridShowProbilities = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Show a ruler with column- and row indices around the grid
        /// </summary>
        [DefaultValue(typeof(bool), "false")]
        public bool GridShowRuler
        {
            get { return gridShowRuler; }
            set
            {
                gridShowRuler = value;
                this.Invalidate();
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupancyGridControl"/> control.
        /// </summary>
        public OccupancyGridControl()
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (DesignMode)
                return;

            drawGrid(pe.Graphics);

            if (gridShowRuler)
                drawRulers(pe.Graphics);
        }

        /// <summary>
        /// Method to draw the occupancy grid on top of the picturebox
        /// </summary>
        /// <param name="g">The graphics on which to draw on</param>
        private void drawGrid(Graphics g)
        {
            for (int columnIndex = 0; columnIndex < grid.Columns; columnIndex++)
                for (int rowIndex = 0; rowIndex < grid.Rows; rowIndex++)
                    drawCell(g, columnIndex, rowIndex);
        }

        private void drawCell(Graphics graphics, int x, int y)
        {
            Vector2D topleft = getPixelCoordinates(x, y);
            Vector2D bottomright = getPixelCoordinates(x + 1, y + 1);

            RectangleF r = RectangleF.FromLTRB(topleft.X, topleft.Y, bottomright.X, bottomright.Y);

            using (SolidBrush brush = new SolidBrush(setColor(grid[x, y])))
                graphics.FillRectangle(brush, r);

            if (gridShowBorders)
                graphics.DrawRectangle(Pens.Black, r.X, r.Y, r.Width, r.Height);
            if (gridShowProbilities && (!gridHideUnexplored || grid[x, y] != 0.5))
                graphics.DrawString(grid[x,y].ToString(), this.Font, Brushes.Black, r.Location);
        }

        private Vector2D getPixelCoordinates(int x, int y)
        {
            Vector2D vector = conv.ConvertActualToPixel(new Vector2D(grid.X, grid.Y) + new Vector2D(grid.CellSize * x, grid.CellSize * y));
            if (gridShowRuler)
                vector += new Vector2D(RULER_HEIGHT_WIDTH, RULER_HEIGHT_WIDTH);
            return vector + new Vector2D(Padding.Left, Padding.Top);
        }

        /// <summary>
        /// Draws a ruler to make it easier to identify rows and columns
        /// </summary>
        /// <param name="g">The graphics on which to draw on</param>
        private void drawRulers(Graphics g)
        {
            using (SolidBrush rulerBackgroundBrush = new SolidBrush(Color.FromArgb(225, Color.White)))
            {
                // Columns ruler 
                for (int i = 0; i < grid.Columns; i++)
                {
                    Rectangle r = new Rectangle();
                    r.Location = new Point(i * rectangleWidth + GridActualLocation.X + Padding.Left, GridActualLocation.Y - RULER_HEIGHT_WIDTH + Padding.Top);
                    r.Size = new Size(rectangleWidth, RULER_HEIGHT_WIDTH);

                    g.FillRectangle(rulerBackgroundBrush, r);
                    g.DrawRectangle(Pens.Gray, r);
                    g.DrawString((i + 1).ToString(), this.Font, Brushes.Black, r.Location);
                }

                // Rows ruler
                for (int i = 0; i < grid.Rows; i++)
                {
                    Rectangle r = new Rectangle();
                    r.Location = new Point(GridActualLocation.X - RULER_HEIGHT_WIDTH + Padding.Left, i * rectangleHeight + GridActualLocation.Y + Padding.Top);
                    r.Size = new Size(RULER_HEIGHT_WIDTH, rectangleHeight);

                    g.FillRectangle(rulerBackgroundBrush, r);
                    g.DrawRectangle(Pens.Gray, r);
                    g.DrawString((i + 1).ToString(), this.Font, Brushes.Black, r.Location);
                }
            }
        }

        /// <summary>
        /// Normalizes the probability in the range from 0-255 to fit a char value (for either a R,G or B value of a color)
        /// </summary>
        /// <param name="probability">The probability to normalize</param>
        /// <param name="minProbability">The lowest value the probability can have</param>
        /// <param name="maxProbability">The highest value the probability can have</param>
        /// <returns></returns>
        private int normalizeColor(double probability, double minProbability, double maxProbability)
        {
            double colormax = 255; // The value to normalize for
            return (int)((probability - minProbability) * (colormax / (maxProbability - minProbability)));
        }

        /// <summary>
        /// Generates a color to represent a probability in the occupancy grid
        /// Low probability (of occupied) generates a greenish color
        /// High probility (of occupied, above 0,5) generates a redish color
        /// Probability of 0,5 returns a yellow color to represent an unexplored area
        /// </summary>
        /// <param name="probability">The probability to generate a color for</param>
        /// <returns>A color representing a probability</returns>
        private Color setColor(double probability)
        {
            if (probability < 0.5) // greens (turn up the reds to make color more yellow)
            {
                int red = normalizeColor(probability, 0.0, 0.5);
                return Color.FromArgb(GRID_TRANSPARANCY, red, 255, 0);
            }
            else if (probability > 0.5) // reds (turn up the green to make color more yellow)
            {
                int green = normalizeColor(probability, 0.5, 1.0);
                return Color.FromArgb(GRID_TRANSPARANCY, 255, 255 - green, 0);
            }
            else // yellow
            {
                int yellowAlpha = GRID_TRANSPARANCY;

                // Only substract the transparancy of unexplored area if below the transparancy of a grid cell
                if (GRID_TRANSPARANCY > UNEXPLORED_TRANSPARANC_TO_SUBSTRACT)
                {
                    yellowAlpha = GRID_TRANSPARANCY - UNEXPLORED_TRANSPARANC_TO_SUBSTRACT;
                }

                return Color.FromArgb(yellowAlpha, 255, 255, 0);
            }
        }
    }
}