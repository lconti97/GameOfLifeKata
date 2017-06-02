using GameOfLifeKata.Models;

namespace GameOfLifeKata.Domain
{
    public interface IGameOfLife
    {
        Generation GetNextGeneration(Generation currentGeneration);
    }
}