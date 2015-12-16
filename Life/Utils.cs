using System;

namespace Life
{
    /// <summary>
    /// A class for various utility functions
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Generate seed data for a 2 dimensional array based on random 0 or 1 values
        /// </summary>
        /// <param name="rows">The upper bound of the first dimension of the array</param>
        /// <param name="cols">The upper bound of the second dimension of the array</param>
        /// <returns>A 2 dimensional array containing random 1 or 0 values</returns>
        public static int[,] GenerateSeedData(int rows, int cols)
        {
            // Init a new 2 dimensional array based on the bounds passed in
            var data = new int[rows, cols];
            var rnd = new Random();

            // Set a random value in each cell, either 1 or 0
            // Fill from 1 to count - 1 for rows and cols, this leaves a border of cells with all 0 values
            for (var row = 1; row < rows - 1; row++)
            {
                for (var col = 1; col < cols - 1; col++)
                {
                    data[row, col] = (int)Math.Round(rnd.NextDouble());
                }
            }

            return data;
        }
    }
}
