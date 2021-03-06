namespace ChessEngineLib.ChessPieces
{
    using MovingStrategies;

    public class Pawn : ChessPiece
    {
        private const int PAWN_CHAIN_RANK_FOR_WHITE = 2;
        private const int PAWN_CHAIN_RANK_FOR_BLACK = 7;

        private readonly MovingStrategy _movingStrategy;

        public Pawn(Board board, PieceColor color)
            : base(board, color)
        {
            _movingStrategy = new PawnMovingStrategy(Board);
        }

        public override bool IsLegalMove(Square origin, Square destination)
        {
            if (origin.Color == destination.Color) return false;

            if (MovingSingleRankForward(origin, destination)) return true;
            if (Attacking(origin, destination)) return true;
            if (MovingTwoRanksForwardFromStartingRank(origin, destination)) return true;

            return false;
        }

        public override bool Attacks(Square origin, Square destination)
        {
            return Attacking(origin, destination);
        }

        private bool MovingSingleRankForward(Square origin, Square destination)
        {
            return (origin.ForwardTo(destination)
                    && origin.DistanceOfRanksIsOneTo(destination)
                    && destination.Color == PieceColor.Empty);
        }

        private bool Attacking(Square origin, Square destination)
        {
            return (origin.Color != destination.Color
                    && destination.Color != PieceColor.Empty
                    && origin.DistanceOfRanksIsOneTo(destination)
                    && origin.DiagonallyForwardTo(destination));
        }

        private bool MovingTwoRanksForwardFromStartingRank(Square origin, Square destination)
        {
            return (FromStartingRank(origin)
                    && origin.ForwardTo(destination)
                    && origin.DistanceOfRanksIsTwoTo(destination)
                    && PathIsFree(origin, destination));
        }

        private static bool FromStartingRank(Square origin)
        {
            return origin.Color == PieceColor.White
                       ? origin.Rank == PAWN_CHAIN_RANK_FOR_WHITE
                       : origin.Rank == PAWN_CHAIN_RANK_FOR_BLACK;
        }

        public override MovingStrategy GetMovingStrategy()
        {
            return _movingStrategy;
        }

        public override ChessPiece Clone(Board board)
        {
            var clone = new Pawn(board, Color)
                {
                    MovingStrategy = MovingStrategy.Clone(board)
                };

            return clone;
        }

        private bool Equals(Pawn other)
        {
            return !ReferenceEquals(null, other) && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Pawn)) return false;

            return Equals((Pawn)obj);
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