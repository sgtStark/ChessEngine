using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class KnightTests : ChessEngineTestBase
    {
        [TestInitialize]
        public void InitializationBeforeEveryTest()
        {
            InitializeBoard();
        }

        [TestMethod]
        public void Equals_TwoOppositeColorKnights_AreNotEqual()
        {
            Knight knight1 = new Knight(new Board(), PieceColor.White);
            Knight knight2 = new Knight(new Board(), PieceColor.Black);

            Assert.AreNotEqual(knight1, knight2);
        }

        [TestMethod]
        public void Equals_TwoSameColorKnights_AreEqual()
        {
            Knight knight1 = new Knight(new Board(), PieceColor.White);
            Knight knight2 = new Knight(new Board(), PieceColor.White);
            Knight knight3 = new Knight(new Board(), PieceColor.Black);
            Knight knight4 = new Knight(new Board(), PieceColor.Black);

            Assert.AreEqual(knight1, knight2);
            Assert.AreEqual(knight3, knight4);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndOneSquaresRight_ReturnsTrue()
        {
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(2, 1), GetSquare(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndTwoSquaresRight_ReturnsFalse()
        {
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(2, 1), GetSquare(4, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRight_ReturnsTrue()
        {
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(2, 1), GetSquare(4, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareKnightMovesTwoSquaresForwardAndOneSquareLeft_ReturnsTrue()
        {
            Board.SetSquare(7, 1, new Knight(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(7, 1), GetSquare(6, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareKnightMovesOneSquareForwardAndTwoSquaresLeft_ReturnsTrue()
        {
            Board.SetSquare(7, 1, new Knight(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(7, 1), GetSquare(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresBackwardAndOneSquaresRight_ReturnsTrue()
        {
            Board.SetSquare(3, 3, new Knight(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(3, 3), GetSquare(2, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareBackwardAndTwoSquaresRight_ReturnsTrue()
        {
            Board.SetSquare(4, 2, new Knight(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(4, 2), GetSquare(2, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareKnightMovesTwoSquaresForwardAndOneSquareRight_ReturnsTrue()
        {
            Board.SetSquare(2, 8, new Knight(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(2, 8), GetSquare(1, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareKnightMovesOneSquareForwardAndTwoSquaresLeft_ReturnsTrue()
        {
            Board.SetSquare(2, 8, new Knight(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(2, 8), GetSquare(4, 7));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndOneSquareRightWhileObscuredInFront_ReturnsTrue()
        {
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));
            Board.SetSquare(2, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(2, 1), GetSquare(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRightToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));
            Board.SetSquare(3, 3, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(2, 1), GetSquare(3, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRightToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));
            Board.SetSquare(3, 3, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(2, 1), GetSquare(3, 3));

            Assert.IsTrue(result);
        }
    }
}
