using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectFromColor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Image input = Bitmap.FromFile("snapshot.jpg");
            src.Image = input;

            output.Image = ColorTracking.TrackColor(input as Bitmap);
        }
    }
}
