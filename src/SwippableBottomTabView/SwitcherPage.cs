﻿using SwippableBottomTabView.Controls;
using Xamarin.Forms;

namespace SwippableBottomTabView
{
    public class SwitcherPage : ContentPage
    {
        public SwitcherPage()
        {
            Title = "Pager Layout w/ Indicators";

            var none = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "No pager indicator",
                Command = new Command((obj) => Navigation.PushAsync(new HomePage(CarouselLayout.IndicatorStyleEnum.None)))
            };
            var dots = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "Dots",
                Command = new Command((obj) => Navigation.PushAsync(new HomePage(CarouselLayout.IndicatorStyleEnum.Dots)))
            };
            var tabs = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "Tabs",
                Command = new Command((obj) => Navigation.PushAsync(new HomePage(CarouselLayout.IndicatorStyleEnum.Tabs)))
            };
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 20,
                Children = {
					none,
					dots,
					tabs
				}
            };
        }
    }
}