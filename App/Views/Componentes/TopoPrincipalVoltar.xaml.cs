using System.Windows.Input;
using App.ViewModels.Componentes;

namespace App.Views.Componentes;

public partial class TopoPrincipalVoltar : ContentView
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

    public TopoPrincipalVoltar()
    {
        InitializeComponent();
        BindingContext = MauiProgram.Services.GetService<TopoPrincipalVoltarViewModel>();
    }

    private static void OnTituloChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TopoPrincipalVoltar)bindable;
        var viewModel = (TopoPrincipalVoltarViewModel)control.BindingContext;
        viewModel.Titulo = (string)newValue;
    }
}
