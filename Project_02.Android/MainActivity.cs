﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Project_02.Services;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Project_02.Droid;
using Project_02.Assistants;
using AndroidX.Core.View;
using System.Threading.Tasks;
using Xamarin.Essentials;
using DependencyAttribute = Xamarin.Forms.DependencyAttribute;
using static Project_02.Droid.MainActivity;

[assembly: Dependency(typeof(MakeMessager))]
[assembly: Dependency(typeof(Project_02.Droid.MainActivity.Environment))]

namespace Project_02.Droid
{
    [Activity(Label = "Project_02", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class MakeMessager : IMessage
        {
            public void MakeMessage(string message)
            {
                Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
            }
        }
        public class Environment : IEnvir
        {
            public async void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
            {
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                    return;

                var activity = Platform.CurrentActivity;
                var window = activity.Window;

                //this may not be necessary(but may be fore older than M)
                window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);


                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                {
                    await Task.Delay(50);
                    WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightStatusBars = darkStatusBarTint;
                }

                window.SetStatusBarColor(color.ToPlatformColor());
            }
        }
    }
}