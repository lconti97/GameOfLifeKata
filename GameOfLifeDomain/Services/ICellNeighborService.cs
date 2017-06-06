using GameOfLifeDomain.Models;
using System;

namespace GameOfLifeDomain.Services
{
    public interface ICellNeighborService
    {
        Int32 GetAliveNeighborsCount(Cell[,] cells, Int32 row, Int32 column);
    }
}