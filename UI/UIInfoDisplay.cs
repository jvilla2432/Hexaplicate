using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Hexaplicate.UI
{
    internal static class UIInfoDisplay
    {

        public static Texture2D[] UITextures;
        public static SpriteFont font;


        public static void SetTexture(Texture2D[] texture)
        {
            UITextures = (Texture2D[])texture.Clone();
        }

        public static void CreateBox(SpriteBatch _spriteBatch,(HexagonContainer, (int, int)) displayingHexagon,
            (string, string)[] stats)
        {
            (int, int) offset = ((int)(Constants.SCREEN_SIZE.Item1 * Constants.INVENTORY_DISPLAY_OFFSET.Item1),
                (int)(Constants.SCREEN_SIZE.Item2 * Constants.INVENTORY_DISPLAY_OFFSET.Item2));
            _spriteBatch.Draw(UITextures[0], new Rectangle(offset.Item1,offset.Item2,150,300), Color.White);
            _spriteBatch.DrawString(font, "Statistics", new Vector2(offset.Item1, offset.Item2), Color.White);
            Hexagon hex = displayingHexagon.Item1.getHexagon(displayingHexagon.Item2);
            hex.Draw(_spriteBatch, offset.Item1, offset.Item2 + 50);
            if(stats.Length > 5)
            {
                throw new Exception("Too many items in stats list");
            }
            for(int i = 0; i < stats.Length; i++)
            {
                (string, string) stat = stats[i];
                _spriteBatch.DrawString(font, stat.Item1 + " :" + stat.Item2,
                    new Vector2(offset.Item1, offset.Item2 + 100 + i*50), Color.White);
            }
        }
    }
}
