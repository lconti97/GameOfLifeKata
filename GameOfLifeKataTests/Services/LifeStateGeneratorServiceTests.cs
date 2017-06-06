using GameOfLifeDomain.Models.LifeStates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLifeDomain.Services;

namespace GameOfLifeKataTests.Services
{
    [TestClass]
    public class LifeStateGeneratorServiceTests
    {
        private LifeStateGeneratorService lifeStateGenerator;

        public LifeStateGeneratorServiceTests()
        {
            lifeStateGenerator = new LifeStateGeneratorService();
        }

        [TestMethod]
        public void GenerateWithGenerationsSinceAliveNegativeOneReturnsNewNeverAliveLifeState()
        {
            var resultLifeState = lifeStateGenerator.Generate(-1);

            Assert.IsInstanceOfType(resultLifeState, typeof(NeverAliveLifeState));
        }

        [TestMethod]
        public void GenerateWithGenerationsSinceAliveZeroReturnsNewAliveLifeState()
        {
            var resultLifeState = lifeStateGenerator.Generate(0);

            Assert.IsInstanceOfType(resultLifeState, typeof(AliveLifeState));
        }

        [TestMethod]
        public void GenerateWithGenerationsSinceAliveOneReturnsNewDeadLifeState()
        {
            var resultLifeState = lifeStateGenerator.Generate(1);

            Assert.IsInstanceOfType(resultLifeState, typeof(DeadLifeState));
        }

        [TestMethod]
        public void GenerateWithGenerationsSinceAliveThreeReturnsDeadLifeStateWithGenerationsSinceAliveThree()
        {
            var resultLifeState = lifeStateGenerator.Generate(3);

            Assert.IsInstanceOfType(resultLifeState, typeof(DeadLifeState));
            Assert.AreEqual(3, resultLifeState.GenerationsSinceAlive);
        }
    }
}
