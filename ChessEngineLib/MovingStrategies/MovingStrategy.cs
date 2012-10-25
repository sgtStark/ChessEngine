using ChessEngineLib.ChessPieces;

namespace ChessEngineLib.MovingStrategies
{
    public abstract class MovingStrategy
    {
        protected readonly Board Board;

        protected MovingStrategy(Board board)
        {
            Board = board;
        }

        public int MoveCount { get; private set; }

        public virtual void Move(Square origin, Square destination)
        {
            MoveCount++;
            Board.SetPosition(origin.File, origin.Rank, new NullPiece());
            Board.SetPosition(destination.File, destination.Rank, origin.Occupier);
        }

        public abstract bool IsSpecialMove(Square origin, Square destination);
    }
}
