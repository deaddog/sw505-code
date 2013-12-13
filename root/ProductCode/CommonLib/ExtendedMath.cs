using System;
using System.Collections;


namespace CommonLib
{
    public static class ExtendedMath
    {
        //private const long INTEGRAL_DIVISIONS = long.MaxValue;
        private const long INTEGRAL_DIVISIONS = 100000;


        /// <summary>
        /// Calculates the definite integral of a real-valued function.
        /// </summary>
        /// <param name="f">the function</param>
        /// <param name="lower">the lower bound</param>
        /// <param name="upper">the upper bound</param>
        /// <returns>the definite integral of the function</returns>
        public static double DefIntegrate(Func<double, double> f, double lower, double upper)
        {
            return DefIntegrate(f, lower, upper, INTEGRAL_DIVISIONS);
        }
        
        /// <summary>
        /// Calculates the definite integral of a real-valued function.
        /// </summary>
        /// <param name="f">the function</param>
        /// <param name="lower">the lower bound</param>
        /// <param name="upper">the upper bound</param>
        /// <param name="pres">the precision</param>
        /// <returns>the definite integral of the function</returns>
        public static double DefIntegrate(Func<double, double> f, double lower, double upper, long pres)
        {
            if (lower > upper) throw new ApplicationException("lower bound must be smaller than the upper bound!");
            double deltaX = (upper - lower) / pres;
            double xi;
            double areaSum = 0;

            for (long i = 1; i <= pres; i++)
            {
                xi = lower + i * deltaX;
                areaSum += deltaX * f(xi);
            }
            return areaSum;
        }
    }
}
