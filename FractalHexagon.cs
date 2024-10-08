﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Hexaplicate
{
    [DataContract]
    internal class FractalHexagon : Hexagon
    {
        [DataMember]
        public Grid grid;
        private static Texture2D[] hexagonTexture;

        public FractalHexagon()
        {
            grid = new Grid();
        }

        public static void SetTexture(Texture2D[] texture)
        {
            hexagonTexture = (Texture2D[])texture.Clone();
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

        internal void switchGrid(Grid parent)
        {
            grid.setParent(parent);
            Game1.game.SetGrid(grid);
        }
    }
}
