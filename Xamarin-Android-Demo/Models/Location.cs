using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xamarin_Android_Demo.Models
{
    public class Location
    {
        public double lat {set; get;}
        public double lng {set; get;}
        public string time {set; get;}

    }
}