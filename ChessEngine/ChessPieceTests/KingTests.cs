using ChessEngineLib;
using ChessEngineLib.ChessPieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    /// <summary>
    /// Sisältää kaikki kuningas shakkinappulaan kohdistuvat yksikkötestit.
    /// HUOM! Perii yhteisestä ChessEngineTestBase-luokasta yleisiä setup-metodeita.
    /// </summary>
    [TestClass]
    public class KingTests : ChessEngineTestBase
    {
        #region Equals testit

        [TestMethod]
        public void Equals_TwoOppositeColorKings_AreNotEqual()
        {
            King king1 = new King(PieceColor.White);
            King king2 = new King(PieceColor.Black);

            Assert.AreNotEqual(king1, king2);
        }

        [TestMethod]
        public void Equals_TwoSameColorKings_AreEqual()
        {
            King king1 = new King(PieceColor.White);
            King king2 = new King(PieceColor.White);
            King king3 = new King(PieceColor.Black);
            King king4 = new King(PieceColor.Black);
    
            Assert.AreEqual(king1, king2);
            Assert.AreEqual(king3, king4);
        }

        #endregion Equals testit

        #region IsLegalMove

        #region Normal Moves

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForward_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesTwoSquaresForward_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(6, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(6, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));
            board.SetPosition(5, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(5, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(5, 7, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(5, 7));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(5, 7, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(5, 7));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.Black));
            board.SetPosition(5, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(5, 2));

            Assert.IsTrue(result);
        }

        #endregion Normal Moves

        #region Kingside Castling

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));
            board.SetPosition(8, 1, new Rook(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(7, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionAndPathIsObscured_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));
            board.SetPosition(6, 1, new Bishop(PieceColor.White));
            board.SetPosition(8, 1, new Rook(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionWhenItHasAlreadyBeenMovedAndPathIsNotObscured_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));
            board.SetPosition(8, 1, new Rook(PieceColor.White));
            board.Move(board.GetPosition(5, 1), board.GetPosition(6, 1));
            board.Move(board.GetPosition(6, 1), board.GetPosition(5, 1));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(7, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(8, 8, new Rook(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(7, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionAndPathIsObscured_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(6, 8, new Bishop(PieceColor.Black));
            board.SetPosition(8, 8, new Rook(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionWhenItHasAlreadyBeenMovedAndPathIsNotObscured_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(8, 8, new Rook(PieceColor.Black));
            board.Move(board.GetPosition(5, 8), board.GetPosition(6, 8));
            board.Move(board.GetPosition(6, 8), board.GetPosition(5, 8));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToKingsideCastlingPositionOnBlackTerritoryAndPathIsNotObscured_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.White));
            board.SetPosition(8, 8, new Rook(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(7, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToKingsideCastlingPositionOnWhiteTerritoryAndPathIsNotObscured_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.Black));
            board.SetPosition(8, 1, new Rook(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(7, 1));

            Assert.IsFalse(result);
        }

        #endregion Kingside Castling

        #region Queenside Castling

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToQueensideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));
            board.SetPosition(1, 1, new Rook(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(3, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToQueensideCastlingPositionAndPathIsNotObscured_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(1, 8, new Rook(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(3, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteKingMovesToQueensideCastlingPositionAndThereIsNoRookInTheCorner_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 1, new King(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(5, 1), board.GetPosition(3, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackKingMovesToQueensideCastlingPositionAndThereIsNoRookInTheCorner_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(5, 8, new King(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(5, 8), board.GetPosition(3, 8));

            Assert.IsFalse(result);
        }

        #endregion Queenside Castling

        #endregion IsLegalMove
    }
}
