using System;

namespace Life
{
    public class Grid
    {
        /// <summary>
        /// Create a new grid based on seed data
        /// </summary>
        /// <param name="seedData">A 2 dimensional array containing the data to use for the grid</param>
        /// <returns>A 2 dimension array</returns>
        public static int[,] CreateGrid(int[,] seedData)
        {
            // Set the the row and col count for the grid
            var rowCount = seedData.GetUpperBound(0) + 1;
            var colCount = seedData.GetUpperBound(1) + 1;

            // Set the array dimensions 
            var grid = new int[rowCount, colCount];

            // Populate the grid with the seed data
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    grid[row, col] = seedData[row, col];
                }
            }

            return grid;
        }

        /// <summary>
        /// Get the data for the next iteration of the grid based on the values in the current grid
        /// </summary>
        /// <param name="currentGrid">The current grid</param>
        /// <returns>A new 2 dimensional array</returns>
        public static int[,] GetNextGenerationData(int[,] currentGrid)
        {
            var gridRows = currentGrid.GetUpperBound(0);
            var gridCols = currentGrid.GetUpperBound(1);
            var newGrid = new int[gridRows + 1, gridCols + 1];

            // Loop on all items in the grid excluding the border cells
            for (var i = 1; i < gridRows; i++)
            {
                for (var j = 1; j < gridCols; j++)
                {
                    // Count the live neighbours for each individual cell
                    var liveNeighbours = Grid.GetLiveNeighboursTotalForOneCell(currentGrid, i, j);

                    // If current cell is alive
                    if (currentGrid[i, j] == 1)
                    {
                        newGrid[i, j] = liveNeighbours == 2 || liveNeighbours == 3 ? 1 : 0;
                    }
                    else
                    {
                        newGrid[i, j] = liveNeighbours == 3 ? 1 : 0;
                    }
                }
            }

            return newGrid;
        }

        /// <summary>
        /// Get the total value for all the neighbours of a cell
        /// </summary>
        /// <param name="currentGrid">The current grid</param>
        /// <param name="row">The current row being calculating</param>
        /// <param name="col">The current column being calculated</param>
        /// <returns>An int containing the total number of live neighbour cells</returns>
        public static int GetLiveNeighboursTotalForOneCell(int[,] currentGrid, int row, int col)
        {
            var total = 0;

            for (var i = -1; i < 2; i++)
            {
                for (var j = -1; j < 2; j++)
                {
                    if (currentGrid[row + i, col + j] == 1)
                    {
                        total++;
                    }
                }
            }

            // If the current cell is alive remove one as it would have been included in the count
            return currentGrid[row, col] == 1 ? total-1 : total;
        }

        /// <summary>
        /// Render a grid to display the state of its cells
        /// </summary>
        /// <param name="grid">The grid to render</param>
        /// <param name="description">A description of the grid being rendered</param>
        public static void RenderGrid(int[,] grid, string description)
        {
            // Get the number of rows and columns in the grid
            var rowCount = grid.GetUpperBound(0);
            var colCount = grid.GetUpperBound(1);

            Console.WriteLine(description + ":");

            // Render the grid writing a character or blank space per cell and a new line for each row
            for (var i = 0; i <= rowCount; i++)
            {
                for (var j = 0; j <= colCount; j++)
                {
                    Console.Write(grid[i, j] == 0 ? "| " : "|X");
                }
                Console.Write("|");
                Console.WriteLine("");
            }

            Console.WriteLine("--------");
        }
    }
}
