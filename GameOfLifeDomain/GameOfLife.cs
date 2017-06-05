using GameOfLifeDomain.Models;
using GameOfLifeKata.Models;
using GameOfLifeKata.Services;
using System;

namespace GameOfLifeKata.Domain
{
    public class GameOfLife : IGameOfLife
    {
        private ICellNeighborService cellNeighborService;

        public GameOfLife(ICellNeighborService cellNeighborService)
        {
            this.cellNeighborService = cellNeighborService;
        }

        public Generation GetNextGeneration(Generation currentGeneration)
        {
            var nextGenerationCells = new Cell[Generation.Rows, Generation.Columns];
            for (var row = 0; row < Generation.Rows; row++)
            {
                for (var column = 0; column < Generation.Columns; column++)
                {
                    var aliveNeighborsCount = cellNeighborService.GetAliveNeighborsCount(currentGeneration.Cells, row, column);
                    if (aliveNeighborsCount == 3 || (currentGeneration.Cells[row, column].IsAlive() && aliveNeighborsCount == 2))
                        UpdateCell(currentGeneration.Cells[row, column], true);
                    else
                        UpdateCell(currentGeneration.Cells[row, column], false);

                    nextGenerationCells[row, column] = currentGeneration.Cells[row, column];
                }
            }

            return new Generation(nextGenerationCells);
        }

        private void UpdateCell(Cell cell, Boolean survivedGeneration)
        {
            if (survivedGeneration)
            {
                cell.State = LifeState.Alive;
                cell.GenerationsSinceAlive = 0;
            }
            else if (cell.State == LifeState.Alive)
            {
                cell.State = LifeState.Dead;
                cell.GenerationsSinceAlive = 1;
            }
            else if (cell.State == LifeState.Dead)
            {
                cell.GenerationsSinceAlive++;
            }
        }
    }
}