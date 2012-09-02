using System.Linq;
using System.Collections.Generic;

namespace ChessEngineLib.ChessPieces
{
    /// <summary>
    /// Abstrakti yliluokka shakkinappulalle, joka sisältää kaikille 
    /// konkreettisille shakkinappuloille yhteiset toiminnot ja tiedot
    /// sekä tarjoaa mahdollisuuden hyödyntää monimuotoisuutta(polymorphism).
    /// </summary>
    public abstract class ChessPiece
    {
        #region Propertyt

        public PieceColor Color { get; private set; }

        public int MoveCount { get; set; }

        #endregion Propertyt

        #region Konstruktorit

        protected ChessPiece(PieceColor color)
        {
            Color = color;
            MoveCount = 0;
        }

        #endregion Konstruktorit

        #region Julkiset metodit

        /// <summary>
        /// Tarkastaa onko siirto laillinen annetulla laudalla.
        /// </summary>
        /// <param name="board">Shakkilauta, jolla siirto tehdään.</param>
        /// <param name="origin">Lähtöpiste, jolla olevan shakkinappulan siirtoa tarkastetaan.</param>
        /// <param name="destination">Päätepiste, johon lähtöpisteen shakkinappulaa ollaan siirtämässä.</param>
        /// <returns>True, jos siirto on laillinen. False, jos siirto on laiton.</returns>
        public abstract bool IsLegalMove(Board board, Position origin, Position destination);

        #endregion Julkiset metodit

        #region Yksityiset metodit

        /// <summary>
        /// Tarkastaa onko shakkilaudalla tehtävän siirron linjalla muita blokkaavia shakkinappuloita.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Siirron lähtöpiste</param>
        /// <param name="destination">Siirron päätepiste</param>
        /// <returns>True, jos siirtolinjalla on blokkaavia shakkinappuloita. False, jos siirtolinja on vapaa.</returns>
        protected bool IsPathObscured(Board board, Position origin, Position destination)
        {
            IList<Position> path = null;

            if (origin.Color == PieceColor.White)
            {
                path = GetPositionsBetween(board, origin, destination);
            }
            else if (origin.Color == PieceColor.Black)
            {
                path = GetPositionsBetween(board, destination, origin);
            }

            // Poistetaan lähtöpiste, koska siinä meillä on nappula jota ollaan siirtämässä.
            path.Remove(origin);
            path.Remove(destination);

            return path.Any(position => position.Color != PieceColor.Empty);
        }

        /// <summary>
        /// Hakee siirtolinjan pisteet, lähtö- ja päätepisteet mukaanlukien.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöpiste</param>
        /// <param name="destination">Päätepiste</param>
        /// <returns>Lista siirtolinjan pisteistä.</returns>
        private IList<Position> GetPositionsBetween(Board board, Position origin, Position destination)
        {
            var positionsBetweenToReturn = new List<Position>();
            
            // Jos siirretään oikealle
            for (int file = origin.File; file <= destination.File; file++)
            {
                for (int rank = origin.Rank; rank <= destination.Rank; rank++)
                {
                    positionsBetweenToReturn.Add(board.GetPosition(file, rank));
                }
            }

            // Jos siirretään vasemmalle
            for (int file = origin.File; file > destination.File; file--)
            {
                for (int rank = origin.Rank; rank <= destination.Rank; rank++)
                {
                    positionsBetweenToReturn.Add(board.GetPosition(file, rank));
                }
            }

            return positionsBetweenToReturn;
        }

        #endregion Yksityiset metodit
    }
}