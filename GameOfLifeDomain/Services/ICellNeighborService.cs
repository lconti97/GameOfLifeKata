using GameOfLifeKata.Models;
using System;

namespace GameOfLifeKata.Services
{
    public interface ICellNeighborService
    {
        Int32 GetAliveNeighborsCount(Cell[,] cells, Int32 row, Int32 column);
    }
}