namespace ChessEngineLib.MovingStrategies
{
    using ChessPieces;

    public abstract class MovingStrategy
    {
        protected readonly Board Board;

        protected MovingStrategy(Board board)
        {
            Board = board;
        }

        public int MoveCount { get; protected set; }

        public virtual void Move(Square origin, Square destination)
        {
            MoveCount++;
            Board.SetSquare(origin.File, origin.Rank, new NullPiece());
            Board.SetSquare(destination.File, destination.Rank, origin.Occupier);
        }

        public abstract bool IsSpecialMove(Square origin, Square destination);

        public abstract MovingStrategy Clone(Board board);
    }
}
