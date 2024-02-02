using System.Windows.Input;
using App.ViewModels.Componentes;

namespace App.Views.Componentes;

public partial class TopoPrincipal : ContentView
{
    public static readonly BindableProperty TituloProperty = BindableProperty.Create(
        nameof(Titulo),
        typeof(string),
        typeof(TopoPrincipal),
        default(string),
        propertyChanged: OnTituloChanged
    );

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public delegate void CommandMenuEventHandler(object sender, EventArgs e);
    public event CommandMenuEventHandler? CommandMenuExecuted;

    public TopoPrincipal()
    {
        InitializeComponent();
        BindingContext = MauiProgram.Services.GetService<TopoPrincipalViewModel>();
    }

    private static void OnTituloChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TopoPrincipal)bindable;
        var viewModel = (TopoPrincipalViewModel)control.BindingContext;
        viewModel.Titulo = (string)newValue;
    }

    void Menu_Clicked(System.Object sender, System.EventArgs e)
    {
        CommandMenuExecuted?.Invoke(this, EventArgs.Empty);
    }
}
