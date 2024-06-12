using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Hexaplicate
{
    internal class UIManager
    {

        public delegate (HexagonContainer,(int,int)) onClick();
        bool pressed = false;
        (HexagonContainer, (int, int)) previous;
        bool prevClicked = false;
        private List<(onClick, Func<int,int,Boolean>)> registeredFunctions = new();
        private Grid centerGrid;


        public UIManager(Grid center)
        {
            centerGrid = center;
        }
        /// <summary>
        /// Checks the current inputs and calls all necessary callback functions
        /// </summary>
        public void checkState()
        {
            //Left click(swap)
            MouseState currentState = Mouse.GetState();
            if (currentState.LeftButton == ButtonState.Pressed && !pressed)
            { 
                pressed = true; 
                foreach ((onClick, Func<int, int, Boolean>) mouseEvent in registeredFunctions)
                {
                    if (mouseEvent.Item2(currentState.Position.X, currentState.Position.Y))
                    {
                        if (prevClicked)
                        {
                            //Swap the hexagons
                            Hexagon hex = previous.Item1.getHexagon(previous.Item2);
                            (HexagonContainer, (int, int)) current = mouseEvent.Item1();
                            previous.Item1.setHexagon(previous.Item2, current.Item1.getHexagon(current.Item2));
                            current.Item1.setHexagon(current.Item2, hex);
                            prevClicked = false;
                        }
                        else
                        {
                            prevClicked = true;
                            previous = mouseEvent.Item1();
                        }
                    }
                }
            }
            if(currentState.LeftButton == ButtonState.Released)
            {
                pressed = false;
            }
            if(currentState.RightButton == ButtonState.Pressed && !pressed)
            {
                pressed = true;
                foreach ((onClick, Func<int, int, Boolean>) mouseEvent in registeredFunctions)
                {
                    if (mouseEvent.Item2(currentState.Position.X, currentState.Position.Y))
                    {
                        if (prevClicked)
                        {
                            //Add connection
                            List<List<Hexagon>> BFStree = new();
                            Hexagon hex = previous.Item1.getHexagon(previous.Item2);
                            (HexagonContainer, (int, int)) current = mouseEvent.Item1();
                            if (HexagonOperations.CheckAdjancency(previous.Item2, current.Item2)){
                                if(previous.Item1 == current.Item1)
                                {
                                    if(previous.Item1 is Grid)
                                    {
                                        Grid grid = (Grid)previous.Item1;
                                        if (!grid.checkConnection(current.Item2, previous.Item2))
                                        {
                                            grid.toggleConnection(previous.Item2, current.Item2);
                                            if(!centerGrid.BFS((3, 3), BFStree))
                                            {
                                                grid.toggleConnection(previous.Item2, current.Item2);
                                            }
                                        }
                                    }
                                }
                            }
                            prevClicked = false;
                        }
                    }
                }
            }

            //Right click(other stuff?)
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
