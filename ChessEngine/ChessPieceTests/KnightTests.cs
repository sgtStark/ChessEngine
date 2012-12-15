using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class KnightTests : ChessEngineTestBase
    {
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
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 1, new Knight(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(2, 1), board.GetSquare(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndTwoSquaresRight_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 1, new Knight(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(2, 1), board.GetSquare(4, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 1, new Knight(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(2, 1), board.GetSquare(4, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareKnightMovesTwoSquaresForwardAndOneSquareLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(7, 1, new Knight(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(7, 1), board.GetSquare(6, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareKnightMovesOneSquareForwardAndTwoSquaresLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(7, 1, new Knight(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(7, 1), board.GetSquare(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresBackwardAndOneSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(3, 3, new Knight(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(3, 3), board.GetSquare(2, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareBackwardAndTwoSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(4, 2, new Knight(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(4, 2), board.GetSquare(2, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareKnightMovesTwoSquaresForwardAndOneSquareRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 8, new Knight(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetSquare(2, 8), board.GetSquare(1, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareKnightMovesOneSquareForwardAndTwoSquaresLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 8, new Knight(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetSquare(2, 8), board.GetSquare(4, 7));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndOneSquareRightWhileObscuredInFront_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 1, new Knight(board, PieceColor.White));
            board.SetSquare(2, 2, new Pawn(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(2, 1), board.GetSquare(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRightToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 1, new Knight(board, PieceColor.White));
            board.SetSquare(3, 3, new Pawn(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetSquare(2, 1), board.GetSquare(3, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRightToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetSquare(2, 1, new Knight(board, PieceColor.White));
            board.SetSquare(3, 3, new Pawn(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetSquare(2, 1), board.GetSquare(3, 3));

            Assert.IsTrue(result);
        }
    }
}
