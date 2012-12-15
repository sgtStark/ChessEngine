using System.Linq;
using System.Collections.Generic;

namespace ChessEngineLib.ChessPieces
{
    using MovingStrategies;

    public abstract class ChessPiece
    {
        protected readonly Board Board;
        protected MovingStrategy MovingStrategy;

        public PieceColor Color { get; private set; }
        public int MoveCount { get { return GetMovingStrategy().MoveCount; } }

        protected ChessPiece(Board board, PieceColor color)
        {
            Board = board;
            Color = color;
            MovingStrategy = new NormalMovingStrategy(Board);
        }

        public abstract bool IsLegalMove(Square origin, Square destination);

        public abstract bool Attacks(Square origin, Square destination);

        public virtual MovingStrategy GetMovingStrategy()
        {
            return MovingStrategy;
        }

        // TODO: Refactoroi
        protected bool PathIsFree(Square origin, Square destination)
        {
            IList<Square> path = new List<Square>();

            path = GetPositionsBetween(origin, destination);

            return path.All(position => position.Color == PieceColor.Empty);
        }

        // TODO: Refactoroi
        private IList<Square> GetPositionsBetween(Square origin, Square destination)
        {
            var positionsBetweenToReturn = new List<Square>();
   
            if (origin.AlongFile(destination))
            {
                for (int file = origin.File; file < destination.File; file++)
                    positionsBetweenToReturn.Add(Board.GetSquare(file, origin.Rank));

                for (int file = origin.File; file > destination.File; file--)
                    positionsBetweenToReturn.Add(Board.GetSquare(file, origin.Rank));
            }
            else if (origin.AlongRank(destination))
            {
                for (int rank = origin.Rank; rank < destination.Rank; rank++)
                    positionsBetweenToReturn.Add(Board.GetSquare(origin.File, rank));

                for (int rank = origin.Rank; rank > destination.Rank; rank--)
                    positionsBetweenToReturn.Add(Board.GetSquare(origin.File, rank));
            }
            else if (origin.DiagonallyTo(destination))
            {
                var startingFile = origin.File;
                var startingRank = origin.Rank;

                if (origin.File < destination.File
                    && origin.Rank > destination.Rank)
                {
                    while (startingFile <= destination.File && startingRank <= destination.File)
                    {
                        positionsBetweenToReturn.Add(Board.GetSquare(startingFile, startingRank));
                        startingFile += 1;
                        startingRank -= 1;
                    }
                }
                else if (origin.File > destination.File
                    && origin.Rank < destination.Rank)
                {
                    while (startingFile >= destination.File && startingRank <= destination.Rank)
                    {
                        positionsBetweenToReturn.Add(Board.GetSquare(startingFile, startingRank));
                        startingFile -= 1;
                        startingRank += 1;
                    }
                }
                else if (origin.File < destination.File
                    && origin.Rank < destination.Rank)
                {
                    while (startingFile <= destination.File && startingRank <= destination.Rank)
                    {
                        positionsBetweenToReturn.Add(Board.GetSquare(startingFile, startingRank));
                        startingFile += 1;
                        startingRank += 1;
                    }
                }
                else if (origin.File > destination.File
                    && origin.Rank > destination.Rank)
                {
                    while (startingFile >= destination.File && startingRank >= destination.Rank)
                    {
                        positionsBetweenToReturn.Add(Board.GetSquare(startingFile, startingRank));
                        startingFile -= 1;
                        startingRank -= 1;
                    }
                }
            }

            positionsBetweenToReturn.Remove(origin);
            positionsBetweenToReturn.Remove(destination);

            return positionsBetweenToReturn;
        }

        public abstract ChessPiece Clone(Board board);
    }
}