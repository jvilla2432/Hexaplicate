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
        void UIManagerState.handle_input(UIManager manager, (HexagonContainer, (int, int)) hex, MouseState state)
        {
            manager.UIHexState = UIManagerState.waitingState;
        }
        void UIManagerState.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            UIInfoDisplay.CreateBox((400, 400), batch);
        }
    }
}
