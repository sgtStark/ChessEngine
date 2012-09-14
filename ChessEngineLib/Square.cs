using System;
using ChessEngineLib.ChessPieces;

namespace ChessEngineLib
{
    /// <summary>
    /// Luokka, joka sis‰lt‰‰ kaikki tarvittavat toiminnot
    /// shakkilaudan ruutujen vertailuun.
    /// </summary>
    public class Square
    {
        #region Sis‰iset dataj‰senet

        /// <summary>Sarake</summary>
        private readonly int _file;

        /// <summary>Rivi</summary>
        private readonly int _rank;

        private readonly ChessPiece _occupier;

        #endregion Sis‰iset dataj‰senet

        #region Luokkakohtaiset propertyt

        /// <summary>Sarake</summary>
        public int File
        {
            get { return _file; }
        }

        /// <summary>Rivi</summary>
        public int Rank
        {
            get { return _rank; }
        }

        /// <summary>Tila, eli onko tyhj‰ vai sis‰lt‰‰kˆ valkoisen tai mustan shakkinappulan</summary>
        public PieceColor Color
        {
            get
            {
                var pieceColorToReturn = PieceColor.Empty;

                if (Occupier != null)
                {
                    pieceColorToReturn = Occupier.Color;
                }

                return pieceColorToReturn;
            }
        }

        public ChessPiece Occupier
        {
            get { return _occupier; }
        }

        #endregion Luokkakohtaiset propertyt

        #region Konstruktorit

        public Square(int file, int rank)
        {
            _file = file;
            _rank = rank;
            _occupier = null;
        }

        public Square(int file, int rank, ChessPiece occupier)
        {
            _file = file;
            _rank = rank;
            _occupier = occupier;
        }

        #endregion Konstruktorit

        #region Julkiset metodit

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
            if (result.IsAlongDiagonal() 
                && GetDistanceOfFiles(destination) != GetDistanceOfRanks(destination))
            {
                result = Direction.Irregular;
            }

            return result;
        }

        /// <summary>
        /// Palauttaa et‰isyyden kohderuutuun riveiss‰.
        /// </summary>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>Kokonaisluku</returns>
        public int GetDistanceOfRanks(Square destination)
        {
            return Math.Abs(Rank - destination.Rank);
        }

        /// <summary>
        /// Palauttaa et‰isyyden kohderuutuun sarakkeissa.
        /// </summary>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>Kokonaisluku</returns>
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

        #endregion Julkiset metodit
    }
}