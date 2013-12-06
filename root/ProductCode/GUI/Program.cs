using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemInterface.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (CommonLib.ConnectionSettings.Default.NXTPort == 0)
            {
                int port = -1;
                while (port <= 0 || port > 255)
                    int.TryParse(Microsoft.VisualBasic.Interaction.InputBox(
                        "Select the port number you would like to use for the NXT:", "NXT Port"), out port);

                CommonLib.ConnectionSettings.Default.NXTPort = (byte)port;
                CommonLib.ConnectionSettings.Default.Save();
            }

            bool go = true;
#if !LOWTECH
            using (SelectColorsForm colorForm = new SelectColorsForm())
            {
                if (colorForm.ShowDialog() == DialogResult.OK)
                {
                    Control.LocationControl.Instance.FrontColor = colorForm.Color1;
                    Control.LocationControl.Instance.RearColor = colorForm.Color2;
                }
                else
                    go = false;
            }
#endif

            if (go)
            {
#if !LOWTECH
                Control.LocationControl.Instance.SetActualSize(308, 231);
                Control.LocationControl.Instance.SetImageSize(640, 480);
                RobotInterface.RobotCommandInterpreter interpreter = new RobotInterface.RobotCommandInterpreter();
#endif
                OccupancyGridForm form = new OccupancyGridForm(new Data.OccupancyGrid(30, 23, 10, -150, -115));
                Application.Run(form);
            }
        }
    }
}
