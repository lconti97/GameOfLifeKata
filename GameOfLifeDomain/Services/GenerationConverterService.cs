using GameOfLifeDomain.Models;
using System;

namespace GameOfLifeDomain.Services
{
    public class GenerationConverterService : IGenerationConverterService
    {
        private ILifeStateGeneratorService lifeStateGeneratorService;
        public GenerationConverterService(ILifeStateGeneratorService lifeStateGeneratorService)
        {
            this.lifeStateGeneratorService = lifeStateGeneratorService;
        }
        public Generation CreateGeneration(Int32[,] generationsSinceAliveGrid)
        {
            var rows = generationsSinceAliveGrid.GetLength(0);
            var columns = generationsSinceAliveGrid.GetLength(1);

            var cellGrid = new Cell[rows, columns];

            for (var row = 0; row < rows; row++)
                for (var column = 0; column < columns; column++)
                    cellGrid[row, column] = new Cell(lifeStateGeneratorService.Generate(generationsSinceAliveGrid[row, column]));

            return new Generation(cellGrid);
        }

        public Int32[,] CreateGenerationsSinceAliveGrid(Generation generation)
        {
            var cellGrid = generation.Cells;
            var rows = cellGrid.GetLength(0);
            var columns = cellGrid.GetLength(1);

            var nums = new Int32[rows, columns];

            for (var row = 0; row < rows; row++)
                for (var column = 0; column < columns; column++)
                    nums[row, column] = cellGrid[row, column].LifeState.GenerationsSinceAlive;

            return nums;
        }
    }
}
