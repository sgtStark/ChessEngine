namespace ChessEngineLib
{
    public enum PieceColor
    {
        Empty = 0,
        White = 1,
        Black = 2
    }

    public static class PieceColorExtensions
    {
        public static bool IsOppositeColor(this PieceColor pieceColor, PieceColor otherPieceColor)
        {
            return (pieceColor != otherPieceColor && otherPieceColor != PieceColor.Empty);
        }
    }
}