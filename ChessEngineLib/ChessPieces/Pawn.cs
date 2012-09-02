namespace ChessEngineLib.ChessPieces
{
    using MovingStrategies;

    /// <summary>
    /// Luokka, joka sis�lt�� toiminnallisuuden sotilastyyppiselle shakkinappulalle.
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
        /// <param name="board">Shakkilauta, jolla siirto tehd��n.</param>
        /// <param name="origin">L�ht�piste, jolla olevan shakkinappulan siirtoa tarkastetaan.</param>
        /// <param name="destination">P��tepiste, johon l�ht�pisteen shakkinappulaa ollaan siirt�m�ss�.</param>
        /// <returns>True, jos siirto on laillinen. False, jos siirto on laiton.</returns>
        public override bool IsLegalMove(Board board, Position origin, Position destination)
        {
            var boolToReturn = false;
            var directionOfTheMove = origin.GetDirectionTo(destination);

            // Jos siirret��n yksi ruutu eteenp�in
            if (origin.GetDistanceOfRanks(destination) == 1
                && directionOfTheMove == Direction.Forward
                && destination.Color == PieceColor.Empty)
            {
                boolToReturn = true;
            }
            // Jos hy�k�t��n oikealla tai vasemmalla edess� olevalle ruudulle
            else if (IsAttackMove(origin, destination, directionOfTheMove))
            {
                boolToReturn = true;
            }
            // Jos siirret��n kaksi ruutua eteenp�in aloitus rivilt�
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
        /// Hakee shakkinappulaan liittyv�n siirto strategian.
        /// Siirto strategia k�sittelee erikoistapaukset kuten En Passant -siirto sotilaalla,
        /// Castling-siirrot kuningkaalla.
        /// </summary>
        public override IMovingStrategy GetMovingStrategy()
        {
            return new PawnMovingStrategy(base.GetMovingStrategy());
        }

        #endregion Overrides of ChessPiece

        /// <summary>
        /// Tarkastaa onko t�m� sotilasnappula sama kuin
        /// toinen sotilasnappula.
        /// </summary>
        /// <param name="other">Toinen sotilasnappula, johon t�t� nappulaa ollaan vertaamassa.</param>
        /// <returns>True, jos kummatkin nappulat ovat yksi ja sama. False, jos nappulat eroavat toisistaan.</returns>
        public bool Equals(Pawn other)
        {
            return !ReferenceEquals(null, other)
                && Color == other.Color;
        }

        /// <summary>
        /// Tarkastaa onko t�m� sotilasnappula sama kuin
        /// toinen olio.
        /// </summary>
        /// <param name="obj">Toinen olio, johon t�t� nappulaa ollaan vertaamassa.</param>
        /// <returns>True, jos kummatkin ovat yksi ja sama olio. False, jos oliot eroavat toisistaan.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Pawn)) return false;
            return Equals((Pawn)obj);
        }

        /// <summary>
        /// Palauttaa t�m�n olion tiivisteen.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 0;
        }

        #endregion Julkiset metodit

        #region Yksityiset metodit

        /// <summary>
        /// Tarkastaa onko kyseess� sotilasnappulan aloitusruutu.
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
        /// Tarkastaa onko kyseess� normaali hy�kk�yssiirto.
        /// </summary>
        /// <param name="origin">L�ht�piste, jolla olevan shakkinappulan siirto tarkastetaan.</param>
        /// <param name="destination">P��tepiste, johon l�ht�pisteen shakkinappulaa ollaan siirt�m�ss�.</param>
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