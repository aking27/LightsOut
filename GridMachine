using System;
using System.Collections.Generic;
using System.Linq;

namespace LightsOutConsoleAppV2
{
    class GridMachine
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GridMachine() { }

        /// <summary>
        /// Generate an n by n grid
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public Grid GenerateGrid(int rows, int cols)
        {
            Random rnd = new Random();
            var layout = new int[rows, cols];

            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    layout[i, j] = rnd.Next(0, 2);
                }
            }

            var grid = new Grid
            {
                Score = 0,
                Rows = rows,
                Columns = cols,
                Layout = layout
            };

            return grid;
        }

        /// <summary>
        /// Based on the selection of the user, flip the bits of the row or column of the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rowSelection"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public Grid FlipBits(Grid grid, int row, int col)
        {
            var tempGrid = grid;
            var locations = GetAdjacent(grid, row, col);
            locations.Add(new int[] { row, col });

            for (int i = 0; i < tempGrid.Layout.GetLength(1); i++)
            {
                for (int j = 0; j < tempGrid.Layout.GetLength(0); j++)
                {
                    var tempLocation = new int[] { i, j };

                    foreach (var location in locations)
                    {
                        if (tempLocation.SequenceEqual(location))
                        {
                            tempGrid.Layout[i, j] = tempGrid.Layout[i, j] == 0 ? 1 : 0;
                        }
                    }
                }
            }

            tempGrid.Score += 1;
            return tempGrid;
        }

        /// <summary>
        /// Checks if the game is over and saves to the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns>Bool if the grid is a winner</returns>
        public bool IsWinner(Grid grid)
        {
            grid.Winner = grid.Layout.Cast<int>().All(x => x == 0);
            return grid.Winner;
        }

        /// <summary>
        /// Based on a selection, check for the 4 adjacent tiles
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>The locations of the valid adjacent selections</returns>
        public List<int[]> GetAdjacent(Grid grid, int row, int col)
        {
            var adjacentList = new List<int[]>();

            // Check for valid adjacent positions
            if (row - 1 >= 0)
            {
                adjacentList.Add(new int[] { row - 1, col });
            }
            if (col - 1 >= 0)
            {
                adjacentList.Add(new int[] { row, col - 1 });
            }
            if (row + 1 < grid.Rows)
            {
                adjacentList.Add(new int[] { row + 1, col });
            }
            if (col + 1 < grid.Columns)
            {
                adjacentList.Add(new int[] { row, col + 1 });
            }

            return adjacentList;
        }
    }
}
