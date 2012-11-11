using System.Collections.Generic;
using System.Linq;

namespace ChessEngineLib
{
    using GameStateDetection;

    public class Game
    {
        private readonly List<Square> _allSquares;
        private readonly List<Square> _squaresOccupiedByPlayer;
        private readonly List<Square> _squaresOccupiedByOpponent;
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
            _squaresOccupiedByOpponent = new List<Square>();
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
            UpdatePlayerToMove(lastMovedColor);
            UpdateSquareDistribution();

            if (EitherKingIsChecked())
            {
                State = PlayerHasLegalMoves() ? GameState.Check : GameState.CheckMate;
            }
            else
            {
                State = PlayerHasLegalMoves() && OpponentHasLegalMoves() ? GameState.Normal : GameState.StaleMate;
            }
        }

        private void UpdatePlayerToMove(PieceColor lastMovedColor)
        {
            PlayerToMove = lastMovedColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        private void UpdateSquareDistribution()
        {
            _allSquares.Clear();
            Board.Iterate(square => _allSquares.Add(square));

            _squaresOccupiedByPlayer.Clear();
            Board.Iterate(square =>
            {
                if (square.Color == PlayerToMove)
                    _squaresOccupiedByPlayer.Add(square);
            });

            _squaresOccupiedByOpponent.Clear();
            Board.Iterate(square =>
            {
                if (square.Color.IsOppositeColor(PlayerToMove))
                    _squaresOccupiedByOpponent.Add(square);
            });
        }

        private bool EitherKingIsChecked()
        {
            return _checkStateDetector.KingIsChecked(Board, PieceColor.White)
                   || _checkStateDetector.KingIsChecked(Board, PieceColor.Black);
        }

        private bool PlayerHasLegalMoves()
        {
            var boolToReturn = false;

            foreach (var origin in _squaresOccupiedByPlayer)
            {
                _allSquares.Remove(origin);

                if (_allSquares.Any(destination => KingIsReleasedBy(origin, destination)))
                    boolToReturn = true;
            }

            return boolToReturn;
        }

        private bool OpponentHasLegalMoves()
        {
            var boolToReturn = false;

            foreach (var origin in _squaresOccupiedByOpponent)
            {
                _allSquares.Remove(origin);

                if (_allSquares.Any(destination => KingIsReleasedBy(origin, destination)))
                    boolToReturn = true;
            }

            return boolToReturn;
        }

        private bool KingIsReleasedBy(Square origin, Square destination)
        {
            if (!Board.IsLegalMove(origin, destination)) return false;
            var simulation = Board.SimulatedMove(origin, destination);
            return !_checkStateDetector.KingIsChecked(simulation, PlayerToMove);
        }
    }
}