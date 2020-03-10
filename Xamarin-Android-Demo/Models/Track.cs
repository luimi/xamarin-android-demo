using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xamarin_Android_Demo.Models
{
    class Track
    {
        public string title { set; get; }
        public Location[] locations { set; get; }
        public Track()
        {
            locations = new Location[0];
        }
        public void addLocation(Location location)
        {
            Location[] newLocations = new Location[locations.Length + 1];
            for (int i = 0; i < locations.Length; i++)
            {
                newLocations[i] = locations[i];
            }
            newLocations[locations.Length] = location;
            locations = newLocations;
        }
        public void clear()
        {
            locations = new Location[0];
        }
    }
}