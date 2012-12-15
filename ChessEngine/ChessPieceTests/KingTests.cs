using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class KingTests : ChessEngineTestBase
    {
        private Board _board;

        [TestInitialize]
        public void InitializationBeforeEveryTest()
        {
            _board = CreateEmptyBoard();
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
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesTwoSquaresForward_ReturnsFalse()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareRight_ReturnsTrue()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(6, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardOnTheRightDiagonal_ReturnsTrue()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(6, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));
            _board.SetSquare(5, 2, new Pawn(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(5, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));
            _board.SetSquare(5, 7, new Pawn(_board, PieceColor.Black));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(5, 7));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));
            _board.SetSquare(5, 7, new Pawn(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(5, 7));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.Black));
            _board.SetSquare(5, 2, new Pawn(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareLeftAndSevenSquaresForward_ReturnsFalse()
        {
            _board.SetSquare(4, 1, new King(_board, PieceColor.White));
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));

            var result = _board.IsLegalMove(_board.GetSquare(4, 1), _board.GetSquare(5, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));
            _board.SetSquare(8, 1, new Rook(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(7, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionAndPathIsObscured_ReturnsFalse()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));
            _board.SetSquare(6, 1, new Bishop(_board, PieceColor.White));
            _board.SetSquare(8, 1, new Rook(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionWhenItHasAlreadyBeenMovedAndPathIsNotObscured_ReturnsFalse()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));
            _board.SetSquare(8, 1, new Rook(_board, PieceColor.White));
            _board.Move(_board.GetSquare(5, 1), _board.GetSquare(6, 1));
            _board.Move(_board.GetSquare(6, 1), _board.GetSquare(5, 1));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));
            _board.SetSquare(8, 8, new Rook(_board, PieceColor.Black));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(7, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionAndPathIsObscured_ReturnsFalse()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));
            _board.SetSquare(6, 8, new Bishop(_board, PieceColor.Black));
            _board.SetSquare(8, 8, new Rook(_board, PieceColor.Black));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionWhenItHasAlreadyBeenMovedAndPathIsNotObscured_ReturnsFalse()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));
            _board.SetSquare(8, 8, new Rook(_board, PieceColor.Black));
            _board.Move(_board.GetSquare(5, 8), _board.GetSquare(6, 8));
            _board.Move(_board.GetSquare(6, 8), _board.GetSquare(5, 8));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionOnBlackTerritoryAndPathIsNotObscured_ReturnsFalse()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.White));
            _board.SetSquare(8, 8, new Rook(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionOnWhiteTerritoryAndPathIsNotObscured_ReturnsFalse()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.Black));
            _board.SetSquare(8, 1, new Rook(_board, PieceColor.Black));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToQueensideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));
            _board.SetSquare(1, 1, new Rook(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(3, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToQueensideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));
            _board.SetSquare(1, 8, new Rook(_board, PieceColor.Black));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(3, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToQueensideCastlingPositionAndThereIsNoRookInTheCorner_ReturnsFalse()
        {
            _board.SetSquare(5, 1, new King(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(5, 1), _board.GetSquare(3, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToQueensideCastlingPositionAndThereIsNoRookInTheCorner_ReturnsFalse()
        {
            _board.SetSquare(5, 8, new King(_board, PieceColor.Black));

            var result = _board.IsLegalMove(_board.GetSquare(5, 8), _board.GetSquare(3, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesIntoAttackedSquare_ReturnsFalse()
        {
            _board.SetSquare(1, 8, new King(_board, PieceColor.Black));
            _board.SetSquare(1, 6, new King(_board, PieceColor.White));

            var result = _board.IsLegalMove(_board.GetSquare(1, 8), _board.GetSquare(1, 7));

            Assert.IsFalse(result);
        }
    }
}
