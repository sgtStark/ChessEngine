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

            return (origin.DiagonallyTo(destination) && PathIsFree(origin, destination));
        }

        public override bool Attacks(Square origin, Square destination)
        {
            return IsLegalMove(origin, destination);
        }

        public override ChessPiece Clone(Board board)
        {
            var newBishopToReturn = new Bishop(board, Color)
                {
                    MovingStrategy = MovingStrategy.Clone(board)
                };

            return newBishopToReturn;
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