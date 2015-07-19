using Android.Graphics;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SwippableBottomTabView.Controls;
using SwippableBottomTabView.Droid.Renderers;
using System.ComponentModel;
using System.Reflection;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]

namespace SwippableBottomTabView.Droid.Renderers
{
    public class CarouselLayoutRenderer : ScrollViewRenderer
    {
        private int _prevScrollX;
        private int _deltaX;
        private bool _motionDown;
        private Timer _deltaXResetTimer;
        private Timer _scrollStopTimer;
        private HorizontalScrollView _scrollView;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            var a = new ViewPager(this.Context);
            var b = a.ClipBounds;
            base.OnElementChanged(e);
            if (e.NewElement == null) return;

            _deltaXResetTimer = new Timer(100) { AutoReset = false };
            _deltaXResetTimer.Elapsed += (object sender, ElapsedEventArgs args) => _deltaX = 0;

            _scrollStopTimer = new Timer(200) { AutoReset = false };
            _scrollStopTimer.Elapsed += (object sender, ElapsedEventArgs args2) => UpdateSelectedIndex();

            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        private void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                _scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
                    .GetField("hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this);

                _scrollView.HorizontalScrollBarEnabled = false;
                _scrollView.Touch += HScrollViewTouch;
            }
            if (e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !_motionDown)
            {
                ScrollToIndex(((CarouselLayout)this.Element).SelectedIndex);
            }
        }

        private void HScrollViewTouch(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            switch (e.Event.Action)
            {
                case MotionEventActions.Move:
                    _deltaXResetTimer.Stop();
                    _deltaX = _scrollView.ScrollX - _prevScrollX;
                    _prevScrollX = _scrollView.ScrollX;

                    UpdateSelectedIndex();

                    _deltaXResetTimer.Start();
                    break;

                case MotionEventActions.Down:
                    _motionDown = true;
                    _scrollStopTimer.Stop();
                    break;

                case MotionEventActions.Up:
                    _motionDown = false;
                    SnapScroll();
                    _scrollStopTimer.Start();
                    break;
            }
        }

        private void UpdateSelectedIndex()
        {
            var center = _scrollView.ScrollX + (_scrollView.Width / 2);
            var carouselLayout = (CarouselLayout)this.Element;
            carouselLayout.SelectedIndex = (center / _scrollView.Width);
        }

        private void SnapScroll()
        {
            var roughIndex = (float)_scrollView.ScrollX / _scrollView.Width;

            var targetIndex =
                _deltaX < 0 ? Math.Floor(roughIndex)
                : _deltaX > 0 ? Math.Ceil(roughIndex)
                : Math.Round(roughIndex);

            ScrollToIndex((int)targetIndex);
        }

        private void ScrollToIndex(int targetIndex)
        {
            var targetX = targetIndex * _scrollView.Width;
            _scrollView.Post(new Runnable(() =>
            {
                _scrollView.SmoothScrollTo(targetX, 0);
            }));
        }

        private bool _initialized = false;

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            if (_initialized) return;
            _initialized = true;
            var carouselLayout = (CarouselLayout)this.Element;
            _scrollView.ScrollTo(carouselLayout.SelectedIndex * Width, 0);
        }
    }
}