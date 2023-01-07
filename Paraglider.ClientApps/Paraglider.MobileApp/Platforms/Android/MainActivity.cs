﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Paraglider.MobileApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        RequestedOrientation = ScreenOrientation.Portrait;

        base.OnCreate(savedInstanceState);
    }
}