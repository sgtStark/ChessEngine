namespace ChessEngineTests
{
    using ChessEngineLib;

    public class ChessEngineTestBase
    {
        protected static Board CreateEmptyBoard()
        {
            return new Board();
        }

        protected static Game CreateNewGame()
        {
            return new Game(CreateSetupBoard());
        }

        private static Board CreateSetupBoard()
        {
            var boardToReturn = CreateEmptyBoard();
            boardToReturn.Setup();
            return boardToReturn;
        }
    }
}
