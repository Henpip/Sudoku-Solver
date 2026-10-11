## Sudoku Solver
### Importing the sudoku grid into python
- I can do this by making a 9 by 9 grid (matrix), separated by rows
  - I need to look up how to do this in c#
    - Including how to find numbers in a matrix (check inside), get numbers from matrix, change numbers in matrix, etc
      - I can do this by using a 2D array in c#

### Basics of algorithm
- I can solve a sudoku puzzle by using a backtracking algorithm
  - This involves recursively trying different numbers in each empty cell until a solution is found or all possibilities are exhausted
  - I can implement this algorithm by using a recursive function that tries each possible number in each empty cell, and backtracks if a number leads to a dead end

#### First step of algorithm
- I can start by finding the first empty cell in the grid
  - I can do this by iterating through the grid and checking each cell for a value of 0 (assuming 0 represents an empty cell)
- I can use coordinates ('r' for row and 'c' for column) to find these cells
  - In this scenario, I can use nested loops to iterate through each row and column of the grid, checking each cell for a value of 0
    - If a cell with a value of 0 is found, I can store its coordinates (r, c) and break out of the loops

#### Second step of algorithm
- Once I have found the coordinates of an empty cell, I can try each possible number (1-9) in that cell
  - I can do this by using a loop to iterate through the numbers 1-9, and checking if each number is valid in the current cell
    - A number is valid if it does not already exist in the same row, column, or 3x3 subgrid
      - Use 'grid' for the whole grid, use 'r' for the row index, 'c' for the column index, and 'k' for the number being checked
        - Check:
          - k not in grid[r]
          - k not in [grid[i][c] for i in range(9)]
          - k not in [grid[i][j] for i in range(r//3*3, r//3*3+3) for j in range(c//3*3, c//3*3+3)]
    - If a valid number is found, I can assign it to the cell and move on to the next empty cell
    - If no valid number is found, I can backtrack to the previous cell and try a different number
- Make this a function

### Third step of algorithm
- Once I have found a valid number for a cell, I can move on to the next empty cell and repeat the process
  - If I reach the end of the grid and all cells are filled, I have solved the Sudoku puzzle
  - If I backtrack to the first cell and cannot find a valid number, the puzzle has no solution