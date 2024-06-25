using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		  

		public int getEssence(EssenceType essenceNumber)
        {
			return essences[(int)essenceNumber];
        }

		public int getResource(ResourceType resourceNumber)
		{
			return resources[(int)resourceNumber];
		}


		/// <summary>
		/// Attempts to execute the recipe.
		/// </summary>
		/// <param name="recipeID">Recipe to attempt</param>
		/// <returns>True and mutates hexagon if sufficient resources, false and doesn't mutate if insufficient resources</returns>
		public bool attemptRecipe(int recipeID)
        {
			Recipe recipe = Constants.recipes[recipeID];
			foreach(var (resource, cost, type) in recipe.Inputs)
            {
				int amount = type ? getEssence((EssenceType)resource) : getResource((ResourceType)resource);
				if (amount < cost)
				{
					return false;
				}
			}
			foreach (var (resource, cost, type) in recipe.Inputs)
			{
				_ = type ? AddEssence((EssenceType)resource, -cost) : AddResource((ResourceType)resource, -cost);
			}

			foreach (var (resource,cost, type) in recipe.Outputs)
            {
				_ = type ? AddEssence((EssenceType)resource, cost) : AddResource((ResourceType)resource, cost);
            }
			return true;
		}


		public EssenceHexagon()
        {
			for (int i = 0; i < Constants.NUM_RESOURCES; i++)
			{
				resources[i] = 0;
			}
			for (int i = 0; i < Constants.NUM_ESSENCES; i++)
			{
				essences[i] = 0;
			}

		}

		/// <summary>
		/// Attempts to add essenceAmount of essence to this hexagon. Returns true if sucessfully added and mutates Hexagon,
		/// returns false is fails.
		/// </summary>
		internal bool AddEssence(EssenceType essence, int essenceAmount)
		{
			int totalEssenceValue = essences.Aggregate((total, current) => total + current);
			int expected = essenceAmount + totalEssenceValue;
			if (Constants.MAX_ESSENCE < expected || expected < 0)
			{
				return false;
			}
			essences[(int)essence] += essenceAmount;
			return true;
		}

		public bool AddResource(ResourceType resource, int resourceAmount)
        {
			if(resources[(int)resource] + resourceAmount < 0)
            {
				return false;
            }
			resources[(int)resource] = resources[(int)resource] + resourceAmount;
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
