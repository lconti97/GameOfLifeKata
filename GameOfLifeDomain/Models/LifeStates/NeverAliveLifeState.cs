using System;

namespace GameOfLifeDomain.Models.LifeStates
{
    public class NeverAliveLifeState : ILifeState
    {
        public Int32 GenerationsSinceAlive { get; set; }

        public NeverAliveLifeState()
        {
            GenerationsSinceAlive = -1;
        }
    }
}
