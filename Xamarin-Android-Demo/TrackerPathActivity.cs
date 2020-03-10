using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin_Android_Demo.Models;

namespace Xamarin_Android_Demo
{
    [Activity(Label = "TrackerPathActivity")]
    public class TrackerPathActivity : Activity, IOnMapReadyCallback
    {
        Track track;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_trackerpath);
            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            track = JsonConvert.DeserializeObject<Track>(Intent.GetStringExtra("track"));
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            PolylineOptions path = new PolylineOptions();
            LatLngBounds.Builder builder = new LatLngBounds.Builder();
            foreach (Location l in track.locations)
            {
                LatLng pos = new LatLng(l.lat, l.lng);
                MarkerOptions m = new MarkerOptions();
                m.SetPosition(pos);
                m.SetTitle(l.time);
                path.Add(pos);
                builder.Include(pos);
                googleMap.AddMarker(m);
            }
            googleMap.AddPolyline(path);
            LatLngBounds bounds = builder.Build();
            CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds(bounds, 50);
            googleMap.AnimateCamera(cu);
        }
    }
}