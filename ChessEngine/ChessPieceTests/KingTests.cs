using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class KingTests : ChessEngineTestBase
    {
        [TestInitialize]
        public void InitializationBeforeEveryTest()
        {
            InitializeBoard();
        }

        [TestMethod]
        public void Equals_TwoOppositeColorKings_AreNotEqual()
        {
            var king1 = new King(new Board(), PieceColor.White);
            var king2 = new King(new Board(), PieceColor.Black);

            Assert.AreNotEqual(king1, king2);
        }

        [TestMethod]
        public void Equals_TwoSameColorKings_AreEqual()
        {
            var king1 = new King(new Board(), PieceColor.White);
            var king2 = new King(new Board(), PieceColor.White);
            var king3 = new King(new Board(), PieceColor.Black);
            var king4 = new King(new Board(), PieceColor.Black);
    
            Assert.AreEqual(king1, king2);
            Assert.AreEqual(king3, king4);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForward_ReturnsTrue()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesTwoSquaresForward_ReturnsFalse()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareRight_ReturnsTrue()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(6, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(6, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(5, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(5, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(5, 7, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(5, 7));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(5, 7, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(5, 7));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.Black));
            Board.SetSquare(5, 2, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareLeftAndSevenSquaresForward_ReturnsFalse()
        {
            Board.SetSquare(4, 1, new King(Board, PieceColor.White));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(4, 1), GetSquare(5, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(8, 1, new Rook(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(7, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionAndPathIsObscured_ReturnsFalse()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(6, 1, new Bishop(Board, PieceColor.White));
            Board.SetSquare(8, 1, new Rook(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionWhenItHasAlreadyBeenMovedAndPathIsNotObscured_ReturnsFalse()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(8, 1, new Rook(Board, PieceColor.White));
            Board.Move(Board.GetSquare(5, 1), Board.GetSquare(6, 1));
            Board.Move(Board.GetSquare(6, 1), Board.GetSquare(5, 1));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(8, 8, new Rook(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(7, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionAndPathIsObscured_ReturnsFalse()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(6, 8, new Bishop(Board, PieceColor.Black));
            Board.SetSquare(8, 8, new Rook(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionWhenItHasAlreadyBeenMovedAndPathIsNotObscured_ReturnsFalse()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(8, 8, new Rook(Board, PieceColor.Black));
            Board.Move(Board.GetSquare(5, 8), Board.GetSquare(6, 8));
            Board.Move(Board.GetSquare(6, 8), Board.GetSquare(5, 8));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionOnBlackTerritoryAndPathIsNotObscured_ReturnsFalse()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.White));
            Board.SetSquare(8, 8, new Rook(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionOnWhiteTerritoryAndPathIsNotObscured_ReturnsFalse()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.Black));
            Board.SetSquare(8, 1, new Rook(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToQueensideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(1, 1, new Rook(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(3, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToQueensideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 8, new Rook(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(3, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToQueensideCastlingPositionAndThereIsNoRookInTheCorner_ReturnsFalse()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(5, 1), GetSquare(3, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToQueensideCastlingPositionAndThereIsNoRookInTheCorner_ReturnsFalse()
        {
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(5, 8), GetSquare(3, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesIntoAttackedSquare_ReturnsFalse()
        {
            Board.SetSquare(1, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 6, new King(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(1, 8), GetSquare(1, 7));

            Assert.IsFalse(result);
        }
    }
}
