using GameOfLifeKata.Models;
using System;

namespace GameOfLifeKata.Validators
{
    public class GenerationValidator : IGenerationValidator
    {
        public void Validate(Generation generation)
        {
            if (generation == null)
                throw new ArgumentNullException("Generation cannot be null");
            else if (generation.Cells == null)
                throw new ArgumentException("Generation cells cannot be null");
        }
    }
}