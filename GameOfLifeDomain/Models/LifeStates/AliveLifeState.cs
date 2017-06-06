using System;

namespace GameOfLifeDomain.Models.LifeStates
{
    public class AliveLifeState : ILifeState
    {
        public const Int32 DefaultGenerationsSinceAlive = 0;
        public Int32 GenerationsSinceAlive { get; set; }

        public AliveLifeState()
        {
            GenerationsSinceAlive = 0;
        }
    }
}
