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

        public override MovingStrategy Clone(Board board)
        {
            return new NormalMovingStrategy(board)
                {
                    MoveCount = MoveCount
                };
        }
    }
}
