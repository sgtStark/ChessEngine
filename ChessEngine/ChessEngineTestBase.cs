namespace ChessEngineTests
{
    using ChessEngineLib;

    public class ChessEngineTestBase
    {
        protected Board Board;
        protected Game Game;

        protected void InitializeBoard()
        {
            Board = new Board();
        }

        protected Square GetSquare(int file, int rank)
        {
            return Board.GetSquare(file, rank);
        }

        protected bool IsLegalMove(Square origin, Square destination)
        {
            var position = Board.GetPosition();
            return position.MoveIsLegal(origin, destination);
        }

        protected void InitializeGame()
        {
            InitializeBoard();
            Game = new Game(Board);
        }
    }
}
