using System;

namespace ChessEngineLib
{
    public class MoveEventArgs : EventArgs
    {
        public Square From { get; private set; }

        public Square To { get; private set; }

        public MoveEventArgs(Square from, Square to)
        {
            From = from;
            To = to;
        }
    }
}