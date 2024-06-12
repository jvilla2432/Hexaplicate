using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal static class HexagonOperations
    {
        public static readonly (float, float) qBasis = (3f / 2f * Constants.HEXAGON_RADIUS,
            (float)Math.Sqrt(3) / 2 * Constants.HEXAGON_RADIUS);
        public static readonly (float, float) rBasis = (0, (float)Math.Sqrt(3) * Constants.HEXAGON_RADIUS);
        public static readonly HashSet<(int, int)> axialVectors = new HashSet<(int, int)> { (1, 0), (1, -1), (0, -1), (-1, 0), (-1, 1), (0, 1) };
        public static readonly Dictionary<(int, int), float> axialVectors2 =
            new Dictionary<(int, int), float> { { (1, 0), (float)(2*Math.PI/3)}, { (1, -1), (float)Math.PI/3 }, { (0, -1), 0 },
                { (-1, 0), (float)(5*Math.PI/3) }, { (-1, 1), (float)(4*Math.PI/3) }, { (0, 1), (float)Math.PI } };

        /// <summary>
        /// Takes in axial hexagon coordinates and returns pixel locations
        /// </summary>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static (float,float) AxialToPixel(float q, float r)
        {
            return (qBasis.Item1 * q + rBasis.Item1*r,
                qBasis.Item2 * q + rBasis.Item2*r);
        }

        public static bool CheckAdjancency((int,int) coord1, (int,int) coord2)
        {
            (int, int) vector = (coord1.Item1 - coord2.Item1, coord1.Item2 - coord2.Item2);
            if (axialVectors.Contains(vector))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns a function to check if a point is inside of a hexagon of size hexSize centered at offset.x,offset.y
        /// </summary>
        /// <param name="hexSize">Size of hexagon to check</param>
        /// <param name="offset">Center of hexagon to check</param>
        /// <returns>Function that checks x,y point</returns>
        public static Func<int, int, bool> HexagonHitBox(int hexSize, (int,int) offset)
        {
            // See https://www.codeproject.com/Tips/84226/Is-a-Point-inside-a-Polygon 
            return (int x, int y) =>
            {
                //Debug.WriteLine("test 4");
                x = x - offset.Item1;
                y = y - offset.Item2;

                double[] vertx = { 0.5d * hexSize, 1d * hexSize, 0.5d * hexSize, 
                    -0.5d * hexSize, -1d * hexSize, -0.5 * hexSize};
                double[] verty = { Math.Sqrt(3)/2d * hexSize, 0, -Math.Sqrt(3)/2d * hexSize, 
                    -Math.Sqrt(3)/2d * hexSize, 0, Math.Sqrt(3)/2d * hexSize};
                int nvert = 6;
                int i, j = 0;
                bool c = false;
                for (i = 0, j = nvert - 1; i < nvert; j = i++)
                {
                    if (((verty[i] > y) != (verty[j] > y)) &&
                     (x < (vertx[j] - vertx[i]) * (y - verty[i]) / (verty[j] - verty[i]) + vertx[i]))
                        c = !c;
                }
                return c;
            };

        }

        internal static (int, int) FractalHex((int, int) diff)
        {
            return (3 + (diff.Item1 * -3), 3 + (diff.Item2 * -3));
        }
    }
}
