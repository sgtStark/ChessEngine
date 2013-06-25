namespace ChessEngineLib
{
    public abstract class BoardObserver
    {
        public abstract void OnMove(Square from, Square to, Position into);
    }
}
