namespace ChessEngineLib.MovingStrategies
{
    using ChessPieces;

    /// <summary>
    /// Kuningas shakkinappulan siirtostrategia. Sisältää linnoitus siirtojen erikoistapausten 
    /// mukaiset lisäkäsittelyt.
    /// </summary>
    public class KingMovingStrategy : IMovingStrategy
    {
        #region Vakiot

        private const int NUMBER_OF_THE_FIRST_FILE = 1;
        private const int NUMBER_OF_THE_LAST_FILE = 8;

        private const int OFFICER_RANK_FOR_WHITE = 1;
        private const int OFFICER_RANK_FOR_BLACK = 8;

        #endregion Vakiot

        #region Sisäiset datajäsenet

        /// <summary>Perus-siirtostrategia, joka ajetaan ensin.</summary>
        private readonly IMovingStrategy _baseMovingStrategy;

        #endregion Sisäiset datajäsenet

        #region Konstruktorit

        /// <summary>
        /// Parametrimuodostin, joka ottaa vastaan tarvittavat riippuvuudet.
        /// </summary>
        /// <param name="baseMovingStrategy">Pohja/perus siirtostrategia.</param>
        public KingMovingStrategy(IMovingStrategy baseMovingStrategy)
        {
            _baseMovingStrategy = baseMovingStrategy;
        }

        #endregion Konstruktorit

        #region Implementation of IMovingStrategy

        /// <summary>
        /// Suoritetaan siirto strategian erikoistapaukset huomioon ottaen.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        public void Move(Board board, Square origin, Square destination)
        {
            _baseMovingStrategy.Move(board, origin, destination);

            HandleCastling(board, origin, destination);
        }

        /// <summary>
        /// Sisältääkö siirto erikoistarkastuksia, jotka ohittavat normaalin tarkastuksen tuloksen.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>True, jos siirto sisältää normaalin tarkastuksen tuloksen ohittavan tuloksen. False, muutoin.</returns>
        public bool IsSpecialMove(Board board, Square origin, Square destination)
        {
            return IsCastlingMove(board, origin, destination);
        }

        #endregion Implementation of IMovingStrategy

        #region Yksityiset metodit

        /// <summary>
        /// Tarkastaa onko kyseessä Castling-siirto.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>True, jos kyseessä on Castling-siirto. False, muussa tapauksessa.</returns>
        private bool IsCastlingMove(Board board, Square origin, Square destination)
        {
            // Apumuuttuja tulokselle
            bool boolToReturn = false;

            // Haetaan kuninkaan puolen torni ruutu
            var kingsideRookPosition = origin.Color == PieceColor.White
                                           ? board.GetPosition(NUMBER_OF_THE_LAST_FILE, OFFICER_RANK_FOR_WHITE)
                                           : board.GetPosition(NUMBER_OF_THE_LAST_FILE, OFFICER_RANK_FOR_BLACK);

            // Haetaan kuninkaan puolen blokkaava ruutu
            var kingsideBlockingPosition = origin.Color == PieceColor.White
                                               ? board.GetPosition(origin.File + 1, OFFICER_RANK_FOR_WHITE)
                                               : board.GetPosition(origin.File + 1, OFFICER_RANK_FOR_BLACK);

            // Haetaan kuningattaren puolen torni ruutu
            var queensideRookPosition = origin.Color == PieceColor.White
                                            ? board.GetPosition(NUMBER_OF_THE_FIRST_FILE, OFFICER_RANK_FOR_WHITE)
                                            : board.GetPosition(NUMBER_OF_THE_FIRST_FILE, OFFICER_RANK_FOR_BLACK);


            var queensideBlockingPosition = origin.Color == PieceColor.White
                                               ? board.GetPosition(origin.File - 1, OFFICER_RANK_FOR_WHITE)
                                               : board.GetPosition(origin.File - 1, OFFICER_RANK_FOR_BLACK);

            // Tarkastetaan castling siirrot kuningkaan puolelle
            if (destination.Color == PieceColor.Empty
                && kingsideBlockingPosition.Color == PieceColor.Empty
                && kingsideRookPosition.Occupier is Rook
                && origin.Occupier.MoveCount == 0)
            {
                boolToReturn = true;
            }

            // Tarkistetaan castling siirrot kuningattaren puolelle
            if (destination.Color == PieceColor.Empty
                && queensideBlockingPosition.Color == PieceColor.Empty
                && queensideRookPosition.Occupier is Rook
                && origin.Occupier.MoveCount == 0)
            {
                boolToReturn = true;
            }

            // Palautetaan lopputulos
            return boolToReturn;
        }

        /// <summary>
        /// Käsittelee Castling-siirrot.
        /// </summary>
        /// <param name="origin">Siirron lähtöruutu.</param>
        /// <param name="destination">Siirron kohderuutu.</param>
        private void HandleCastling(Board board, Square origin, Square destination)
        {
            // Haetaan kuninkaan puoleinen torni ruutu
            var kingsideRookPosition = origin.Color == PieceColor.White
                                           ? board.GetPosition(NUMBER_OF_THE_LAST_FILE, OFFICER_RANK_FOR_WHITE)
                                           : board.GetPosition(NUMBER_OF_THE_LAST_FILE, OFFICER_RANK_FOR_BLACK);

            // Haetaan kuningattaren puoleinen torni ruutu
            var queensideRookPosition = origin.Color == PieceColor.White
                                            ? board.GetPosition(NUMBER_OF_THE_FIRST_FILE, OFFICER_RANK_FOR_WHITE)
                                            : board.GetPosition(NUMBER_OF_THE_FIRST_FILE, OFFICER_RANK_FOR_BLACK);

            if (origin.Occupier is King
                && kingsideRookPosition.Occupier is Rook
                && origin.Color == kingsideRookPosition.Color
                && origin.GetDistanceOfFiles(destination) == 2)
            {
                board.SetPosition(kingsideRookPosition.File, kingsideRookPosition.Rank, null);
                board.SetPosition(origin.File + 1, origin.Rank, kingsideRookPosition.Occupier);
            }

            if (origin.Occupier is King
                && queensideRookPosition.Occupier is Rook
                && origin.Color == queensideRookPosition.Color
                && origin.GetDistanceOfFiles(destination) == 2)
            {
                board.SetPosition(queensideRookPosition.File, queensideRookPosition.Rank, null);
                board.SetPosition(origin.File - 1, queensideRookPosition.Rank, queensideRookPosition.Occupier);
            }
        }

        #endregion Yksityiset metodit
    }
}
