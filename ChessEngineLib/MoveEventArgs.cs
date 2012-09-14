using System;

namespace ChessEngineLib
{
    public class MoveEventArgs : EventArgs
    {
        public Square From { get; private set; }

        private Square To { get; set; }

        public MoveEventArgs(Square from, Square to)
        {
            From = from;
            To = to;
        }
    }
}