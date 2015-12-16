using System.Linq;
using NUnit.Framework;

namespace Life.Tests
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void Grid_Should_Return_Correct_Dimensions_On_Create()
        {
            // Arrange
            var seedData = new int[5, 5];

            // Act
            var grid = Grid.CreateGrid(seedData);

            // Assert
            Assert.AreEqual(seedData.GetUpperBound(0), grid.GetUpperBound(0));
            Assert.AreEqual(seedData.GetUpperBound(1), grid.GetUpperBound(1));
        }

        [Test]
        public void Grid_Should_Return_Correct_Data_On_Create()
        {
            // Arrange
            var seedData = Utils.GenerateSeedData(5, 5);

            // Act
            var grid = Grid.CreateGrid(seedData);

            // Check that the values in both 2 dimensional arrays are equal
            var equal =
                seedData.Rank == grid.Rank &&
                Enumerable
                    .Range(0, seedData.Rank)
                    .All(dimension => seedData.GetLength(dimension) == grid.GetLength(dimension)) &&
                seedData.Cast<int>().SequenceEqual(grid.Cast<int>());

            // Assert
            Assert.IsTrue(equal);
        }

        [Test]
        public void Grid_Should_Have_Border_Of_Cells_All_With_Zero_Values()
        {
            // Arrange
            int rows = 5, cols = 5;
            var allZeroes = true;
            var seedData = Utils.GenerateSeedData(rows, cols);

            // Act
            var grid = Grid.CreateGrid(seedData);

            // Check all top and bottom row cells are 0
            for (var i = 0; i < cols; i++)
            {
                if ((grid[0, i] != 0) || (grid[rows-1, i] != 0))
                {
                    allZeroes = false;
                }
            }

            // Check all left-most and right-most column cells are 0
            for (var i = 0; i < rows; i++)
            {
                if ((grid[i, 0] != 0) || (grid[i, cols - 1] != 0))
                {
                    allZeroes = false;
                }
            }

            // Assert 
            Assert.IsTrue(allZeroes);
        }

        [Test]
        public void Grid_GetLiveNeighbours_Should_Return_Correct_Live_Cell_Count_For_Block()
        {
            // Arrange
            int[,] gridBlock = {
                {0, 0, 0, 0},
                {0, 1, 1, 0},
                {0, 1, 1, 0},
                {0, 0, 0, 0}
            };
            var gridRows = gridBlock.GetUpperBound(0);
            var gridCols = gridBlock.GetUpperBound(1);
            var newGridBlock = new int[gridRows+1, gridCols+1];

            // Act
            // Loop on all items in the grid excluding the border cells
            for (var i = 1; i < gridRows; i++)
            {
                for (var j = 1; j < gridCols; j++)
                {
                    // Count the live neighbours for each individual cell
                    var liveNeighbours = Grid.GetLiveNeighboursTotalForOneCell(gridBlock, i, j);

                    // If current cell is alive
                    if (gridBlock[i, j] == 1)
                    {
                        newGridBlock[i, j] = liveNeighbours == 2 || liveNeighbours == 3 ? 1 : 0;
                    }
                    else
                    {
                        newGridBlock[i, j] = liveNeighbours == 3 ? 1 : 0;
                    }
                }
            }

            // The block pattern doesn't change, so the new grid should be the same as the original
            var equal =
                gridBlock.Rank == newGridBlock.Rank &&
                Enumerable
                    .Range(0, gridBlock.Rank)
                    .All(dimension => gridBlock.GetLength(dimension) == newGridBlock.GetLength(dimension)) &&
                gridBlock.Cast<int>().SequenceEqual(newGridBlock.Cast<int>());

            // Assert
            Assert.IsTrue(equal);
        }

        [Test]
        public void Grid_GetLiveNeighbours_Should_Return_Correct_Live_Cell_Count_For_Oscillator_Blinker()
        {
            // Arrange
            int[,] gridOscillatorBlinker = {
                {0, 0, 0, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0}
            };
            int[,] gridOscillatorBlinkerNextExpected = {
                {0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0}
            };
            var gridRows = gridOscillatorBlinker.GetUpperBound(0);
            var gridCols = gridOscillatorBlinker.GetUpperBound(1);
            var newGridOscillator = new int[gridRows + 1, gridCols + 1];

            // Act
            // Loop on all items in the grid excluding the border cells
            for (var i = 1; i < gridRows; i++)
            {
                for (var j = 1; j < gridCols; j++)
                {
                    var liveNeighbours = Grid.GetLiveNeighboursTotalForOneCell(gridOscillatorBlinker, i, j);

                    if (gridOscillatorBlinker[i, j] == 1)
                    {
                        newGridOscillator[i, j] = liveNeighbours == 2 || liveNeighbours == 3 ? 1 : 0;
                    }
                    else
                    {
                        newGridOscillator[i, j] = liveNeighbours == 3 ? 1 : 0;
                    }
                }
            }

            // The new grid should equal gridOscillatorBlinkerNext
            var equal =
                gridOscillatorBlinkerNextExpected.Rank == newGridOscillator.Rank &&
                Enumerable
                    .Range(0, gridOscillatorBlinkerNextExpected.Rank)
                    .All(dimension => gridOscillatorBlinkerNextExpected.GetLength(dimension) == newGridOscillator.GetLength(dimension)) &&
                gridOscillatorBlinkerNextExpected.Cast<int>().SequenceEqual(newGridOscillator.Cast<int>());

            // Assert
            Assert.IsTrue(equal);
        }

        [Test]
        public void Grid_GetLiveNeighbours_Should_Return_Correct_Live_Cell_Count_For_Oscillator_Toad()
        {
            // Arrange
            int[,] gridOscillatorToad = {
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 1, 1, 1, 0},
                {0, 1, 1, 1, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
            };
            int[,] gridOscillatorToadNextExpected = {
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0},
                {0, 1, 0, 0, 1, 0},
                {0, 1, 0, 0, 1, 0},
                {0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
            };
            var gridRows = gridOscillatorToad.GetUpperBound(0);
            var gridCols = gridOscillatorToad.GetUpperBound(1);
            var newGridOscillator = new int[gridRows + 1, gridCols + 1];

            // Act
            // Loop on all items in the grid excluding the border cells
            for (var i = 1; i < gridRows; i++)
            {
                for (var j = 1; j < gridCols; j++)
                {
                    var liveNeighbours = Grid.GetLiveNeighboursTotalForOneCell(gridOscillatorToad, i, j);

                    if (gridOscillatorToad[i, j] == 1)
                    {
                        newGridOscillator[i, j] = liveNeighbours == 2 || liveNeighbours == 3 ? 1 : 0;
                    }
                    else
                    {
                        newGridOscillator[i, j] = liveNeighbours == 3 ? 1 : 0;
                    }
                }
            }

            // The new grid should equal gridOscillatorToadNext
            var equal =
                gridOscillatorToadNextExpected.Rank == newGridOscillator.Rank &&
                Enumerable
                    .Range(0, gridOscillatorToadNextExpected.Rank)
                    .All(dimension => gridOscillatorToadNextExpected.GetLength(dimension) == newGridOscillator.GetLength(dimension)) &&
                gridOscillatorToadNextExpected.Cast<int>().SequenceEqual(newGridOscillator.Cast<int>());

            // Assert
            Assert.IsTrue(equal);
        }

        [Test]
        public void Grid_GetNextGeneration_Should_Return_Correct_Grid_For_Block()
        {
            // Arrange
            int[,] gridBlock = {
                {0, 0, 0, 0},
                {0, 1, 1, 0},
                {0, 1, 1, 0},
                {0, 0, 0, 0}
            };

            // Act
            // Get next generation data for block pattern
            var gridBlockNext = Grid.GetNextGenerationData(gridBlock);

            // The block pattern doesn't change, so the new grid should be the same as the original
            var equal =
                gridBlock.Rank == gridBlockNext.Rank &&
                Enumerable
                    .Range(0, gridBlock.Rank)
                    .All(dimension => gridBlock.GetLength(dimension) == gridBlockNext.GetLength(dimension)) &&
                gridBlock.Cast<int>().SequenceEqual(gridBlockNext.Cast<int>());

            // Assert
            Assert.IsTrue(equal);
        }

        [Test]
        public void Grid_GetNextGeneration_Should_Return_Correct_Grid_For_Oscillator_Blinker()
        {
            // Arrange
            int[,] gridOscillatorBlinker = {
                {0, 0, 0, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0}
            };
            int[,] gridOscillatorBlinkerNextExpected = {
                {0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0},
                {0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0}
            };

            // Act
            // Get next generation data
            var gridOscillatorBlinkerNext = Grid.GetNextGenerationData(gridOscillatorBlinker);

            // The new grid should equal gridOscillatorBlinkerNext
            var equal =
                gridOscillatorBlinkerNextExpected.Rank == gridOscillatorBlinkerNext.Rank &&
                Enumerable
                    .Range(0, gridOscillatorBlinkerNextExpected.Rank)
                    .All(dimension => gridOscillatorBlinkerNextExpected.GetLength(dimension) == gridOscillatorBlinkerNext.GetLength(dimension)) &&
                gridOscillatorBlinkerNextExpected.Cast<int>().SequenceEqual(gridOscillatorBlinkerNext.Cast<int>());

            // Assert
            Assert.IsTrue(equal);
        }

        [Test]
        public void Grid_GetNextGeneration_Should_Return_Correct_Grid_For_Oscillator_Toad()
        {
            // Arrange
            int[,] gridOscillatorToad = {
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 1, 1, 1, 0},
                {0, 1, 1, 1, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
            };
            int[,] gridOscillatorToadNextExpected = {
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0},
                {0, 1, 0, 0, 1, 0},
                {0, 1, 0, 0, 1, 0},
                {0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
            };

            // Act
            // Get next generation data
            var gridOscillatorToadNext = Grid.GetNextGenerationData(gridOscillatorToad);

            // The new grid should equal gridOscillatorToadNext
            var equal =
                gridOscillatorToadNextExpected.Rank == gridOscillatorToadNext.Rank &&
                Enumerable
                    .Range(0, gridOscillatorToadNextExpected.Rank)
                    .All(dimension => gridOscillatorToadNextExpected.GetLength(dimension) == gridOscillatorToadNext.GetLength(dimension)) &&
                gridOscillatorToadNextExpected.Cast<int>().SequenceEqual(gridOscillatorToadNext.Cast<int>());

            // Assert
            Assert.IsTrue(equal);
        }
    }
}
