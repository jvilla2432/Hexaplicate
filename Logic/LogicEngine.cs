﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate.Logic
{
    internal class LogicEngine
    {
        //
        public void RunEssence(float deltaT)
        {
            List<List<(Grid, (int, int))>> depthList = new();
            Grid.BFS(depthList);
            foreach(List<(Grid, (int, int))> depth in depthList)
            {
                foreach((Grid,(int,int)) hex in depth)
                {
                    EssenceHexagon essHex = (EssenceHexagon)hex.Item1.getHexagon(hex.Item2);
                    for(EssenceType essType = 0; (int)essType < Constants.NUM_ESSENCES; essType++)
                    {
                        Essence ess = Essence.essences[(int)essType];
                        ess.performEffect(hex.Item1, hex.Item2, deltaT, essHex.getEssence(essType));
                    }
                }
            }
        } 
    }
}
