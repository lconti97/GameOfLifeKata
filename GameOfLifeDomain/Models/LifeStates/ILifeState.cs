using System;

namespace GameOfLifeDomain.Models.LifeStates
{
    public interface ILifeState {
        
        Int32 GenerationsSinceAlive { get; set; }
    }
}
