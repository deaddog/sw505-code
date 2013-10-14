using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Drawing;

namespace BackgroundSubtraction
{
    //Uses Frame Differencing for finding objects between two Bitmaps
    public class FrameDifferencing
    {
        public FrameDifferencing() { }

        //Finds the absolute difference and thresholdes it
        public static Bitmap Diff(Bitmap current, Bitmap previous, int threshold = 50)
        {
            Image<Bgr, Byte> frame = new Image<Bgr, byte>(current);
            Image<Bgr, Byte> previousFrame = new Image<Bgr, byte>(previous);
            Image<Bgr, Byte> diff;

            diff = frame.AbsDiff(previousFrame);
            diff = diff.ThresholdBinary(new Bgr(threshold, threshold, threshold), new Bgr(255, 255, 255));

            return diff.ToBitmap();
        }

        //Finds the contour from the thresholded bitmap and draws it on to the targetBitmap with a cross in the middle
        //contourThreshold is the size of the contour you want to find
        public static Bitmap FindContour(Bitmap thresholdedBitmap, Bitmap targetBitmap, int contourThreshold = 500)
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
                    //Create a contour for the current variable to work with
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    //Draw the detected contour on the image
                    if (currentContour.Area > contourThreshold) //only consider contours with area greater than 100 as default then take from form control
                    {
                        target.Draw(currentContour.BoundingRectangle, new Bgr(Color.Red), 2);

                        //x and y coordinates for the middle of the contour
                        int x = currentContour.BoundingRectangle.X + (currentContour.BoundingRectangle.Width/2);
                        int y = currentContour.BoundingRectangle.Y + (currentContour.BoundingRectangle.Height/2);
                        target.Draw(new Cross2DF(new PointF(x, y), 10, 10), new Bgr(Color.Red), 1);
                    }
                }
            return target.ToBitmap();
        }

        public static Point FindContourMiddleCoordinates(Bitmap thresholdedBitmap, Bitmap targetBitmap, int contourThreshold = 500)
        {
            Image<Bgr, Byte> difference = new Image<Bgr, byte>(thresholdedBitmap);
            Image<Bgr, Byte> target = new Image<Bgr, byte>(targetBitmap);
            int x = 0, y = 0;

            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                //detect the contours and loop through each of them
                for (Contour<Point> contours = difference.Convert<Gray, Byte>().FindContours(
                      Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                      Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST,
                      storage);
                   contours != null;
                   contours = contours.HNext)
                {
                    //Create a contour for the current variable to work with
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    //Draw the detected contour on the image
                    if (currentContour.Area > contourThreshold) //only consider contours with area greater than 100 as default then take from form control
                    {
                        //x and y coordinates for the middle of the contour
                        x = currentContour.BoundingRectangle.X + (currentContour.BoundingRectangle.Width / 2);
                        y = currentContour.BoundingRectangle.Y + (currentContour.BoundingRectangle.Height / 2);
                    }
                }
            return new Point(x, y);
        }
    }
}
