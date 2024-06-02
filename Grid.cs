using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

//shoutout to https://www.redblobgames.com/grids/hexagons/
namespace Hexaplicate
{
    internal class Grid
    {
        private Hexagon[,] gridHexagons = new Hexagon[7,7];
        /// <summary>
        /// Initalize Grid with all empty hexagons.
        /// </summary>
        public Grid()
        {
            //Only sums greater than 2 or less than 10 map to valid hexagon spots.
            for(int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    if((i+j > 2) && (i+j < 10))
                    {
                        gridHexagons[i, j] = new EmptyHexagon();
                    }
                }
            }
        }

        /// <summary>
        /// Draws the grid on the given batch
        /// </summary>
        /// <param name="batch">SpriteBatch to draw on</param>
        public void Draw(SpriteBatch batch, (int,int) offset)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i + j > 2) && (i + j < 10))
                    {
                        (float, float) pixel = HexagonOperations.axialToPixel(i-3, j-3);
                        gridHexagons[i, j].Draw(batch, (int)(pixel.Item1*50f) + offset.Item1, 
                            (int)(pixel.Item2*50f) + offset.Item2);
                    }
                }
            }
        }
    }
}
