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
using Parse;

namespace Xamarin_Android_Demo
{
    [Application]
    class App : Application
    {
        public App(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId = "UrBuMScXG4HHZ7li7ssnfofAVOEMG99io1pAmzMK",
                WindowsKey = "vIH7dXSipajCCKMMneiWb00A61u43oLXt0VoaA7M",
                Server = "https://parseapi.back4app.com/"
            });
        }
    }
}