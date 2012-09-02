namespace ChessEngineLib.ChessPieces
{
    /// <summary>
    /// Luokka, joka sisältää toiminnallisuuden kuningas tyyppiselle shakkinappulalle.
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
        /// <param name="board">Shakkilauta, jolla siirto tehdään.</param>
        /// <param name="origin">Lähtöpiste, jolla olevan shakkinappulan siirtoa tarkastetaan.</param>
        /// <param name="destination">Päätepiste, johon lähtöpisteen shakkinappulaa ollaan siirtämässä.</param>
        /// <returns>True, jos siirto on laillinen. False, jos siirto on laiton.</returns>
        public override bool IsLegalMove(Board board, Position origin, Position destination)
        {
            // Kuningas saa liikkua mihin tahansa suuntaan yhden ruudun verran
            bool boolToReturn = origin.GetDistanceOfRanks(destination) == 1
                                || origin.GetDistanceOfFiles(destination) == 1;

            // Jos kyseessä on Castling siirto
            if (IsCastlingMove(board, origin, destination))
            {
                boolToReturn = true;
            }

            // Jos kohde ruudussa on saman värinen nappula on siirto automaattisesti laiton
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

        #region Yksityiset metodit

        /// <summary>
        /// Tarkastaa onko kyseessä Castling-siirto.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>True, jos kyseessä on Castling-siirto. False, muussa tapauksessa.</returns>
        private bool IsCastlingMove(Board board, Position origin, Position destination)
        {
            // Apumuuttuja tulokselle
            bool boolToReturn = false;

            // Haetaan kuninkaan puolen torni ruutu
            var kingsideRookPosition = origin.Color == PieceColor.White
                                           ? board.GetPosition(8, 1)
                                           : board.GetPosition(8, 8);

            // Haetaan kuningattaren puolen torni ruutu
            var queensideRookPosition = origin.Color == PieceColor.White
                                            ? board.GetPosition(1, 1)
                                            : board.GetPosition(1, 8);

            // Tarkastetaan castling siirrot kuningkaan puolelle
            if (destination.Color == PieceColor.Empty
                && kingsideRookPosition.Occupier is Rook
                && !IsPathObscured(board, origin, destination)
                && origin.Occupier.MoveCount == 0)
            {
                boolToReturn = true;
            }

            if (destination.Color == PieceColor.Empty
                && queensideRookPosition.Occupier is Rook
                && !IsPathObscured(board, origin, destination)
                && origin.Occupier.MoveCount == 0)
            {
                boolToReturn = true;
            }

            // Palautetaan lopputulos
            return boolToReturn;
        }

        #endregion Yksityiset metodit
    }
}