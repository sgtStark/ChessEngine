namespace ChessEngineLib
{
    public enum PieceColor
    {
        Empty = 0,
        White = 1,
        Black = 2
    }

    public enum GameState
    {
        SetupMode,
        Normal,
        Promotion,
        Check,
        CheckMate,
        StaleMate,
    }

    public static class PieceColorExtensions
    {
        private const int PAWN_CHAIN_RANK_FOR_WHITE = 2;
        private const int PAWN_CHAIN_RANK_FOR_BLACK = 7;

        private const int OFFICER_RANK_FOR_WHITE = 1;
        private const int OFFICER_RANK_FOR_BLACK = 8;

        public static int PawnChainRank(this PieceColor pieceColor)
        {
            return pieceColor == PieceColor.White ? PAWN_CHAIN_RANK_FOR_WHITE : PAWN_CHAIN_RANK_FOR_BLACK;
        }

        public static int OfficerRank(this PieceColor pieceColor)
        {
            return pieceColor == PieceColor.White ? OFFICER_RANK_FOR_WHITE : OFFICER_RANK_FOR_BLACK;
        }

        public static PieceColor GetOppositeColor(this PieceColor pieceColor)
        {
            return pieceColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }
    }
}