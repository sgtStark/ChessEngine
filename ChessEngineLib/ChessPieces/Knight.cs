using System;

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

        private bool MovingTwoRanksAndOneFile(Square origin, Square destination)
        {
            return (Math.Abs(destination.Rank - origin.Rank) == 2
                    && Math.Abs(destination.File - origin.File) == 1);
        }

        private bool MovingOneRankAndTwoFiles(Square origin, Square destination)
        {
            return (Math.Abs(destination.Rank - origin.Rank) == 1
                    && Math.Abs(destination.File - origin.File) == 2);
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
    }
}