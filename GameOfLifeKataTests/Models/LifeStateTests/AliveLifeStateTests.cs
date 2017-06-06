using GameOfLifeDomain.Models.LifeStates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeKataTests.Models.LifeStateTests
{
    [TestClass]
    public class AliveLifeStateTests
    {
        [TestMethod]
        public void NewAliveLifeStateHasGenerationsSinceAliveZero()
        {
            var aliveLifeState = new AliveLifeState();

            Assert.AreEqual(0, aliveLifeState.GenerationsSinceAlive);
        }
    }
}
