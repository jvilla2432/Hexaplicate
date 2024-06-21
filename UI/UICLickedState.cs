using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Hexaplicate.UI
{
    internal class UIClickedState : UIManagerState
    {
        private (HexagonContainer, (int, int)) prevClicked;

        public UIClickedState setClicked((HexagonContainer, (int, int)) prevC)
        {
            prevClicked = prevC;
            return this;
        }
        void UIManagerState.handle_input(UIManager manager, (HexagonContainer, (int, int)) current, MouseState state, KeyboardState keyboardState)
        {
            Hexagon prevHex = prevClicked.Item1.getHexagon(prevClicked.Item2);
            Hexagon currentHex = current.Item1.getHexagon(current.Item2);
            if (state.LeftButton == ButtonState.Pressed)
            {
                prevClicked.Item1.setHexagon(prevClicked.Item2, currentHex);
                current.Item1.setHexagon(current.Item2, prevHex);
            }
            else if (state.RightButton == ButtonState.Pressed)
            {
                if (prevClicked.Item1 is Grid && current.Item1 == prevClicked.Item1
                    && HexagonOperations.CheckAdjancency(prevClicked.Item2, current.Item2))
                {
                    Grid grid = (Grid)prevClicked.Item1;
                    if (!grid.checkConnection(current.Item2, prevClicked.Item2))
                    {
                        grid.toggleConnection(prevClicked.Item2, current.Item2);
                        if (Grid.checkCycle((grid, current.Item2),
                            (grid, prevClicked.Item2)))
                        {
                            grid.toggleConnection(prevClicked.Item2, current.Item2);
                        }
                    }
                }
            }
            manager.UIHexState = UIManagerState.waitingState;
        }
    }
}
