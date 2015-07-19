using Xamarin.Forms;

namespace SwippableBottomTabView.ViewModels
{
    class TabOneViewModel : BaseViewModel, ICarouselViewModel
    {
        public Color PageOneColor
        {
            get { return Color.Red; }
        }

        public ContentView View
        {
            get { return new TabPageOne(); }
        }
    }
}
