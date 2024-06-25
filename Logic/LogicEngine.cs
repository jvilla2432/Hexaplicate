using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate.Logic
{
    internal class LogicEngine
    {
        //
        int timeSinceTick = 0;
        public void RunEssence(int deltaT)
        {
            timeSinceTick += deltaT;
            if(timeSinceTick > 1000)
            {
                timeSinceTick -= 1000;
                List<List<(Grid, (int, int))>> depthList = new();
                Grid.BFS(depthList);
                foreach (List<(Grid, (int, int))> depth in depthList)
                {
                    foreach ((Grid, (int, int)) hex in depth)
                    {
                        Hexagon baseHex = hex.Item1.getHexagon(hex.Item2);
                        if (baseHex is EssenceHexagon)
                        {
                            EssenceHexagon essHex = (EssenceHexagon)baseHex;
                            for (EssenceType essType = 0; (int)essType < Constants.NUM_ESSENCES; essType++)
                            {
                                Essence ess = Essence.essences[(int)essType];
                                ess.performEffect(hex, essHex.getEssence(essType));
                            }
                            continue;
                        }
                        if(baseHex is RecipeHexagon)
                        {
                            RecipeHexagon recHex = (RecipeHexagon)baseHex;
                            foreach(var neighbor in hex.Item1.getNeighbors(hex.Item2))
                            {
                                Hexagon adj = Grid.getHexagon(neighbor);
                                if(adj is EssenceHexagon)
                                {
                                    recHex.ExecuteRecipe((EssenceHexagon)adj);
                                }
                            }
                        }
                    }
                }
            }
        } 
    }
}
