using System;

namespace ChessEngineLib
{
    using ChessPieces;

    public class Square
    {
        private readonly int _file;
        private readonly int _rank;
        private readonly ChessPiece _occupier;

        public int File { get { return _file; } }
        public int Rank { get { return _rank; } }
        public PieceColor Color { get { return Occupier.Color; } }
        public ChessPiece Occupier { get { return _occupier; } }

        public Square(int file, int rank, ChessPiece occupier)
        {
            _file = file;
            _rank = rank;
            _occupier = occupier;
        }

        public bool ForwardTo(Square destination)
        {
            if (Color == PieceColor.White && Rank < destination.Rank && File == destination.File) return true;
            if (Color == PieceColor.Black && Rank > destination.Rank && File == destination.File) return true;
            
            return false;
        }

        public bool AlongFileOrRank(Square destination)
        {
            if (AlongRank(destination)) return true;
            if (AlongFile(destination)) return true;

            return false;
        }

        private bool AlongRank(Square destination)
        {
            return Rank > destination.Rank && File == destination.File
                   || Rank < destination.Rank && File == destination.File;
        }

        private bool AlongFile(Square destination)
        {
            return Rank == destination.Rank && File > destination.File
                   || Rank == destination.Rank && File < destination.File;
        }

        public bool DiagonallyForwardTo(Square destination)
        {
            if (GetDistanceOfFiles(destination) != GetDistanceOfRanks(destination)) return false;
            if (DiagonallyForwardToLeftForWhite(destination)) return true;
            if (DiagonallyForwardToRightForWhite(destination)) return true;
            if (DiagonallyForwardToLeftForBlack(destination)) return true;
            if (DiagonallyForwardToRightForBlack(destination)) return true;

            return false;
        }

        private bool DiagonallyForwardToLeftForBlack(Square destination)
        {
            return Color == PieceColor.Black
                   && File < destination.File && Rank > destination.Rank;
        }

        private bool DiagonallyForwardToRightForBlack(Square destination)
        {
            return Color == PieceColor.Black
                   && File > destination.File && Rank > destination.Rank;
        }

        private bool DiagonallyForwardToLeftForWhite(Square destination)
        {
            return Color == PieceColor.White
                   && File < destination.File && Rank < destination.Rank;
        }

        private bool DiagonallyForwardToRightForWhite(Square destination)
        {
            return Color == PieceColor.White
                   && File > destination.File && Rank < destination.Rank;
        }

        public bool DiagonallyTo(Square destination)
        {
            if (GetDistanceOfFiles(destination) != GetDistanceOfRanks(destination)) return false;
            if (File < destination.File && Rank < destination.Rank) return true;
            if (File > destination.File && Rank > destination.Rank) return true;
            if (File > destination.File && Rank < destination.Rank) return true;
            if (File < destination.File && Rank > destination.Rank) return true;

            return false;
        }

        public int GetDistanceOfRanks(Square destination)
        {
            return Math.Abs(Rank - destination.Rank);
        }

        public int GetDistanceOfFiles(Square destination)
        {
            return Math.Abs(File - destination.File);
        }

        public bool Equals(Square other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Occupier, Occupier)
                && other._rank == _rank && other._file == _file;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Square)) return false;
            return Equals((Square) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Occupier != null ? Occupier.GetHashCode() : 0);
                result = (result*397) ^ _rank;
                result = (result*397) ^ _file;
                return result;
            }
        }
    }
}