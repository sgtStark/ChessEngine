namespace ChessEngineLib.MovingStrategies
{
    using ChessPieces;

    public class PawnMovingStrategy : MovingStrategy
    {
        private const int EN_PASSANT_RANK_FOR_WHITE = 5;
        private const int EN_PASSANT_RANK_FOR_BLACK = 4;

        public PawnMovingStrategy(Board board)
            : base(board)
        {
        }

        public override void Move(Square origin, Square destination)
        {
            base.Move(origin, destination);

            HandleEnPassant(origin, destination);
        }

        private void HandleEnPassant(Square origin, Square destination)
        {
            var enPassantPosition = GetEnPassantSquare(origin, destination);
            if (origin.Color == enPassantPosition.Color) return;

            var enPassantOccupier = enPassantPosition.Occupier;
            if (enPassantOccupier.MoveCount != 1) return;

            Board.SetPosition(enPassantPosition.File, enPassantPosition.Rank, new NullPiece());
        }

        private Square GetEnPassantSquare(Square origin, Square destination)
        {
            return origin.Color == PieceColor.White
                       ? Board.GetPosition(destination.File, EN_PASSANT_RANK_FOR_WHITE)
                       : Board.GetPosition(destination.File, EN_PASSANT_RANK_FOR_BLACK);
        }

        public override bool IsSpecialMove(Square origin, Square destination)
        {
            if (origin.Color == GetEnPassantSquare(origin, destination).Color) return false;

            var occupierOfEnPassantPosition = GetEnPassantSquare(origin, destination).Occupier;

            return (origin.DiagonallyForwardTo(destination)
                    && origin.DistanceOfRanksIsOneTo(destination)
                    && destination.Color == PieceColor.Empty
                    && occupierOfEnPassantPosition.MoveCount == 1);
        }
    }
}
