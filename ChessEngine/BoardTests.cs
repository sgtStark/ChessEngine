using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessEngineLib;
using ChessEngineLib.Exceptions;
using ChessEngineLib.ChessPieces;

namespace ChessEngineTests
{
    using Helpers;
    
    /// <summary>
    /// Sisältää kaikki shakkilautaan kohdistuvat yksikkötestit.
    /// HUOM! Perii yhteisestä ChessEngineTestBase-luokasta yleisiä setup-metodeita.
    /// </summary>
    [TestClass]
    public class BoardTests : ChessEngineTestBase
    {
        #region Aloitus tilanteen/asetelman testit

        [TestMethod]
        public void Setup_WhenBoardIsSetup_PositionsAlongSecondRankReturnWhitePawns()
        {
            Board board = CreateEmptyBoard();
            var expected = new Pawn(PieceColor.White);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(1, 2).Occupier);
            Assert.AreEqual(expected, board.GetPosition(2, 2).Occupier);
            Assert.AreEqual(expected, board.GetPosition(3, 2).Occupier);
            Assert.AreEqual(expected, board.GetPosition(4, 2).Occupier);
            Assert.AreEqual(expected, board.GetPosition(5, 2).Occupier);
            Assert.AreEqual(expected, board.GetPosition(6, 2).Occupier);
            Assert.AreEqual(expected, board.GetPosition(7, 2).Occupier);
            Assert.AreEqual(expected, board.GetPosition(8, 2).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_PositionsAlongSeventhRankReturnBlackPawns()
        {
            Board board = CreateEmptyBoard();
            var expected = new Pawn(PieceColor.Black);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(1, 7).Occupier);
            Assert.AreEqual(expected, board.GetPosition(2, 7).Occupier);
            Assert.AreEqual(expected, board.GetPosition(3, 7).Occupier);
            Assert.AreEqual(expected, board.GetPosition(4, 7).Occupier);
            Assert.AreEqual(expected, board.GetPosition(5, 7).Occupier);
            Assert.AreEqual(expected, board.GetPosition(6, 7).Occupier);
            Assert.AreEqual(expected, board.GetPosition(7, 7).Occupier);
            Assert.AreEqual(expected, board.GetPosition(8, 7).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FirstAndEightFilePositionsOnTheFirstRankReturnWhiteRooks()
        {
            Board board = CreateEmptyBoard();
            var expected = new Rook(PieceColor.White);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(1, 1).Occupier);
            Assert.AreEqual(expected, board.GetPosition(8, 1).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FirstAndEightFilePositionsOnTheEightRankReturnBlackRooks()
        {
            Board board = CreateEmptyBoard();
            var expected = new Rook(PieceColor.Black);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(1, 8).Occupier);
            Assert.AreEqual(expected, board.GetPosition(8, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_SecondAndSeventhFilePositionsOnTheFirstRankReturnWhiteKnights()
        {
            Board board = CreateEmptyBoard();
            var expected = new Knight(PieceColor.White);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(2, 1).Occupier);
            Assert.AreEqual(expected, board.GetPosition(7, 1).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_SecondAndSeventhFilePositionsOnTheEightRankReturnBlackKnights()
        {
            Board board = CreateEmptyBoard();
            var expected = new Knight(PieceColor.Black);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(2, 8).Occupier);
            Assert.AreEqual(expected, board.GetPosition(7, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_ThirdAndSixthFilePositionsOnTheFirstRankReturnWhiteBishops()
        {
            Board board = CreateEmptyBoard();
            var expected = new Bishop(PieceColor.White);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(3, 1).Occupier);
            Assert.AreEqual(expected, board.GetPosition(6, 1).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_ThirdAndSixthFilePositionsOnTheEightRankReturnBlackBishops()
        {
            Board board = CreateEmptyBoard();
            var expected = new Bishop(PieceColor.Black);

            board.Setup();

            Assert.AreEqual(expected, board.GetPosition(3, 8).Occupier);
            Assert.AreEqual(expected, board.GetPosition(6, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FourthFilePositionsOnTheFirstAndEightRankReturnQueens()
        {
            Board board = CreateEmptyBoard();
            var expectedWhiteQueen = new Queen(PieceColor.White);
            var expectedBlackQueen = new Queen(PieceColor.Black);

            board.Setup();

            Assert.AreEqual(expectedWhiteQueen, board.GetPosition(4, 1).Occupier);
            Assert.AreEqual(expectedBlackQueen, board.GetPosition(4, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FifthFilePositionsOnTheFirstAndEightRankReturnKings()
        {
            Board board = CreateEmptyBoard();
            var expectedWhiteKing = new King(PieceColor.White);
            var expectedBlackKing = new King(PieceColor.Black);

            board.Setup();

            Assert.AreEqual(expectedWhiteKing, board.GetPosition(5, 1).Occupier);
            Assert.AreEqual(expectedBlackKing, board.GetPosition(5, 8).Occupier);
        }

        #endregion Aloitus tilanteen/asetelman testit

        // TODO: Toteuta Check siirtojen tarkastukset

        // TODO: Toteuta End Of Game tarkastus

        #region GetPosition

        [TestMethod]
        public void GetPosition_FirstFileAndRankWhenBoardIsEmpty_ReturnsEmptyPosition()
        {
            Board board = CreateEmptyBoard();
            Position expected = new Position(1, 1, null);

            Position actual = board.GetPosition(1, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPosition_SecondFileAndRankWhenBoardIsEmpty_ReturnsEmptyPosition()
        {
            Board board = CreateEmptyBoard();
            Position expected = new Position(2, 2, null);

            Position actual = board.GetPosition(2, 2);

            Assert.AreEqual(expected, actual);
        }

        #endregion GetPosition

        #region SetPosition

        #region Pawn

        [TestMethod]
        public void SetPosition_FirstFileAndRankWhenBoardIsEmpty_GetPositionReturnsTheValueThatWasSet()
        {
            Board board = CreateEmptyBoard();
            Position expected = new Position(1, 1, new Pawn(PieceColor.White));

            board.SetPosition(1, 1, new Pawn(PieceColor.White));

            Position actual = board.GetPosition(1, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_FirstFileAndRankWhenBoardIsEmpty_GetPositionForSecondFileAndRankReturnsEmptyPosition()
        {
            Board board = CreateEmptyBoard();
            Position expected = new Position(2, 2, null);

            board.SetPosition(1, 1, new Pawn(PieceColor.White));

            Position actual = board.GetPosition(2, 2);

            Assert.AreEqual(expected, actual);
        }

        #endregion Pawn

        #region Rook

        [TestMethod]
        public void SetPosition_WhiteRookToFirstFileAndRankWhenBoardIsEmpty_GetPositionReturnsTheSetValue()
        {
            Board board = CreateEmptyBoard();
            var expected = new Position(1, 1, new Rook(PieceColor.White));

            board.SetPosition(1, 1, new Rook(PieceColor.White));
            var actual  = board.GetPosition(1, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_BlackRookToFirstFileAndEightRankWhenBoardIsEmpty_GetPositionReturnsTheSetValue()
        {
            Board board = CreateEmptyBoard();
            var expected = new Position(1, 8, new Rook(PieceColor.Black));

            board.SetPosition(1, 8, new Rook(PieceColor.Black));
            var actual = board.GetPosition(1, 8);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_WhiteRookToFirstFileAndRankWhenBoardIsEmpty_GetPositionFromDifferingCoordinatesIsNotEqual()
        {
            Board board = CreateEmptyBoard();
            var expected = new Position(1, 1, new Rook(PieceColor.White));

            board.SetPosition(1, 1, new Rook(PieceColor.White));
            var actual = board.GetPosition(2, 4);

            Assert.AreNotEqual(expected, actual);
        }

        #endregion Rook

        #endregion SetPosition

        #region IsLightSquare

        [TestMethod]
        public void IsLightSquare_FirstFileAndFirstRank_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();

            var result = board.IsLightSquare(1, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLightSquare_SecondFileAndFirstRank_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();

            var result = board.IsLightSquare(2, 1);

            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void IsLightSquare_FirstFileAndSecondRank_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();

            var result = board.IsLightSquare(1, 2);

            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void IsLightSquare_SecondFileAndSecondRank_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();

            var result = board.IsLightSquare(2, 2);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void IsLightSquare_SixthFileAndThirdRank_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();

            var result = board.IsLightSquare(6, 3);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLightSquare_FifthFileAndFifthRank_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();

            var result = board.IsLightSquare(5, 5);

            Assert.IsFalse(result);
        }

        #endregion IsLightSquare

        #region Move

        [TestMethod]
        public void Move_WhenIllegalMoveMade_ThrowsIllegalMoveExceptionWithSpecificMessage()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));

            AssertHelper.Throws<IllegalMoveException>(
                () => board.Move(board.GetPosition(1, 2), board.GetPosition(1, 5)), string.Empty);
        }

        [TestMethod]
        public void Move_ChessPieceIsMovedToEmptyPosition_TargetPositionReturnsTheChessPieceAsOccupier()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));

            board.Move(board.GetPosition(1, 2), board.GetPosition(1, 3));

            Assert.AreEqual(new Pawn(PieceColor.White), board.GetPosition(1, 3).Occupier);
        }

        [TestMethod]
        public void Move_ChessPieceIsMovedToEmptyPosition_OriginalPositionsOccupierReturnsNull()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));

            board.Move(board.GetPosition(1, 2), board.GetPosition(1, 3));

            Assert.IsNull(board.GetPosition(1, 2).Occupier);
        }

        [TestMethod]
        public void Move_WhiteMakesAnEnPassantMove_OriginalPositionIsEmptyAndTargetPositionReturnsTheChessPieceAsOccupier()
        {
            Board board = CreateEmptyBoard();
            board.Setup();
            Pawn expected = new Pawn(PieceColor.White);

            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            board.Move(board.GetPosition(4, 4), board.GetPosition(4, 5));
            board.Move(board.GetPosition(3, 7), board.GetPosition(3, 5));
            board.Move(board.GetPosition(4, 5), board.GetPosition(3, 6));
            var actual = board.GetPosition(3, 6).Occupier;

            Assert.IsNull(board.GetPosition(4, 5).Occupier);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_WhiteMakesAnEnPassantMove_ChessPieceUnderEnPassantThreatIsRemoved()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            board.Move(board.GetPosition(4, 4), board.GetPosition(4, 5));
            board.Move(board.GetPosition(3, 7), board.GetPosition(3, 5));
            board.Move(board.GetPosition(4, 5), board.GetPosition(3, 6));

            var positionUnderEnPassantThreat = board.GetPosition(3, 5);

            Assert.IsNull(positionUnderEnPassantThreat.Occupier);
        }

        [TestMethod]
        public void Move_BlackMakesAnEnPassantMove_ChessPieceUnderEnPassantThreatIsRemoved()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            board.Move(board.GetPosition(4, 7), board.GetPosition(4, 5));
            board.Move(board.GetPosition(4, 5), board.GetPosition(4, 4));
            board.Move(board.GetPosition(3, 2), board.GetPosition(3, 4));
            board.Move(board.GetPosition(4, 4), board.GetPosition(3, 3));

            var positionUnderEnPassantThreat = board.GetPosition(3, 4);

            Assert.IsNull(positionUnderEnPassantThreat.Occupier);
        }

        [TestMethod]
        public void Move_WhiteKingMakesKingsideCastlingMove_KingsideRookIsMovedToKingsLeftsidePosition()
        {
            Board board = CreateEmptyBoard();
            var expected = new Rook(PieceColor.White);
            board.SetPosition(5, 1, new King(PieceColor.White));
            board.SetPosition(8, 1, expected);


            board.Move(board.GetPosition(5, 1), board.GetPosition(7, 1));
            var actual = board.GetPosition(6, 1).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_BlackKingMakesKingsideCastlingMove_KingsideRookIsMovedToKingsRightsidePosition()
        {
            Board board = CreateEmptyBoard();
            var expected = new Rook(PieceColor.Black);
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(8, 8, expected);

            board.Move(board.GetPosition(5, 8), board.GetPosition(7, 8));
            var actual = board.GetPosition(6, 8).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_WhiteKingMakesQueensideCastlingMove_QueensideRookIsMovedToKingsRightsidePosition()
        {
            Board board = CreateEmptyBoard();
            var expected = new Rook(PieceColor.White);
            board.SetPosition(5, 1, new King(PieceColor.White));
            board.SetPosition(1, 1, expected);

            board.Move(board.GetPosition(5, 1), board.GetPosition(3, 1));
            var actual = board.GetPosition(4, 1).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_BlackKingMakesQueensideCastlingMove_QueensideRookIsMoveToKingsLeftsidePosition()
        {
            Board board = CreateEmptyBoard();
            var expected = new Rook(PieceColor.Black);
            board.SetPosition(5, 8, new King(PieceColor.Black));
            board.SetPosition(1, 8, expected);

            board.Move(board.GetPosition(5, 8), board.GetPosition(3, 8));
            var actual = board.GetPosition(4, 8).Occupier;

            Assert.AreEqual(expected, actual);
        }

        #endregion Move
    }
}
