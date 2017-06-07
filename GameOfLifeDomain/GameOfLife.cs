using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
using GameOfLifeDomain.Services;
using System;

namespace GameOfLifeDomain
{
    public class GameOfLife : IGameOfLife
    {
        public static Generation currentGeneration = new Generation(50, 50);
        private ICellNeighborService cellNeighborService;

        public GameOfLife(ICellNeighborService cellNeighborService)
        {
            this.cellNeighborService = cellNeighborService;
        }

        public Generation GetNextGeneration(Generation currentGeneration)
        {
            Int32 rows = currentGeneration.Cells.GetLength(0);
            Int32 columns = currentGeneration.Cells.GetLength(1);

            var nextGenerationCells = new Cell[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var cell = currentGeneration.Cells[row, column];

                    var aliveNeighborsCount = cellNeighborService.GetAliveNeighborsCount(currentGeneration.Cells, row, column);
                    var cellWillBeAlive = CellWillBeAlive(cell, aliveNeighborsCount);
                    var nextLifeState = GetNextLifeState(cell.LifeState, cellWillBeAlive);

                    nextGenerationCells[row, column] = new Cell(nextLifeState);
                }
            }

            return new Generation(nextGenerationCells);
        }

        private Boolean CellWillBeAlive(Cell cell, Int32 aliveNeighborsCount)
        {
            var cellIsAliveAndSurvives = cell.IsAlive() && (aliveNeighborsCount == 2 || aliveNeighborsCount == 3);
            var cellIsNotAliveAndWillBePopulated = !cell.IsAlive() && aliveNeighborsCount == 3;

            return cellIsAliveAndSurvives || cellIsNotAliveAndWillBePopulated;
        }

        private ILifeState GetNextLifeState(ILifeState lifeState, Boolean willBeAlive)
        {
            if (lifeState is NeverAliveLifeState && willBeAlive) {
                return new AliveLifeState();
            }
            else if (lifeState is AliveLifeState && !willBeAlive) {
                return new DeadLifeState();
            }
            else if (lifeState is DeadLifeState) {
                if (willBeAlive)
                    return new AliveLifeState();
                else 
                    (lifeState as DeadLifeState).GenerationsSinceAlive++;
            }
            return lifeState;
        }
    }
}