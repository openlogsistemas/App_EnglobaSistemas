using System;
#if ANDROID
using Android.Content.PM;
using Android.App;
#elif IOS
using UIKit;
using Foundation;
#endif

namespace App.Services;

public class OrientacaoService
{

    public void Landscape()
    {
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity!.RequestedOrientation = ScreenOrientation.Landscape;
#elif IOS
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
            UIViewController.AttemptRotationToDeviceOrientation();
#endif
    }

    public void Portrait()
    {
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity!.RequestedOrientation = ScreenOrientation.Portrait;
#elif IOS
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
            UIViewController.AttemptRotationToDeviceOrientation();
#endif
    }
}

