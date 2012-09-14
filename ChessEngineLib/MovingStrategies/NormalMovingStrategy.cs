namespace ChessEngineLib.MovingStrategies
{
    /// <summary>
    /// Normaali siirtostrategia, ei sisällä mitään erikois käsittelyjä.
    /// </summary>
    internal class NormalMovingStrategy : IMovingStrategy
    {
        #region Implementation of IMovingStrategy

        /// <summary>
        /// Normaalitapauksessa tehdään pelkästään siirto lähderuudusta kohderuutuun.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        public void Move(Board board, Square origin, Square destination)
        {
                var occupier = origin.Occupier;
                occupier.MoveCount++;
                board.SetPosition(origin.File, origin.Rank, null);
                board.SetPosition(destination.File, destination.Rank, occupier);
        }

        /// <summary>
        /// Sisältääkö siirto erikoistarkastuksia, jotka ohittavat normaalin tarkastuksen tuloksen.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>False</returns>
        public bool IsSpecialMove(Board board, Square origin, Square destination)
        {
            return false;
        }

        #endregion Implementation of IMovingStrategy
    }
}
