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
    class MainAdapter : BaseAdapter<Module>
    {
        Context context;
        Module[] data;

        public MainAdapter(Context context, Module[] data)
        {
            this.context = context;
            this.data = data;
        }

        public override Module this[int position] => this.data[position];

        public override int Count => this.data != null ? data.Length : 0;

        public override long GetItemId(int position) => 0;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Module module = data[position];
            View v = LayoutInflater.From(context).Inflate(Resource.Layout.adapter_main, null, true);
            TextView title = (TextView)v.FindViewById(Resource.Id.tv_title);
            title.Text = module.title;
            v.Click += (sender, e) =>
            {
                context.StartActivity(module.activity);
            };
            return v;
        }
    }
}