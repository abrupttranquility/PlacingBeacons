using PlacingBeacons.Models;
using PlacingBeacons.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PlacingBeacons
{
    public partial class PlacingBeaconsPage : ContentPage
    {
        Slider xSlider;
        Slider ySlider;
        Button removeBeaconButton;
        Button navigateStatsPageButton;
        BeaconImage selectedImage;
            BeaconImage mapImage;
            AbsoluteLayout layout = new AbsoluteLayout();
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        double circleSize = 0.16;
        public List<BeaconImage>  beaconImages;
        public PlacingBeaconsPage()
        {
            InitializeComponent();

            beaconImages = new List<BeaconImage>();

            mapImage = new BeaconImage
            {
                Source = "szkolarzut.png",
                Aspect = Aspect.Fill
            };

            AbsoluteLayout.SetLayoutBounds(mapImage, new Rectangle(0.5, 0.5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(mapImage, AbsoluteLayoutFlags.All);

            navigateStatsPageButton = new Button
            {
                Text = "Stats",
                IsVisible = false
            };
            AbsoluteLayout.SetLayoutBounds(navigateStatsPageButton, new Rectangle(0.1, 0.8, 0.2, 0.1));
            AbsoluteLayout.SetLayoutFlags(navigateStatsPageButton, AbsoluteLayoutFlags.All);

            navigateStatsPageButton.Clicked += NavigateStatsPageButton_Clicked;

            var addBeaconButton = new Button
            {
                Text = "Dodaj beacon",
                IsVisible = true
            };
            AbsoluteLayout.SetLayoutBounds(addBeaconButton, new Rectangle(0.5, 0.8, 0.4, 0.1));
            AbsoluteLayout.SetLayoutFlags(addBeaconButton, AbsoluteLayoutFlags.All);

            addBeaconButton.Clicked += AddBeaconButton_Clicked;

            removeBeaconButton = new Button
            {
                Text = "Usuń to",
                IsVisible = false
            };
            AbsoluteLayout.SetLayoutBounds(removeBeaconButton, new Rectangle(0.9, 0.8, 0.2, 0.1));
            AbsoluteLayout.SetLayoutFlags(removeBeaconButton, AbsoluteLayoutFlags.All);

            removeBeaconButton.Clicked += RemoveBeaconButton_Clicked;


            xSlider = new Slider
            {
                Minimum = 0 - circleSize / 2,
                Maximum = 1 + circleSize / 2,
                Value = 0.5,
                IsVisible = false,
            };

            AbsoluteLayout.SetLayoutBounds(xSlider, new Rectangle(0.5, 0.9, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(xSlider, AbsoluteLayoutFlags.All);

            xSlider.ValueChanged += OnSliderValueChanged;

            ySlider = new Slider
            {
                Minimum = 0 - circleSize / 2,
                Maximum = 1 + circleSize / 2,
                Value = 0.5,
                IsVisible = false,
            };

            AbsoluteLayout.SetLayoutBounds(ySlider, new Rectangle(0.5, 1, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(ySlider, AbsoluteLayoutFlags.All);

            ySlider.ValueChanged += OnSliderValueChanged;

            
            layout.Children.Add(mapImage);
            layout.Children.Add(xSlider);
            layout.Children.Add(ySlider);
            layout.Children.Add(addBeaconButton);
            layout.Children.Add(removeBeaconButton);
            layout.Children.Add(navigateStatsPageButton);

            Content = layout;

        }

        public void NavigateStatsPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new StatsPage(beaconImages));
        }

        public void OnImageTap(object sender, EventArgs e)
        {
            var tappedImage = (BeaconImage)sender;
            if (tappedImage == selectedImage)
                return;
            double tempX = tappedImage.saveX;
            double tempY = tappedImage.saveY;
            tappedImage.Source = "circleblue.png";
            selectedImage.Source = "circle.png";
            selectedImage = tappedImage;

            xSlider.Value = tempX;
            ySlider.Value = tempY;
            removeBeaconButton.IsVisible = true;
            xSlider.IsVisible = true;
            ySlider.IsVisible = true;
        }

        public void AddBeaconButton_Clicked(object sender, EventArgs e)
        {
            var tempImage = new BeaconImage
            {
                Source = "circleblue.png"
            };
            AbsoluteLayout.SetLayoutBounds(tempImage, new Rectangle(0.5, 0.5, circleSize, circleSize));
            AbsoluteLayout.SetLayoutFlags(tempImage, AbsoluteLayoutFlags.All);
            layout.Children.Add(tempImage);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnImageTap;
            tempImage.GestureRecognizers.Add(tapGestureRecognizer);

            if (beaconImages.Count != 0)
            {
                selectedImage.Source = "circle.png";
            };

            beaconImages.Add(tempImage);
            selectedImage = tempImage;

            removeBeaconButton.IsVisible = true;
            xSlider.IsVisible = true;
            ySlider.IsVisible = true;
            xSlider.Value = 0.5;
            ySlider.Value = 0.5;
            navigateStatsPageButton.IsVisible = true;
        }

        private void RemoveBeaconButton_Clicked(object sender, EventArgs e)
        {
            beaconImages.Remove(selectedImage);
            layout.Children.Remove(selectedImage);
            removeBeaconButton.IsVisible = false;
            xSlider.IsVisible = false;
            ySlider.IsVisible = false;

            if (beaconImages.Count == 0)
                navigateStatsPageButton.IsVisible = false;
        }

        public void OnSliderValueChanged(object sender, EventArgs e)
        {
            AbsoluteLayout.SetLayoutBounds(selectedImage, new Rectangle(xSlider.Value, ySlider.Value, circleSize, circleSize));
            selectedImage.saveX = xSlider.Value;
            selectedImage.saveY = ySlider.Value;
        }
    }
}