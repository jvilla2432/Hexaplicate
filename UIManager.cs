using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Hexaplicate
{
    internal class UIManager
    {

        public delegate void onClick();
        private List<(onClick, Func<int,int,Boolean>)> registeredFunctions;
        /// <summary>
        /// Checks the current inputs and calls all necessary callback functions
        /// </summary>
        void checkState()
        {
            MouseState currentState = Mouse.GetState();
            foreach((onClick, Func<int, int, Boolean>) mouseEvent in registeredFunctions)
            {
                if(mouseEvent.Item2(currentState.Position.X, currentState.Position.Y))
                {
                    mouseEvent.Item1();
                }
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
