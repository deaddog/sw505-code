using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Microsoft.Kinect;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;

/*
 * http://msdn.microsoft.com/en-us/library/hh973078.aspx#floor_determination
 * http://en.wikipedia.org/wiki/Plane_(geometry)
 */

namespace KinectWPFOpenCV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor sensor;
        WriteableBitmap depthBitmap;
        WriteableBitmap colorBitmap;
        WriteableBitmap planeBitmap;
        Skeleton[] skeletonData;
        DepthImagePixel[] depthPixels;
        Image<Gray, Byte> gray_bg;
        List<PointF> points;
        Tuple<float, float, float, float> plane;
        bool work = false;
        bool auto = false;
        bool bg = false;
        byte[] colorPixels;

        int blobCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            this.MouseDown += MainWindow_MouseDown;
            points = new List<PointF>();
            points.Add(new PointF(0, 0));
        }


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }


            if (null != this.sensor)
            {

                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                this.sensor.SkeletonStream.Enable();
                skeletonData = new Skeleton[this.sensor.SkeletonStream.FrameSkeletonArrayLength];
                this.sensor.SkeletonFrameReady += sensor_SkeletonFrameReady;
                this.colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];
                this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];

                this.colorBitmap = new WriteableBitmap(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                this.depthBitmap = new WriteableBitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                this.planeBitmap = new WriteableBitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96, 96, PixelFormats.Pbgra32, null);

                this.colorImg.Source = this.colorBitmap;

                this.sensor.AllFramesReady += this.sensor_AllFramesReady;

                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                this.outputViewbox.Visibility = System.Windows.Visibility.Collapsed;
                this.txtError.Visibility = System.Windows.Visibility.Visible;
                this.txtInfo.Text = "No Kinect Found";

            }

        }

        void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame s = e.OpenSkeletonFrame())
            {
                if (s != null)
                {
                    plane = s.FloorClipPlane;
                    //planeBitmap = BitmapFactory.ConvertToPbgra32Format(colorBitmap);
                    //planeBitmap.DrawLine(0, (int)(-plane.Item4 / plane.Item1), (int)(-plane.Item4 / plane.Item2), 0, 100);
                    //planeBitmap.DrawRectangle(0, 0, 100, 100, Colors.Red);
                }
            }
        }

        float calculate_point_in_plane(PointF p)
        {
            return (plane.Item1 * p.X + plane.Item2 * p.Y + plane.Item4) / -plane.Item3;
        }

        private void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            if (bg == false)
            {
                using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
                {
                    if (depthFrame != null)
                    {
                        BitmapSource bgBmp = depthFrame.SliceDepthImage(0, 3000);
                        Image<Bgr, Byte> bgOpenCV = new Image<Bgr, byte>(bgBmp.ToBitmap());
                        gray_bg = bgOpenCV.Convert<Gray, byte>();
                        bg = true;
                    }
                }
            }

            if (work == true)
            {
                BitmapSource depthBmp = null;
                blobCount = 0;

                using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
                {
                    using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
                    {

                        if (depthFrame != null)
                        {

                            blobCount = 0;

                            List<PointF> temp = new List<PointF>();

                            depthBmp = depthFrame.SliceDepthImage((int)sliderMin.Value, (int)sliderMax.Value);

                            Image<Bgr, Byte> openCVImg = new Image<Bgr, byte>(depthBmp.ToBitmap());
                            Image<Gray, byte> gray_image = openCVImg.Convert<Gray, byte>();

                            gray_image=  gray_image.AbsDiff(gray_bg);
                            using (MemStorage stor = new MemStorage())
                            {
                                //Find contours with no holes try CV_RETR_EXTERNAL to find holes
                                Contour<System.Drawing.Point> contours = gray_image.FindContours(
                                 Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                                 Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL,
                                 stor);

                                for (int i = 0; contours != null; contours = contours.HNext)
                                {
                                    i++;

                                    if ((contours.Area > Math.Pow(sliderMinSize.Value, 2)) && (contours.Area < Math.Pow(sliderMaxSize.Value, 2)))
                                    {
                                        MCvBox2D box = contours.GetMinAreaRect();
                                        openCVImg.Draw(box, new Bgr(System.Drawing.Color.Red), 2);
                                        blobCount++;
                                        temp.Add(box.center);
                                        openCVImg.Draw(new CircleF(box.center, 1), new Bgr(System.Drawing.Color.Red), 2);
                                        MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 2, 2);
                                        //openCVImg.Draw(calculate_point_in_plane(box.center).ToString(), ref f, new System.Drawing.Point(10, 50), new Bgr(System.Drawing.Color.MediumSpringGreen));
                                        openCVImg.Draw(plane.Item1.ToString(), ref f, new System.Drawing.Point(10, 100), new Bgr(System.Drawing.Color.MediumSpringGreen));
                                        openCVImg.Draw(plane.Item2.ToString(), ref f, new System.Drawing.Point(10, 150), new Bgr(System.Drawing.Color.MediumSpringGreen));
                                        openCVImg.Draw(plane.Item3.ToString(), ref f, new System.Drawing.Point(10, 200), new Bgr(System.Drawing.Color.MediumSpringGreen));
                                        openCVImg.Draw(plane.Item4.ToString(), ref f, new System.Drawing.Point(10, 250), new Bgr(System.Drawing.Color.MediumSpringGreen));
                                    }
                                }
                                points.Add(FindClosest(points.Last(), temp));
                            }

                            this.outImg.Source = ImageHelpers.ToBitmapSource(openCVImg);
                            this.bgImg.Source = ImageHelpers.ToBitmapSource(gray_bg);
                            this.origGray.Source = ImageHelpers.ToBitmapSource(gray_image);

                            txtBlobCount.Text = blobCount.ToString();
                        }
                    }


                    if (colorFrame != null)
                    {

                        colorFrame.CopyPixelDataTo(this.colorPixels);
                        this.colorBitmap.WritePixels(
                            new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                            this.colorPixels,
                            this.colorBitmap.PixelWidth * sizeof(int),
                            0);

                    }
                    work = false;
                }
            }
        }

        private PointF FindClosest(PointF p, List<PointF> points)
        {
            float dist = float.MaxValue;
            PointF closestPoint = new PointF();
            foreach (var point in points)
            {
                float v = (point.X - p.X) + (point.Y - p.Y);
                if (v < dist)
                {
                    dist = v;
                    closestPoint = point;
                }
            }
            return closestPoint;
        }


        #region Window Stuff
        void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        private void CloseBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            work = !work;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            work = true;
        }

        private void toggleRefresh_Click(object sender, RoutedEventArgs e)
        {
            bg = !bg;
        }
    }
}
