using System;
using System.Windows.Input;

namespace App.Views.Componentes;

public class Input : Entry
{
    public static readonly BindableProperty TextoAlteradoCommandProperty = BindableProperty.Create(
        nameof(TextoAlteradoCommand),
        typeof(ICommand),
        typeof(Rodape),
        null
    );

    public ICommand TextoAlteradoCommand
    {
        get => (ICommand)GetValue(TextoAlteradoCommandProperty);
        set => SetValue(TextoAlteradoCommandProperty, value);
    }

    int altura = 35;

    public Input()
    {
        this.HandlerChanged += OnHandlerChanged!;

        this.HeightRequest = altura;

        this.TextChanged += OnTextChanged!;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (TextoAlteradoCommand != null && TextoAlteradoCommand.CanExecute(e.NewTextValue))
        {
            TextoAlteradoCommand.Execute(e.NewTextValue);
        }
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
