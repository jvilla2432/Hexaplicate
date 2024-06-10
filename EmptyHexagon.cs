using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal class EmptyHexagon : Hexagon
    {
        private static Texture2D[] hexagonTexture;
        public static void SetTexture(Texture2D[] texture)
        {
            hexagonTexture = texture;
        }
        void Hexagon.Draw(SpriteBatch batch, int xOffset, int yOffset)
        {
            if (hexagonTexture[0] == null)
            {
                throw new InvalidOperationException("Image has not been loaded for this hexagon");
            }
            batch.Draw(hexagonTexture[0], new Rectangle(xOffset, yOffset,
                Constants.ADJUSTED_HEX_SIZE.Item1, Constants.ADJUSTED_HEX_SIZE.Item2), Color.White);
        }

    }
}
