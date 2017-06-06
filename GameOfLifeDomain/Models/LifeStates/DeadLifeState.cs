using System;

namespace GameOfLifeDomain.Models.LifeStates
{
    public class DeadLifeState : ILifeState
    {
        public int GenerationsSinceAlive { get; set; }

        public DeadLifeState()
        {
            GenerationsSinceAlive = 1;
        }

        public DeadLifeState(Int32 generationsSinceAlive)
        {
            GenerationsSinceAlive = generationsSinceAlive;
        }
    }
}
