namespace ChessEngineLib.ChessPieces
{
    public class Knight : ChessPiece
    {
        public Knight(Board board, PieceColor color)
            : base(board, color)
        {
        }

        public override bool IsLegalMove(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;

            if (MovingTwoRanksAndOneFile(origin, destination)) return true;
            if (MovingOneRankAndTwoFiles(origin, destination)) return true;

            return false;
        }

        public override bool Attacks(Square origin, Square destination)
        {
            return IsLegalMove(origin, destination);
        }

        public override ChessPiece Clone(Board board)
        {
            var clone = new Knight(board, Color)
                {
                    MovingStrategy = MovingStrategy.Clone(board)
                };

            return clone;
        }

        private bool MovingTwoRanksAndOneFile(Square origin, Square destination)
        {
            return (origin.DistanceOfRanksIsTwoTo(destination)
                    && origin.DistanceOfFilesIsOneTo(destination));
        }

        private bool MovingOneRankAndTwoFiles(Square origin, Square destination)
        {
            return (origin.DistanceOfRanksIsOneTo(destination)
                    && origin.DistanceOfFilesIsTwoTo(destination));
        }

        private bool Equals(Knight other)
        {
            return !ReferenceEquals(null, other)
                && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Knight)) return false;

            return Equals((Knight)obj);
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