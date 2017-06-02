using GameOfLifeKata.Models;
using GameOfLifeKata.Services;

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
            for (var row = 0; row < Generation.Rows; row++)
            {
                for (var column = 0; column < Generation.Columns; column++)
                {
                    var neighbors = cellNeighborService.GetAliveNeighborsCount(currentGeneration.Cells, row, column);
                }
            }

            return currentGeneration;
        }
    }
}