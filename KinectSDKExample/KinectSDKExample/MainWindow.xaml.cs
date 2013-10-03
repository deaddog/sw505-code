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
using Microsoft.Kinect;

namespace KinectSDKExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        KinectSensor sensor;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (KinectSensor.KinectSensors.Count > 0)
            {
                this.sensor = KinectSensor.KinectSensors[0];
                StartSensor();
                this.sensor.ColorStream.Enable();
                this.sensor.DepthStream.Enable();
                this.sensor.SkeletonStream.Enable();
                this.sensor.ColorFrameReady += sensor_ColorFrameReady;
            }

            else
            {
                MessageBox.Show("No Kinect connected");
                this.Close();
            }

        }

        private void StartSensor()
        {
            if (this.sensor != null && !this.sensor.IsRunning)
            {
                this.sensor.Start();
            }
        }


        byte[] pixelData = null;
        void sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame imageFrame = e.OpenColorImageFrame())
            {
                if (imageFrame == null)
                    return;

                this.pixelData = new byte[imageFrame.PixelDataLength];

                imageFrame.CopyPixelDataTo(this.pixelData);

                int stride = imageFrame.Width * imageFrame.BytesPerPixel;

                this.KinectImage.Source = BitmapSource.Create(
                imageFrame.Width, imageFrame.Height, 96, 96, PixelFormats.Bgr32,
                null, pixelData, stride);
            }
        }    
    }
}
