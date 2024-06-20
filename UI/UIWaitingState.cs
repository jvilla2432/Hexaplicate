using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate.UI
{
    internal class UIWaitingState : UIManagerState
    {
        void UIManagerState.handle_input(UIManager manager, (HexagonContainer, (int, int)) container, MouseState state)
        {
            if(state.LeftButton == ButtonState.Pressed)
            {
                manager.UIHexState = UIManagerState.clickedState.setClicked(container);
            }
            if(state.RightButton == ButtonState.Pressed)
            {
                manager.UIHexState = UIManagerState.displayingState;
            }
        }
    }
}
