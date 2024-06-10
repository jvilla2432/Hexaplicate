﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
	internal class EssenceHexagon : Hexagon
	{
		private int[] resources = new int[Constants.NUM_RESOURCES];
		private int[] essences = new int[Constants.NUM_ESSENCES];
		private Tuple<Hexagon, Boolean>[] neighbors = new Tuple<Hexagon, Boolean>[6];
		private static Texture2D[] hexagonTexture;


		/// <summary>
		/// Attempts to add essenceAmount of essence to this hexagon. Returns true if sucessfully added and mutates Hexagon,
		/// returns false is fails.
		/// </summary>
		internal Boolean AddEssence(EssenceType essence, int essenceAmount)
		{
			int totalEssenceValue = essences.Aggregate((total, current) => total + current);
			if (Constants.MAX_ESSENCE > essenceAmount + totalEssenceValue)
			{

				return false;
			}
			essences[(int)essence] += essenceAmount;
			return true;
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
	}
}
