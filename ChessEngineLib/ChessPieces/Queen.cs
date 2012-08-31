namespace ChessEngineLib.ChessPieces
{
    /// <summary>
    /// Luokka, joka sisältää toiminnallisuuden kuningatar tyyppiselle shakkinappulalle.
    /// </summary>
    public class Queen : ChessPiece
    {
        #region Konstruktorit

        public Queen(PieceColor color)
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
            var boolToReturn = false;
            var directionOfTheMove = origin.GetDirectionTo(destination);

            // Jos siirto kulkee pitkin saraketta tai riviä tai jos
            // siirto kulkee viistoon on se laillinen
            if (directionOfTheMove.IsAlongFileOrRank()
                || directionOfTheMove.IsAlongDiagonal())
            {
                boolToReturn = true;
            }

            // Jos kohderuudussa on saman värinen shakkinappula,
            // on siirto automaattisesti laiton.
            if (destination.Color == Color)
            {
                boolToReturn = false;
            }

            return boolToReturn;
        }

        #endregion Overrides of ChessPiece

        public bool Equals(Queen other)
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
            if (obj.GetType() != typeof (Queen)) return false;
            return Equals((Queen) obj);
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