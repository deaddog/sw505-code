﻿using CommonLib;
using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace GridViewer
{
    public class OccupancyGridControl : System.Windows.Forms.Control
    {
        private const float DEFAULT_AREASIZE = 1;
        private const int GRID_TRANSPARANCY = 150;
        private const float POSE_VECTOR_LENGTH = 50;

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

        private Image image;
        public Image Image
        {
            get { return image; }
            set
            {
                Size size = image != null ? image.Size : Size.Empty;

                if (value != null && value.Size != size)
                {
                    conv.SetPixelSize(value.Width, value.Height);
                    this.Size = value.Size + new Size(Padding.Horizontal, Padding.Vertical);
                }

                image = value;
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

        private CoordinateCollection drawCoordinates;
        private PoseCollection drawPoses;
        private const float pointradius = 3;

        public DrawCollection<ICoordinate> Coordinates
        {
            get { return drawCoordinates; }
        }
        public DrawCollection<IPose> Poses
        {
            get { return drawPoses; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupancyGridControl"/> control.
        /// </summary>
        public OccupancyGridControl()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true);

            this.drawCoordinates = new CoordinateCollection(this);
            this.drawPoses = new PoseCollection(this);

            this.Coordinates.SetColor("robot", Color.Black);
            this.Coordinates["robot"] = new Vector2D(0, 0);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (DesignMode)
            {
                pe.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
                return;
            }

            if (image != null)
                pe.Graphics.DrawImage(image, Padding.Left, Padding.Top);

            for (int columnIndex = 0; columnIndex < grid.Columns; columnIndex++)
                for (int rowIndex = 0; rowIndex < grid.Rows; rowIndex++)
                    drawCell(pe.Graphics, columnIndex, rowIndex);

            if (gridShowRuler)
                drawRulers(pe.Graphics);

            pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            drawCoordinates.Draw(pe.Graphics);
            drawPoses.Draw(pe.Graphics);
        }

        private void drawCell(Graphics graphics, int x, int y)
        {
            Vector2D topleft = getPixelCoordinates(x, y);
            Vector2D bottomright = getPixelCoordinates(x + 1, y + 1);

            RectangleF r = RectangleF.FromLTRB(topleft.X, topleft.Y, bottomright.X, bottomright.Y);

            if (grid[x, y] != 0.5)
                using (SolidBrush brush = new SolidBrush(getColor(grid[x, y])))
                    graphics.FillRectangle(brush, r);

            if (gridShowBorders)
                using (Pen pen = new Pen(Color.FromArgb(70, Color.White)))
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

        private void drawRulerRectangle(Graphics g, Brush brush, Vector2D topleft, Vector2D bottomright, bool row)
        {
            RectangleF r = RectangleF.FromLTRB(topleft.X, topleft.Y, bottomright.X, bottomright.Y);

            g.FillRectangle(brush, r);
            g.DrawRectangle(Pens.Gray, r.X, r.Y, r.Width, r.Height);

            var point = conv.ConvertPixelToActual(topleft - new Vector2D(Padding.Left, Padding.Top));
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
            else
                return Color.FromArgb(0, 0, 0, 0);
        }
        public CellIndex GetCellIndex(Point location)
        {
            return GetCellIndex(location.X, location.Y);
        }
        public CellIndex GetCellIndex(int x, int y)
        {
            return grid.GetIndex(conv.ConvertPixelToActual(new Vector2D(x - Padding.Left, y - Padding.Top)));
        }

        public class PoseCollection : DrawCollection<IPose>
        {
            public PoseCollection(OccupancyGridControl parent)
                : base(parent, new Dictionary<string, IPose>())
            {
            }

            protected override void DrawElement(Graphics graphics, IPose element, Color color)
            {
                double a = element.Angle * (Math.PI / 180);

                Vector2D p = ConvertActualToPixel(new Vector2D(element.X, element.Y)) + Padding;
                Vector2D p2 = new Vector2D((float)Math.Cos(a), (float)Math.Sin(a)) * POSE_VECTOR_LENGTH + p;

                AdjustableArrowCap bigArrow = new AdjustableArrowCap(3, 4);
                using (Pen pen = new Pen(color) { CustomEndCap = bigArrow })
                    graphics.DrawLine(pen, (PointF)p, (PointF)p2);
            }
        }
        public class CoordinateCollection : DrawCollection<ICoordinate>
        {
            public CoordinateCollection(OccupancyGridControl parent)
                : base(parent, new Dictionary<string, ICoordinate>())
            {
            }

            protected override void DrawElement(Graphics graphics, ICoordinate element, Color color)
            {
                Vector2D p = ConvertActualToPixel(new Vector2D(element.X, element.Y)) + Padding - new Vector2D(pointradius, pointradius);

                using (SolidBrush brush = new SolidBrush(color))
                    graphics.FillEllipse(brush, p.X, p.Y, pointradius * 2, pointradius * 2);
            }
        }

        public abstract class DrawCollection<T>
        {
            private Dictionary<string, T> elements;
            private Dictionary<string, Color> colors;

            private OccupancyGridControl parent;

            protected DrawCollection(OccupancyGridControl parent, Dictionary<string, T> newDictionary)
            {
                this.parent = parent;
                this.elements = newDictionary;
                this.colors = new Dictionary<string, Color>();
            }

            public T this[string key]
            {
                get
                {
                    if (key == null)
                        throw new ArgumentNullException("key");

                    return elements[key];
                }
                set
                {
                    if (key == null)
                        throw new ArgumentNullException("key");

                    elements[key] = value;
                    if (!colors.ContainsKey(key))
                        colors.Add(key, Color.Red);

                    parent.Invalidate();
                }
            }
            public void SetColor(string key, Color color)
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                else
                {
                    colors[key] = color;
                    parent.Invalidate();
                }
            }

            protected abstract void DrawElement(Graphics graphics, T element, Color color);

            protected Vector2D Padding
            {
                get { return new Vector2D(parent.Padding.Left, parent.Padding.Top); }
            }
            protected Vector2D ConvertActualToPixel(Vector2D point)
            {
                return parent.conv.ConvertActualToPixel(point);
            }

            public void Draw(Graphics graphics)
            {
                string[] keys = elements.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                    DrawElement(graphics, elements[keys[i]], colors[keys[i]]);
            }
        }
    }

}
