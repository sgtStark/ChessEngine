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
    }
}