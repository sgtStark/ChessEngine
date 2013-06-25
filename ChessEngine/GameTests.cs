using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class GameTests : ChessEngineTestBase
    {
        [TestInitialize]
        public void InitializationBeforeEveryTest()
        {
            InitializeGame();
        }

        [TestMethod]
        public void GetBoard_AfterCreatingNewGame_ReturnsSetupBoard()
        {
            Board.Setup();
            Board expected = new Board();
            expected.Setup();

            var actual = Board;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayerToMove_BeforeGameIsStarted_ReturnsEmpty()
        {
            var result = Game.PlayerToMove;

            Assert.AreEqual(PieceColor.Empty, result);
        }

        [TestMethod]
        public void PlayerToMove_AfterGameIsStarted_ReturnsWhite()
        {
            Game.Start();
            var result = Game.PlayerToMove;

            Assert.AreEqual(PieceColor.White, result);
        }

        [TestMethod]
        public void PlayerToMove_WhiteMakesAMoveBeforeStartingTheGame_ReturnsBlack()
        {
            const PieceColor expected = PieceColor.Black;

            Board.Setup();
            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            var actual = Game.PlayerToMove;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayerToMove_AfterBothPlayersHaveMovedOnce_ReturnsWhite()
        {
            const PieceColor expected = PieceColor.White;

            Board.Setup();
            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            Board.Move(GetSquare(4, 7), GetSquare(4, 5));
            var actual = Game.PlayerToMove;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GameState_WhenGameIsCreatedWithSetupBoard_GameStateIsSetupMode()
        {
            Assert.AreEqual(GameState.SetupMode, Game.State);
        }

        [TestMethod]
        public void GameState_WhenGameIsCreatedWithSetupBoardAndStartHasBeenCalled_GameStateIsNormal()
        {
            Game.Start();

            Assert.AreEqual(GameState.Normal, Game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteMovesToCheckTheBlackKing_GameStateIsCheck()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 1, new Rook(Board, PieceColor.White));

            Game.Start();
            Board.Move(GetSquare(1, 1), GetSquare(1, 2));
            Board.Move(GetSquare(5, 8), GetSquare(4, 8));
            Board.Move(GetSquare(1, 2), GetSquare(4, 2));

            Assert.AreEqual(GameState.Check, Game.State);
        }

        [TestMethod]
        public void GameState_AfterBlackMovesToCheckTheWhiteKing_GameStateIsCheck()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 8, new Rook(Board, PieceColor.Black));

            Game.Start();
            Board.Move(GetSquare(1, 8), GetSquare(1, 1));

            Assert.AreEqual(GameState.Check, Game.State);
        }

        [TestMethod]
        public void GameState_AfterBlackKingIsMovedToSafeSquare_GameStateIsNormal()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 1, new Rook(Board, PieceColor.White));

            Game.Start();
            Board.Move(GetSquare(1, 1), GetSquare(1, 2));
            Board.Move(GetSquare(5, 8), GetSquare(4, 8));
            Board.Move(GetSquare(1, 2), GetSquare(4, 2));
            Board.Move(GetSquare(4, 8), GetSquare(3, 7));

            Assert.AreEqual(GameState.Normal, Game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteKingIsMovedToSafeSquare_GameStateIsNormal()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 8, new Rook(Board, PieceColor.Black));

            Game.Start();
            Board.Move(GetSquare(1, 8), GetSquare(1, 1));
            Board.Move(GetSquare(5, 1), GetSquare(5, 2));

            Assert.AreEqual(GameState.Normal, Game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteMovesAnotherPieceToRemoveTheCheckOnWhiteKing_GameStateIsNormal()
        {
            Board.SetSquare(5, 1, new King(Board, PieceColor.White));
            Board.SetSquare(4, 2, new Rook(Board, PieceColor.White));
            Board.SetSquare(5, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 8, new Rook(Board, PieceColor.Black));

            Game.Start();
            Board.Move(GetSquare(1, 8), GetSquare(1, 1)); // BlackRook Checks WhiteKing
            Board.Move(GetSquare(4, 2), GetSquare(4, 1)); // WhiteRook Is Moved To Block The Check

            Assert.AreEqual(GameState.Normal, Game.State);
        }

        [TestMethod]
        public void GameState_WhiteMovesToCheckTheBlackKingAndBlackHasNoLegalMoves_GameStateIsCheckMate()
        {
            Board.SetSquare(1, 6, new King(Board, PieceColor.White));
            Board.SetSquare(1, 8, new King(Board, PieceColor.Black));
            Board.SetSquare(4, 6, new Bishop(Board, PieceColor.White));
            Board.SetSquare(2, 5, new Bishop(Board, PieceColor.White));

            Game.Start(Board.GetPosition(), PieceColor.White);
            Board.Move(GetSquare(2, 5), GetSquare(3, 6));

            Assert.AreEqual(GameState.CheckMate, Game.State);
        }

        [TestMethod]
        public void GameState_BlackMovesToCheckTheWhiteKingAndWhiteHasNoLegalMoves_GameStateIsCheckMate()
        {
            Board.SetSquare(8, 1, new King(Board, PieceColor.White));
            Board.SetSquare(8, 3, new King(Board, PieceColor.Black));
            Board.SetSquare(7, 4, new Bishop(Board, PieceColor.Black));
            Board.SetSquare(5, 3, new Bishop(Board, PieceColor.Black));

            Game.Start(Board.GetPosition(), PieceColor.Black);
            Board.Move(GetSquare(7, 4), GetSquare(6, 3));

            Assert.AreEqual(GameState.CheckMate, Game.State);
        }

        [TestMethod]
        public void GameState_BlackMovesKingToGetOutOfCheckAndWhiteCanNotCheckMateInOneMove_GameStateIsStaleMate()
        {
            Board.SetSquare(3, 4, new King(Board, PieceColor.White));
            Board.SetSquare(3, 6, new Queen(Board, PieceColor.White));
            Board.SetSquare(1, 6, new King(Board, PieceColor.Black));

            Game.Start(Board.GetPosition(), PieceColor.Black);
            Assert.AreEqual(GameState.Check, Game.State);

            Board.Move(GetSquare(1, 6), GetSquare(1, 5));

            Assert.AreEqual(GameState.StaleMate, Game.State);
        }

        // http://en.wikipedia.org/wiki/Stalemate
        [TestMethod]
        public void GameState_PlayMatulovicVersusMinevPositionIntoStaleMate_GameStateIsStaleMate()
        {
            Board.SetSquare(1, 6, new Pawn(Board, PieceColor.White));
            Board.SetSquare(2, 6, new Rook(Board, PieceColor.White));
            Board.SetSquare(6, 3, new Pawn(Board, PieceColor.White));
            Board.SetSquare(7, 3, new King(Board, PieceColor.White));
            Board.SetSquare(6, 5, new King(Board, PieceColor.Black));
            Board.SetSquare(1, 2, new Rook(Board, PieceColor.Black));

            Game.Start(Board.GetPosition(), PieceColor.White);
            Board.Move(GetSquare(2, 6), GetSquare(3, 6));
            Board.Move(GetSquare(6, 5), GetSquare(7, 5));
            Board.Move(GetSquare(7, 3), GetSquare(8, 3));
            Board.Move(GetSquare(7, 5), GetSquare(8, 5));
            Board.Move(GetSquare(6, 3), GetSquare(6, 4));
            Board.Move(GetSquare(1, 2), GetSquare(1, 6));
            Board.Move(GetSquare(3, 6), GetSquare(1, 6));

            Assert.AreEqual(GameState.StaleMate, Game.State);
        }

        [TestMethod]
        public void GameState_PlayEvansVersusReshevskyPositionIntoStaleMate_GameStateIsStaleMate()
        {
            Board.SetSquare(2, 4, new Pawn(Board, PieceColor.White));
            Board.SetSquare(5, 4, new Pawn(Board, PieceColor.White));
            Board.SetSquare(6, 3, new Pawn(Board, PieceColor.White));
            Board.SetSquare(7, 3, new Pawn(Board, PieceColor.White));
            Board.SetSquare(8, 3, new Pawn(Board, PieceColor.White));
            Board.SetSquare(6, 7, new Rook(Board, PieceColor.White));
            Board.SetSquare(3, 8, new Queen(Board, PieceColor.White));
            Board.SetSquare(8, 2, new King(Board, PieceColor.White));
            Board.SetSquare(2, 5, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(5, 5, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(7, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(8, 5, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(6, 4, new Knight(Board, PieceColor.Black));
            Board.SetSquare(5, 3, new Rook(Board, PieceColor.Black));
            Board.SetSquare(7, 5, new Queen(Board, PieceColor.Black));
            Board.SetSquare(8, 7, new King(Board, PieceColor.Black));

            Game.Start(Board.GetPosition(), PieceColor.White);
            Board.Move(GetSquare(8, 3), GetSquare(8, 4));
            Board.Move(GetSquare(5, 3), GetSquare(5, 2));
            Board.Move(GetSquare(8, 2), GetSquare(8, 1));
            Board.Move(GetSquare(7, 5), GetSquare(7, 3));
            Board.Move(GetSquare(3, 8), GetSquare(8, 8));
            Board.Move(GetSquare(8, 7), GetSquare(8, 8));
            Board.Move(GetSquare(6, 7), GetSquare(7, 7));
            Board.Move(GetSquare(8, 8), GetSquare(7, 7));

            Assert.AreEqual(GameState.StaleMate, Game.State);
        }

        [TestMethod]
        public void GameState_WhitePawnMovesToTheEightRank_GameStateIsPromotion()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.White));

            Game.Start(Board.GetPosition(), PieceColor.White);
            Board.Move(GetSquare(1, 7), GetSquare(1, 8));

            Assert.AreEqual(GameState.Promotion, Game.State);
        }
    }
}
