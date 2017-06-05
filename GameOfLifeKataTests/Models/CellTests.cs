using GameOfLifeDomain.Models;
using GameOfLifeKata.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeKataTests.Models
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void CellCreatedAliveIsAliveReturnsTrue()
        {
            var cell = new Cell(LifeState.Alive);

            Assert.IsTrue(cell.IsAlive());
        }

        [TestMethod]
        public void CellCreatedNotAliveIsAliveReturnsFalse()
        {
            var cell = new Cell(LifeState.NeverAlive);

            Assert.IsFalse(cell.IsAlive());
        }
    }
}
