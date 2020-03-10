using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin_Android_Demo.Models;
using Android.Views;
using Android.Content;
using Xamarin_Android_Demo.Adapters;

namespace Xamarin_Android_Demo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Module[] modules = new Module[] {
            new Module("REST",typeof(RESTActivity)),
            new Module("Google Map",typeof(GoogleMapActivity)),
            new Module("Tracker",typeof(TrackerActivity)),
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            ListView list = FindViewById<ListView>(Resource.Id.lv_modules);
            MainAdapter adapter = new MainAdapter(this, this.modules);
            list.Adapter = adapter;
        }
        
    }
}