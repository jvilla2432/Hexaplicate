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

        void UIManagerState.keyboardInput(KeyboardState keyboardState)
        {
            var container = UIManagerState.manager.checkHexes();
            if(container != null)
            {
                var hex = container ?? throw new Exception();
                if (keyboardState.IsKeyDown(Keys.X) && hex.Item1 is Grid)
                {
                    var hexagon = hex.Item1.getHexagon(hex.Item2);
                    if(hexagon is FractalHexagon)
                    {
                        System.Diagnostics.Debug.WriteLine("test");
                        var fractalHex = (FractalHexagon)hexagon;
                        fractalHex.switchGrid((Grid)hex.Item1);
                    }
                }
            }
            if (keyboardState.IsKeyDown(Keys.B))
            {
                Game1.game.returnParentGrid();
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Game1.game.Save();
            }
            if (keyboardState.IsKeyDown(Keys.L))
            {
                Game1.game.Load();
            }

        }
    }
}
