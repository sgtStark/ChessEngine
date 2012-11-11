﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class RookTests : ChessEngineTestBase
    {
        [TestMethod]
        public void Equals_TwoOppositeColorRooks_AreNotEqual()
        {
            Rook rook1 = new Rook(new Board(), PieceColor.White);
            Rook rook2 = new Rook(new Board(), PieceColor.Black);

            Assert.AreNotEqual(rook1, rook2);
        }
        
        [TestMethod]
        public void Equals_TwoSameColorRooks_AreEqual()
        {
            Rook rook1 = new Rook(new Board(), PieceColor.White);
            Rook rook2 = new Rook(new Board(), PieceColor.White);
            Rook rook3 = new Rook(new Board(), PieceColor.Black);
            Rook rook4 = new Rook(new Board(), PieceColor.Black);

            Assert.AreEqual(rook1, rook2);
            Assert.AreEqual(rook3, rook4);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesTwoSquaresForward_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(1, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackRookMovesTwoSquaresForward_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 8, new Rook(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 8), board.GetPosition(1, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresBackward_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 4, new Rook(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 4), board.GetPosition(1, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackRookMovesThreeSquaresBackward_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 5, new Rook(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 5), board.GetPosition(1, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(4, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackRookMovesThreeSquaresRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(4, 8, new Rook(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(4, 8), board.GetPosition(1, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(4, 1, new Rook(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(4, 1), board.GetPosition(1, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackRookMovesThreeSquaresLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 8, new Rook(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 8), board.GetPosition(4, 8));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresForwardWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));
            board.SetPosition(1, 2, new Pawn(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(1, 4));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackRookMovesThreeSquaresForwardWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 8, new Rook(board, PieceColor.Black));
            board.SetPosition(1, 7, new Pawn(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 8), board.GetPosition(1, 5));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesTwoSquaresLeftWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));
            board.SetPosition(2, 1, new Pawn(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(3, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackRookMovesTwoSquaresLeftWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(3, 8, new Rook(board, PieceColor.Black));
            board.SetPosition(2, 8, new Pawn(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(3, 8), board.GetPosition(1, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));
            board.SetPosition(4, 1, new Pawn(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(4, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));
            board.SetPosition(4, 1, new Pawn(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(4, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresRightToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));
            board.SetPosition(1, 4, new Pawn(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(1, 4));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresRightToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));
            board.SetPosition(1, 4, new Pawn(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(1, 4));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresLeftToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 8, new Rook(board, PieceColor.White));
            board.SetPosition(1, 5, new Pawn(board, PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 8), board.GetPosition(1, 5));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhiteRookMovesThreeSquaresLeftToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 8, new Rook(board, PieceColor.White));
            board.SetPosition(1, 5, new Pawn(board, PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 8), board.GetPosition(1, 5));

            Assert.IsFalse(result);
        }
    }
}
