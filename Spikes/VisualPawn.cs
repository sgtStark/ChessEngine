using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ChessUI.VisualChessPieces
{
    using ChessEngineLib;

    public class VisualPawn : Shape
    {
        private static readonly Random Random = new Random(unchecked((int)DateTime.Now.Ticks));
        private readonly Canvas _pieceLayer;
        private readonly Square _square;

        public VisualPawn(Canvas pieceLayer, Square square)
        {
            _pieceLayer = pieceLayer;
            _square = square;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                var geometry = CreateGeometry();
                RenderTransform = CreateTransform();
                _pieceLayer.Children.Add(this);
                _pieceLayer.RegisterName(CreateName(_square), this);
                return geometry;
            }
        }

        private Geometry CreateGeometry()
        {
            var geometry = new PathGeometry
                               {
                                   Figures =
                                       new PathFigureCollection
                                           {
                                               new PathFigure(new Point(80, 89),
                                                              CreatePathSegments(),
                                                              true)
                                           }
                               };

            return geometry;
        }

        private static IEnumerable<PathSegment> CreatePathSegments()
        {
            var pathSegments = new PathSegmentCollection
                                   {
                                       new LineSegment {Point = new Point(20, 89)},
                                       new QuadraticBezierSegment
                                           {
                                               Point1 = new Point(20, 69),
                                               Point2 = new Point(43, 55)
                                           },
                                       new BezierSegment
                                           {
                                               Point1 = new Point(30, 48),
                                               Point2 = new Point(30, 35),
                                               Point3 = new Point(43, 28)
                                           },
                                       new ArcSegment
                                           {
                                               Size = new Size(11, 11),
                                               RotationAngle = 0,
                                               IsLargeArc = true,
                                               SweepDirection = SweepDirection.Clockwise,
                                               Point = new Point(57, 28)
                                           },
                                       new BezierSegment
                                           {
                                               Point1 = new Point(70, 35),
                                               Point2 = new Point(70, 48),
                                               Point3 = new Point(57, 55)
                                           },
                                       new QuadraticBezierSegment
                                           {
                                               Point1 = new Point(80, 69),
                                               Point2 = new Point(80, 89)
                                           }
                                   };

            return pathSegments;
        }

        private Transform CreateTransform()
        {
            var transform = new TranslateTransform
            {
                X = (8 - _square.File) * CalculateSquareSize(),
                Y = (8 - _square.Rank) * CalculateSquareSize()
            };

            return transform;
        }

        private int CalculateSquareSize()
        {
            return (int)_pieceLayer.Width / 8;
        }

//                <DrawingBrush x:Key="WhitePawn" Stretch="Uniform" Viewbox="0,0 100,100" ViewboxUnits="Absolute">
//            <DrawingBrush.Drawing>
//                <DrawingGroup>
//                    <DrawingGroup.Children>
//                        <GeometryDrawing Brush="{StaticResource PieceShadowBrush}">
//                            <GeometryDrawing.Geometry>
//                                <PathGeometry Figures="{StaticResource PawnGeometryFigures}" Transform="{StaticResource PieceShadowTransform}"/>
//                            </GeometryDrawing.Geometry>
//                        </GeometryDrawing>
//                        <GeometryDrawing Brush="{StaticResource WhitePieceBackgroundBrush}" Pen="{StaticResource PieceBackgroundPen}">
//                            <GeometryDrawing.Geometry>
//                                <PathGeometry Figures="{StaticResource PawnGeometryFigures}" Transform="{StaticResource PieceTransform}"/>
//                            </GeometryDrawing.Geometry>
//                        </GeometryDrawing>
//                        <GeometryDrawing Brush="{StaticResource PawnHighlight}">
//                            <GeometryDrawing.Geometry>
//                                <PathGeometry Figures="{StaticResource PawnGeometryFigures}" Transform="{StaticResource PieceTransform}"/>
//                            </GeometryDrawing.Geometry>
//                        </GeometryDrawing>
//                    </DrawingGroup.Children>
//                </DrawingGroup>
//            </DrawingBrush.Drawing>
//        </DrawingBrush>

        private static string CreateName(Square square)
        {
            return square.Occupier.GetType().Name +
                   Random.Next(100000000, 1000000000).ToString(CultureInfo.InvariantCulture);
        }

        public bool Equals(object other)
        {
            if (!(other is VisualPawn)) return false;
            var visualPawn = other as VisualPawn;
            if (!(visualPawn.RenderTransform is TranslateTransform)) return false;
            if (!(RenderTransform is TranslateTransform)) return false;
            var renderTransform = RenderTransform as TranslateTransform;
            var othersTransform = visualPawn.RenderTransform as TranslateTransform;

            return (renderTransform.X.Equals(othersTransform.X)
                && renderTransform.Y.Equals(othersTransform.Y));
        }
    }
}
