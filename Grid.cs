using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

//shoutout to https://www.redblobgames.com/grids/hexagons/
namespace Hexaplicate
{
    internal class Grid : HexagonContainer
    {
        private Hexagon[,] gridHexagons = new Hexagon[7,7];
        private (int, int) coordinates = (0, 0);


        public Hexagon getHexagon((int, int) coords)
        {
            return gridHexagons[coords.Item1, coords.Item2];
        }
        public void setHexagon((int, int) coords, Hexagon hex)
        {
            gridHexagons[coords.Item1, coords.Item2] = hex;
        }

        public IEnumerable<(int,int)> returnHexagonPairs()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i + j > 2) && (i + j < 10))
                    {
                        yield return (i, j);
                    }
                }
            }
        }
        /// <summary>
        /// Initalize Grid with all empty hexagons.
        /// </summary>
        public Grid()
        {
            //Only sums greater than 2 or less than 10 map to valid hexagon spots.
            foreach (var pair in returnHexagonPairs())
            {
                gridHexagons[pair.Item1,pair.Item2] = new EmptyHexagon();

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

        public void setCoordinates( (int,int) coords)
        {
            coordinates = coords;
        }

        /// <summary>
        /// Draws the grid on the given batch
        /// </summary>
        /// <param name="batch">SpriteBatch to draw on</param>
        public void Draw(SpriteBatch batch)
        {
            foreach (var pair in returnHexagonPairs())
            {
                int i = pair.Item1;
                int j = pair.Item2;
                (float, float) pixel = HexagonOperations.AxialToPixel(i - 3, j - 3);
                gridHexagons[i, j].Draw(batch, (int)(pixel.Item1 * Constants.HEXAGON_GAP) + coordinates.Item1,
                    (int)(pixel.Item2 * Constants.HEXAGON_GAP) + coordinates.Item2);
            }
        }

        public void RegisterHexs(UIManager manager)
        {
            foreach (var pair in returnHexagonPairs())
            {
                int i = pair.Item1;
                int j = pair.Item2;
                int localI = i;
                int localJ = j;
                (float, float) pixel = HexagonOperations.AxialToPixel(i - 3, j - 3);
                //TODO: Simplify the following code. Currently has to adjust for drawing being top right, but 
                // hitbox being central
                (int, int) pixelInt = ((int)(pixel.Item1* Constants.HEXAGON_GAP) + coordinates.Item1 + (int)(Constants.HEXAGON_SCALE/2f * Constants.HEXAGON_IMG_SIZE.Item1)
                    , (int)(pixel.Item2* Constants.HEXAGON_GAP) + coordinates.Item2 + (int)(Constants.HEXAGON_SCALE/2f * Constants.HEXAGON_IMG_SIZE.Item2));
                (HexagonContainer, (int,int)) clickFunction() {
                    return (this, (localI, localJ));
                }
                manager.registerClick(clickFunction, 
                    HexagonOperations.HexagonHitBox(Constants.HEXAGON_SIZE/2, pixelInt));  
            }
        }
    }
}
