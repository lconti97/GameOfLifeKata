using System;

namespace GameOfLifeDomain.Models.LifeStates
{
    public class NeverAliveLifeState : ILifeState
    {
        public const Int32 DefaultGenerationsSinceAlive = -1;
        public Int32 GenerationsSinceAlive { get; set; }

        public NeverAliveLifeState()
        {
            GenerationsSinceAlive = -1;
        }
    }
}
