using System;

namespace ChessEngineLib.Exceptions
{
    public class IllegalMoveException : Exception
    {
        public IllegalMoveException()
        {
            
        }

        public IllegalMoveException(string message)
            : base(message)
        {
        }
    }
}