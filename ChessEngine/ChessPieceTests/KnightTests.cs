using ChessEngineLib;
using ChessEngineLib.ChessPieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    /// <summary>
    /// Sisältää kaikki hevos shakkinappulaan kohdistuvat yksikkötestit.
    /// HUOM! Perii yhteisestä ChessEngineTestBase-luokasta yleisiä setup-metodeita.
    /// </summary>
    [TestClass]
    public class KnightTests : ChessEngineTestBase
    {
        #region Equals testit

        [TestMethod]
        public void Equals_TwoOppositeColorKnights_AreNotEqual()
        {
            Knight knight1 = new Knight(PieceColor.White);
            Knight knight2 = new Knight(PieceColor.Black);

            Assert.AreNotEqual(knight1, knight2);
        }

        [TestMethod]
        public void Equals_TwoSameColorKnights_AreEqual()
        {
            Knight knight1 = new Knight(PieceColor.White);
            Knight knight2 = new Knight(PieceColor.White);
            Knight knight3 = new Knight(PieceColor.Black);
            Knight knight4 = new Knight(PieceColor.Black);

            Assert.AreEqual(knight1, knight2);
            Assert.AreEqual(knight3, knight4);
        }

        #endregion Equals testit

        #region IsLegalMove testit

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndOneSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(2, 1), board.GetPosition(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndTwoSquaresRight_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(2, 1), board.GetPosition(4, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(2, 1), board.GetPosition(4, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareKnightMovesTwoSquaresForwardAndOneSquareLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(7, 1, new Knight(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(7, 1), board.GetPosition(6, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareKnightMovesOneSquareForwardAndTwoSquaresLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(7, 1, new Knight(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(7, 1), board.GetPosition(5, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresBackwardAndOneSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(3, 3, new Knight(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(3, 3), board.GetPosition(2, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareBackwardAndTwoSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(4, 2, new Knight(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(4, 2), board.GetPosition(2, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareKnightMovesTwoSquaresForwardAndOneSquareRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 8, new Knight(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(2, 8), board.GetPosition(1, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareKnightMovesOneSquareForwardAndTwoSquaresLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 8, new Knight(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(2, 8), board.GetPosition(4, 7));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesTwoSquaresForwardAndOneSquareRightWhileObscuredInFront_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));
            board.SetPosition(2, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(2, 1), board.GetPosition(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRightToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));
            board.SetPosition(3, 3, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(2, 1), board.GetPosition(3, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareKnightMovesOneSquareForwardAndTwoSquaresRightToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));
            board.SetPosition(3, 3, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(2, 1), board.GetPosition(3, 3));

            Assert.IsTrue(result);
        }

        // NOTE: Tähän voisi lisätä myös mustalle hevoselle siirrot miehitettyihin ruutuihin testaukset

        #endregion IsLegalMove testit
    }
}
