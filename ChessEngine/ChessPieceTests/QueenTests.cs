using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class QueenTests : ChessEngineTestBase
    {
        [TestInitialize]
        public void InitializationBeforeEveryTest()
        {
            InitializeBoard();
        }

        [TestMethod]
        public void Equals_TwoOppositeColorQueens_AreNotEqual()
        {
            Queen queen1 = new Queen(new Board(), PieceColor.White);
            Queen queen2 = new Queen(new Board(), PieceColor.Black);

            Assert.AreNotEqual(queen1, queen2);
        }

        [TestMethod]
        public void Equals_TwoSameColorQueens_AreEqual()
        {
            Queen queen1 = new Queen(new Board(), PieceColor.White);
            Queen queen2 = new Queen(new Board(), PieceColor.White);
            Queen queen3 = new Queen(new Board(), PieceColor.Black);
            Queen queen4 = new Queen(new Board(), PieceColor.Black);

            Assert.AreEqual(queen1, queen2);
            Assert.AreEqual(queen3, queen4);
        }

        [TestMethod]
        public void IsLegalMove_WhiteQueenMovesOneSquareForward_ReturnsTrue()
        {
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(4, 1), GetSquare(4, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteQueenMovesFiveSquaresForwardAndThreeSquaresRight_ReturnsFalse()
        {
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(4, 1), GetSquare(7, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteQueenMovesTwoSquaresForwardAndOneSquareRight_ReturnsFalse()
        {
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(4, 1), GetSquare(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteQueenMovesOneSquareForwardOnTheRightDiagonalToPositionOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));
            Board.SetSquare(5, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(4, 1), GetSquare(5, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackQueenMovesOneSquareForwardOnTheRightDiagonalToPositionOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(5, 8, new Queen(Board, PieceColor.Black));
            Board.SetSquare(4, 7, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(4, 7));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteQueenMovesOneSquareForwardOnTheRightDiagonalToPositionOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));
            Board.SetSquare(5, 2, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(4, 1), GetSquare(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackQueenMovesOneSquareForwardOnTheRightDiagonalToPositionOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board.SetSquare(4, 8, new Queen(Board, PieceColor.Black));
            Board.SetSquare(3, 7, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(4, 8), GetSquare(3, 7));

            Assert.IsTrue(result);
        }
    }
}
