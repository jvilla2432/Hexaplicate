using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    public enum EssenceType
    {
        Essence1
    }

    public enum ResourceType
    {
        Resource1
    }

    internal static class Constants
    {
        internal static readonly int NUM_RESOURCES = 1;
        internal static readonly string[] RESOURCE_NAMES = { " Mana " };
        // Type : 0 is resource, 1 is essence
        internal static Recipe[] recipes;
        internal static readonly int NUM_ESSENCES = 1;
        internal static readonly string[] ESSENCE_NAMES = { " Essence " };
        internal static readonly int MAX_ESSENCE = 100;
        internal static readonly int HEXAGON_RADIUS = 25;
        internal static readonly int HEXAGON_SIZE = HEXAGON_RADIUS * 2;
        internal static readonly float HEXAGON_GAP = 1;
        internal static readonly (int,int) HEXAGON_IMG_SIZE = (612,530);
        internal static readonly float HEXAGON_SCALE = (float)HEXAGON_SIZE / (float)HEXAGON_IMG_SIZE.Item1;
        internal static readonly (int, int) ADJUSTED_HEX_SIZE = ((int)(HEXAGON_IMG_SIZE.Item1 * HEXAGON_SCALE),
            (int)(HEXAGON_IMG_SIZE.Item2 * HEXAGON_SCALE));

        internal static readonly Essence[] essences = { new Essence1() };
        internal static readonly (int, int) SCREEN_SIZE = (1280, 720);
        internal static readonly (float, float) GRID_OFFSET = (0.3f, 0.5f);
        internal static readonly (float, float) INVENTORY_GRID_OFFSET = (0.8f, 0.1f);
        internal static readonly (float, float) INVENTORY_DISPLAY_OFFSET = (0.8f, 0.5f);

    }
}
