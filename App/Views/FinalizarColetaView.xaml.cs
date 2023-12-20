using App.Services;
using App.ViewModels;

namespace App.Views;

public partial class FinalizarColetaView : ContentPage
{
    readonly OrientacaoService _orientacaoService;

    public FinalizarColetaView(FinalizarColetaViewModel viewModel, OrientacaoService orientacaoService)
    {
        InitializeComponent();

        _orientacaoService = orientacaoService;

        BindingContext = viewModel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _orientacaoService.Portrait();
    }
}
