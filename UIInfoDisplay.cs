using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Hexaplicate
{
    internal static class UIInfoDisplay
    {

        public static Texture2D[] UITextures;

        public static void SetTexture(Texture2D[] texture)
        {
            UITextures = (Texture2D[])texture.Clone();
        }

        public static void CreateBox((int,int) offset, SpriteBatch _spriteBatch
            )
        {
            _spriteBatch.Draw(UITextures[0], new Rectangle(offset.Item1,offset.Item2,30,30), Color.White);
        }
    }
}
