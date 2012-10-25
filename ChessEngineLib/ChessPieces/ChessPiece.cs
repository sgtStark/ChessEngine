using System.Linq;
using System.Collections.Generic;

namespace ChessEngineLib.ChessPieces
{
    using MovingStrategies;

    public abstract class ChessPiece
    {
        protected readonly Board Board;

        public PieceColor Color { get; private set; }
        public int MoveCount { get { return GetMovingStrategy().MoveCount; } }

        protected ChessPiece(Board board, PieceColor color)
        {
            Board = board;
            Color = color;
        }

        public abstract bool IsLegalMove(Square origin, Square destination);

        public virtual MovingStrategy GetMovingStrategy()
        {
            return new NormalMovingStrategy(Board);
        }

        protected bool PathIsFree(Square origin, Square destination)
        {
            IList<Square> path = new List<Square>();

            if (origin.Color == PieceColor.White)
            {
                path = GetPositionsBetween(origin, destination);
            }
            else if (origin.Color == PieceColor.Black)
            {
                path = GetPositionsBetween(destination, origin);
            }

            return path.All(position => position.Color == PieceColor.Empty);
        }

        private IList<Square> GetPositionsBetween(Square origin, Square destination)
        {
            var positionsBetweenToReturn = new List<Square>();
            
            // Jos siirretään oikealle
            for (int file = origin.File; file <= destination.File; file++)
            {
                for (int rank = origin.Rank; rank <= destination.Rank; rank++)
                {
                    positionsBetweenToReturn.Add(Board.GetPosition(file, rank));
                }
            }

            // Jos siirretään vasemmalle
            for (int file = origin.File; file > destination.File; file--)
            {
                for (int rank = origin.Rank; rank <= destination.Rank; rank++)
                {
                    positionsBetweenToReturn.Add(Board.GetPosition(file, rank));
                }
            }

            positionsBetweenToReturn.Remove(origin);
            positionsBetweenToReturn.Remove(destination);

            return positionsBetweenToReturn;
        }
    }
}