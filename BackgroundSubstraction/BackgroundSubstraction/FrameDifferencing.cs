using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Drawing;

namespace BackgroundSubstraction
{
    public class FrameDifferencing
    {
        public FrameDifferencing()
        {
        }

        public Bitmap Diff(Bitmap current, Bitmap previous, int threshold = 50)
        {
            Image<Bgr, Byte> frame = new Image<Bgr, byte>(current);
            Image<Bgr, Byte> previousFrame = new Image<Bgr, byte>(previous);
            Image<Bgr, Byte> diff;

            diff = frame.AbsDiff(previousFrame);
            diff = diff.ThresholdBinary(new Bgr(threshold, threshold, threshold), new Bgr(255, 255, 255));

            return diff.ToBitmap();
        }

        public Bitmap FindContour(Bitmap thresholdedBitmap, Bitmap targetBitmap, int countourThreshold = 500)
        {
            Image<Bgr, Byte> difference = new Image<Bgr, byte>(thresholdedBitmap);
            Image<Bgr, Byte> target = new Image<Bgr, byte>(targetBitmap);

            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                //detect the contours and loop through each of them
                for (Contour<Point> contours = difference.Convert<Gray, Byte>().FindContours(
                      Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                      Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST,
                      storage);
                   contours != null;
                   contours = contours.HNext)
                {
                    //Create a contour for the current variable for us to work with
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    //Draw the detected contour on the image
                    if (currentContour.Area > countourThreshold) //only consider contours with area greater than 100 as default then take from form control
                    {
                        target.Draw(currentContour.BoundingRectangle, new Bgr(Color.Red), 2);
                        //Frame.Draw(new Cross2DF(new PointF(100,100), currentContour.), new Bgr(Color.Red), 5);
                    }
                }
            return target.ToBitmap();
        }
    }
}
