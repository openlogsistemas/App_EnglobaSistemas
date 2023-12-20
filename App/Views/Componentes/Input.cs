using System;
namespace App.Views.Componentes;

public class Input : Entry
{
    int altura = 35;

    public Input()
    {
        this.HandlerChanged += OnHandlerChanged!;

        this.HeightRequest = altura;
    }

    private void OnHandlerChanged(object sender, EventArgs e)
    {
#if ANDROID
        if (Handler?.PlatformView is AndroidX.AppCompat.Widget.AppCompatEditText nativeAndroidEntry)
        {
            // Remove as bordas no Android
            nativeAndroidEntry.Background = null;

        }
#endif

#if IOS || MACCATALYST
        if (Handler?.PlatformView is UIKit.UITextField nativeIOSEntry)
        {
            // Remove as bordas no iOS
            nativeIOSEntry.BorderStyle = UIKit.UITextBorderStyle.None;

        }
#endif
    }
}