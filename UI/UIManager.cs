using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Hexaplicate.UI
{
    internal class UIManager
    {

        public delegate (HexagonContainer,(int,int)) onClick();
        bool pressed = false;
        private List<(onClick, Func<int,int,bool>)> registeredFunctions = new();
        public UIManagerState UIHexState = UIManagerState.waitingState;

        
        public UIManager()
        {
            UIManagerState.manager = this;
        }
        public void Draw(SpriteBatch batch)
        {
            UIHexState.Draw(batch);
        }

        public (HexagonContainer, (int, int))? checkHexes()
        {
            MouseState currentMouse = Mouse.GetState();
            foreach ((onClick, Func<int, int, Boolean>) mouseEvent in registeredFunctions)
            {
                if (mouseEvent.Item2(currentMouse.Position.X, currentMouse.Position.Y))
                {
                    return mouseEvent.Item1();
                }
            }
            return null;
        }

        /// <summary>
        /// Checks the current inputs and calls all necessary callback functions for hexagons
        /// </summary>
        public void checkState()
        {
            MouseState currentMouse = Mouse.GetState();
            KeyboardState currentKeyboard = Keyboard.GetState();
            if ( (currentMouse.LeftButton == ButtonState.Pressed ||
                currentMouse.RightButton == ButtonState.Pressed || currentKeyboard.GetPressedKeyCount() > 0) && !pressed)
            { 
                pressed = true;
                UIHexState.mouseInput(currentMouse);
            }
            if(currentMouse.LeftButton == ButtonState.Released &&
                currentMouse.RightButton == ButtonState.Released && currentKeyboard.GetPressedKeyCount() == 0)
            {
                pressed = false;
            }
        }

        /// <summary>
        /// Registers a click function to be executed whenever the mouse sastifies clickCoordinates 
        /// </summary>
        /// <param name="clickFunction">Function to be called when clickCoordinates evalautes to true</param>
        /// <param name="clickCoordinates">Takes in an x and a y parameter and returns True if clickFunction should be called</param>
        public void registerClick(onClick clickFunction, Func<int,int,Boolean> clickCoordinates){
            registeredFunctions.Add((clickFunction,clickCoordinates)); 
        }

        
    }
}
