namespace ChessEngineLib
{
    using ChessPieces;

    public class Game : BoardObserver
    {
        public GameState State { get; private set; }
        public PieceColor PlayerToMove { get; private set; }

        public Game(Board board)
        {
            board.Attach(this);
            State = GameState.SetupMode;
            PlayerToMove = PieceColor.Empty;
        }

        public void Start()
        {
            State = GameState.Normal;
            PlayerToMove = PieceColor.White;
        }

        public void Start(Position startingPosition, PieceColor playerToMove)
        {
            State = GameState.Normal;
            PlayerToMove = playerToMove;
            UpdateGameState(new Square(0, 0, new NullPiece(null)),
                            new Square(0, 0, new NullPiece(null)),
                            startingPosition,
                            PlayerToMove.GetOppositeColor());
        }

        private void UpdateGameState(Square from, Square to, Position newPosition, PieceColor playerWhoDidTheMove)
        {
            if (PlayerMovedPawnToPromotionRank(from, to))
            {
                State = GameState.Promotion;
            }
            else if (newPosition.WhiteKingIsInCheck() || newPosition.BlackKingIsInCheck())
            {
                State = newPosition.HasLegalMoves(PlayerToMove) ? GameState.Check : GameState.CheckMate;
            }
            else
            {
                State = newPosition.HasLegalMoves(PlayerToMove) &&
                        newPosition.HasLegalMoves(playerWhoDidTheMove)
                            ? GameState.Normal
                            : GameState.StaleMate;
            }
        }

        private static bool PlayerMovedPawnToPromotionRank(Square from, Square to)
        {
            if (!(from.Occupier is Pawn))
                return false;

            var playerMovedPawnToPromotionRank = false;

            if (from.Color == PieceColor.White)
                playerMovedPawnToPromotionRank = from.Rank == 7 && to.Rank == 8;

            if (from.Color == PieceColor.Black)
                playerMovedPawnToPromotionRank = from.Rank == 2 && to.Rank == 1;

            return playerMovedPawnToPromotionRank;
        }

        public override void OnMove(Square from, Square to, Position newPosition)
        {
            var playerWhoDidTheMove = from.Color;
            UpdatePlayerToMove(playerWhoDidTheMove);
            UpdateGameState(from, to, newPosition, playerWhoDidTheMove);
        }

        private void UpdatePlayerToMove(PieceColor lastMovedColor)
        {
            PlayerToMove = lastMovedColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }
    }
}