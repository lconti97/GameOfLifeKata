using GameOfLifeDomain.Models.LifeStates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeKataTests.Models.LifeStateTests
{
    [TestClass]
    public class DeadStateTests
    {
        [TestMethod]
        public void NewDeadLifeStateHasGenerationsSinceAliveOne()
        {
            var deadLifeState = new DeadLifeState();

            Assert.AreEqual(1, deadLifeState.GenerationsSinceAlive);
        }

        [TestMethod]
        public void NewDeadLifeStateGivenGenerationsSinceAliveFiveHasGenerationsSinceAliveFive()
        {
            var deadLifeState = new DeadLifeState(5);

            Assert.AreEqual(5, deadLifeState.GenerationsSinceAlive);
        }
    }
}