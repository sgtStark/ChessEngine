using System;

namespace ChessEngineLib
{
    public class Pawn
    {
        #region Sis‰iset dataj‰senet

        private readonly Position _startingPosition;
        private readonly Position _currentPosition;

        #endregion Sis‰iset dataj‰senet

        #region Konstruktorit

        public Pawn(Position startingPosition)
        {
            _startingPosition = startingPosition;
            _currentPosition = _startingPosition;
        }

        #endregion Konstruktorit

        #region Julkiset metodit

        public bool IsLegalMove(Position nextPosition)
        {
            var boolToReturn = false;

            // Jos siirret‰‰n yksi ruutu eteenp‰in
            if (_startingPosition.GetDirectionTo(nextPosition) == Direction.Forward
                && GetDistanceOfRanks(_startingPosition, nextPosition) == 1)
            {
                boolToReturn = true;
            }
            // Jos siirret‰‰n kaksi ruutua eteenp‰in aloitus rivilt‰
            else if (_startingPosition.GetDirectionTo(nextPosition) == Direction.Forward
                     && GetDistanceOfRanks(_startingPosition, nextPosition) == 2
                     && _startingPosition.Rank == 2)
            {
                boolToReturn = true;
            }

            // Jos kohde ruutu ei ole tyhj‰ on siirto automaattisesti laiton
            if (nextPosition.Status != PositionStatus.Empty)
            {
                boolToReturn = false;
            }

            return boolToReturn;
        }

        #endregion Julkiset metodit

        #region Yksityiset metodit

        private int GetDistanceOfRanks(Position origin, Position destination)
        {
            return Math.Abs(origin.Rank - destination.Rank);
        }

        #endregion Yksityiset metodit
    }
}