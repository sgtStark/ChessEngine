﻿using ChessEngineLib;
using ChessEngineLib.ChessPieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    /// <summary>
    /// Sisältää kaikki lähetti shakkinappulaan kohdistuvat yksikkötestit.
    /// HUOM! Perii yhteisestä ChessEngineTestBase-luokasta yleisiä setup-metodeita.
    /// </summary>
    [TestClass]
    public class BishopTests : ChessEngineTestBase
    {
        #region Equals testit

        [TestMethod]
        public void Equals_TwoOppositeColorBishops_AreNotEqual()
        {
            Bishop bishop1 = new Bishop(PieceColor.White);
            Bishop bishop2 = new Bishop(PieceColor.Black);

            Assert.AreNotEqual(bishop1, bishop2);
        }

        [TestMethod]
        public void Equals_TwoSameColorBishops_AreEqual()
        {
            Bishop bishop1 = new Bishop(PieceColor.White);
            Bishop bishop2 = new Bishop(PieceColor.White);
            Bishop bishop3 = new Bishop(PieceColor.Black);
            Bishop bishop4 = new Bishop(PieceColor.Black);

            Assert.AreEqual(bishop1, bishop2);
            Assert.AreEqual(bishop3, bishop4);
        }

        #endregion Equals testit

        #region IsLegalMove

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesThreeSquaresForwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(3, 1, new Bishop(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(3, 1), board.GetPosition(6, 4));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesThreeSquaresRight_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(3, 1, new Bishop(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(3, 1), board.GetPosition(6, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(3, 1, new Bishop(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(3, 1), board.GetPosition(1, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresBackwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 3, new Bishop(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 3), board.GetPosition(3, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresBackwardOnTheRightDiagonalWhileSurroundedAroundThePath_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(PieceColor.White));
            board.SetPosition(1, 2, new Pawn(PieceColor.White));
            board.SetPosition(1, 3, new Bishop(PieceColor.White));
            board.SetPosition(2, 1, new Knight(PieceColor.White));
            board.SetPosition(3, 2, new Pawn(PieceColor.White));
            board.SetPosition(4, 1, new Queen(PieceColor.White));
            board.SetPosition(4, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 3), board.GetPosition(3, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresForwardOnTheRightDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(6, 8, new Bishop(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(6, 8), board.GetPosition(4, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(6, 8, new Bishop(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(6, 8), board.GetPosition(8, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresBackwardOnTheLeftDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(4, 6, new Bishop(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(4, 6), board.GetPosition(6, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareBishopMovesTwoSquaresForwardOnTheRightDiagonalWhenObscuredBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(6, 1, new Bishop(PieceColor.White));
            board.SetPosition(7, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(6, 1), board.GetPosition(8, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresForwardOnTheRightDiagonalWhenObscuredBySameColorPiecesInAllDirections_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));
            board.SetPosition(2, 2, new Pawn(PieceColor.White));
            board.SetPosition(3, 1, new Bishop(PieceColor.White));
            board.SetPosition(3, 2, new Pawn(PieceColor.White));
            board.SetPosition(4, 1, new Queen(PieceColor.White));
            board.SetPosition(4, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(3, 1), board.GetPosition(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonalWhenObscuredBySameColorPieceInAllDirections_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 1, new Knight(PieceColor.White));
            board.SetPosition(2, 2, new Pawn(PieceColor.White));
            board.SetPosition(3, 1, new Bishop(PieceColor.White));
            board.SetPosition(3, 2, new Pawn(PieceColor.White));
            board.SetPosition(4, 1, new Queen(PieceColor.White));
            board.SetPosition(4, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(3, 1), board.GetPosition(1, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesTwoSquaresForwardOnTheLeftDiagonalWhenObscuredBySameColorPieceInAllDirections_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(7, 8, new Knight(PieceColor.Black));
            board.SetPosition(7, 7, new Pawn(PieceColor.Black));
            board.SetPosition(6, 8, new Bishop(PieceColor.Black));
            board.SetPosition(6, 7, new Pawn(PieceColor.Black));
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(5, 7, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(6, 8), board.GetPosition(8, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteDarkSquareBishopMovesOneSquareForwardOnTheRightDiagonalWhenObscuredBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(3, 1, new Bishop(PieceColor.White));
            board.SetPosition(4, 2, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(3, 1), board.GetPosition(4, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackLightSquareBishopMovesOneSquareForwardOnTheRightDiagonalWhenObscuredBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(3, 8, new Bishop(PieceColor.Black));
            board.SetPosition(2, 7, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(3, 8), board.GetPosition(2, 7));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareBishopMovesThreeSquaresForwardAndTwoSquaresLeft_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(6, 1, new Bishop(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(6, 1), board.GetPosition(4, 4));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteLightSquareBishopMovesFourSquaresForwardOnTheLeftDiagonalToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(6, 1, new Bishop(PieceColor.White));
            board.SetPosition(2, 5, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(6, 1), board.GetPosition(2, 5));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackDarkSquareBishopMovesFiveSquaresForwardOnTheRightDiagonalToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(6, 8, new Bishop(PieceColor.Black));
            board.SetPosition(1, 3, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(6, 8), board.GetPosition(1, 3));

            Assert.IsTrue(result);
        }

        #endregion IsLegalMove
    }
}
