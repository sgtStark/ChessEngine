using ChessEngineLib;

namespace ChessEngineTests
{
    public class ChessEngineTestBase
    {
        #region Suojatut metodit

        protected static Board CreateEmptyBoard()
        {
            return new Board();
        }

        protected static Game CreateNewGame()
        {
            return new Game(CreateSetupBoard());
        }

        #endregion Suojatut metodit

        #region Yksityiset metodit

        private static Board CreateSetupBoard()
        {
            var boardToReturn = CreateEmptyBoard();
            boardToReturn.Setup();
            return boardToReturn;
        }

        #endregion Yksityiset metodit
    }
}
