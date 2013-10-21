using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXTRobot
{
    public class UltraSonicData : IEnumerable<NXTRobot.UltraSonicData.SensorDegrees>
    {
        private static SensorDegrees[] data;
        private readonly int degreesInArray = 360;

        public UltraSonicData()
        {
            data = InitializeArray<SensorDegrees>(degreesInArray);
            InitializeValues();
        }

        public class SensorDegrees
        {
            private int degree;
            public int Degree
            {
                get { return degree; }
                set { degree = value; }
            }
            
            public byte Value { get; set; }
            public bool Inferred { get; set; }
        }

        public List<byte> ToList()
        {
            List<byte> temp = new List<byte>();
            foreach (var item in data)
            {
                temp.Add(item.Value);
            }
            return temp;
        }

        #region Initialization of array and objects
        private void InitializeValues()
        {
            int degree = 1;
            foreach (SensorDegrees item in data)
            {
                item.Degree = degree;
                item.Value = 0;
                item.Inferred = true;
                degree++;
            }
        }

        T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }
            return array;
        }
        #endregion

        #region Get and set sensor values
        public byte GetByteValueAtDegree(int degree)
        {
            if (degree > 0 && degree <= 360)
            {
                return data[degree - 1].Value;
            }
            else
            {
                throw new IndexOutOfRangeException("Value has to be between 1 and 360 degrees");
            }
        }

        public bool SetByteValueAtDegree(int degree, byte value)
        {
            if (degree > 0 && degree <= 360)
            {
                data[degree - 1].Value = value;
                data[degree - 1].Inferred = false;
            }
            else
            {
                return false;
            }
            return true;
        }

        public void NormalizeValues()
        {
            int lastNonInferredDegree = 1;
            foreach (var item in data)
            {
                if (item.Inferred == true)
                {
                    item.Value = GetByteValueAtDegree(lastNonInferredDegree);
                }
                else
                {
                    lastNonInferredDegree = item.Degree;
                }
            }
        }

        public void ClearValues()
        {
            foreach (var reading in data)
            {
                reading.Value = 0;
                reading.Inferred = true;
            }
        }
        #endregion

        #region IEnumerator<SensorDegrees>
        public IEnumerator<SensorDegrees> GetEnumerator()
        {
            for (int i = 0; i < degreesInArray; i++)
            {
                yield return data[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
