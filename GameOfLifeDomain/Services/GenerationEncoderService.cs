using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
using System;

namespace GameOfLifeDomain.Services
{
    public class GenerationEncoderService : IGenerationEncoderService
    {
        public const Int32 NeverAliveCode = -1;
        public const Int32 AliveCode = 0;

        public Generation Decode(Int32[,] grid)
        {
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            var cellGrid = new Cell[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var lifeState = DecodeLifeState(grid[row, column]);
                    cellGrid[row, column] = new Cell(lifeState);
                }
            }

            return new Generation(cellGrid);
        }

        public Int32[,] Encode(Generation generation)
        {
            var cellGrid = generation.Cells;
            var rows = cellGrid.GetLength(0);
            var columns = cellGrid.GetLength(1);

            var grid = new Int32[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var lifeStateCode = EncodeLifeState(cellGrid[row, column].LifeState);
                    grid[row, column] = lifeStateCode;
                }
            }

            return grid;
        }

        private Int32 EncodeLifeState(ILifeState lifeState)
        {
            if (lifeState is NeverAliveLifeState)
                return NeverAliveCode;
            else if (lifeState is AliveLifeState)
                return AliveCode;
            else
                return (lifeState as DeadLifeState).GenerationsSinceAlive;
        }

        private ILifeState DecodeLifeState(Int32 lifeStateCode)
        {
            if (lifeStateCode == NeverAliveCode)
                return new NeverAliveLifeState();
            else if (lifeStateCode == AliveCode)
                return new AliveLifeState();
            else
                return new DeadLifeState(lifeStateCode);
        }
    }
}
