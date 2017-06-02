using System;

namespace GameOfLifeKata.Models
{
    public class Cell
    {
        public const Int32 NeverAlive = -1;
        
        public Int32 GenerationsSinceAlive { get; private set; }

        public Cell(Boolean isAlive)
        {
            
        }

        //public Boolean HasBeenAlive()
        //{

        //}

        //public Boolean IsAlive()
        //{

        //}
    }
}