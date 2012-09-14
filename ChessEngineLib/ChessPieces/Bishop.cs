namespace ChessEngineLib.ChessPieces
{
    /// <summary>
    /// Luokka, jonka toiminnallisuuden lähetti tyyppiselle shakkinappulalle.
    /// </summary>
    public class Bishop : ChessPiece
    {
        #region Konstruktorit

        public Bishop(PieceColor color)
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
        public override bool IsLegalMove(Board board, Square origin, Square destination)
        {
            var boolToReturn = false;
            var directionOfTheMove = origin.GetDirectionTo(destination);

            // Jos siirretään viistottain eikä siirtolinjaa ole blokattu
            // on siirto laillinen
            if (directionOfTheMove.IsAlongDiagonal()
                && !IsPathObscured(board, origin, destination))
            {
                boolToReturn = true;
            }

            // Jos kohde ruudulla on saman värinen shakkinappula on siirto automaattisesti laiton
            if (destination.Color == origin.Color)
            {
                boolToReturn = false;
            }

            return boolToReturn;
        }

        #endregion Overrides of ChessPiece 

        private bool Equals(Bishop other)
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
            if (obj.GetType() != typeof (Bishop)) return false;
            return Equals((Bishop) obj);
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