using PlacingBeacons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlacingBeacons.Pages
{
    public partial class StatsPage : ContentPage
    {
        StackLayout layout = new StackLayout();
        public StatsPage(List<BeaconImage> beaconList)
        {

            int i = 0;
            foreach(BeaconImage image in beaconList)
            {
                i++;
                Label temp = new Label
                {
                    Text = "Beacon № " + i + "\nX: " + image.saveX + "\nY:" + image.saveY,
                };
                layout.Children.Add(temp);
            }

            Content = layout;
        }
    }
}