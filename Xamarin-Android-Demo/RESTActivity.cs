using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin_Android_Demo.Adapters;
using Xamarin_Android_Demo.Models;

namespace Xamarin_Android_Demo
{
    [Activity(Label = "REST")]
    public class RESTActivity : Activity
    {
        HttpClient client;
        Button get;
        TextView getResponse;
        ProgressBar getProgress;
        ListView getList;
        RestAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rest);
            client = new HttpClient();
            
            get = FindViewById<Button>(Resource.Id.btn_get);
            getResponse = FindViewById<TextView>(Resource.Id.tv_get_response);
            getProgress = FindViewById<ProgressBar>(Resource.Id.pb_get_response);
            getList = FindViewById<ListView>(Resource.Id.lv_rest);
            adapter = new RestAdapter(this);
            getList.SetAdapter(adapter);
            get.Click += async delegate
            {
                getProgress.Visibility = ViewStates.Visible;
                Uri url = new Uri("https://jsonplaceholder.typicode.com/posts");
                try
                {
                    var response = await client.GetAsync(url);
                    getProgress.Visibility = ViewStates.Invisible;
                    var json = await response.Content.ReadAsStringAsync();
                    Post[] posts = Newtonsoft.Json.JsonConvert.DeserializeObject<Post[]>(json);
                    adapter.data = posts;
                    adapter.NotifyDataSetChanged();
                    getResponse.Text = "llegaron " + posts.Length + " Posts";
                }
                catch (Exception e)
                {
                    getResponse.Text = "Fallo la peticion: "+e.Message;
                    getProgress.Visibility = ViewStates.Invisible;
                }
                
            };
        }
    }
}