using System;
using System.Windows.Input;

namespace App.Views.Componentes;

public class Radio : RadioButton
{
    public Radio()
    {
        this.HandlerChanged += OnHandlerChanged!;
    }

    private void OnHandlerChanged(object sender, EventArgs e)
    {
        // Personalização para Android
#if ANDROID
        if (Handler?.PlatformView is Android.Widget.RadioButton nativeAndroidRadioButton)
        {
            // Definir a cor do círculo do RadioButton quando não está selecionado para vermelho
            int[][] states = new int[][]
            {
                new int[] { Android.Resource.Attribute.StateEnabled }, // enabled
                new int[] { -Android.Resource.Attribute.StateEnabled }, // disabled
                new int[] { -Android.Resource.Attribute.StateChecked }, // unchecked
                new int[] { Android.Resource.Attribute.StatePressed } // pressed
            };

            int[] colors = new int[]
            {
                Android.Graphics.Color.Black, // enabled
                Android.Graphics.Color.Gray, // disabled
                Android.Graphics.Color.Black, // unchecked
                Android.Graphics.Color.Black // pressed
            };

            Android.Content.Res.ColorStateList myList = new Android.Content.Res.ColorStateList(
                states,
                colors
            );
            nativeAndroidRadioButton.ButtonTintList = myList;

            // Definir o fundo do RadioButton para transparente
            nativeAndroidRadioButton.Background = null;

            nativeAndroidRadioButton.SetTextColor(Android.Graphics.Color.Black);
        }
#endif

        // Personalização para iOS
#if IOS || MACCATALYST
        // if (Handler?.PlatformView is UIKit.UIButton nativeIOSRadioButton)
        // {
        //     // Para o iOS, você precisará modificar o controle para se parecer com um RadioButton
        //     // Configurar a cor do texto quando não está selecionado
        //     nativeIOSRadioButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

        //     // Aqui você definiria outras propriedades visuais para se parecer mais com um RadioButton
        //     // Por exemplo, você pode querer adicionar um círculo ou imagem que represente o estado não selecionado
        // }
#endif
    }
}
