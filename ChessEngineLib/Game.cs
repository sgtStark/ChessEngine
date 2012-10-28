using System.Collections.Generic;
using System.Linq;
using ChessEngineLib.GameStateDetection;

namespace ChessEngineLib
{
    public class Game
    {
        private readonly List<Square> _allSquares;
        private readonly List<Square> _squaresOccupiedByPlayer;
        private readonly CheckStateDetector _checkStateDetector;


        public Board Board { get; private set; }
        public GameState State { get; private set; }
        public PieceColor PlayerToMove { get; private set; }


        public Game(Board board)
        {
            Board = board;
            board.OnMove += OnChessPieceMoved;
            State = GameState.SetupMode;
            PlayerToMove = PieceColor.Empty;

            _allSquares = new List<Square>();
            _squaresOccupiedByPlayer = new List<Square>();
            _checkStateDetector = new CheckStateDetector();
        }

        public void Start()
        {
            State = GameState.Normal;
            PlayerToMove = PieceColor.White;
        }

        public void Start(PieceColor playerToMove)
        {
            State = GameState.Normal;
            PlayerToMove = playerToMove;
            Update(playerToMove == PieceColor.White ? PieceColor.Black : PieceColor.White);
        }

        private void OnChessPieceMoved(object sender, MoveEventArgs e)
        {
            Update(e.From.Color);
        }

        private void Update(PieceColor lastMovedColor)
        {
            PlayerToMove = lastMovedColor == PieceColor.White ? PieceColor.Black : PieceColor.White;

            if (_checkStateDetector.KingIsChecked(Board, PieceColor.White)
                || _checkStateDetector.KingIsChecked(Board, PieceColor.Black))
            {
                if (HasLegalMoves())
                    State = GameState.Check;
                else
                    State = GameState.CheckMate;
            }
            else
            {
                State = GameState.Normal;
            }
        }

        private bool HasLegalMoves()
        {
            // Apumuuttuja
            var boolToReturn = false;

            // Haetaan kaikki laudan ruudut
            Board.Iterate(square => _allSquares.Add(square));

            // Haetaan siirtävän pelaajan ruudit
            Board.Iterate(PlayerSquareHandler);

            // Käydään kaikki siirtävän pelaajan ruudut läpi tarkastaen voidaanko
            // ruudun nappulaa siirtää mihinkään
            foreach (var origin in _squaresOccupiedByPlayer)
            {
                var destinationSquares = _allSquares.Where(square => !square.Equals(origin));

                foreach (var destinationSquare in destinationSquares)
                {
                    if (Board.IsLegalMove(origin, destinationSquare))
                    {
                        var simulated = Board.SimulatedMove(origin, destinationSquare);

                        if (!_checkStateDetector.KingIsChecked(simulated, PlayerToMove))
                        {
                            boolToReturn = true;
                            break;
                        }
                    }
                }
            }

            return boolToReturn;
        }

        private void PlayerSquareHandler(Square square)
        {
            if (square.Color == PlayerToMove)
            {
                _squaresOccupiedByPlayer.Add(square);
            }
        }
    }
}