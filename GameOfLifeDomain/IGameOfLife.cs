using GameOfLifeDomain.Models;

namespace GameOfLifeDomain
{
    public interface IGameOfLife
    {
        Generation GetNextGeneration(Generation currentGeneration);
    }
}