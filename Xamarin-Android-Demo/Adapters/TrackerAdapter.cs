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
using Newtonsoft.Json;
using Xamarin_Android_Demo.Models;

namespace Xamarin_Android_Demo.Adapters
{
    class TrackerAdapter : BaseAdapter<Track>
    {
        public Track[] data;
        Context context;
        public TrackerAdapter(Track[] data, Context context)
        {
            this.data = data;
            this.context = context;
        }
        public override Track this[int position] => data[position];

        public override int Count => data!=null?data.Length:0;

        public override long GetItemId(int position) => 0;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Track track = this[position];
            View v = LayoutInflater.From(context).Inflate(Resource.Layout.adapter_tracker, null, true);
            v.FindViewById<TextView>(Resource.Id.tv_title).Text = track.title;
            v.FindViewById<TextView>(Resource.Id.tv_locations).Text = track.locations.Length.ToString();
            v.Click += delegate
            {
                Intent i = new Intent(context, typeof(TrackerPathActivity));
                i.PutExtra("track", JsonConvert.SerializeObject(track));
                context.StartActivity(i);
            };
            return v;
        }
    }
}