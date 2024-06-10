using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


namespace Hexaplicate
{
    internal interface HexagonContainer
    {
        public Hexagon getHexagon((int, int) coords);
        public void setHexagon((int, int) coords, Hexagon hex);

    }
}
