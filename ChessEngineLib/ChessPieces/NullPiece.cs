namespace ChessEngineLib.ChessPieces
{
    public class NullPiece : ChessPiece
    {
        public NullPiece()
            : base(null, PieceColor.Empty)
        {
        }

        public override bool IsLegalMove(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;

            return true;
        }

        private bool Equals(NullPiece other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((NullPiece) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
