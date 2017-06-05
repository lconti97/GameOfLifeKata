﻿using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
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
            Int32 rows = currentGeneration.Cells.GetLength(0);
            Int32 columns = currentGeneration.Cells.GetLength(1);

            var nextGenerationCells = new Cell[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var cell = currentGeneration.Cells[row, column];

                    var aliveNeighborsCount = cellNeighborService.GetAliveNeighborsCount(currentGeneration.Cells, row, column);
                    UpdateCell(cell, CellWillBeAlive(cell, aliveNeighborsCount));

                    nextGenerationCells[row, column] = currentGeneration.Cells[row, column];
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

        private void UpdateCell(Cell cell, Boolean willBeAlive)
        {
            if (cell.LifeState is NeverAliveLifeState && willBeAlive) {
                cell.LifeState = new AliveLifeState();
            }
            else if (cell.LifeState is AliveLifeState && !willBeAlive) {
                cell.LifeState = new DeadLifeState();
            }
            else if (cell.LifeState is DeadLifeState) {
                if (willBeAlive)
                    cell.LifeState = new AliveLifeState();
                else 
                    cell.LifeState.GenerationsSinceAlive++;
            }
            /*
            if (cell.State == LifeState.NeverAlive)
            {
                if (willBeAlive)
                {
                    cell.State = LifeState.Alive;
                    cell.GenerationsSinceAlive = 0;
                }
            }
            else if (cell.State == LifeState.Alive)
            {
                if (!willBeAlive)
                {
                    cell.State = LifeState.Dead;
                    cell.GenerationsSinceAlive = 1;
                }
            }
            else if (cell.State == LifeState.Dead)
            {
                if (willBeAlive)
                {
                    cell.State = LifeState.Alive;
                    cell.GenerationsSinceAlive = 0;
                }
                else
                {
                    cell.GenerationsSinceAlive++;
                }
            }
            */
        }
    }
}