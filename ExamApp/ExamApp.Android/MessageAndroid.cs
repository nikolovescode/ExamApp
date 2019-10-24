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
using ExamApp.Droid;
using ExamApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]

namespace ExamApp.Droid
{
    public class MessageAndroid : IMessage
    {

        public async void LongAlert(string message)
        {

            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();

        }


        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }


    }

}