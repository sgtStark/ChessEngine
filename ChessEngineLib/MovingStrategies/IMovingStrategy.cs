namespace ChessEngineLib.MovingStrategies
{
    /// <summary>
    /// Shakkinappulan siirtostrategian rajapinta.
    /// </summary>
    public interface IMovingStrategy
    {
        /// <summary>
        /// Suoritetaan siirto strategian erikoistapaukset huomioon ottaen.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        void Move(Board board, Square origin, Square destination);

        /// <summary>
        /// Sisältääkö siirto erikoistarkastuksia, jotka ohittavat normaalin tarkastuksen tuloksen.
        /// </summary>
        /// <param name="board">Shakkilauta</param>
        /// <param name="origin">Lähtöruutu</param>
        /// <param name="destination">Kohderuutu</param>
        /// <returns>True, jos siirto sisältää normaalin tarkastuksen tuloksen ohittavan tuloksen. False, muutoin.</returns>
        bool IsSpecialMove(Board board, Square origin, Square destination);
    }
}
