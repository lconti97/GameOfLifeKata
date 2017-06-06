using GameOfLifeDomain.Models.LifeStates;
using System;

namespace GameOfLifeDomain.Services
{
    public class LifeStateGeneratorService : ILifeStateGeneratorService
    {
        public ILifeState Generate(Int32 generationsSinceAlive)
        {
            if (generationsSinceAlive == AliveLifeState.DefaultGenerationsSinceAlive)
                return new AliveLifeState();
            else if (generationsSinceAlive == NeverAliveLifeState.DefaultGenerationsSinceAlive)
                return new NeverAliveLifeState();
            else
                return new DeadLifeState(generationsSinceAlive);
        }
    }
}
