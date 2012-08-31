using ChessEngineLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests
{
    /// <summary>
    /// Yksikkötestit luokalle, jonka tehtävänä on sisältää paikkatiedot pelilaudalla.
    /// </summary>
    /// <remarks>
    ///     Changes
    ///         06.08.2012 Timo Paajanen
    ///             Lisätty testit ja toteutus GetDirectionTo-metodille, joka palauttaa tiedon
    ///             mihin suuntaan ollaan laudalla kulkemassa kahden paikan välillä.
    ///         08.08.2012 Timo Paajanen
    ///             Lisätty PositionStatus ja muutettu alustus niin että käytetään konstruktoria.
    /// </remarks>
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void GetDirection_MoveFromFirstToSecondRank_ReturnsForward()
        {
            Position origin = new Position(1, 1, PositionStatus.White);
            Position destination = new Position(1, 2, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.Forward, result);
        }

        [TestMethod]
        public void GetDirection_MoveFromFirstToThirdRank_ReturnForward()
        {
            Position origin = new Position(1, 1, PositionStatus.White);
            Position destination = new Position(1, 3, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.Forward, result);
        }

        [TestMethod]
        public void GetDirection_MoveFromThirdToFirstRank_ReturnBackward()
        {
            Position origin = new Position(1, 3, PositionStatus.White);
            Position destination = new Position(1, 2, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.Backward, result);
        }

        [TestMethod]
        public void GetDirection_MoveFromAFileToBFileOnFirstRank_ReturnsRight()
        {
            Position origin = new Position(1, 1, PositionStatus.White);
            Position destination = new Position(2, 1, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.Right, result);
        }

        [TestMethod]
        public void GetDirection_MoveFromBFileToAFileOnFirstRank_ReturnsLeft()
        {
            Position origin = new Position(2, 1, PositionStatus.White);
            Position destination = new Position(1, 1, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.Left, result);
        }
        
        [TestMethod]
        public void GetDirection_MoveARankForwardAndAFileForward_ReturnsForwardOnRightDiagonal()
        {
            Position origin = new Position(1, 1, PositionStatus.White);
            Position destination = new Position(2, 2, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.ForwardOnRightDiagonal, result);
        }

        [TestMethod]
        public void GetDirection_MoveARankBackwardAndAFileBackward_ReturnsBackwardOnRightDiagonal()
        {
            Position origin = new Position(2, 2, PositionStatus.White);
            Position destination = new Position(1, 1, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.BackwardOnRightDiagonal, result);
        }

        [TestMethod]
        public void GetDirection_MoveARankForwardAndAFileBackward_ReturnsForwardOnLeftDiagonal()
        {
            Position origin = new Position(2, 1, PositionStatus.White);
            Position destination = new Position(1, 2, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.ForwardOnLeftDiagonal, result);
        }

        [TestMethod]
        public void GetDirection_MoveARankBackwardAndAFileForward_ReturnsBackwardsOnLeftDiagonal()
        {
            Position origin = new Position(1, 2, PositionStatus.White);
            Position destination = new Position(2, 1, PositionStatus.White);

            Direction result = origin.GetDirectionTo(destination);

            Assert.AreEqual(Direction.BackwardOnLeftDiagonal, result);
        }
    }
}
