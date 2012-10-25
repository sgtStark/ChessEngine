namespace ChessEngineLib.ChessPieces
{
    public class Bishop : ChessPiece
    {
        public Bishop(Board board, PieceColor color)
            : base(board, color)
        {
        }

        public override bool IsLegalMove(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;

            var moving = origin.GetDirectionTo(destination);

            return (moving.Diagonally() && PathIsFree(origin, destination));
        }

        private bool Equals(Bishop other)
        {
            return !ReferenceEquals(null, other)
                && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Bishop)) return false;
            return Equals((Bishop) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}