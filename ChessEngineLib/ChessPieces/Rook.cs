namespace ChessEngineLib.ChessPieces
{
    /// <summary>
    /// Luokka, joka sisältää toiminnallisuuden tornityyppiselle shakkinappulalle.
    /// </summary>
    public class Rook : ChessPiece
    {
        #region Konstruktorit

        public Rook(PieceColor color)
            : base(color)
        {
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
        public override bool IsLegalMove(Board board, Position origin, Position destination)
        {
            var boolToReturn = false;

            var directionOfTheMove = origin.GetDirectionTo(destination);

            if (directionOfTheMove.IsAlongFileOrRank()
                && !IsPathObscured(board, origin, destination)
                && destination.Color != origin.Color)
            {
                boolToReturn = true;
            }

            return boolToReturn;
        }

        /// <summary>
        /// Tarkastaa onko tämä torninappula sama kuin
        /// toinen torninappula.
        /// </summary>
        /// <param name="other">Toinen sotilasnappula, johon tätä nappulaa ollaan vertaamassa.</param>
        /// <returns>True, jos kummatkin nappulat ovat yksi ja sama. False, jos nappulat eroavat toisistaan.</returns>
        public bool Equals(Rook other)
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
            //if (obj == null || GetType() != obj.GetType())
            //    return false;

            //var other = obj as Rook;

            //return (Color == other.Color);
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Rook)) return false;
            return Equals((Rook)obj);
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
