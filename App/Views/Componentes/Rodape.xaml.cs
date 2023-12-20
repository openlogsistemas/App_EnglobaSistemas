using System.Windows.Input;
using App.ViewModels.Componentes;

namespace App.Views.Componentes;

public partial class Rodape : ContentView
{
    public static readonly BindableProperty OtimizarRoteiroCommandProperty =
        BindableProperty.Create(
            nameof(OtimizarRoteiroCommand),
            typeof(ICommand),
            typeof(Rodape),
            null,
            propertyChanged: OnOtimizarRoteiroCommand
        );

    public ICommand OtimizarRoteiroCommand
    {
        get => (ICommand)GetValue(OtimizarRoteiroCommandProperty);
        set => SetValue(OtimizarRoteiroCommandProperty, value);
    }

    public Rodape()
    {
        InitializeComponent();
        BindingContext = MauiProgram.Services.GetService<RodapeViewModel>();
    }

    private static void OnOtimizarRoteiroCommand(
        BindableObject bindable,
        object oldValue,
        object newValue
    )
    {
        var control = (Rodape)bindable;
        var viewModel = (RodapeViewModel)control.BindingContext;
        viewModel.OtimizarRoteiro = (Command)newValue;
    }
}
