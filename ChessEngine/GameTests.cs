using ChessEngineLib;
using ChessEngineLib.ChessPieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests
{
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

        #region PlayerToMove tests

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
            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            var actual = game.PlayerToMove;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayerToMove_AfterBothPlayersHaveMovedOnce_ReturnsWhite()
        {
            Game game = CreateNewGame();
            const PieceColor expected = PieceColor.White;

            Board board = game.Board;
            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            board.Move(board.GetPosition(4, 7), board.GetPosition(4, 5));
            var actual = game.PlayerToMove;

            Assert.AreEqual(expected, actual);
        }

        #endregion PlayerToMove tests

        #region GameState tests

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
            board.SetPosition(5, 1, new King(board, PieceColor.White));
            board.SetPosition(5, 8, new King(board, PieceColor.Black));
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));

            game.Start();
            board.Move(board.GetPosition(1, 1), board.GetPosition(1, 2));
            board.Move(board.GetPosition(5, 8), board.GetPosition(4, 8));
            board.Move(board.GetPosition(1, 2), board.GetPosition(4, 2));

            Assert.AreEqual(GameState.Check, game.State);
        }

        [TestMethod]
        public void GameState_AfterBlackMovesToCheckTheWhiteKing_GameStateIsCheck()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetPosition(5, 1, new King(board, PieceColor.White));
            board.SetPosition(5, 8, new King(board, PieceColor.Black));
            board.SetPosition(1, 8, new Rook(board, PieceColor.Black));

            game.Start();
            board.Move(board.GetPosition(1, 8), board.GetPosition(1, 1));

            Assert.AreEqual(GameState.Check, game.State);
        }

        [TestMethod]
        public void GameState_AfterBlackKingIsMovedToSafeSquare_GameStateIsNormal()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetPosition(5, 1, new King(board, PieceColor.White));
            board.SetPosition(5, 8, new King(board, PieceColor.Black));
            board.SetPosition(1, 1, new Rook(board, PieceColor.White));

            game.Start();
            board.Move(board.GetPosition(1, 1), board.GetPosition(1, 2));
            board.Move(board.GetPosition(5, 8), board.GetPosition(4, 8));
            board.Move(board.GetPosition(1, 2), board.GetPosition(4, 2));
            board.Move(board.GetPosition(4, 8), board.GetPosition(3, 7));

            Assert.AreEqual(GameState.Normal, game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteKingIsMovedToSafeSquare_GameStateIsNormal()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetPosition(5, 1, new King(board, PieceColor.White));
            board.SetPosition(5, 8, new King(board, PieceColor.Black));
            board.SetPosition(1, 8, new Rook(board, PieceColor.Black));

            game.Start();
            board.Move(board.GetPosition(1, 8), board.GetPosition(1, 1));
            board.Move(board.GetPosition(5, 1), board.GetPosition(5, 2));

            Assert.AreEqual(GameState.Normal, game.State);
        }

        [TestMethod]
        public void GameState_AfterWhiteMovesAnotherPieceToRemoveTheCheckOnWhiteKing_GameStateIsNormal()
        {
            Game game = new Game(new Board());
            Board board = game.Board;
            board.SetPosition(5, 1, new King(board, PieceColor.White));
            board.SetPosition(4, 2, new Rook(board, PieceColor.White));
            board.SetPosition(5, 8, new King(board, PieceColor.Black));
            board.SetPosition(1, 8, new Rook(board, PieceColor.Black));

            game.Start();
            board.Move(board.GetPosition(1, 8), board.GetPosition(1, 1)); // BlackRook Checks WhiteKing
            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 1)); // WhiteRook Is Moved To Block The Check

            Assert.AreEqual(GameState.Normal, game.State);
        }

        #endregion GameState tests
    }
}
