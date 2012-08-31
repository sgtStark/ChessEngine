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

        // TODO: Toteuta Castling siirtojen tarkastukset kuninkaalle

        #region IsLegalMove

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

        #endregion IsLegalMove
    }
}
