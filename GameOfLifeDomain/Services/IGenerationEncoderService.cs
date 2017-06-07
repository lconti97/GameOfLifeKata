using GameOfLifeDomain.Models;
using System;

namespace GameOfLifeDomain.Services
{
    public interface IGenerationEncoderService
    {
        Generation Decode(Int32[][] generationsSinceAliveGrid);
        Int32[,] Encode(Generation generation);
    }
}
