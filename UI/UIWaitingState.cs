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
        void UIManagerState.mouseInput(MouseState state)
        {
            var container = UIManagerState.manager.checkHexes();
            if (container != null)
            {
                var hex = container ?? throw new Exception();
                if (state.LeftButton == ButtonState.Pressed)
                {
                    UIManagerState.manager.UIHexState = UIManagerState.clickedState.setClicked(hex);
                }
                if (state.RightButton == ButtonState.Pressed)
                {
                    UIManagerState.manager.UIHexState = UIManagerState.displayingState.setDisplay(hex);
                }
            }
        }
    }
}
