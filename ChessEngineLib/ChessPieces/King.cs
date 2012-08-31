namespace ChessEngineLib.ChessPieces
{
    /// <summary>
    /// Luokka, joka sis�lt�� toiminnallisuuden kuningas tyyppiselle shakkinappulalle.
    /// </summary>
    public class King : ChessPiece
    {
        #region Konstruktorit

        public King(PieceColor color)
            : base(color)
        {
        }

        #endregion Konstruktorit

        #region Julkiset metodit

        #region Overrides of ChessPiece

        /// <summary>
        /// Tarkastaa onko siirto laillinen annetulla laudalla.
        /// </summary>
        /// <param name="board">Shakkilauta, jolla siirto tehd��n.</param>
        /// <param name="origin">L�ht�piste, jolla olevan shakkinappulan siirtoa tarkastetaan.</param>
        /// <param name="destination">P��tepiste, johon l�ht�pisteen shakkinappulaa ollaan siirt�m�ss�.</param>
        /// <returns>True, jos siirto on laillinen. False, jos siirto on laiton.</returns>
        public override bool IsLegalMove(Board board, Position origin, Position destination)
        {
            // Kuningas saa liikkua mihin tahansa suuntaan yhden ruudun verran
            bool boolToReturn = origin.GetDistanceOfRanks(destination) == 1
                                || origin.GetDistanceOfFiles(destination) == 1;

            // Jos kohde ruudussa on saman v�rinen nappula on siirto automaattisesti laiton
            if (destination.Color == origin.Color)
            {
                boolToReturn = false;
            }

            return boolToReturn;
        }

        #endregion Overrides of ChessPiece

        public bool Equals(King other)
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
            if (obj.GetType() != typeof (King)) return false;
            return Equals((King) obj);
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