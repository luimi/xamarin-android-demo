using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin_Android_Demo.Adapters;
using Xamarin_Android_Demo.Models;

namespace Xamarin_Android_Demo
{
    [Activity(Label = "Tracker", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TrackerActivity : AppCompatActivity, ILocationListener
    {
        Button startStop, clear, save;
        TextView result;
        ListView list;
        Track[] tracks;
        Track currentTrack;
        Boolean status = false;
        LocationManager locationManager;
        TrackerAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_tracker);

            this.currentTrack = new Track();
            this.tracks = GetTracks();
            this.locationManager = (LocationManager)GetSystemService(Context.LocationService);

            startStop = FindViewById<Button>(Resource.Id.btn_start_stop);
            clear = FindViewById<Button>(Resource.Id.btn_clear);
            save = FindViewById<Button>(Resource.Id.btn_save);
            result = FindViewById<TextView>(Resource.Id.tv_result);
            list = FindViewById<ListView>(Resource.Id.lv_saved);

            adapter = new TrackerAdapter(tracks, this);
            list.Adapter = adapter;
            
            save.Enabled = false;
            clear.Enabled = false;

            startStop.Click += delegate
            {
                if (status)
                {
                    startStop.Text = "Start Traking";
                    locationManager.RemoveUpdates(this);
                    status = !status;
                }
                else
                {
                    if (this.CheckSelfPermission( Manifest.Permission.AccessFineLocation) == Permission.Granted)
                    {
                        startStop.Text = "Stop Traking";
                        updateResultLocations();
                        
                        locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 1, this);
                        status = !status;
                    }
                    else
                    {
                        askPermission();
                        result.Text = "Allow GPS permission";
                    }
                    
                }
                
            };
            clear.Click += delegate
            {
                this.currentTrack.clear();
                save.Enabled = false;
                updateResultLocations();
            };
            save.Click += delegate
            {
                SaveTrack();
            };

        }
        protected override void OnDestroy()
        {
            if (status)
            {
                locationManager.RemoveUpdates(this);
            }
            base.OnDestroy();
        }
        private Track[] GetTracks()
        {
            string json = GetSharedPreferences().GetString("trackers", "[]");
            Track[] result = JsonConvert.DeserializeObject<Track[]>(json);
            return result;
        }
        private void SaveTrack()
        {
            Track[] newTracks = new Track[tracks.Length+1];
            currentTrack.title = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            newTracks[0] = currentTrack;
            for(int i = 0; i < tracks.Length; i++)
            {
                newTracks[i + 1] = tracks[i];
            }
            string json = JsonConvert.SerializeObject(newTracks);
            GetSharedPreferences().Edit().PutString("trackers", json).Commit();
            tracks = GetTracks();
            adapter.data = tracks;
            adapter.NotifyDataSetChanged();
            clear.Enabled = false;
            save.Enabled = false;
            updateResultLocations();
        }
        private void updateResultLocations()
        {
            this.result.Text = string.Format("Locations: {0}", currentTrack.locations.Length);
        }
        private ISharedPreferences GetSharedPreferences()
        {
             return PreferenceManager.GetDefaultSharedPreferences(this);
        }
        private void askPermission()
        {
            var requiredPermissions = new String[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation };
            RequestPermissions(requiredPermissions,0 );
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            Models.Location _location = new Models.Location();
            _location.time = DateTime.Now.ToString("HH:mm");
            _location.lat = location.Latitude;
            _location.lng = location.Longitude;
            currentTrack.addLocation(_location);
            updateResultLocations();
            save.Enabled = true;
            clear.Enabled = true;
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }
    }
}