using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal class Hexagon
    {
        private int[] resources = new int[Constants.NUM_RESOURCES];
        private int[] essences = new int[Constants.NUM_ESSENCES];
        private Tuple<Hexagon, Boolean>[] neighbors = new Tuple<Hexagon, Boolean>[6];

		/// <summary>
		/// Attempts to add essenceAmount of essence to this hexagon. Returns true if sucessfully added and mutates Hexagon,
		/// returns false is fails.
		/// </summary>
		internal Boolean AddEssence(EssenceType essence, int essenceAmount)
		{
			int totalEssenceValue = essences.Aggregate((total, current) => total + current);
			if (Constants.MAX_ESSENCE > essenceAmount + totalEssenceValue) {
			
				return false;
			}
			essences[(int)essence] += essenceAmount;
			return true;
		}

	}
}
