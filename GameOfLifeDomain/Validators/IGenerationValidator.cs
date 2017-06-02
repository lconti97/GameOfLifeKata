using GameOfLifeKata.Models;

namespace GameOfLifeKata.Validators
{
    public interface IGenerationValidator
    {
        void Validate(Generation generation);
    }
}