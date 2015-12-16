using System;

namespace Life
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int rows = 5, cols = 5;
            var seedRandom = Utils.GenerateSeedData(rows, cols);
            int[,] seedBlock = {
                {0, 0, 0, 0},
                {0, 1, 1, 0},
                {0, 1, 1, 0},
                {0, 0, 0, 0}
            };
            int[,] seedOscillatorBlinker = {
                {0, 0, 0, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0}
            };
            int[,] seedOscillatorToad = {
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 1, 1, 1, 0},
                {0, 1, 1, 1, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
            };

            // Create grids
            var gridRandom = Grid.CreateGrid(seedRandom);
            var gridRandomNext = Grid.GetNextGenerationData(gridRandom);
            var gridBlock = Grid.CreateGrid(seedBlock);
            var gridBlockNext = Grid.GetNextGenerationData(gridBlock);
            var gridOscillatorBlinker = Grid.CreateGrid(seedOscillatorBlinker);
            var gridOscillatorBlinkerNext = Grid.GetNextGenerationData(gridOscillatorBlinker);
            var gridOscillatorToad = Grid.CreateGrid(seedOscillatorToad);
            var gridOscillatorToadNext = Grid.GetNextGenerationData(gridOscillatorToad);

            // Render grids
            Grid.RenderGrid(gridRandom, "Random pattern");
            Grid.RenderGrid(gridRandomNext, "Next generation of random pattern");
            Grid.RenderGrid(gridBlock, "Still life block pattern");
            Grid.RenderGrid(gridBlockNext, "Next generation of still life block pattern (unchanged)");
            Grid.RenderGrid(gridOscillatorBlinker, "Oscillator blink pattern");
            Grid.RenderGrid(gridOscillatorBlinkerNext, "Next generation of oscillator blink pattern");
            Grid.RenderGrid(gridOscillatorToad, "Oscillator toad pattern");
            Grid.RenderGrid(gridOscillatorToadNext, "Next generation of oscillator toad pattern");

            Console.ReadLine();
        }
    }
}
