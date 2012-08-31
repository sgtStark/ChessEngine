using System;

namespace ChessEngineLib.ChessPieces
{
    /// <summary>
    /// Luokka, joka sisältää toiminnallisuuden hevostyyppiselle shakkinappulalle.
    /// </summary>
    public class Knight : ChessPiece
    {
        #region Konstruktorit

        public Knight(PieceColor color)
            : base(color)
        {
        }

        #endregion Konstruktorit

        #region Julkiset metodit

        #region Overrides of ChessPiece

        /// <summary>
        /// Tarkastaa onko siirto laillinen annetulla laudalla.
        /// </summary>
        /// <param name="board">Shakkilauta, jolla siirto tehdään.</param>
        /// <param name="origin">Lähtöpiste, jolla olevan shakkinappulan siirtoa tarkastetaan.</param>
        /// <param name="destination">Päätepiste, johon lähtöpisteen shakkinappulaa ollaan siirtämässä.</param>
        /// <returns>True, jos siirto on laillinen. False, jos siirto on laiton.</returns>
        public override bool IsLegalMove(Board board, Position origin, Position destination)
        {
            // Liikutaan kaksi ruutua eteen- tai taaksepäin ja yksi ruutu vasemmalle tai oikealle
            bool boolToReturn = Math.Abs(destination.Rank - origin.Rank) == 2
                                && Math.Abs(destination.File - origin.File) == 1;

            // Liikutaan yksi ruutu eteen- tai taaksepäin ja kaksi ruutua vasemmalle tai oikealle
            if (Math.Abs(destination.Rank - origin.Rank) == 1
                && Math.Abs(destination.File - origin.File) == 2)
            {
                boolToReturn = true;
            }

            // Jos kohde ruutu on miehitetty saman värisellä shakkinappulalla, on 
            // siirto automaattisesti laiton.
            if (destination.Occupier != null
                && destination.Color == origin.Color)
            {
                boolToReturn = false;
            }

            return boolToReturn;
        }

        #endregion

        public bool Equals(Knight other)
        {
            return !ReferenceEquals(null, other)
                && Color == other.Color;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Knight)) return false;
            return Equals((Knight)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return 0;
        }

        #endregion Julkiset metodit
    }
}