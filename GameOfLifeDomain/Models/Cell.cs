using GameOfLifeDomain.Models;
using System;

namespace GameOfLifeKata.Models
{
    public class Cell
    {
        public LifeState State { get; set; }
        public Int32 GenerationsSinceAlive { get; set; }

        public Cell(LifeState state)
        {
            State = state;

            if (state == LifeState.Alive)
                GenerationsSinceAlive = 0;
            else if (state == LifeState.Dead)
                GenerationsSinceAlive = 1;
            else if (state == LifeState.NeverAlive)
                GenerationsSinceAlive = -1;
        }

        public Cell(LifeState state, Int32 generationsSinceAlive)
        {
            State = state;
            GenerationsSinceAlive = generationsSinceAlive;
        }

        public Boolean IsAlive()
        {
            return State == LifeState.Alive;
        }
    }
}