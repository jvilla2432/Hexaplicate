using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Hexaplicate.UI
{
    internal class UIManager
    {

        public delegate (HexagonContainer,(int,int)) onClick();
        bool pressed = false;
        (HexagonContainer, (int, int)) previous;
        bool prevClicked = false;
        private List<(onClick, Func<int,int,Boolean>)> registeredFunctions = new();
        private Grid centerGrid;
        public UIManagerState UIHexState = UIManagerState.waitingState;


        public UIManager(Grid center)
        {
            centerGrid = center;
        }
        /// <summary>
        /// Checks the current inputs and calls all necessary callback functions for hexagons
        /// </summary>
        public void checkState()
        {
            MouseState currentState = Mouse.GetState();
            if ( (currentState.LeftButton == ButtonState.Pressed ||
                currentState.RightButton == ButtonState.Pressed) && !pressed)
            { 
                pressed = true;
                bool clicked = false;
                foreach ((onClick, Func<int, int, Boolean>) mouseEvent in registeredFunctions)
                {
                    if (mouseEvent.Item2(currentState.Position.X, currentState.Position.Y))
                    {
                        UIHexState.handle_input(this, mouseEvent.Item1(), currentState);
                        clicked = true;
                    }
                }
                if (!clicked)
                {
                    UIHexState = UIManagerState.waitingState;
                }
            }
            if(currentState.LeftButton == ButtonState.Released &&
                currentState.RightButton == ButtonState.Released)
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
