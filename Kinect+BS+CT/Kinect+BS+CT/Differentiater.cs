using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_BS_CT
{
    public abstract class Differentiater
    {
        public event EventHandler UpdatePicture1, UpdatePicture2, UpdatePicture3;

        public abstract void Start(Bitmap bitmap);

        private Image p1, p2, p3;
        public Image Picture1
        {
            get { return p1; }
            set
            {
                p1 = value;
                if (UpdatePicture1 != null)
                    UpdatePicture1(this, EventArgs.Empty);
            }
        }
        public Image Picture2
        {
            get { return p2; }
            set
            {
                p2 = value;
                if (UpdatePicture2 != null)
                    UpdatePicture2(this, EventArgs.Empty);
            }
        }
        public Image Picture3
        {
            get { return p3; }
            set
            {
                p3 = value;
                if (UpdatePicture3 != null)
                    UpdatePicture3(this, EventArgs.Empty);
            }
        }
    }
}
