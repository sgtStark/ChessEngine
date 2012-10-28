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

        // TODO: Refactoroi
        private IList<Square> GetPositionsBetween(Square origin, Square destination)
        {
            var positionsBetweenToReturn = new List<Square>();

            if (origin.AlongFile(destination))
            {
                for (int file = origin.File; file < destination.File; file++)
                    positionsBetweenToReturn.Add(Board.GetPosition(file, origin.Rank));

                for (int file = origin.File; file > destination.File; file--)
                    positionsBetweenToReturn.Add(Board.GetPosition(file, origin.Rank));
            }
            else if (origin.AlongRank(destination))
            {
                for (int rank = origin.Rank; rank < destination.Rank; rank++)
                    positionsBetweenToReturn.Add(Board.GetPosition(origin.File, rank));
                for (int rank = origin.Rank; rank > destination.Rank; rank--)
                    positionsBetweenToReturn.Add(Board.GetPosition(origin.File, rank));
            }
            else if (origin.DiagonallyTo(destination))
            {
                for (int file = origin.File; file < destination.File; file++)
                {
                    for (int rank = origin.Rank; rank <= destination.Rank; rank++)
                    {
                        if (SquareBackgroundDiffersFromOrigin(origin, rank, file))
                            continue;

                        positionsBetweenToReturn.Add(Board.GetPosition(file, rank));
                    }
                }

                for (int file = origin.File; file > destination.File; file--)
                {
                    for (int rank = origin.Rank; rank <= destination.Rank; rank++)
                    {
                        if (SquareBackgroundDiffersFromOrigin(origin, rank, file))
                            continue;

                        positionsBetweenToReturn.Add(Board.GetPosition(file, rank));
                    }
                }
            }

            positionsBetweenToReturn.Remove(origin);

            return positionsBetweenToReturn;
        }

        //TODO: täytyy refactoroida!!
        private bool SquareBackgroundDiffersFromOrigin(Square origin, int rank, int file)
        {
            return Board.IsLightSquare(file, rank) != Board.IsLightSquare(origin.File, origin.Rank);
        }

        public abstract ChessPiece Clone(Board board);
    }
}