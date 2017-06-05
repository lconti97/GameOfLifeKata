using GameOfLifeKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeKata.Services
{
    public class CellNeighborService : ICellNeighborService
    {
        public Int32 GetAliveNeighborsCount(Cell[,] cells, Int32 row, Int32 column)
        {
            var neighbors = GetNeighbors(cells, row, column);

            return neighbors.Where(n => n.IsAlive()).Count();
        }

        private List<Cell> GetNeighbors(Cell[,] cells, Int32 row, Int32 column)
        {
            var neighbors = new List<Cell>();

            for (var neighborRow = row - 1; neighborRow <= row + 1; neighborRow++)
            {
                for (var neighborColumn = column - 1; neighborColumn <= column + 1; neighborColumn++)
                {
                    var isCenterCell = neighborRow == row && neighborColumn == column;
                    if (IndicesAreInBounds(cells, neighborRow, neighborColumn) && !isCenterCell)
                        neighbors.Add(cells[neighborRow, neighborColumn]);
                }
            }

            return neighbors;
        }

        private Boolean IndicesAreInBounds(Cell[,] cells, Int32 row, Int32 column)
        {
            var rowIsInBounds = 0 <= row && row < cells.GetLength(0);
            var columnIsInBounds = 0 <= column && column < cells.GetLength(1);
            return rowIsInBounds && columnIsInBounds;
        }
    }
}