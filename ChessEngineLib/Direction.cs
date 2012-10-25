namespace ChessEngineLib
{
    public enum Direction
    {
        Forward,
        Backward,
        Right,
        Left,
        ForwardOnRightDiagonal,
        BackwardOnRightDiagonal,
        ForwardOnLeftDiagonal,
        BackwardOnLeftDiagonal,
        Irregular
    }

    /// <summary>
    /// Extension metodit Direction enumeraatiolle.
    /// Note: Mielenkiintoista kuinka tällä tavalla päästään eroon ongelmasta.
    /// (How the problem was finessed away with this...)
    /// </summary>
    public static class DirectionExtensions
    {
        public static bool Forward(this Direction movingDirection)
        {
            return (movingDirection == Direction.Forward);
        }

        public static bool AlongFileOrRank(this Direction movingDirection)
        {
            return (movingDirection == Direction.Forward
                    || movingDirection == Direction.Backward
                    || movingDirection == Direction.Right
                    || movingDirection == Direction.Left);
        }

        public static bool Diagonally(this Direction movingDirection)
        {
            return (movingDirection == Direction.ForwardOnRightDiagonal
                    || movingDirection == Direction.ForwardOnLeftDiagonal
                    || movingDirection == Direction.BackwardOnRightDiagonal
                    || movingDirection == Direction.BackwardOnLeftDiagonal);
        }

        public static bool DiagonallyForward(this Direction movingDirection)
        {
            return (movingDirection == Direction.ForwardOnRightDiagonal
                    || movingDirection == Direction.ForwardOnLeftDiagonal);
        }
    }
}
