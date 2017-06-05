using GameOfLifeKata.Controllers;
using GameOfLifeKata.Domain;
using GameOfLifeKata.Models;
using GameOfLifeKata.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http.Results;

namespace GameOfLifeKataTests.Controllers
{
    [TestClass]
    public class GameOfLifeControllerTests
    {
        private Mock<IGameOfLife> mockGameOfLife;
        private Mock<IGenerationValidator> mockGenerationValidator;
        private GameOfLifeController controller;
        private Generation currentGeneration;

        public GameOfLifeControllerTests()
        {
            mockGameOfLife = new Mock<IGameOfLife>();
            mockGenerationValidator = new Mock<IGenerationValidator>();
            controller = new GameOfLifeController(mockGameOfLife.Object, mockGenerationValidator.Object);
            currentGeneration = new Generation(50, 50);
        }

        [TestMethod]
        public void AdvanceGenerationReturnsOkResultsOnSuccess()
        {
            var result = controller.AdvanceGeneration(currentGeneration) as OkNegotiatedContentResult<Generation>;

            Assert.AreEqual(typeof(OkNegotiatedContentResult<Generation>), result.GetType());
        }

        [TestMethod]
        public void AdvanceGenerationReturnsContentOnSuccess()
        {
            mockGameOfLife.Setup(g => g.GetNextGeneration(currentGeneration)).Returns(currentGeneration);

            var result = controller.AdvanceGeneration(currentGeneration) as OkNegotiatedContentResult<Generation>;

            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void AdvanceGenerationReturnsBadRequestWhenValidationFails()
        {
            mockGenerationValidator.Setup(v => v.Validate(currentGeneration)).Throws(new Exception());
            
            var result = controller.AdvanceGeneration(currentGeneration) as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
        }
    }
}
