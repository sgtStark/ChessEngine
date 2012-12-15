using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    [TestClass]
    public class GameTests : ChessEngineTestBase
    {
        [TestMethod]
        public void GetBoard_AfterCreatingNewGame_ReturnsSetupBoard()
        {
            Game game = CreateNewGame();
            Board expected = new Board();
            expected.Setup();

            var actual = game.Board;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayerToMove_BeforeGameIsStarted_ReturnsEmpty()
        {
            Game game = CreateNewGame();

            var result = game.PlayerToMove;

            Assert.AreEqual(PieceColor.Empty, result);
        }

        [TestMethod]
        public void PlayerToMove_AfterGameIsStarted_ReturnsWhite()
        {
            Game game = CreateNewGame();

            game.Start();
            var result = game.PlayerToMove;

            Assert.AreEqual(PieceColor.White, result);
        }

        [TestMethod]
        public void PlayerToMove_WhiteMakesAMoveBeforeStartingTheGame_ReturnsBlack()
        {
            Game game = CreateNewGame();
            const PieceColor expected = PieceColor.Black;

            Board board = game.Board;
            board.Move(board.GetSquare(4, 2), board.GetSquare(4, 4));
            var actual = game.PlayerToMove;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayerToMove_AfterBothPlayersHaveMovedOnce_ReturnsWhite()
        {
            Game game = CreateNewGame();
            const PieceColor expected = PieceColor.White;

            Board board = game.Board;
            board.Move(board.GetSquare(4, 2), board.GetSquare(4, 4));
            board.Move(board.GetSquare(4, 7), board.GetSquare(4, 5));
            var actual = game.PlayerToMove;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GameState_WhenGameIsCreatedWithSetupBoard_GameStateIsSetupMode()
        {
            Game game = CreateNewGame();

            Assert.AreEqual(GameState.SetupMode, game.State);
        }

        [TestMethod]
        public void GameState_WhenGameIsCreatedWithSetupBoardAndStartHasBeenCalled_GameStateIsNormal()
        {
            Game game = CreateNewGame();

            game.Start();

            Assert.AreEqual(GameState.Normal, game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteMovesToCheckTheBlackKing_GameStateIsCheck()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(5, 1, new King(board, PieceColor.White));
            board.SetSquare(5, 8, new King(board, PieceColor.Black));
            board.SetSquare(1, 1, new Rook(board, PieceColor.White));

            game.Start();
            board.Move(board.GetSquare(1, 1), board.GetSquare(1, 2));
            board.Move(board.GetSquare(5, 8), board.GetSquare(4, 8));
            board.Move(board.GetSquare(1, 2), board.GetSquare(4, 2));

            Assert.AreEqual(GameState.Check, game.State);
        }

        [TestMethod]
        public void GameState_AfterBlackMovesToCheckTheWhiteKing_GameStateIsCheck()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(5, 1, new King(board, PieceColor.White));
            board.SetSquare(5, 8, new King(board, PieceColor.Black));
            board.SetSquare(1, 8, new Rook(board, PieceColor.Black));

            game.Start();
            board.Move(board.GetSquare(1, 8), board.GetSquare(1, 1));

            Assert.AreEqual(GameState.Check, game.State);
        }

        [TestMethod]
        public void GameState_AfterBlackKingIsMovedToSafeSquare_GameStateIsNormal()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(5, 1, new King(board, PieceColor.White));
            board.SetSquare(5, 8, new King(board, PieceColor.Black));
            board.SetSquare(1, 1, new Rook(board, PieceColor.White));

            game.Start();
            board.Move(board.GetSquare(1, 1), board.GetSquare(1, 2));
            board.Move(board.GetSquare(5, 8), board.GetSquare(4, 8));
            board.Move(board.GetSquare(1, 2), board.GetSquare(4, 2));
            board.Move(board.GetSquare(4, 8), board.GetSquare(3, 7));

            Assert.AreEqual(GameState.Normal, game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteKingIsMovedToSafeSquare_GameStateIsNormal()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(5, 1, new King(board, PieceColor.White));
            board.SetSquare(5, 8, new King(board, PieceColor.Black));
            board.SetSquare(1, 8, new Rook(board, PieceColor.Black));

            game.Start();
            board.Move(board.GetSquare(1, 8), board.GetSquare(1, 1));
            board.Move(board.GetSquare(5, 1), board.GetSquare(5, 2));

            Assert.AreEqual(GameState.Normal, game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteMovesAnotherPieceToRemoveTheCheckOnWhiteKing_GameStateIsNormal()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(5, 1, new King(board, PieceColor.White));
            board.SetSquare(4, 2, new Rook(board, PieceColor.White));
            board.SetSquare(5, 8, new King(board, PieceColor.Black));
            board.SetSquare(1, 8, new Rook(board, PieceColor.Black));

            game.Start();
            board.Move(board.GetSquare(1, 8), board.GetSquare(1, 1)); // BlackRook Checks WhiteKing
            board.Move(board.GetSquare(4, 2), board.GetSquare(4, 1)); // WhiteRook Is Moved To Block The Check

            Assert.AreEqual(GameState.Normal, game.State);
        }

        [TestMethod]
        public void GameState_WhiteMovesToCheckTheBlackKingAndBlackHasNoLegalMoves_GameStateIsCheckMate()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(1, 6, new King(board, PieceColor.White));
            board.SetSquare(1, 8, new King(board, PieceColor.Black));
            board.SetSquare(4, 6, new Bishop(board, PieceColor.White));
            board.SetSquare(2, 5, new Bishop(board, PieceColor.White));

            game.Start(PieceColor.White);
            board.Move(board.GetSquare(2, 5), board.GetSquare(3, 6));

            Assert.AreEqual(GameState.CheckMate, game.State);
        }

        [TestMethod]
        public void GameState_BlackMovesToCheckTheWhiteKingAndWhiteHasNoLegalMoves_GameStateIsCheckMate()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(8, 1, new King(board, PieceColor.White));
            board.SetSquare(8, 3, new King(board, PieceColor.Black));
            board.SetSquare(7, 4, new Bishop(board, PieceColor.Black));
            board.SetSquare(5, 3, new Bishop(board, PieceColor.Black));

            game.Start(PieceColor.Black);
            board.Move(board.GetSquare(7, 4), board.GetSquare(6, 3));

            Assert.AreEqual(GameState.CheckMate, game.State);
        }

        [TestMethod]
        public void GameState_BlackMovesKingToGetOutOfCheckAndWhiteCanNotCheckMateInOneMove_GameStateIsStaleMate()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(3, 4, new King(board, PieceColor.White));
            board.SetSquare(3, 6, new Queen(board, PieceColor.White));
            board.SetSquare(1, 6, new King(board, PieceColor.Black));

            game.Start(PieceColor.Black);
            Assert.AreEqual(GameState.Check, game.State);

            board.Move(board.GetSquare(1, 6), board.GetSquare(1, 5));

            Assert.AreEqual(GameState.StaleMate, game.State);
        }

        // http://en.wikipedia.org/wiki/Stalemate
        [TestMethod]
        public void GameState_PlayMatulovicVersusMinevPositionIntoStaleMate_GameStateIsStaleMate()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(1, 6, new Pawn(board, PieceColor.White));
            board.SetSquare(2, 6, new Rook(board, PieceColor.White));
            board.SetSquare(6, 3, new Pawn(board, PieceColor.White));
            board.SetSquare(7, 3, new King(board, PieceColor.White));
            board.SetSquare(6, 5, new King(board, PieceColor.Black));
            board.SetSquare(1, 2, new Rook(board, PieceColor.Black));

            game.Start(PieceColor.White);
            board.Move(board.GetSquare(2, 6), board.GetSquare(3, 6));
            board.Move(board.GetSquare(6, 5), board.GetSquare(7, 5));
            board.Move(board.GetSquare(7, 3), board.GetSquare(8, 3));
            board.Move(board.GetSquare(7, 5), board.GetSquare(8, 5));
            board.Move(board.GetSquare(6, 3), board.GetSquare(6, 4));
            board.Move(board.GetSquare(1, 2), board.GetSquare(1, 6));
            board.Move(board.GetSquare(3, 6), board.GetSquare(1, 6));

            Assert.AreEqual(GameState.StaleMate, game.State);
        }

        [TestMethod]
        public void GameState_PlayEvansVersusReshevskyPositionIntoStaleMate_GameStateIsStaleMate()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetSquare(2, 4, new Pawn(board, PieceColor.White));
            board.SetSquare(5, 4, new Pawn(board, PieceColor.White));
            board.SetSquare(6, 3, new Pawn(board, PieceColor.White));
            board.SetSquare(7, 3, new Pawn(board, PieceColor.White));
            board.SetSquare(8, 3, new Pawn(board, PieceColor.White));
            board.SetSquare(6, 7, new Rook(board, PieceColor.White));
            board.SetSquare(3, 8, new Queen(board, PieceColor.White));
            board.SetSquare(8, 2, new King(board, PieceColor.White));
            board.SetSquare(2, 5, new Pawn(board, PieceColor.Black));
            board.SetSquare(5, 5, new Pawn(board, PieceColor.Black));
            board.SetSquare(7, 7, new Pawn(board, PieceColor.Black));
            board.SetSquare(8, 5, new Pawn(board, PieceColor.Black));
            board.SetSquare(6, 4, new Knight(board, PieceColor.Black));
            board.SetSquare(5, 3, new Rook(board, PieceColor.Black));
            board.SetSquare(7, 5, new Queen(board, PieceColor.Black));
            board.SetSquare(8, 7, new King(board, PieceColor.Black));

            game.Start(PieceColor.White);
            board.Move(board.GetSquare(8, 3), board.GetSquare(8, 4));
            board.Move(board.GetSquare(5, 3), board.GetSquare(5, 2));
            board.Move(board.GetSquare(8, 2), board.GetSquare(8, 1));
            board.Move(board.GetSquare(7, 5), board.GetSquare(7, 3));
            board.Move(board.GetSquare(3, 8), board.GetSquare(8, 8));
            board.Move(board.GetSquare(8, 7), board.GetSquare(8, 8));
            board.Move(board.GetSquare(6, 7), board.GetSquare(7, 7));
            board.Move(board.GetSquare(8, 8), board.GetSquare(7, 7));

            Assert.AreEqual(GameState.StaleMate, game.State);
        }
    }
}
