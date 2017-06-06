using GameOfLifeDomain;
using GameOfLifeDomain.Models;
using GameOfLifeDomain.Services;
using System;
using System.Web.Http;

namespace GameOfLifeKata.Controllers
{
    public class GameOfLifeController : ApiController
    {
        private IGameOfLife gameOfLife;
        private IGenerationConverterService generationConverterService;

        public GameOfLifeController(IGameOfLife gameOfLife, IGenerationConverterService generationConverterService)
        {
            this.gameOfLife = gameOfLife;
            this.generationConverterService = generationConverterService;
        }

        [HttpPost]
        public IHttpActionResult PostInitialGeneration(Int32[,] generationsSinceAliveGrid)
        {
            try
            {
                var cells = new Cell[generationsSinceAliveGrid.GetLength(0), generationsSinceAliveGrid.GetLength(1)];
                for (var i = 0; i < cells.GetLength(0); i++)
                    for (var j = 0; j < cells.GetLength(1); j++)
                        cells[i, j] = new Cell(new LifeStateGeneratorService().Generate(generationsSinceAliveGrid[i, j]));

                var initialGeneration = new Generation(cells);
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

                return Ok(generationConverterService.CreateGenerationsSinceAliveGrid(nextGeneration));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
