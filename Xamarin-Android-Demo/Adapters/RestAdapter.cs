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
using Xamarin_Android_Demo.Models;

namespace Xamarin_Android_Demo.Adapters
{
    class RestAdapter : BaseAdapter<Post>
    {
        public Post[] data;
        Context context;

        public RestAdapter(Context context)
        {
            this.context = context;
        }
        public override Post this[int position] => data[position];

        public override int Count => data!=null?data.Length:0;

        public override long GetItemId(int position) => 0;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Post post = this[position];
            View v = LayoutInflater.From(context).Inflate(Resource.Layout.adapter_rest, null, true);
            TextView title = v.FindViewById<TextView>(Resource.Id.tv_title);
            TextView body = v.FindViewById<TextView>(Resource.Id.tv_body);
            title.Text = post.title;
            body.Text = post.body;
            return v;
        }
    }
}