using System;

namespace GameOfLifeKata.Services
{
    public interface ICellNeighborService
    {
        Int32 GetAliveNeighborsCount(Int32[,] cells, Int32 row, Int32 column);
    }
}