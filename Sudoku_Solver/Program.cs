namespace Sudoku_Solver
{
    class Program
    {
        static void Main()
        {
            Menu();
        }

        static void Menu()
        {
            string answer = "";
            Console.WriteLine("-----------------");
            Console.WriteLine("| Sudoku Solver |");
            Console.WriteLine("-----------------");
            Console.WriteLine();
            while (answer != "c")
            {
                Console.WriteLine("a: Solve Grid");
                Console.WriteLine("b: Try a Sudoku Puzzle");
                Console.WriteLine("c: Quit");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                answer = Console.ReadLine() ?? "c";
                if (answer == "a")
                {
                    Console.WriteLine("a: Enter Grid One by One");
                    Console.WriteLine("b: Enter Grid Line by Line");
                    Console.WriteLine();
                    Console.Write("Enter your choice: ");
                    answer = Console.ReadLine() ?? "";
                    if (answer == "a")
                    {
                        int[,] grid = EnterGridManually();
                        SolveAndReturn(grid);
                    }
                    else if (answer == "b")
                    {
                        int[,] grid = EnterGridLines();
                        SolveAndReturn(grid);
                    }
                }
                else if (answer == "b")
                {
                    Console.WriteLine("a: Enter Grid to Solve One by One");
                    Console.WriteLine("b: Enter Grid to Solve Line by Line");
                    Console.WriteLine();
                    Console.Write("Enter your choice: ");
                    answer = Console.ReadLine() ?? "";
                    if (answer == "a")
                    {
                        int[,] grid = EnterGridManually();
                        UserSolve(grid);
                    }
                    else if (answer == "b")
                    {
                        int[,] grid = EnterGridLines();
                        UserSolve(grid);
                    }
                }
            }
        }

        static int[,] CopyGrid(int[,] grid)
        {
            int[,] copy = EmptyGrid();
            for (int row = 0; row <= 8; row++)
            {
                for (int col = 0; col <= 8; col++)
                {
                    copy[row, col] = grid[row, col];
                }
            }
            return copy;
        }

        static void UserSolve(int[,] grid)
        {
            int[,] completedGrid = SolveAndReturn(CopyGrid(grid));
            PrintGrid(grid);
            while (ContainsZero(grid))
            {
                Console.Write("Enter the row of the square you want to add: ");
                int row = Convert.ToInt32(Console.ReadLine());
                row--;
                Console.Write("Enter the column of the square you want to add: ");
                int column = Convert.ToInt32(Console.ReadLine());
                column--;
                Console.Write("Enter the number you want to add: ");
                int number = Convert.ToInt32(Console.ReadLine());
                grid[row, column] = number;
                PrintGrid(grid);
            }
            if (GridsAreEqual(grid, completedGrid))
            {
                Console.WriteLine("You have solved the sudoku grid");
            }
            else
            {
                Console.WriteLine("You have not solved the sudoku grid");
                Console.Write("Do you want to solve the grid again (y/n): ");
                string answer = Console.ReadLine() ?? "";
                if (answer == "y")
                {
                    UserSolve(grid);
                }
            }
        }

        static bool GridsAreEqual(int[,] firstGrid, int[,] secondGrid)
        {
            for (int row = 0; row <= 8; row++)
            {
                for (int col = 0; col <= 8; col++)
                {
                    if (firstGrid[row, col] != secondGrid[row, col])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static int[,] EnterGridLines()
        {
            int[,] grid = EmptyGrid();
            Console.WriteLine("Enter each column of the row, separated by spaces:");
            Console.WriteLine("X X X X X X X X X");
            for (int row = 0; row <= 8; row++)
            {
                Console.Write("Enter row {0}: ", row + 1);
                string input = Console.ReadLine() ?? "";
                string[] numbers = input.Split(' ');
                for (int col = 0; col <= 8; col++)
                {
                    grid[row, col] = Convert.ToInt32(numbers[col]);
                }
            }
            return grid;
        }

        static int[,] EnterGridManually()
        {
            int[,] grid = EmptyGrid();
            for (int row = 0; row <= 8; row++)
            {
                for (int col = 0; col <= 8; col++)
                {
                    Console.Write("Enter value for row {0}, column {1}: ", row + 1, col + 1);
                    grid[row, col] = Convert.ToInt32(Console.ReadLine());
                }
            }
            return grid;
        }

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
            Console.WriteLine("   1 2 3  4 5 6  7 8 9");
            for (int row = 0; row <= 8; row++)
            {
                Console.Write("{0} |", row + 1);
                for (int col = 0; col <= 8; col++)
                {
                    Console.Write(grid[row, col]);
                    if (col != 8)
                    {
                        Console.Write(" ");
                    }
                    if (col == 2 || col == 5)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("|");
                Console.WriteLine();
                if (row ==2 || row == 5)
                {
                    Console.WriteLine();
                }
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
                return EmptyGrid();
            }
            return grid;
        }

        static bool ContainsZero(int[,] grid)
        {
            for (int row = 0; row <= 8; row++)
            {
                for (int col = 0; col <= 8; col++)
                {
                    if (grid[row, col] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        static int[,] EmptyGrid()
        {
            int[,] grid =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            return grid;
        }
    }
}
