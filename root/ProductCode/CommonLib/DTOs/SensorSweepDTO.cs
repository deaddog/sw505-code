using System;
using System.Collections.Generic;
using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    public struct SensorSweepDTO : IEnumerable<ISensorData>
    {
        private ISensorData[] data;
        
        public SensorSweepDTO(ISensorData[] data) {
            this.data = data;
        }

        #region IEnumerable Interface

        public IEnumerator<ISensorData> GetEnumerator()
        {
            for(uint i=0;i<data.Length;i++) {
                
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
