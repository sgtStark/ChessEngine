namespace ChessEngineLib.MovingStrategies
{
    using ChessPieces;

    /// <summary>
    /// Sotilas shakkinappulan siirtostrategia. Sisältää En Passant -erikois siirron lisäkäsittelyt.
    /// </summary>
    public class PawnMovingStrategy : IMovingStrategy
    {
        #region Vakiot

        private const int EN_PASSANT_RANK_FOR_WHITE = 5;
        private const int EN_PASSANT_RANK_FOR_BLACK = 4;

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
        public PawnMovingStrategy(IMovingStrategy baseMovingStrategy)
        {
            _baseMovingStrategy = baseMovingStrategy;
        }

        #endregion Konstruktorit

        #region Implementation of IMovingStrategy

        /// <summary>
        /// Sotilas-nappilan siirto strategiassa otetaan huomioon En Passant -siirron lisätoimet.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        public void Move(Board board, Position origin, Position destination)
        {
            _baseMovingStrategy.Move(board, origin, destination);

            HandleEnPassant(board, origin, destination);
        }

        /// <summary>
        /// Sisältääkö siirto erikoistarkastuksia, jotka ohittavat normaalin tarkastuksen tuloksen.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>True, jos kyseessä on En Passant -siirto. False, muussa tapauksissa.</returns>
        public bool IsSpecialMove(Board board, Position origin, Position destination)
        {
            return IsEnPassantMove(board, origin, destination);
        }

        #endregion Implementation of IMovingStrategy

        #region Yksityiset metodit

        /// <summary>
        /// Tarkastaa onko kyseessä En Passant-siirto, joka on laillinen.
        /// </summary>
        /// <param name="board">Shakkilauta, jolla siirto tehdään.</param>
        /// <param name="origin">Lähtöpiste, jolla olevan shakkinappulan siirtoa tarkastetaan.</param>
        /// <param name="destination">Päätepiste, johon lähtöpisteen shakkinappulaa ollaan siirtämässä.</param>
        /// <returns>True, jos siirto on En Passant. False, muussa tapauksessa.</returns>
        private bool IsEnPassantMove(Board board, Position origin, Position destination)
        {
            // Oletusarvo
            var boolToReturn = false;

            // Haetaan siirrettävä shakkinappula
            var occupier = origin.Occupier;

            // Haetaan siirron suunta
            var directionOfTheMove = origin.GetDirectionTo(destination);

            // Haetaan En Passant -uhan alainen ruutu
            var positionUnderEnPassant = occupier.Color == PieceColor.White
                                                  ? board.GetPosition(destination.File, EN_PASSANT_RANK_FOR_WHITE)
                                                  : board.GetPosition(destination.File, EN_PASSANT_RANK_FOR_BLACK);

            // Haetaan En Passant -ruudun miehittäjä
            var occupierOfEnPassantPosition = positionUnderEnPassant != null
                                                         ? positionUnderEnPassant.Occupier
                                                         : null;

            // Tarkastetaan onko kyseessä En Passant -hyökkäys-siirto
            if (positionUnderEnPassant != null
                && occupierOfEnPassantPosition != null
                && origin.GetDistanceOfRanks(destination) == 1
                && directionOfTheMove.IsOnForwardDiagonal()
                && destination.Color == PieceColor.Empty
                && positionUnderEnPassant.Color.IsOppositeColor(origin.Color)
                && occupierOfEnPassantPosition.MoveCount == 1)
            {
                boolToReturn = true;
            }

            // Palautetaan tulos
            return boolToReturn;
        }

        /// <summary>
        /// Käsittelee En Passant-siirrot.
        /// </summary>
        /// <param name="origin">Siirron lähtöruutu.</param>
        /// <param name="destination">Siirron kohderuutu.</param>
        private void HandleEnPassant(Board board, Position origin, Position destination)
        {
            // Apumuuttujat, haetaan lähtöruudun miehittäjä ja En Passant -hyökkäyksen alainen
            // ruutu. Ruutu voi myös olla NULL, jos ollaan ensimmäisellä tai viimeisellä rivillä.
            var occupier = origin.Occupier;
            var enPassantPosition = origin.Color == PieceColor.White
                                        ? board.GetPosition(destination.File, EN_PASSANT_RANK_FOR_WHITE)
                                        : board.GetPosition(destination.File, EN_PASSANT_RANK_FOR_BLACK);

            // Otetaan NULL mahdollisuus huomioon ja haetaan En Passant -ruudun miehittäjä, 
            // joka voi niin ikään olla NULL.
            var enPassantOccupier = enPassantPosition != null ? enPassantPosition.Occupier : null;

            // Tehdään tarkastukset: lähtöruudun ja En Passant -ruutujen miehittäjien pitää 
            // olla sotilaita sekä En Passant -miehittäjää saa olla siirretty vain kerran.
            if (occupier is Pawn
                && enPassantOccupier is Pawn
                && occupier.Color.IsOppositeColor(enPassantOccupier.Color)
                && enPassantOccupier.MoveCount == 1)
            {
                // Poistetaan En Passant-miehittäjä.
                board.SetPosition(enPassantPosition.File, enPassantPosition.Rank, null);
            }
        }

        #endregion Yksityiset metodit
    }
}
