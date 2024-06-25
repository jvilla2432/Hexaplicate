using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal class Inventory : HexagonContainer
    {
        static int INVENTORY_GAP_X = 50;
        static int INVENTORY_GAP_Y = 50;
        private Hexagon[,] inventoryHexagons = new Hexagon[4, 3];
        private (int, int) coordinates = (0, 0);

        public Hexagon getHexagon((int, int) coords)
        {
            return inventoryHexagons[coords.Item1, coords.Item2];
        }
        public void setHexagon((int, int) coords, Hexagon hex)
        {
            inventoryHexagons[coords.Item1, coords.Item2] = hex;
        }

        public IEnumerable<(int, int)> returnHexagonPairs()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    yield return (i, j);
                }
            }
        }

        public Inventory()
        {
            foreach (var pair in returnHexagonPairs())
            {
                int i = pair.Item1;
                int j = pair.Item2;
                EssenceHexagon essHex = new EssenceHexagon();
                essHex.AddEssence(EssenceType.Essence1, 1);
                inventoryHexagons[i, j] = essHex;
                if (i == 0 && j == 0)
                {
                    RecipeHexagon recHex = new RecipeHexagon();
                    inventoryHexagons[i, j] = recHex;
                }
                if(i == 0 && j == 1){
                    FractalHexagon fracHex = new FractalHexagon();
                    inventoryHexagons[i, j] = fracHex;
                }
            }
        }

        public void setCoordinates((int, int) coords)
        {
            coordinates = coords;
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var pair in returnHexagonPairs())
            {
                int i = pair.Item1;
                int j = pair.Item2;
                (int, int) pixel = (i * INVENTORY_GAP_X, j * INVENTORY_GAP_Y);
                inventoryHexagons[i, j].Draw(batch, (int)(pixel.Item1) + coordinates.Item1,
                    (int)(pixel.Item2) + coordinates.Item2);
            }
        }

        public void RegisterHexs(Hexaplicate.UI.UIManager manager)
        {
            foreach (var pair in returnHexagonPairs())
            {
                int i = pair.Item1;
                int j = pair.Item2;
                int localI = i;
                int localJ = j;
                (int, int) pixel = (i * INVENTORY_GAP_X, j * INVENTORY_GAP_Y);
                //TODO: Simplify the following code. Currently has to adjust for drawing being top right, but 
                // hitbox being central
                (int, int) pixelInt = ((int)(pixel.Item1) + coordinates.Item1 + (int)(Constants.HEXAGON_SCALE / 2f * Constants.HEXAGON_IMG_SIZE.Item1)
                    , (int)(pixel.Item2) + coordinates.Item2 + (int)(Constants.HEXAGON_SCALE / 2f * Constants.HEXAGON_IMG_SIZE.Item2));
                (HexagonContainer, (int,int)) clickFunction()
                {
                    return (this, (localI,localJ));
                }
                manager.registerInvClick(clickFunction,
                    HexagonOperations.HexagonHitBox(Constants.HEXAGON_SIZE / 2, pixelInt));
            }
        }
    }
}
