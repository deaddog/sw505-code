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

            bool go = true;
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

            if (go)
            {
                RobotInterface.RobotCommandInterpreter interpreter = new RobotInterface.RobotCommandInterpreter();
                Application.Run(new OccupancyGridForm(new Data.OccupancyGrid(30, 23, 10, -150, -115)));
            }
        }
    }
}
