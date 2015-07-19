using SwippableBottomTabView.ViewModels;
using Xamarin.Forms;

namespace SwippableBottomTabView
{
    class TabPageOne: ContentView
    {
        public TabPageOne()
        {
            var box = new BoxView()
            {
                WidthRequest = 100,
                HeightRequest = 100,
            };
            box.SetBinding<TabOneViewModel>(BoxView.ColorProperty, vm => vm.PageOneColor);
            Content = box;
        }
    }
}
