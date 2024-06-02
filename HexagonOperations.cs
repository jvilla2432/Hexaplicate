using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal static class HexagonOperations
    {
        public static readonly (float,float) qBasis = (3f/2f, (float)Math.Sqrt(3)/2);
        public static readonly (float, float) rBasis = (0, (float)Math.Sqrt(3));

        public static (float,float) axialToPixel(int q, int r)
        {
            return (qBasis.Item1 * q + rBasis.Item1*r, qBasis.Item2 * q + rBasis.Item2*r);
        }
    }
}
