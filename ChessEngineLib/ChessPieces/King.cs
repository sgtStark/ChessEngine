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
            if (MovingOneSquareLeftOrRight(origin, destination)) return true;
            if (MovingOneSquareForwardOrBackward(origin, destination)) return true;
            if (MovingOneSquareDiagonally(origin, destination)) return true;

            return false;
        }

        private bool MovingOneSquareLeftOrRight(Square origin, Square destination)
        {
            return (origin.GetDistanceOfRanks(destination) == 0
                   && origin.GetDistanceOfFiles(destination) == 1);
        }

        private bool MovingOneSquareForwardOrBackward(Square origin, Square destination)
        {
            return (origin.GetDistanceOfRanks(destination) == 1
                    && origin.GetDistanceOfFiles(destination) == 0);
        }

        private bool MovingOneSquareDiagonally(Square origin, Square destination)
        {
            return (origin.GetDistanceOfRanks(destination) == 1
                    && origin.GetDistanceOfFiles(destination) == 1);
        }

        public override MovingStrategy GetMovingStrategy()
        {
            return _movingStrategy;
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
    }
}