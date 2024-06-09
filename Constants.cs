using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal static class Constants
    {
        internal static readonly int NUM_RESOURCES = 1;
        internal static readonly int NUM_ESSENCES = 1;
        internal static readonly int MAX_ESSENCE = 100;
        internal static readonly int HEXAGON_SIZE = 50;
        internal static readonly float HEXAGON_GAP = 50f;
        internal static readonly (int,int) HEXAGON_IMG_SIZE = (612,530);
        internal static readonly float HEXAGON_SCALE = (float)HEXAGON_SIZE / (float)HEXAGON_IMG_SIZE.Item1;

        internal static readonly Essence[] essences = { new Essence1() };
    }
}
