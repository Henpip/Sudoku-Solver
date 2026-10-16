namespace Sudoku_Solver
{
    class Program
    {
        static void Main()
        { 
            int[,] grid = EnterGridLines();
            if (Solve(grid))
            {
                PrintGrid(grid);
            }
            else
            {
                Console.WriteLine("No solution exists");
            }
        }
        
        static int[,] EnterGridLines()
        {
            int[,] grid = { {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0} };
            Console.WriteLine("Enter each column of the row, separated by spaces:");
            Console.WriteLine("X X X X X X X X X");
            for (int row = 0; row <= 8; row++)
            {
                Console.Write("Enter row {0}: ", row);
                string input = Console.ReadLine() ?? "";
                string[] numbers = input.Split(' ');
                for (int col = 0; col <= 8; col++)
                {
                    grid[row, col] = Convert.ToInt32(numbers[col]);
                }
            }
            return grid;
        }

        /*static int[,] EnterGridManually()
        {
            int[,] grid = { {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0} };
            for (int row = 0; row <= 8; row++)
            {
                for (int col = 0; col <= 8; col++)
                {
                    Console.Write("Enter value for row {0}, column {1}: ", row, col);
                    grid[row, col] = Convert.ToInt32(Console.ReadLine());
                }
            }

            return grid;
        }*/

        static bool IsValid(int[,] grid, int r, int c, int k)
        {
            // checks every number in a row to check if any of them equal k, and if they do, return false
            for (int col = 0; col < 9; col++)
            {
                if (grid[r, col] == k)
                {
                    return false;
                }
            }
            
            // checks every number in a column to check if any of them equal k, and if they do, return false
            for (int row = 0; row < 9; row++)
            {
                if (grid[row, c] == k)
                {
                    return false;
                }
            }
            
            // finds the coordinates of the top left corner of the 3x3 box that the current cell is in
            int boxStartRow = r / 3 * 3;
            int boxStartCol = c / 3 * 3;
            
            // loops through every number in the 3x3 box to check if any of them equal k, and if they do, return false
            for (int row = boxStartRow; row < boxStartRow + 3; row++)
            {
                for (int col = boxStartCol; col < boxStartCol + 3; col++)
                {
                    if (grid[row, col] == k)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool Solve(int[,] grid)
        {
            // goes through each row
            for (int row = 0; row <= 8; row++)
            {
                // goes through each column in that row
                for (int col = 0; col <= 8; col++)
                {
                    // if cell is empty
                    if (grid[row, col] == 0)
                    {
                        // try every number from 1 to 9
                        for (int k = 1; k <= 9; k++)
                        {
                            // check if k works
                            if (IsValid(grid, row, col, k))
                            {
                                grid[row, col] = k;
                                // solve rest of the grid
                                if (Solve(grid))
                                {
                                    // if grid is solved, return true
                                    return true;
                                }
                                grid[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        static void PrintGrid(int[,] grid)
        {
            // goes through each row
            for (int row = 0; row <= 8; row++)
            {
                // goes through each column in that row
                for (int col = 0; col <= 8; col++)
                {
                    // outputs number in grid
                    Console.Write(grid[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
        static int[,] SolveAndReturn(int[,] grid)
        {
            if (Solve(grid))
            {
                // goes through each row
                for (int row = 0; row <= 8; row++)
                {
                    // goes through each column in that row
                    for (int col = 0; col <= 8; col++)
                    {
                        // if cell is empty
                        if (grid[row, col] == 0)
                        {
                            // try every number from 1 to 9
                            for (int k = 1; k <= 9; k++)
                            {
                                // check if k works
                                if (IsValid(grid, row, col, k))
                                {
                                    grid[row, col] = k;
                                    // solve rest of the grid
                                    if (Solve(grid))
                                    {
                                        // if grid is solved, return true
                                        break;
                                    }
                                    grid[row, col] = 0;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return null;
            }
            return grid;
        }
    }
}
