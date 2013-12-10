using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Data;
using CommonLib;
using CommonLib.DTOs;

namespace Services.RouteServices
{
    /// <summary>
    /// Represents an occupancy grid drawn on top of a picturebox
    /// </summary>
    public class OccupancyGridControl : System.Windows.Forms.Control
    {
        private const float DEFAULT_AREASIZE = 1;
        private const int GRID_TRANSPARANCY = 70;
        private const int CROSS_SIZE = 10;
        // shows unexplored areas more clearly compared to other cells by lowering transparancy
        private const int UNEXPLORED_TRANSPARANC_TO_SUBSTRACT = 25;

        // The image size below (640x480) is updated to match any image set using the Image property
        private CoordinateConverter conv = new CoordinateConverter(640, 480, DEFAULT_AREASIZE, DEFAULT_AREASIZE);

        private OccupancyGrid grid;
        /// <summary>
        /// The grid containing the data. Redrawn each time given a new grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OccupancyGrid Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                this.Invalidate();
            }
        }

        private float areaWidth = DEFAULT_AREASIZE, areaHeight = DEFAULT_AREASIZE;
        [DefaultValue(DEFAULT_AREASIZE)]
        public float AreaWidth
        {
            get { return areaWidth; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Area height must be greather than zero.");

                float old = areaWidth;

                if (old != value)
                    conv.SetActualSize(value, areaHeight);

                areaWidth = value;
                this.Invalidate();
            }
        }
        [DefaultValue(DEFAULT_AREASIZE)]
        public float AreaHeight
        {
            get { return areaHeight; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Area height must be greather than zero.");

                float old = areaHeight;

                if (old != value)
                    conv.SetActualSize(areaWidth, value);

                areaHeight = value;
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

        private LinkedList<LinkedList<Vector2D>> drawnRoutes;

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupancyGridControl"/> control.
        /// </summary>
        public OccupancyGridControl()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            conv.SetPixelSize(640, 480);

            drawnRoutes = new LinkedList<LinkedList<Vector2D>>();
        }

        protected override void OnResize(EventArgs e)
        {
            this.conv.SetPixelSize(this.Width - Padding.Horizontal, this.Height - Padding.Vertical);
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (DesignMode)
                return;

            for (int columnIndex = 0; columnIndex < grid.Columns; columnIndex++)
                for (int rowIndex = 0; rowIndex < grid.Rows; rowIndex++)
                    drawCell(pe.Graphics, columnIndex, rowIndex);

            if (gridShowRuler)
                drawRulers(pe.Graphics);

            foreach (var route in drawnRoutes)
                foreach (var element in route)
                {
                    PointF p = (PointF)(conv.ConvertActualToPixel(element) + new Vector2D(Padding.Left, Padding.Top));
                    drawCross(pe.Graphics, p);
                }
        }

        private void drawCell(Graphics graphics, int x, int y)
        {
            Vector2D topleft = getPixelCoordinates(x, y);
            Vector2D bottomright = getPixelCoordinates(x + 1, y + 1);

            RectangleF r = RectangleF.FromLTRB(topleft.X, topleft.Y, bottomright.X, bottomright.Y);

            using (SolidBrush brush = new SolidBrush(getColor(grid[x, y])))
                graphics.FillRectangle(brush, r);

            if (gridShowBorders)
                using (Pen pen = new Pen(Color.FromArgb(70, Color.Black)))
                    graphics.DrawRectangle(pen, r.X, r.Y, r.Width, r.Height);
            if (gridShowProbilities && (!gridHideUnexplored || grid[x, y] != 0.5))
            {
                string text = grid[x, y].ToString("0.0");
                //Measure text size and calculate position so that text is centered.
                SizeF textSize = graphics.MeasureString(text, this.Font);
                PointF p = new PointF((r.Width - textSize.Width) / 2f + r.X, (r.Height - textSize.Height) / 2f + r.Y);

                graphics.DrawString(text, this.Font, Brushes.Black, p);
            }
        }

        private Vector2D getPixelCoordinates(int x, int y)
        {
            Vector2D vector = conv.ConvertActualToPixel(new Vector2D(grid.X, grid.Y) + new Vector2D(grid.CellSize * x, grid.CellSize * y));
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
                    Vector2D topleft = new Vector2D(getPixelCoordinates(i, 0).X, 0);
                    Vector2D bottomright = new Vector2D(getPixelCoordinates(i + 1, 0).X, Padding.Top);

                    drawRulerRectangle(g, rulerBackgroundBrush, topleft, bottomright, false);
                }

                // Rows ruler
                for (int i = 0; i < grid.Rows; i++)
                {
                    Vector2D topleft = new Vector2D(0, getPixelCoordinates(0, i).Y);
                    Vector2D bottomright = new Vector2D(Padding.Left, getPixelCoordinates(0, i + 1).Y);

                    drawRulerRectangle(g, rulerBackgroundBrush, topleft, bottomright, true);
                }
            }
        }

        private void drawCross(Graphics g, PointF p)
        {
            g.DrawLine(Pens.Red, new PointF(p.X - CROSS_SIZE, p.Y), new PointF(p.X + CROSS_SIZE, p.Y));
            g.DrawLine(Pens.Red, new PointF(p.X, p.Y - CROSS_SIZE), new PointF(p.X, p.Y + CROSS_SIZE));

        }

        private void drawRulerRectangle(Graphics g, Brush brush, Vector2D topleft, Vector2D bottomright, bool row)
        {
            RectangleF r = RectangleF.FromLTRB(topleft.X, topleft.Y, bottomright.X, bottomright.Y);

            g.FillRectangle(brush, r);
            g.DrawRectangle(Pens.Gray, r.X, r.Y, r.Width, r.Height);

            var point = conv.ConvertPixelToActual(topleft + new Vector2D(Padding.Left, Padding.Top));
            string cm = (row ? point.Y : point.X).ToString("0");

            //Measure text size and calculate position so that text is centered.
            SizeF textSize = g.MeasureString(cm, this.Font);
            PointF p = new PointF((r.Width - textSize.Width) / 2f + r.X, (r.Height - textSize.Height) / 2f + r.Y);

            g.DrawString(cm, this.Font, Brushes.Black, p);
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
        private Color getColor(double probability)
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

        private Vector2D GetPoint(Point location)
        {
            return GetPoint(location.X, location.Y);
        }
        private Vector2D GetPoint(int x, int y)
        {
            return conv.ConvertPixelToActual(new Vector2D(x - Padding.Left, y - Padding.Top));
        }
    }
}