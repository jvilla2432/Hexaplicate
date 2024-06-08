using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Hexaplicate
{
    internal interface Hexagon
    {
        /// <summary>
        /// Draws the grid on the given screen
        /// </summary>
        /// <param name="batch">SpriteBatch to draw on</param>
        public void Draw(SpriteBatch batch, int xOffset, int yOffset);

        
    }
}
