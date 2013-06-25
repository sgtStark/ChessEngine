using System.Linq;

namespace ChessEngineLib.ChessPieces
{
    using MovingStrategies;

    public class King : ChessPiece
    {
        private readonly MovingStrategy _movingStrategy;

        public King(Board board, PieceColor color)
            : base(board, color)
        {
            _movingStrategy = new KingMovingStrategy(Board);
        }

        public override bool IsLegalMove(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;
            if (DestinationIsAttacked(destination)) return false;
            if (MovingOneSquareLeftOrRight(origin, destination)) return true;
            if (MovingOneSquareForwardOrBackward(origin, destination)) return true;
            if (MovingOneSquareDiagonally(origin, destination)) return true;

            return false;
        }

        private bool DestinationIsAttacked(Square destination)
        {
            var currentPosition = Board.GetPosition();

            return currentPosition.SquaresOccupiedByPiecesWith(Color.GetOppositeColor())
                                  .Any(opponentSquare => opponentSquare.Occupier.Attacks(opponentSquare, destination));
        }

        private bool MovingOneSquareLeftOrRight(Square origin, Square destination)
        {
            return (origin.DistanceOfRanksIsZeroTo(destination)
                   && origin.DistanceOfFilesIsOneTo(destination));
        }

        private bool MovingOneSquareForwardOrBackward(Square origin, Square destination)
        {
            return (origin.DistanceOfRanksIsOneTo(destination)
                    && origin.DistanceOfFilesIsZeroTo(destination));
        }

        private bool MovingOneSquareDiagonally(Square origin, Square destination)
        {
            return (origin.DistanceOfRanksIsOneTo(destination)
                    && origin.DistanceOfFilesIsOneTo(destination));
        }

        public override bool Attacks(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;

            if (MovingOneSquareLeftOrRight(origin, destination)) return true;
            if (MovingOneSquareForwardOrBackward(origin, destination)) return true;
            if (MovingOneSquareDiagonally(origin, destination)) return true;

            return false;
        }

        public override MovingStrategy GetMovingStrategy()
        {
            return _movingStrategy;
        }

        public override ChessPiece Clone(Board board)
        {
            var clone = new King(board, Color)
                {
                    MovingStrategy = MovingStrategy.Clone(board)
                };

            return clone;
        }

        private bool Equals(King other)
        {
            return !ReferenceEquals(null, other)
                && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (King)) return false;
            return Equals((King) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return Color + GetType().Name;
        }
    }
}