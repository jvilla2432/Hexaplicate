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
                        gridHexagons[i, j] = new EssenceHexagon();
                    }
                }
            }
        }

        /// <summary>
        /// Places newHex into coordinates and returns the Hexagon at coordinates 
        /// </summary>
        /// <param name="newHex">Hexagon to place into grid</param>
        /// <param name="coordinates">Axial coordinates of hexagon to replace</param>
        /// <returns>Hexagon that was at coordinates</returns>
        public Hexagon swapHexagon(Hexagon newHex, (int,int) coordinates)
        {
            Hexagon oldHex = gridHexagons[coordinates.Item1, coordinates.Item2];
            gridHexagons[coordinates.Item1, coordinates.Item2] = newHex; 
            return oldHex;
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
                        (float, float) pixel = HexagonOperations.AxialToPixel(i-3, j-3);
                        gridHexagons[i, j].Draw(batch, (int)(pixel.Item1*50f) + offset.Item1, 
                            (int)(pixel.Item2*50f) + offset.Item2);
                    }
                }
            }
        }

        public void RegisterHexs(UIManager manager, (int, int) offset)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i + j > 2) && (i + j < 10))
                    {
                        int localI = i;
                        int localJ = j;
                        (float, float) pixel = HexagonOperations.AxialToPixel(i - 3, j - 3);
                        (int, int) pixelInt = ((int)(pixel.Item1*50f) + offset.Item1 + (int)(Constants.HEXAGON_SCALE/2f * Constants.HEXAGON_IMG_SIZE.Item1)
                            , (int)(pixel.Item2*50f) + offset.Item2 + (int)(Constants.HEXAGON_SCALE/2f * Constants.HEXAGON_IMG_SIZE.Item2));
                        void clickFunction() {
                            gridHexagons[localI, localJ] = new EmptyHexagon();
                        }
                        manager.registerClick(clickFunction, 
                            HexagonOperations.HexagonHitBox(Constants.HEXAGON_SIZE/2, pixelInt));
                    }
                }
            }

        }
    }
}
