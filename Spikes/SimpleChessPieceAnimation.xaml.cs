using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Spikes
{
    /// <summary>
    /// Interaction logic for SimpleChessPieceAnimation.xaml
    /// </summary>
    public partial class SimpleChessPieceAnimation : Window
    {
        private Storyboard _storyboard;

        private bool _isAnimating;

        public SimpleChessPieceAnimation()
        {
            InitializeComponent();
        }

        private void Pawn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isAnimating) return;

            _isAnimating = true;
            _storyboard = new Storyboard();
            _storyboard.FillBehavior = FillBehavior.HoldEnd;

            var xAxisAnimation = new DoubleAnimation();
            xAxisAnimation.To = (Pawn.RenderTransform as TranslateTransform).X + 100;
            xAxisAnimation.BeginTime = TimeSpan.FromMilliseconds(100);
            xAxisAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(800));
            xAxisAnimation.EasingFunction = new CubicEase();

            Storyboard.SetTargetName(xAxisAnimation, Pawn.Name);
            Storyboard.SetTargetProperty(xAxisAnimation, new PropertyPath("(0).(1)", RenderTransformProperty, TranslateTransform.XProperty));
            xAxisAnimation.Freeze();

            var yAxisAnimation = new DoubleAnimation
                (
                    (Pawn.RenderTransform as TranslateTransform).Y + 100,
                    new Duration(TimeSpan.FromMilliseconds(800))
                );

            yAxisAnimation.EasingFunction = new CubicEase();
            yAxisAnimation.BeginTime = TimeSpan.FromMilliseconds(100);
            Storyboard.SetTargetName(yAxisAnimation, Pawn.Name);
            Storyboard.SetTargetProperty(yAxisAnimation, new PropertyPath("(0).(1)", RenderTransformProperty, TranslateTransform.YProperty));
            yAxisAnimation.Freeze();

            _storyboard.Children.Add(xAxisAnimation);
            _storyboard.Children.Add(yAxisAnimation);

            _storyboard.Completed += (s, args) => _isAnimating = false;

            _storyboard.Freeze();


            _storyboard.Begin(this, HandoffBehavior.Compose);
        }
    }
}
