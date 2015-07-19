using Xamarin.Forms;

namespace SwippableBottomTabView.ViewModels
{
    class TabTwoViewModel: BaseViewModel, ICarouselViewModel
    {
        public ContentView View
        {
            get { return new TabPageTwo(); }
        }

        public string PageTwoTitle
        {
            get
            {
                return "This is a title";
            }
        }
    }
}
