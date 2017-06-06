using GameOfLifeDomain.Models;
using System;

namespace GameOfLifeDomain.Services
{
    public interface IGenerationConverterService
    {
        Generation CreateGeneration(Int32[,] generationsSinceAliveGrid);
        Int32[,] CreateGenerationsSinceAliveGrid(Generation generation);
    }
}
