using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate.UI
{
    internal class UIDisplayingState : UIManagerState
    {
        private (HexagonContainer, (int, int)) displayingHexagon;
        private List<(string,string)> statsList = new();
        private int maxScreens;
        private int currentScreen = 0;

        public UIDisplayingState setDisplay((HexagonContainer, (int, int)) display)
        {
            displayingHexagon = display;
            Hexagon hex = display.Item1.getHexagon(display.Item2);
            if (hex is EssenceHexagon)
            {
                EssenceHexagon essHex = (EssenceHexagon)hex;
                for (EssenceType i = 0; (int)i < Constants.NUM_ESSENCES; i++)
                {
                    int essence = essHex.getEssence(i);
                    if (essence != 0)
                    {
                        statsList.Add((Constants.ESSENCE_NAMES[(int)i], essence.ToString()));
                    }
                }
                for (ResourceType i = 0; (int)i < Constants.NUM_RESOURCES; i++)
                {
                    int resource = essHex.getResource(i);
                    if (resource != 0)
                    {
                        statsList.Add((Constants.RESOURCE_NAMES[(int)i], resource.ToString()));
                    }
                }
            }
            maxScreens = (int) (statsList.Count / 5f);
            return this;
        }

        void UIManagerState.handle_input(UIManager manager, (HexagonContainer, (int, int)) hex, MouseState state, KeyboardState keyboardState)
        {
            manager.UIHexState = UIManagerState.waitingState;
        }
        void UIManagerState.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            int numStats = Math.Min(statsList.Count, (currentScreen * 5) + 5) - currentScreen * 5;
            (string, string)[] stats = new (string, string)[5];
            for (int i = 0; i < numStats; i++)
            {
                stats[numStats] = statsList[i + currentScreen*5];
            }
            UIInfoDisplay.CreateBox(batch, displayingHexagon, new (string, string)[] {});
        }
    }
}
