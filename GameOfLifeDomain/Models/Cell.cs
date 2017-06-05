using GameOfLifeDomain.Models.LifeStates;
using System;

namespace GameOfLifeKata.Models
{
    public class Cell
    {
        public ILifeState LifeState { get; set; }

        public Cell(ILifeState state)
        {
            LifeState = state;
        }

        public Boolean IsAlive()
        {
            return LifeState is AliveLifeState;
        }
    }
}