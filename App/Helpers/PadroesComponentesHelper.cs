using System;
namespace App.Helpers;

public class PadroesComponentesHelper
{
    public static void Init()
    {
        Microsoft.Maui.Handlers.ViewHandler.ViewMapper.AppendToMapping("Shadow", (handler, view) =>
        {
#if ANDROID
            if (handler.PlatformView is Android.Views.View androidView)
            {
                androidView.Elevation = 10; // Ajuste o valor de elevação conforme necessário
            }
#endif
        });
    }
}

