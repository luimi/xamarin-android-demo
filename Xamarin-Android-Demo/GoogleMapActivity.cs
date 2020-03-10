using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Xamarin_Android_Demo
{
    [Activity(Label = "GoogleMap", Theme = "@style/AppTheme")]
    public class GoogleMapActivity : AppCompatActivity
    {
        // https://docs.microsoft.com/en-us/xamarin/android/platform/maps-and-location/maps/maps-api
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                SetContentView(Resource.Layout.activity_googlemap);
            }
            catch(Exception e)
            {
                Toast.MakeText(this, "Error al cargar mapa "+e.Message, ToastLength.Long).Show();
            }
            
            // Create your application here
        }
    }
}