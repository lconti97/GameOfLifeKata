using System;

namespace GameOfLifeDomain.Models.LifeStates
{
    public class AliveLifeState : ILifeState
    {
        public Int32 GenerationsSinceAlive { get; set; }

        public AliveLifeState()
        {
            GenerationsSinceAlive = 0;
        }
    }
}
