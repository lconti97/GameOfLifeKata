using GameOfLifeDomain;
using GameOfLifeDomain.Services;
using System;
using System.Web.Http;

namespace GameOfLifeKata.Controllers
{
    public class GameOfLifeController : ApiController
    {
        private IGameOfLife gameOfLife;
        private IGenerationEncoderService generationConverterService;

        public GameOfLifeController(IGameOfLife gameOfLife, IGenerationEncoderService generationConverterService)
        {
            this.gameOfLife = gameOfLife;
            this.generationConverterService = generationConverterService;
        }

        [HttpPost]
        public IHttpActionResult PostInitialGeneration(Int32[,] encodedInitialGeneration)
        {
            try
            {
                var initialGeneration = generationConverterService.Decode(encodedInitialGeneration);
                GameOfLifeData.CurrentGeneration = initialGeneration;
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetNextGeneration()
        {
            try
            {
                var nextGeneration = gameOfLife.GetNextGeneration(GameOfLifeData.CurrentGeneration);
                GameOfLifeData.CurrentGeneration = nextGeneration;

                return Ok(generationConverterService.Encode(nextGeneration));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}