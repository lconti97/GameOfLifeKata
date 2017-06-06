using GameOfLifeDomain.Models.LifeStates;
using System;

namespace GameOfLifeDomain.Services
{
    public interface ILifeStateGeneratorService
    {
        ILifeState Generate(Int32 generationsSinceAlive);
    }
}
