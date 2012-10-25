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

        /// <summary>
        /// P‰‰ttelee suunnan l‰hderuudusta kohderuutuun.
        /// </summary>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>Direction-enumeraation mukainen suunta.</returns>
        public Direction GetDirectionTo(Square destination)
        {
            var chessPiece = Occupier ?? destination.Occupier;

            Direction result = chessPiece.Color == PieceColor.White ? Direction.Forward : Direction.Backward;

            if (File < destination.File
                && Rank < destination.Rank)
            {
                result = Occupier.Color == PieceColor.White
                             ? Direction.ForwardOnLeftDiagonal
                             : Direction.BackwardOnRightDiagonal;
            }
            else if (File > destination.File
                     && Rank > destination.Rank)
            {
                result = Occupier.Color == PieceColor.White
                             ? Direction.BackwardOnLeftDiagonal
                             : Direction.ForwardOnRightDiagonal;
            }
            else if (File > destination.File
                     && Rank < destination.Rank)
            {
                result = Occupier.Color == PieceColor.White
                             ? Direction.ForwardOnLeftDiagonal
                             : Direction.BackwardOnRightDiagonal;
            }
            else if (File < destination.File
                     && Rank > destination.Rank)
            {
                result = Occupier.Color == PieceColor.White
                             ? Direction.BackwardOnRightDiagonal
                             : Direction.ForwardOnLeftDiagonal;
            }

            else if (Rank > destination.Rank)
            {
                result = Occupier.Color == PieceColor.White ? Direction.Backward : Direction.Forward;
            }
            else if (File > destination.File)
            {
                result = Direction.Left;
            }
            else if (File < destination.File)
            {
                result = Direction.Right;
            }

            // Tarkastetaan lopuksi onko siirto ep‰s‰‰nnˆllinen
            if (result.Diagonally() 
                && GetDistanceOfFiles(destination) != GetDistanceOfRanks(destination))
            {
                result = Direction.Irregular;
            }

            return result;
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