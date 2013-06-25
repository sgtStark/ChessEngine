using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class BoardTests : ChessEngineTestBase
    {

        [TestInitialize]
        public void InitializationForTests()
        {
            InitializeBoard();
        }

        [TestMethod]
        public void Equals_TwoBoardsWithExactlyTheSameConfiguration_ReturnsTrue()
        {
            Board board1 = new Board();
            Board board2 = new Board();

            board1.Setup();
            board2.Setup();

            Assert.AreEqual(board1, board2);

            board1.Move(board1.GetSquare(1, 2), board1.GetSquare(1, 4));
            board2.Move(board2.GetSquare(1, 2), board2.GetSquare(1, 4));

            Assert.AreEqual(board1, board2);
        }

        [TestMethod]
        public void Equals_TwoBoardsWithDifferentConfigurations_ReturnsFalse()
        {
            Board board1 = new Board();
            Board board2 = new Board();

            board1.Setup();

            Assert.AreNotEqual(board1, board2);

            board1 = new Board();
            board2 = new Board();

            board1.SetSquare(1, 2, new NullPiece(board1));
            board2.SetSquare(1, 2, new Pawn(board2, PieceColor.White));

            Assert.AreNotEqual(board1, board2);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_PositionsAlongSecondRankReturnWhitePawns()
        {
            var expected = new Pawn(Board, PieceColor.White);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(1, 2).Occupier);
            Assert.AreEqual(expected, GetSquare(2, 2).Occupier);
            Assert.AreEqual(expected, GetSquare(3, 2).Occupier);
            Assert.AreEqual(expected, GetSquare(4, 2).Occupier);
            Assert.AreEqual(expected, GetSquare(5, 2).Occupier);
            Assert.AreEqual(expected, GetSquare(6, 2).Occupier);
            Assert.AreEqual(expected, GetSquare(7, 2).Occupier);
            Assert.AreEqual(expected, GetSquare(8, 2).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_PositionsAlongSeventhRankReturnBlackPawns()
        {
            var expected = new Pawn(Board, PieceColor.Black);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(1, 7).Occupier);
            Assert.AreEqual(expected, GetSquare(2, 7).Occupier);
            Assert.AreEqual(expected, GetSquare(3, 7).Occupier);
            Assert.AreEqual(expected, GetSquare(4, 7).Occupier);
            Assert.AreEqual(expected, GetSquare(5, 7).Occupier);
            Assert.AreEqual(expected, GetSquare(6, 7).Occupier);
            Assert.AreEqual(expected, GetSquare(7, 7).Occupier);
            Assert.AreEqual(expected, GetSquare(8, 7).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FirstAndEightFilePositionsOnTheFirstRankReturnWhiteRooks()
        {
            var expected = new Rook(Board, PieceColor.White);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(1, 1).Occupier);
            Assert.AreEqual(expected, GetSquare(8, 1).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FirstAndEightFilePositionsOnTheEightRankReturnBlackRooks()
        {
            var expected = new Rook(Board, PieceColor.Black);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(1, 8).Occupier);
            Assert.AreEqual(expected, GetSquare(8, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_SecondAndSeventhFilePositionsOnTheFirstRankReturnWhiteKnights()
        {
            var expected = new Knight(Board, PieceColor.White);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(2, 1).Occupier);
            Assert.AreEqual(expected, GetSquare(7, 1).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_SecondAndSeventhFilePositionsOnTheEightRankReturnBlackKnights()
        {
            var expected = new Knight(Board, PieceColor.Black);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(2, 8).Occupier);
            Assert.AreEqual(expected, GetSquare(7, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_ThirdAndSixthFilePositionsOnTheFirstRankReturnWhiteBishops()
        {
            var expected = new Bishop(Board, PieceColor.White);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(3, 1).Occupier);
            Assert.AreEqual(expected, GetSquare(6, 1).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_ThirdAndSixthFilePositionsOnTheEightRankReturnBlackBishops()
        {
            var expected = new Bishop(Board, PieceColor.Black);

            Board.Setup();

            Assert.AreEqual(expected, GetSquare(3, 8).Occupier);
            Assert.AreEqual(expected, GetSquare(6, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FourthFilePositionsOnTheFirstAndEightRankReturnQueens()
        {
            var expectedWhiteQueen = new Queen(Board, PieceColor.White);
            var expectedBlackQueen = new Queen(Board, PieceColor.Black);

            Board.Setup();

            Assert.AreEqual(expectedWhiteQueen, GetSquare(4, 1).Occupier);
            Assert.AreEqual(expectedBlackQueen, GetSquare(4, 8).Occupier);
        }

        [TestMethod]
        public void Setup_WhenBoardIsSetup_FifthFilePositionsOnTheFirstAndEightRankReturnKings()
        {
            var expectedWhiteKing = new King(Board, PieceColor.White);
            var expectedBlackKing = new King(Board, PieceColor.Black);

            Board.Setup();

            Assert.AreEqual(expectedWhiteKing, GetSquare(5, 1).Occupier);
            Assert.AreEqual(expectedBlackKing, GetSquare(5, 8).Occupier);
        }

        [TestMethod]
        public void GetPosition_FirstFileAndRankWhenBoardIsEmpty_ReturnsEmptyPosition()
        {
            Square expected = new Square(1, 1, new NullPiece(Board));

            Square actual = GetSquare(1, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPosition_SecondFileAndRankWhenBoardIsEmpty_ReturnsEmptyPosition()
        {
            Square expected = new Square(2, 2, new NullPiece(Board));

            Square actual = GetSquare(2, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_FirstFileAndRankWhenBoardIsEmpty_GetPositionReturnsTheValueThatWasSet()
        {
            Square expected = new Square(1, 1, new Pawn(Board, PieceColor.White));

            Board.SetSquare(1, 1, new Pawn(Board, PieceColor.White));

            Square actual = GetSquare(1, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_FirstFileAndRankWhenBoardIsEmpty_GetPositionForSecondFileAndRankReturnsEmptyPosition()
        {
            Square expected = new Square(2, 2, new NullPiece(Board));

            Board.SetSquare(1, 1, new Pawn(Board, PieceColor.White));

            Square actual = GetSquare(2, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_WhiteRookToFirstFileAndRankWhenBoardIsEmpty_GetPositionReturnsTheSetValue()
        {
            var expected = new Square(1, 1, new Rook(Board, PieceColor.White));

            Board.SetSquare(1, 1, new Rook(Board, PieceColor.White));
            var actual  = GetSquare(1, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_BlackRookToFirstFileAndEightRankWhenBoardIsEmpty_GetPositionReturnsTheSetValue()
        {
            var expected = new Square(1, 8, new Rook(Board, PieceColor.Black));

            Board.SetSquare(1, 8, new Rook(Board, PieceColor.Black));
            var actual = GetSquare(1, 8);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition_WhiteRookToFirstFileAndRankWhenBoardIsEmpty_GetPositionFromDifferingCoordinatesIsNotEqual()
        {
            var expected = new Square(1, 1, new Rook(Board, PieceColor.White));

            Board.SetSquare(1, 1, new Rook(Board, PieceColor.White));
            var actual = GetSquare(2, 4);

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void IsLightSquare_FirstFileAndFirstRank_ReturnsFalse()
        {
            var result = Board.IsLightSquare(1, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLightSquare_SecondFileAndFirstRank_ReturnsTrue()
        {
            var result = Board.IsLightSquare(2, 1);

            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void IsLightSquare_FirstFileAndSecondRank_ReturnsTrue()
        {
            var result = Board.IsLightSquare(1, 2);

            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void IsLightSquare_SecondFileAndSecondRank_ReturnsFalse()
        {
            var result = Board.IsLightSquare(2, 2);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void IsLightSquare_SixthFileAndThirdRank_ReturnsTrue()
        {
            var result = Board.IsLightSquare(6, 3);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLightSquare_FifthFileAndFifthRank_ReturnsTrue()
        {
            var result = Board.IsLightSquare(5, 5);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Move_ChessPieceIsMovedToEmptyPosition_TargetPositionReturnsTheChessPieceAsOccupier()
        {
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));

            Board.Move(GetSquare(1, 2), GetSquare(1, 3));

            Assert.AreEqual(new Pawn(Board, PieceColor.White), GetSquare(1, 3).Occupier);
        }

        [TestMethod]
        public void Move_ChessPieceIsMovedToEmptyPosition_OriginalPositionsOccupierReturnsNull()
        {
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));

            Board.Move(GetSquare(1, 2), GetSquare(1, 3));

            Assert.AreEqual(new NullPiece(Board), GetSquare(1, 2).Occupier);
        }

        [TestMethod]
        public void Move_WhiteMakesAnEnPassantMove_OriginalPositionIsEmptyAndTargetPositionReturnsTheChessPieceAsOccupier()
        {
            Board.Setup();
            Pawn expected = new Pawn(Board, PieceColor.White);

            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            Board.Move(GetSquare(4, 4), GetSquare(4, 5));
            Board.Move(GetSquare(3, 7), GetSquare(3, 5));
            Board.Move(GetSquare(4, 5), GetSquare(3, 6));
            var actual = GetSquare(3, 6).Occupier;

            Assert.AreEqual(new NullPiece(Board), GetSquare(4, 5).Occupier);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_WhiteMakesAnEnPassantMove_ChessPieceUnderEnPassantThreatIsRemoved()
        {
            Board.Setup();

            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            Board.Move(GetSquare(4, 4), GetSquare(4, 5));
            Board.Move(GetSquare(3, 7), GetSquare(3, 5));
            Board.Move(GetSquare(4, 5), GetSquare(3, 6));

            var positionUnderEnPassantThreat = GetSquare(3, 5);

            Assert.AreEqual(new NullPiece(Board), positionUnderEnPassantThreat.Occupier);
        }

        [TestMethod]
        public void Move_BlackMakesAnEnPassantMove_ChessPieceUnderEnPassantThreatIsRemoved()
        {
            Board.Setup();

            Board.Move(GetSquare(4, 7), GetSquare(4, 5));
            Board.Move(GetSquare(4, 5), GetSquare(4, 4));
            Board.Move(GetSquare(3, 2), GetSquare(3, 4));
            Board.Move(GetSquare(4, 4), GetSquare(3, 3));

            var positionUnderEnPassantThreat = GetSquare(3, 4);

            Assert.AreEqual(new NullPiece(Board), positionUnderEnPassantThreat.Occupier);
        }

        [TestMethod]
        public void Move_WhiteKingMakesKingsideCastlingMove_KingsideRookIsMovedToKingsLeftsidePosition()
        {
            var expected = new Rook(Board, PieceColor.White);
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(8, 1, expected);

            Board.Move(GetSquare(5, 1), GetSquare(7, 1));
            var actual = GetSquare(6, 1).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_WhiteKingMakesKingsideCastlingMove_QueensideRookIsNotMoved()
        {
            var expected = new Rook(Board, PieceColor.White);
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(8, 1, new Rook(Board, PieceColor.White));
            Board.SetSquare(1, 1, expected);

            Board.Move(GetSquare(5, 1), GetSquare(7, 1));
            var actual = GetSquare(1, 1).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_BlackKingMakesKingsideCastlingMove_KingsideRookIsMovedToKingsRightsidePosition()
        {
            var expected = new Rook(Board, PieceColor.Black);
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(8, 8, expected);

            Board.Move(GetSquare(5, 8), GetSquare(7, 8));
            var actual = GetSquare(6, 8).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_BlackKingMakesKingsideCastlingMove_QueensideRookIsNotMoved()
        {
            var expected = new Rook(Board, PieceColor.White);
            Board.SetSquare(5, 8, new King(Board, PieceColor.White));
            Board.SetSquare(8, 8, new Rook(Board, PieceColor.White));
            Board.SetSquare(1, 8, expected);

            Board.Move(GetSquare(5, 8), GetSquare(7, 8));
            var actual = GetSquare(1, 8).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_WhiteKingMakesQueensideCastlingMove_QueensideRookIsMovedToKingsRightsidePosition()
        {
            var expected = new Rook(Board, PieceColor.White);
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(1, 1, expected);

            Board.Move(GetSquare(5, 1), GetSquare(3, 1));
            var actual = GetSquare(4, 1).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_WhiteKingMakesQueensideCastlingMove_KingsideRookIsNotMoved()
        {
            var expected = new Rook(Board, PieceColor.White);
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(1, 1, new Rook(Board, PieceColor.White));
            Board.SetSquare(8, 1, expected);

            Board.Move(GetSquare(5, 1), GetSquare(3, 1));
            var actual = GetSquare(8, 1).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_BlackKingMakesQueensideCastlingMove_QueensideRookIsMoveToKingsLeftsidePosition()
        {
            var expected = new Rook(Board, PieceColor.Black);
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 8, expected);

            Board.Move(GetSquare(5, 8), GetSquare(3, 8));
            var actual = GetSquare(4, 8).Occupier;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Move_BlackKingMakesQueensideCastlingMove_KingsideRookIsNotMoved()
        {
            var expected = new Rook(Board, PieceColor.White);
            Board.SetSquare(5, 8, new King(Board, PieceColor.White));
            Board.SetSquare(1, 8, new Rook(Board, PieceColor.White));
            Board.SetSquare(8, 8, expected);

            Board.Move(GetSquare(5, 8), GetSquare(3, 8));
            var actual = GetSquare(8, 8).Occupier;

            Assert.AreEqual(expected, actual);
        }
    }
}
