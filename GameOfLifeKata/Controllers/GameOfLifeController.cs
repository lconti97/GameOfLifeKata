using GameOfLifeKata.Domain;
using GameOfLifeKata.Models;
using GameOfLifeKata.Validators;
using System;
using System.Web.Http;

namespace GameOfLifeKata.Controllers
{
    public class GameOfLifeController : ApiController
    {
        private IGameOfLife gameOfLife;
        private IGenerationValidator generationValidator;

        public GameOfLifeController(IGameOfLife gameOfLife, IGenerationValidator generationValidator)
        {
            this.gameOfLife = gameOfLife;
            this.generationValidator = generationValidator;
        }

        [HttpPost]
        public IHttpActionResult AdvanceGeneration(Generation currentGeneration)
        {
            try
            {
                generationValidator.Validate(currentGeneration);
                var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);
                return Ok(nextGeneration);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
