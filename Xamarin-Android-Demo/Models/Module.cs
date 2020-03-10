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
    class Module
    {
        public string title;
        public Type activity;
        public Module(string title, Type activity)
        {
            this.title = title;
            this.activity = activity;
        }
    }
}