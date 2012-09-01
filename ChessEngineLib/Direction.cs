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
        public static bool IsAlongFileOrRank(this Direction directionOfTheMove)
        {
            return (directionOfTheMove == Direction.Forward
                    || directionOfTheMove == Direction.Backward
                    || directionOfTheMove == Direction.Right
                    || directionOfTheMove == Direction.Left);
        }

        public static bool IsAlongDiagonal(this Direction directionOfTheMove)
        {
            return (directionOfTheMove == Direction.ForwardOnRightDiagonal
                    || directionOfTheMove == Direction.ForwardOnLeftDiagonal
                    || directionOfTheMove == Direction.BackwardOnRightDiagonal
                    || directionOfTheMove == Direction.BackwardOnLeftDiagonal);
        }

        public static bool IsOnForwardDiagonal(this Direction directionOfTheMove)
        {
            return (directionOfTheMove == Direction.ForwardOnRightDiagonal
                    || directionOfTheMove == Direction.ForwardOnLeftDiagonal);
        }
    }
}
