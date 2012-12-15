using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ChessUI.VisualChessPieces;

namespace ChessUI
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    public partial class StandardChessBoard : UserControl
    {
        private readonly Board _board;
        private readonly Game _model;
        private readonly Shape[,] _pieces;
        private string _name;
        private Square _currentSquare;
        private Storyboard _storyboard;

        private static readonly Random random = new Random(unchecked((int)DateTime.Now.Ticks));

        public StandardChessBoard()
        {
            InitializeComponent();
            _board = new Board();
            _model = new Game(_board);
            _model.Board.Setup();
            _pieces = new Shape[9,9];
            DrawBoard();
            _board.OnMove += OnMove;
        }

        private void OnMove(object sender, MoveEventArgs e)
        {
            _storyboard = new Storyboard();
            _storyboard.FillBehavior = FillBehavior.HoldEnd;

            var xAxisAnimation = new DoubleAnimation
                                     {
                                         To = CalculateX(e.To),
                                         Duration = new Duration(TimeSpan.FromMilliseconds(600))
                                     };

            Storyboard.SetTargetName(xAxisAnimation, _pieces[e.From.File, e.From.Rank].Name);
            Storyboard.SetTargetProperty(xAxisAnimation, new PropertyPath("(0).(1)", RenderTransformProperty, TranslateTransform.XProperty));
            xAxisAnimation.EasingFunction = new PowerEase();
            xAxisAnimation.Freeze();

            var yAxisAnimation = new DoubleAnimation
                (
                    CalculateY(e.To),
                    new Duration(TimeSpan.FromMilliseconds(600))
                );

            Storyboard.SetTargetName(yAxisAnimation, _pieces[e.From.File, e.From.Rank].Name);
            Storyboard.SetTargetProperty(yAxisAnimation, new PropertyPath("(0).(1)", RenderTransformProperty, TranslateTransform.YProperty));
            yAxisAnimation.EasingFunction = new PowerEase();
            yAxisAnimation.Freeze();

            _storyboard.Children.Add(xAxisAnimation);
            _storyboard.Children.Add(yAxisAnimation);

            if (e.To.Occupier.Color != PieceColor.Empty)
            {
                var opacityAnimation = new DoubleAnimation
                    (
                        0.0,
                        new Duration(TimeSpan.FromMilliseconds(400))
                    );

                opacityAnimation.BeginTime = TimeSpan.FromMilliseconds(200);
                Storyboard.SetTargetName(opacityAnimation, _pieces[e.To.File, e.To.Rank].Name);
                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));

                opacityAnimation.Freeze();

                _storyboard.Children.Add(opacityAnimation);
            }

            _storyboard.Completed += (s, args) => DrawBoard();
            _storyboard.Freeze();

            _storyboard.Begin(PieceLayer, HandoffBehavior.Compose);
        }

        private void DrawBoard()
        {
            PieceSelection.Visibility = Visibility.Hidden;

            PieceLayer.Children.Clear();

            _board.Iterate(square =>
                               {
                                   if (square.Color != PieceColor.Empty)
                                   {
                                        _pieces[square.File, square.Rank] = CreateVisualPiece(square);
                                   }
                               });
        }

        private Rectangle CreateVisualPiece(Square square)
        {
            var visualPiece = new Rectangle
                                  {
                                      Width = CalculateSquareSize(),
                                      Height = CalculateSquareSize(),
                                  };

            _name = square.Occupier.GetType().Name + random.Next(100000000, 1000000000).ToString(CultureInfo.InvariantCulture);
            visualPiece.Name = _name;
            visualPiece.Fill = GetPieceBrush(square.Occupier);
            visualPiece.RenderTransform = new TranslateTransform(CalculateX(square), CalculateY(square));

            PieceLayer.Children.Add(visualPiece);
            PieceLayer.RegisterName(visualPiece.Name, visualPiece);

            return visualPiece;
        }

        private Brush GetPieceBrush(ChessPiece occupier)
        {
            return FindResource(occupier.ToString()) as Brush;
        }

        private int CalculateSquareSize()
        {
            return (int) GameBoard.Width / 8;
        }

        private void MouseLeftButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var pointClickedAt = e.GetPosition(GameBoard);
            var squareClicked = _board.GetSquare(CalculateFile(pointClickedAt.X), CalculateRank(pointClickedAt.Y));

            if (_currentSquare != null)
            {
                if (_currentSquare.Equals(squareClicked))
                {
                    PieceSelection.Visibility = Visibility.Hidden;
                    _currentSquare = null;
                }
                else
                {
                    PieceSelection.Visibility = Visibility.Hidden;
                    _board.Move(_currentSquare, squareClicked);
                    _currentSquare = null;
                }
            }
            else
            {
                if (squareClicked.Occupier.Equals(new NullPiece())) return;

                PieceSelection.RenderTransform = new TranslateTransform(CalculateX(squareClicked), CalculateY(squareClicked));
                PieceSelection.Visibility = Visibility.Visible;
                _currentSquare = squareClicked;
            }
        }

        private int CalculateRank(double y)
        {
            return 8 - ((int)y / CalculateSquareSize());
        }

        private int CalculateFile(double x)
        {
            return ((int)x / CalculateSquareSize()) + 1;
        }

        private double CalculateX(Square square)
        {
//            var result = (square.File * CalculateSquareSize() - 12.5) - CalculateSquareSize();
            var result = (square.File * CalculateSquareSize()) - CalculateSquareSize();
            return result;
        }

        private double CalculateY(Square square)
        {
//            return (CalculateSquareSize() * 8) - (square.Rank * CalculateSquareSize() + 12.5);
            return (CalculateSquareSize() * 8) - (square.Rank * CalculateSquareSize());
        }
    }
}
