﻿namespace ChessEngineLib.ChessPieces
{
    public class NullPiece : ChessPiece
    {
        public NullPiece(Board board)
            : base(board, PieceColor.Empty)
        {
        }

        public override bool IsLegalMove(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;

            return true;
        }

        public override bool Attacks(Square origin, Square destination)
        {
            return IsLegalMove(origin, destination);
        }

        public override ChessPiece Clone(Board board)
        {
            var clone = new NullPiece(board)
                {
                    MovingStrategy = MovingStrategy.Clone(board)
                };

            return clone;
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

        public override string ToString()
        {
            return Color + GetType().Name;
        }
    }
}
