﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//shoutout to https://www.redblobgames.com/grids/hexagons/
namespace Hexaplicate
{
    [DataContract]
    internal class Grid : HexagonContainer
    {
        private static (int x, int y) HexagonSize = (7, 7);
        [DataMember]
        private Hexagon[][] gridHexagons = new Hexagon[HexagonSize.x][];
        private static (int, int) coordinates = ((int)(Constants.SCREEN_SIZE.Item1 * Constants.GRID_OFFSET.Item1),
                (int)(Constants.SCREEN_SIZE.Item2 * Constants.GRID_OFFSET.Item2));
        [DataMember]
        private Dictionary<(int, int), List<(int, int)>> adjList = new();
        private static Texture2D connectionTexure;
        [IgnoreDataMember]
        public static Grid centerGrid;
        private Grid? parentGrid;


        public void setParent(Grid grid)
        {
            parentGrid = grid;
        }
        public void switchParent()
        {
            if(parentGrid != null)
            {
                Game1.game.SetGrid(parentGrid);
            }
        }
        public static void SetTexture(Texture2D texture)
        {
            connectionTexure = texture;
        }

        public Hexagon getHexagon((int, int) coords)
        {
            return gridHexagons[coords.Item1][coords.Item2];
        }
        public void setHexagon((int, int) coords, Hexagon hex)
        {
            gridHexagons[coords.Item1][coords.Item2] = hex;
        }

        public bool toggleConnection((int,int) hex1, (int,int) hex2)
        {
            foreach (var hex in adjList[hex1])
            {
                if (hex2 == hex)
                {
                    adjList[hex1].Remove(hex);
                    return false;
                }
            }
            adjList[hex1].Add(hex2);
            return true;
        }

        public bool checkConnection((int, int) hex1, (int, int) hex2)
        {
            foreach (var hex in adjList[hex1])
            {
                if (hex2 == hex) { 
                    return true;
                }
            }
            return false;
        }


        public IEnumerable<(int,int)> returnHexagonPairs()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i + j > 2) && (i + j < 10))
                    {
                        yield return (i, j);
                    }
                }
            }
        }
        /// <summary>
        /// Initalize Grid with all empty hexagons.
        /// </summary>
        public Grid(bool central = false)
        {
            if (central) 
            {
                centerGrid = this;
            }
            //Initialize
            for(int x = 0; x < HexagonSize.x; x++)
            {
                gridHexagons[x] = new Hexagon[HexagonSize.y];
            }
            foreach (var pair in returnHexagonPairs())
            {
                setHexagon(pair, new EmptyHexagon());
                adjList[pair] = new List<(int, int)>();
            }
        }
        /// <summary>
        /// Places newHex into coordinates and returns the Hexagon at coordinates 
        /// </summary>
        /// <param name="newHex">Hexagon to place into grid</param>
        /// <param name="coordinates">Axial coordinates of hexagon to replace</param>
        /// <returns>Hexagon that was at coordinates</returns>
        public Hexagon swapHexagon(Hexagon newHex, (int,int) coordinates)
        {
            Hexagon oldHex = getHexagon(coordinates);
            setHexagon(coordinates, newHex);
            return oldHex;
        }

        public void setCoordinates( (int,int) coords)
        {
            coordinates = coords;
        }

        /// <summary>
        /// Draws the grid on the given batch
        /// </summary>
        /// <param name="batch">SpriteBatch to draw on</param>
        public void Draw(SpriteBatch batch)
        {
            foreach (var pair in returnHexagonPairs())
            {
                int i = pair.Item1;
                int j = pair.Item2;
                (float, float) pixel = HexagonOperations.AxialToPixel((i - 3) * (Constants.HEXAGON_GAP + 1), 
                    (j - 3) * (Constants.HEXAGON_GAP + 1));
                getHexagon(pair).Draw(batch, (int)pixel.Item1 + coordinates.Item1, (int)pixel.Item2 + coordinates.Item2);
                foreach (var addConnection in adjList[pair])
                {
                    (int, int) diff = (addConnection.Item1 - i, addConnection.Item2 - j);
                    (float, float) pixel2 = HexagonOperations.AxialToPixel(addConnection.Item1 - i, addConnection.Item2 - j);
                    batch.Draw(connectionTexure, new Rectangle((int)pixel.Item1 + coordinates.Item1 + (int)(Constants.HEXAGON_RADIUS*0.5)
                       + (int)(Constants.HEXAGON_RADIUS / 2) + (int)pixel2.Item1,
                    (int)pixel.Item2 + coordinates.Item2 
                        + (int)(Constants.ADJUSTED_HEX_SIZE.Item2 * Constants.HEXAGON_GAP * 0.5) + (int)pixel2.Item2,
                    (int)(Constants.HEXAGON_RADIUS),
                    (int)(Constants.ADJUSTED_HEX_SIZE.Item2*Constants.HEXAGON_GAP)), null, Color.White,
                    HexagonOperations.axialVectors2[diff], 
                    new Vector2(connectionTexure.Width/2,
                    connectionTexure.Height / 2),
                    0, 0);
                }

            }
        }

        public void RegisterHexs(Hexaplicate.UI.UIManager manager)
        {
            manager.resetHexClick();
            foreach (var pair in returnHexagonPairs())
            {
                int i = pair.Item1;
                int j = pair.Item2;
                int localI = i;
                int localJ = j;
                (float, float) pixel = HexagonOperations.AxialToPixel((i - 3) * (Constants.HEXAGON_GAP + 1),
                    (j - 3) * (Constants.HEXAGON_GAP + 1));
                //TODO: Simplify the following code. Currently has to adjust for drawing being top right, but 
                // hitbox being central
                (int, int) pixelInt = ((int)pixel.Item1 + coordinates.Item1 + (int)(Constants.ADJUSTED_HEX_SIZE.Item1/2f)
                    , (int)pixel.Item2 + coordinates.Item2 + (int)(Constants.ADJUSTED_HEX_SIZE.Item2 / 2f));
                (HexagonContainer, (int,int)) clickFunction() {
                    return (this, (localI, localJ));
                }
                manager.registerHexClick(clickFunction, 
                    HexagonOperations.HexagonHitBox(Constants.HEXAGON_SIZE/2, pixelInt));  
            }
        }

        //DFS check
        //Also create tree for essence
        //create tree when checking?
        //Also create tree for actually executing all essence hexagons
        //Return iterator of hexagons
        //Only need to check the grid that changes(assuming no portal sheninigans changes this) 
        //Returns list of lists by depth.... 
        public static void BFS(List<List<(Grid,(int,int))>> list)
        {
            Queue<(Grid,(int, int))> hexQueue = new();
            HashSet<(Grid,(int, int))> explored = new();
            hexQueue.Enqueue( (centerGrid,(3,3)));
            int depth = 0;
            while(hexQueue.Count > 0)
            {
                Queue<(Grid,(int, int))> nextQueue = new();
                (Grid, (int, int)) hex = hexQueue.Dequeue();
                if (list.Count == depth+1)
                {
                    list[0].Add(hex);
                }
                else
                {
                    list.Add(new List<(Grid, (int, int))> {hex});
                }
                explored.Add(hex);
                foreach ((Grid, (int, int)) neighbor in hex.Item1.getNeighbors(hex.Item2))
                {
                    if (!explored.Contains(neighbor))
                    {
                        nextQueue.Enqueue(neighbor);

                    }
                }
                if(hexQueue.Count == 0)
                {
                    if(nextQueue.Count != 0)
                    {
                        hexQueue = nextQueue;
                        depth++;
                    }
                }
            }
        }

        
        public IEnumerable<(Grid, (int,int))> getNeighbors((int,int) hex)
        {
            foreach ((int, int) neighbor in adjList[hex])
            {
                if (getHexagon(neighbor) is FractalHexagon)
                {
                    FractalHexagon fractalHex = (FractalHexagon)getHexagon(neighbor);
                    (int, int) wrapped = HexagonOperations.FractalHex((neighbor.Item1 - hex.Item1, neighbor.Item2 - hex.Item2));
                    yield return (fractalHex.grid, wrapped);
                }
                yield return (this,neighbor);
            }

        }
        //additional edge
        public static bool checkCycle( (Grid, (int,int)) startingHex, (Grid, (int, int)) targetHex) 
        {
            Stack<(Grid, (int, int))> hexStack = new();
            HashSet<(Grid, (int, int))> explored = new();
            hexStack.Push(startingHex);
            while (hexStack.Count > 0)
            {
                (Grid, (int, int)) hex = hexStack.Pop(); 
                explored.Add(hex);
                foreach ((Grid, (int, int)) neighbor in hex.Item1.getNeighbors(hex.Item2))
                {
                    if(neighbor == targetHex)
                    {
                        return true;
                    }
                    if (!explored.Contains(neighbor))
                    {
                        hexStack.Push(neighbor); 
                    }
                }
            }
            return false;
        }

        public static Hexagon getHexagon( (Grid, (int, int)) pair)
        {
            return pair.Item1.getHexagon(pair.Item2);
        }
    }
}
