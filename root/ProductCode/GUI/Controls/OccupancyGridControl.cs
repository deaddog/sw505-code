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

        // Array of rectangles representing the occupancy grid
        private Rectangle[,] gridRectangles;
        // default values (overridden when grid is set)
        private int gridColumns = 10;
        // default values (overridden when grid is set)
        private int gridRows = 10;

        #region Grid properties
        private OccupancyGrid grid;
        private bool gridHideUnexplored = false;
        private bool gridShowBorders = true;
        private bool gridShowProbilities = false;
        private bool gridShowRuler = false;

        /// <summary>
        /// The location of the grid in the picturebox in pixels
        /// </summary>
        public Point GridActualLocation = new Point(0, 0);

        /// <summary>
        /// The size of the grid in pixels
        /// </summary>
        public Size GridActualSize = new Size(1, 1); // Default value to prevent designer exception

        /// <summary>
        /// The grid containing the data. Redrawn each time given a new grid.
        /// </summary>
        public OccupancyGrid Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                gridRows = grid.Rows;
                gridColumns = grid.Columns;
                this.Invalidate();
            }
        }

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
        /// Default constructor for OccupancyGridControl
        /// </summary>
        /// <param name="point">The location on the image where the grid shall be drawn</param>
        public OccupancyGridControl()
        {
            this.GridActualLocation = new Point(0, 0);
        }

        /// <summary>
        /// Constructor for OccupancyGridControl
        /// </summary>
        /// <param name="point">The location on the image where the grid shall be drawn</param>
        public OccupancyGridControl(Point point)
        {
            this.GridActualLocation = point;
        }

        /// <summary>
        /// Wraps the SetProbability() method on the grid to decrease number of re-drawns of grid
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="probability"></param>
        public void SetProbability(int row, int column, double probability)
        {
            grid.SetProbability(row, column, probability);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (gridRectangles == null)
                initializeRectangles();

            drawGrid(pe.Graphics);

            if (GridShowRuler)
                drawRulers(pe.Graphics);
        }

        /// <summary>
        /// Method to draw the occupancy grid on top of the picturebox
        /// </summary>
        /// <param name="g">The graphics on which to draw on</param>
        private void drawGrid(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 1);
            pen.Alignment = PenAlignment.Center;

            int actualRectangleHeight = GridActualSize.Height / gridRows;
            int actualRectangleWidth = GridActualSize.Width / gridColumns;

            for (int columnIndex = 0; columnIndex < grid.Columns; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < grid.Rows; rowIndex++)
                {
                    SolidBrush rectangleBrush = new SolidBrush(setColor(grid[columnIndex, rowIndex]));
                    Rectangle r = gridRectangles[columnIndex, rowIndex];

                    Point rectanglePoint1 = new Point(GridActualLocation.X, GridActualLocation.Y);
                    Point rectanglePoint2 = new Point(GridActualLocation.X, GridActualLocation.Y);

                    int pointX = columnIndex * actualRectangleWidth + GridActualLocation.X + this.Padding.Left;
                    int pointY = rowIndex * actualRectangleHeight + GridActualLocation.Y + this.Padding.Top;
                    r.Location = new Point(pointX, pointY);
                    r.Size = new Size(actualRectangleWidth, actualRectangleHeight);

                    #region Draw the grid
                    g.FillRectangle(rectangleBrush, r);

                    if (GridShowBorders)
                        g.DrawRectangle(pen, r);

                    if (GridShowProbabilities)
                        drawProbabilities(grid[columnIndex, rowIndex], g, r);

                    #endregion
                    rectangleBrush.Dispose();
                }
            }
        }

        /// <summary>
        /// Draws a probability on the grid
        /// </summary>
        /// <param name="probability"></param>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="r">Rectangle to contain the probability</param>
        private void drawProbabilities(double probability, Graphics g, Rectangle r)
        {
            SolidBrush textBrush = new SolidBrush(Color.FromArgb(255, Color.Black));
            Font drawFont = new System.Drawing.Font("Arial", 9);
            StringFormat drawFormat = new System.Drawing.StringFormat();

            if (GridShowProbabilities && !GridHideUnexplored)
            {
                g.DrawString(probability.ToString(), drawFont, textBrush, r.Location);
            }
            else if (GridShowProbabilities && GridHideUnexplored && probability != 0.5)
            {
                g.DrawString(probability.ToString(), drawFont, textBrush, r.Location);
            }
            textBrush.Dispose();
        }

        /// <summary>
        /// Draws a ruler to make it easier to identify rows and columns
        /// </summary>
        /// <param name="g">The graphics on which to draw on</param>
        private void drawRulers(Graphics g)
        {
            Pen rulerLinesPen = new Pen(Color.Gray);
            SolidBrush rulerBackgroundBrush = new SolidBrush(Color.FromArgb(225, Color.White));

            SolidBrush textBrush = new SolidBrush(Color.Black);
            Font drawFont = new System.Drawing.Font("Arial", 8);

            int rectangleWidth = GridActualSize.Width / gridColumns;
            int rectangleHeight = GridActualSize.Height / gridRows;

            // Column ruler
            for (int i = 0; i < grid.Columns; i++)
            {
                Rectangle r = new Rectangle();
                r.Location = new Point(i * rectangleWidth + GridActualLocation.X + Padding.Left, GridActualLocation.Y - RULER_HEIGHT_WIDTH + Padding.Top);
                r.Size = new Size(rectangleWidth, RULER_HEIGHT_WIDTH);

                g.FillRectangle(rulerBackgroundBrush, r); 
                g.DrawRectangle(rulerLinesPen, r);
                g.DrawString((i + 1).ToString(), drawFont, textBrush, r.Location);
            }

            // Height ruler
            for (int i = 0; i < grid.Rows; i++)
            {
                Rectangle r = new Rectangle();
                r.Location = new Point(GridActualLocation.X - RULER_HEIGHT_WIDTH + Padding.Left, i * rectangleHeight + GridActualLocation.Y + Padding.Top);
                r.Size = new Size(RULER_HEIGHT_WIDTH, rectangleHeight);

                g.FillRectangle(rulerBackgroundBrush, r);
                g.DrawRectangle(rulerLinesPen, r);
                g.DrawString((i + 1).ToString(), drawFont, textBrush, r.Location);
            }
            rulerBackgroundBrush.Dispose();
            textBrush.Dispose();
        }
        
        /// <summary>
        /// Initializes the array of rectangles, each to represent a cell in the occupancy grid
        /// </summary>
        private void initializeRectangles()
        {
            gridRectangles = new Rectangle[grid.Rows, grid.Columns];

            for (int i = 0; i < grid.Columns; i++)
            {
                for (int j = 0; j < grid.Rows; j++)
                {
                    Rectangle r = new Rectangle();

                    gridRectangles[i, j] = r;
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