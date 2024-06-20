using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal class Essence1 : Essence
    {
        void Essence.performEffect(Grid grid, (int, int) hexCoordinate, float deltaT, float strength) { 

            if(grid.getHexagon(hexCoordinate) is EssenceHexagon)
            {
                EssenceHexagon hex = (EssenceHexagon)grid.getHexagon(hexCoordinate);
            }
        }
    }
}
