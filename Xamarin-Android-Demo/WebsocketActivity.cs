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
using WebSocket4Net;
using Xamarin_Android_Demo.Adapters;

namespace Xamarin_Android_Demo
{
    [Activity(Label = "Websocket")]
    public class WebsocketActivity : Activity
    {
        WebSocket ws;
        EditText message;
        ArrayAdapter<string> adapter;
        TextView status;
        Button send;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_websocket);

            send = FindViewById<Button>(Resource.Id.btn_send);
            message = FindViewById<EditText>(Resource.Id.edt_message);
            status = FindViewById<TextView>(Resource.Id.tv_status);
            ListView list = FindViewById<ListView>(Resource.Id.lv_messages);

            ws = new WebSocket("wss://lui2mi-websocket.glitch.me");
            ws.Opened += new EventHandler(onConnected);
            ws.Closed += new EventHandler(onDisconnect);
            ws.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(onError);
            ws.MessageReceived += new EventHandler<WebSocket4Net.MessageReceivedEventArgs>(onMessage);
            ws.Open();
            adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, new List<string>());
            list.Adapter = adapter;

            send.Click += new EventHandler(sendMessage);
        }
        protected override void OnDestroy()
        {
            try
            {
                ws.Close();
            }
            catch(Exception e) { }
            
            base.OnDestroy();
        }
        private void onConnected(object s, EventArgs e)
        {
            RunOnUiThread(() =>
            {
                status.Text = "Connected";
                status.SetBackgroundColor(Android.Graphics.Color.ParseColor("#28a745"));
                send.Enabled = true;
            });
            
        }
        private void onDisconnect(object s, EventArgs e)
        {
            RunOnUiThread(() =>
            {
                status.Text = "Disconnected";
                status.SetBackgroundColor(Android.Graphics.Color.ParseColor("#dc3545"));
                send.Enabled = false;
            });
            
        }
        private void onError(object s, EventArgs e)
        {
            RunOnUiThread(() =>
            {
                status.Text = "Error";
                status.SetBackgroundColor(Android.Graphics.Color.ParseColor("#dc3545"));
                send.Enabled = false;
            });
            
        }
        private void onMessage(object sender, MessageReceivedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                adapter.Add(e.Message);
                adapter.NotifyDataSetChanged();
            });

        }
        private void sendMessage(object s, EventArgs e)
        {
            if (message.Text != "")
            {
                ws.Send(message.Text);
                message.Text = "";
            }
        }
    }
}