using ChessEngineLib.ChessPieces;

namespace ChessEngineLib.MovingStrategies
{
    internal class NormalMovingStrategy : MovingStrategy
    {
        public NormalMovingStrategy(Board board)
            : base(board)
        {
        }

        public override bool IsSpecialMove(Square origin, Square destination)
        {
            return false;
        }
    }
}
