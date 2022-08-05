using System;

namespace LightsOutConsoleAppV2
{
    /// <summary>
    /// Lights out rules: https://en.wikipedia.org/wiki/Lights_Out_(game)
    /// Solver: https://bursyclorave.github.io/mobile-piano/
    /// Online version: https://www.lightsout.ir/
    /// </summary>
    class Program
    {
        private static Grid Grid { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("My electrician messed up all my lights! I need help turning them off before I leave for the day.");
            Console.WriteLine();

            try
            {
                int nGrid = 0;

                // Difficulty selection validation
                do
                {
                    if (nGrid == -1)
                    {
                        Console.WriteLine("Please type a valid level.");
                        Console.Write("Easy, Medium, Hard, Harder, or Extreme: ");
                    }
                    else
                    {
                        Console.WriteLine("Select Level");
                        Console.Write("Easy, Medium, Hard, Harder, or Extreme: ");
                    }
                    var level = Console.ReadLine();
                    nGrid = SelectLevel(level);
                }
                while (nGrid == -1); // We want a valid level selection

                // Generate a new game
                var machine = new GridMachine();
                var grid = machine.GenerateGrid(nGrid, nGrid);
                PrintLayout(grid);

                // Handle game until completion
                do
                {
                    var validNum = true;
                    var withinBounds = true;
                    var rowSelection = "";
                    var colSelection = "";

                    // Row selection validation
                    do
                    {
                        if (!validNum || !withinBounds)
                        {
                            Console.WriteLine("Please type a valid input.");
                            Console.Write("Select a row: ");
                            rowSelection = Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Select a row: ");
                            rowSelection = Console.ReadLine();
                        }

                        validNum = ValidInt(rowSelection);
                        withinBounds = WithinGridBounds(rowSelection, nGrid);
                    }
                    while (!validNum || !withinBounds);

                    // Reset validation variables
                    validNum = true;
                    withinBounds = true;

                    // Column selection valdiation
                    do
                    {
                        if (!validNum || !withinBounds)
                        {
                            Console.WriteLine("Please type a valid input.");
                            Console.Write("Select a column: ");
                            colSelection = Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Select a column: ");
                            colSelection = Console.ReadLine();
                        }

                        validNum = ValidInt(colSelection);
                        withinBounds = WithinGridBounds(colSelection, nGrid);
                    }
                    while (!validNum || !withinBounds);

                    var newgrid = ConvertSelection(int.Parse(rowSelection), int.Parse(colSelection), machine, grid);
                    PrintLayout(newgrid);
                    Grid = newgrid; // Pass grid reference
                }
                while (!machine.IsWinner(grid)); // Play until all bits are 0
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error. Exception: {e.Message}");
            }

            Console.WriteLine("Congrats, you did it! Thanks for helping me out!");
            Console.Write($"Final Score: {Grid.Score}");
        }

        /// <summary>
        /// Display the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private static bool PrintLayout(Grid grid)
        {
            try
            {
                if (grid.Score != 0)
                {
                    Console.WriteLine($"Score {grid.Score}");
                }

                for (int i = 0; i < grid.Layout.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.Layout.GetLength(1); j++)
                    {
                        Console.Write(grid.Layout[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Convert input to get to next level
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="machine"></param>
        /// <param name="grid"></param>
        /// <returns>Grid with flipped bits</returns>
        private static Grid ConvertSelection(int row, int col, GridMachine machine, Grid grid)
        {
            return machine.FlipBits(grid, row, col);
        }

        /// <summary>
        /// Pick level -- easy 3x3, medium 5x5, hard 7x7, harder 9x9, extreme 15x15
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Int values for the grid size</returns>
        private static int SelectLevel(string level)
        {
            return level.ToLower().Trim() switch
            {
                "easy" => 3,
                "medium" => 5,
                "hard" => 7,
                "harder" => 11,
                "extreme" => 15,
                _ => -1,
            };
        }

        /// <summary>
        /// Ensures that the input is within the bounds of the grid
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <returns>True if the input is within the bounds of the grid</returns>
        private static bool WithinGridBounds(string pos, int size)
        {
            try
            {
                if (int.Parse(pos.ToString()) > size - 1 || int.Parse(pos.ToString()) < 0) // Out of bounds
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check the the input is a valid int
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>True is the input is an int</returns>
        private static bool ValidInt(string pos)
        {
            try
            {
                var res = int.Parse(pos.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
