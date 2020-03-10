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
    class Post
    {
        public int userId {get;set;}
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}