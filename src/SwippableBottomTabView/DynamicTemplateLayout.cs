using SwippableBottomTabView.ViewModels;
using Xamarin.Forms;

namespace SwippableBottomTabView
{
    public class DynamicTemplateLayout : ViewCell
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var vm = BindingContext as ICarouselViewModel;
            var page = vm.View;
            page.BindingContext = vm;
            View = page;
        }
    }
}
