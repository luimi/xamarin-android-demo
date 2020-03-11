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
using Parse;

namespace Xamarin_Android_Demo
{
    [Activity(Label = "Parse")]
    public class ParseActivity : AppCompatActivity
    {
        // https://docs.parseplatform.org/dotnet/guide/
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_parse);

            FindViewById<Button>(Resource.Id.btn_save).Click += async delegate
            {
                ParseObject xamarin = new ParseObject("xamarin");
                try
                {
                    await xamarin.SaveAsync();
                    Toast.MakeText(this, "New Object Saved!", ToastLength.Long).Show();
                }
                catch(Exception e)
                {
                    Toast.MakeText(this, "Error saving object! \n"+ e.Message, ToastLength.Long).Show();
                }
                

            };
            FindViewById<Button>(Resource.Id.btn_get).Click += async delegate
            {
                try
                {
                    ParseObject last = await ParseObject.GetQuery("xamarin").FirstAsync();
                    Toast.MakeText(this, "Got last object!", ToastLength.Long).Show();
                }
                catch (Exception e)
                {
                    Toast.MakeText(this, "Error gettinglas  object! \n" + e.Message, ToastLength.Long).Show();
                }
            };
        }
    }
}