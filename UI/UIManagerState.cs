using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Hexaplicate.UI
{
    internal interface UIManagerState
    {
        public static UIWaitingState waitingState = new();
        public static UIClickedState clickedState = new();
        public static UIDisplayingState displayingState = new();
        public static UIManager manager;

        public void mouseInput(MouseState mouseState) { }
        public void keyboardInput(KeyboardState keyboardState) { }

        public void Draw(SpriteBatch batch) { }

    }
}
