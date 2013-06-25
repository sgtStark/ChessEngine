using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class BishopTests : ChessEngineTestBase
    {
        [TestInitialize]
        public void InitializationBeforeEveryTest()
        {
            InitializeBoard();
        }

        [TestMethod]
        public void Equals_TwoOppositeColorBishops_AreNotEqual()
        {
            Bishop bishop1 = new Bishop(new Board(), PieceColor.White);
            Bishop bishop2 = new Bishop(new Board(), PieceColor.Black);

            Assert.AreNotEqual(bishop1, bishop2);
        }

        [TestMethod]
        public void Equals_TwoSameColorBishops_AreEqual()
        {
            Bishop bishop1 = new Bishop(new Board(), PieceColor.White);
            Bishop bishop2 = new Bishop(new Board(), PieceColor.White);
            Bishop bishop3 = new Bishop(new Board(), PieceColor.Black);
            Bishop bishop4 = new Bishop(new Board(), PieceColor.Black);

            Assert.AreEqual(bishop1, bishop2);
            Assert.AreEqual(bishop3, bishop4);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesThreeSquaresForwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board.SetSquare(3, 1, new Bishop(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(3, 1), GetSquare(6, 4));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesThreeSquaresRight_ReturnsFalse()
        {
            Board.SetSquare(3, 1, new Bishop(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(3, 1), GetSquare(6, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonal_ReturnsTrue()
        {
            Board.SetSquare(3, 1, new Bishop(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(3, 1), GetSquare(1, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresBackwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board.SetSquare(1, 3, new Bishop(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(1, 3), GetSquare(3, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresBackwardOnTheRightDiagonalWhileSurroundedAroundThePath_ReturnsTrue()
        {
            Board.SetSquare(1, 1, new Rook(Board, PieceColor.White));
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(1, 3, new Bishop(Board, PieceColor.White));
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));
            Board.SetSquare(3, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));
            Board.SetSquare(4, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(1, 3), GetSquare(3, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresForwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board.SetSquare(6, 8, new Bishop(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(6, 8), GetSquare(4, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonal_ReturnsTrue()
        {
            Board.SetSquare(6, 8, new Bishop(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(6, 8), GetSquare(8, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresBackwardOnTheLeftDiagonal_ReturnsTrue()
        {
            Board.SetSquare(4, 6, new Bishop(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(4, 6), GetSquare(6, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareBishopMovesTwoSquaresForwardOnTheRightDiagonalWhenObscuredBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(6, 1, new Bishop(Board, PieceColor.White));
            Board.SetSquare(7, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(6, 1), GetSquare(8, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresForwardOnTheRightDiagonalWhenObscuredBySameColorPiecesInAllDirections_ReturnsFalse()
        { 
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));
            Board.SetSquare(2, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(3, 1, new Bishop(Board, PieceColor.White));
            Board.SetSquare(3, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));
            Board.SetSquare(4, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(3, 1), GetSquare(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonalWhenObscuredBySameColorPieceInAllDirections_ReturnsFalse()
        {
            Board.SetSquare(2, 1, new Knight(Board, PieceColor.White));
            Board.SetSquare(2, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(3, 1, new Bishop(Board, PieceColor.White));
            Board.SetSquare(3, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(4, 1, new Queen(Board, PieceColor.White));
            Board.SetSquare(4, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(3, 1), GetSquare(1, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresForwardOnTheRightDiagonalWhenObscuredBySameColorPieceInAllDirections_ReturnsFalse()
        {
            Board.SetSquare(7, 8, new Knight(Board, PieceColor.Black));
            Board.SetSquare(7, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(6, 8, new Bishop(Board, PieceColor.Black));
            Board.SetSquare(6, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(5, 7, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(6, 8), GetSquare(4, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonalWhenObscuredBySameColorPieceInAllDirections_ReturnsFalse()
        {
            Board.SetSquare(7, 8, new Knight(Board, PieceColor.Black));
            Board.SetSquare(7, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(6, 8, new Bishop(Board, PieceColor.Black));
            Board.SetSquare(6, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(5, 7, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(6, 8), GetSquare(8, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesOneSquareForwardOnTheRightDiagonalWhenObscuredBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(3, 1, new Bishop(Board, PieceColor.White));
            Board.SetSquare(4, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(3, 1), GetSquare(4, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackLightSquareBishopMovesOneSquareForwardOnTheRightDiagonalWhenObscuredBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(3, 8, new Bishop(Board, PieceColor.Black));
            Board.SetSquare(2, 7, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(3, 8), GetSquare(2, 7));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareBishopMovesThreeSquaresForwardAndTwoSquaresLeft_ReturnsFalse()
        {
            Board.SetSquare(6, 1, new Bishop(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(6, 1), GetSquare(4, 4));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareBishopMovesFourSquaresForwardOnTheLeftDiagonalToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board.SetSquare(6, 1, new Bishop(Board, PieceColor.White));
            Board.SetSquare(2, 5, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(6, 1), GetSquare(2, 5));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesFiveSquaresForwardOnTheRightDiagonalToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board.SetSquare(6, 8, new Bishop(Board, PieceColor.Black));
            Board.SetSquare(1, 3, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(6, 8), GetSquare(1, 3));

            Assert.IsTrue(result);
        }
    }
}
