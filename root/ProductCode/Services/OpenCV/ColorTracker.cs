using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TrackColorForm
{
    public class ColorTracker
    {
        private const float DEFAULT_THRESHOLD = 50;

        private float threshold;
        private Color targetColor;
        private PointF center;

        private DateTime lastUpdate;

        public ColorTracker(Color color)
        {
            this.threshold = DEFAULT_THRESHOLD;
            this.targetColor = color;
        }

        public float Threshold
        {
            get { return threshold; }
        }
        public Color Color
        {
            get { return targetColor; }
            set
            {
                this.targetColor = value;
                threshold = DEFAULT_THRESHOLD;
            }
        }
        public PointF Center
        {
            get { return center; }
        }
    }
}
