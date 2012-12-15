namespace ChessEngineLib.ChessPieces
{
    public class Queen : ChessPiece
    {
        public Queen(Board board, PieceColor color)
            : base(board, color)
        {
        }

        public override bool IsLegalMove(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;
            if (origin.AlongFileOrRank(destination)) return true;

            return origin.DiagonallyTo(destination);
        }

        public override bool Attacks(Square origin, Square destination)
        {
            return IsLegalMove(origin, destination);
        }

        public override ChessPiece Clone(Board board)
        {
            var clone = new Queen(board, Color)
                {
                    MovingStrategy = MovingStrategy.Clone(board)
                };

            return clone;
        }

        private bool Equals(Queen other)
        {
            return !ReferenceEquals(null, other)
                && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Queen)) return false;

            return Equals((Queen) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return Color + GetType().Name;
        }
    }
}