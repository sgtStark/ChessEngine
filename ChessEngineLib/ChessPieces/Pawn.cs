namespace ChessEngineLib.ChessPieces
{
    using MovingStrategies;

    /// <summary>
    /// Luokka, joka sisältää toiminnallisuuden sotilastyyppiselle shakkinappulalle.
    /// </summary>
    public class Pawn : ChessPiece
    {
        #region Vakiot

        private const int PAWN_CHAIN_RANK_FOR_WHITE = 2;
        private const int PAWN_CHAIN_RANK_FOR_BLACK = 7;

        #endregion Vakiot

        #region Konstruktorit

        public Pawn(PieceColor color)
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

            // Jos siirretään yksi ruutu eteenpäin
            if (origin.GetDistanceOfRanks(destination) == 1
                && directionOfTheMove == Direction.Forward
                && destination.Color == PieceColor.Empty)
            {
                boolToReturn = true;
            }
            // Jos hyökätään oikealla tai vasemmalla edessä olevalle ruudulle
            else if (IsAttackMove(origin, destination, directionOfTheMove))
            {
                boolToReturn = true;
            }
            // Jos siirretään kaksi ruutua eteenpäin aloitus riviltä
            else if (origin.GetDistanceOfRanks(destination) == 2
                     && directionOfTheMove == Direction.Forward
                     && IsStartingRank(origin)
                     && !IsPathObscured(board, origin, destination))
            {
                boolToReturn = true;
            }

            return boolToReturn;
        }

        /// <summary>
        /// Hakee shakkinappulaan liittyvän siirto strategian.
        /// Siirto strategia käsittelee erikoistapaukset kuten En Passant -siirto sotilaalla,
        /// Castling-siirrot kuningkaalla.
        /// </summary>
        public override IMovingStrategy GetMovingStrategy()
        {
            return new PawnMovingStrategy(base.GetMovingStrategy());
        }

        #endregion Overrides of ChessPiece

        /// <summary>
        /// Tarkastaa onko tämä sotilasnappula sama kuin
        /// toinen sotilasnappula.
        /// </summary>
        /// <param name="other">Toinen sotilasnappula, johon tätä nappulaa ollaan vertaamassa.</param>
        /// <returns>True, jos kummatkin nappulat ovat yksi ja sama. False, jos nappulat eroavat toisistaan.</returns>
        public bool Equals(Pawn other)
        {
            return !ReferenceEquals(null, other)
                && Color == other.Color;
        }

        /// <summary>
        /// Tarkastaa onko tämä sotilasnappula sama kuin
        /// toinen olio.
        /// </summary>
        /// <param name="obj">Toinen olio, johon tätä nappulaa ollaan vertaamassa.</param>
        /// <returns>True, jos kummatkin ovat yksi ja sama olio. False, jos oliot eroavat toisistaan.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Pawn)) return false;
            return Equals((Pawn)obj);
        }

        /// <summary>
        /// Palauttaa tämän olion tiivisteen.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 0;
        }

        #endregion Julkiset metodit

        #region Yksityiset metodit

        /// <summary>
        /// Tarkastaa onko kyseessä sotilasnappulan aloitusruutu.
        /// </summary>
        /// <param name="origin">Ruutu, jota vastaan sotilasnappulaa verrataan.</param>
        /// <returns>True, jos parametrina saatu ruutu on aloitusruutu. Muussa tapauksessa False.</returns>
        private static bool IsStartingRank(Position origin)
        {
            return origin.Color == PieceColor.White
                       ? origin.Rank == PAWN_CHAIN_RANK_FOR_WHITE
                       : origin.Rank == PAWN_CHAIN_RANK_FOR_BLACK;
        }

        /// <summary>
        /// Tarkastaa onko kyseessä normaali hyökkäyssiirto.
        /// </summary>
        /// <param name="origin">Lähtöpiste, jolla olevan shakkinappulan siirto tarkastetaan.</param>
        /// <param name="destination">Päätepiste, johon lähtöpisteen shakkinappulaa ollaan siirtämässä.</param>
        /// <param name="directionOfTheMove">Siirron suunta</param>
        /// <returns></returns>
        private bool IsAttackMove(Position origin, Position destination, Direction directionOfTheMove)
        {
            bool boolToReturn = (origin.GetDistanceOfRanks(destination) == 1
                                 && directionOfTheMove.IsOnForwardDiagonal()
                                 && origin.Color.IsOppositeColor(destination.Color));

            return boolToReturn;
        }

        #endregion Yksityiset metodit
    }
}