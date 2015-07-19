using SwippableBottomTabView.ViewModels;
using Xamarin.Forms;

namespace SwippableBottomTabView
{
    class TabPageTwo: ContentView
    {
        public TabPageTwo()
        {
            BackgroundColor = Color.White;
            var label = new Label()
            {
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
            };
            label.TextColor = Color.Blue;
            label.SetBinding<TabTwoViewModel>(Label.TextProperty, vm => vm.PageTwoTitle);
            
            Content = label;
        }
    }
}
