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
            Board.SetPosition(origin.File, origin.Rank, new NullPiece());
            Board.SetPosition(destination.File, destination.Rank, origin.Occupier);
        }

        public abstract bool IsSpecialMove(Square origin, Square destination);

        public abstract MovingStrategy Clone(Board board);
    }
}
