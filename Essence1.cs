using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal class Essence1 : Essence
    {
        void Essence.performEffect( (Grid,(int, int)) hex, int strength) {
            Hexagon hexa = Grid.getHexagon(hex);
            if(hexa is EssenceHexagon)
            {
                EssenceHexagon essHex = (EssenceHexagon)hexa;
                essHex.AddResource(ResourceType.Resource1, strength);
            }
        }
    }
}
