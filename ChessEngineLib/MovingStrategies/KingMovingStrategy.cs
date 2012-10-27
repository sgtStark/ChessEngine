namespace ChessEngineLib.MovingStrategies
{
    using ChessPieces;

    public class KingMovingStrategy : MovingStrategy
    {
        private const int NUMBER_OF_THE_FIRST_FILE = 1;
        private const int NUMBER_OF_THE_LAST_FILE = 8;
        private const int OFFICER_RANK_FOR_WHITE = 1;
        private const int OFFICER_RANK_FOR_BLACK = 8;

        public KingMovingStrategy(Board board)
            : base(board)
        {
        }

        public override void Move(Square origin, Square destination)
        {
            base.Move(origin, destination);

            HandleKingsideCastling(origin, destination);
            HandleQueensideCastling(origin, destination);
        }

        private void HandleKingsideCastling(Square origin, Square destination)
        {
            var kingsideRookSquare = GetKingsideRookSquare(origin);

            if (origin.Color != kingsideRookSquare.Color) return;
            if (origin.DistanceOfFilesIsNotTwoTo(destination)) return;
            if (!(kingsideRookSquare.Occupier is Rook)) return;

            Board.SetPosition(kingsideRookSquare.File, kingsideRookSquare.Rank, new NullPiece());
            Board.SetPosition(origin.File + 1, origin.Rank, kingsideRookSquare.Occupier);
        }

        private Square GetKingsideRookSquare(Square origin)
        {
            return origin.Color == PieceColor.White
                       ? Board.GetPosition(NUMBER_OF_THE_LAST_FILE, OFFICER_RANK_FOR_WHITE)
                       : Board.GetPosition(NUMBER_OF_THE_LAST_FILE, OFFICER_RANK_FOR_BLACK);
        }

        private void HandleQueensideCastling(Square origin, Square destination)
        {
            var queensideRookSquare = GetQueensideRookSquare(origin);

            if (origin.Color != queensideRookSquare.Color) return;
            if (origin.DistanceOfFilesIsNotTwoTo(destination)) return;
            if (!(queensideRookSquare.Occupier is Rook)) return;

            Board.SetPosition(queensideRookSquare.File, queensideRookSquare.Rank, new NullPiece());
            Board.SetPosition(origin.File - 1, queensideRookSquare.Rank, queensideRookSquare.Occupier);
        }

        private Square GetQueensideRookSquare(Square origin)
        {
            return origin.Color == PieceColor.White
                       ? Board.GetPosition(NUMBER_OF_THE_FIRST_FILE, OFFICER_RANK_FOR_WHITE)
                       : Board.GetPosition(NUMBER_OF_THE_FIRST_FILE, OFFICER_RANK_FOR_BLACK);
        }

        public override bool IsSpecialMove(Square origin, Square destination)
        {
            return IsCastlingMove(origin, destination);
        }

        private bool IsCastlingMove(Square origin, Square destination)
        {
            if (MoveCount > 0) return false;
            if (destination.Color != PieceColor.Empty) return false;
            if (CheckKingside(origin)) return true;
            if (CheckQueenside(origin)) return true;

            return false;
        }

        private bool CheckKingside(Square origin)
        {
            var kingsideRookSquare = GetKingsideRookSquare(origin);
            var kingsideBlockingSquare = GetKingsideBlockingSquare(origin);

            return kingsideBlockingSquare.Color == PieceColor.Empty
                   && kingsideRookSquare.Occupier is Rook;
        }

        private Square GetKingsideBlockingSquare(Square origin)
        {
            return origin.Color == PieceColor.White
                       ? Board.GetPosition(origin.File + 1, OFFICER_RANK_FOR_WHITE)
                       : Board.GetPosition(origin.File + 1, OFFICER_RANK_FOR_BLACK);
        }

        private bool CheckQueenside(Square origin)
        {
            var queensideRookSquare = GetQueensideRookSquare(origin);
            var queensideBlockingSquare = GetQueensideBlockingSquare(origin);

            return queensideBlockingSquare.Color == PieceColor.Empty
                   && queensideRookSquare.Occupier is Rook;
        }

        private Square GetQueensideBlockingSquare(Square origin)
        {
            return origin.Color == PieceColor.White
                       ? Board.GetPosition(origin.File - 1, OFFICER_RANK_FOR_WHITE)
                       : Board.GetPosition(origin.File - 1, OFFICER_RANK_FOR_BLACK);
        }
    }
}
