using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using App.Views;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class LerPedidoViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    [ObservableProperty]
    private bool exibirPesquisar;

    [ObservableProperty]
    public ObservableCollection<ColetaPendenteModel> lista = new();

    public LerPedidoViewModel(NavigationService navigationService)
    {
        (_navigationService) = (navigationService);

        ExibirPesquisar = false;
    }

    [RelayCommand]
    public async Task Finalizar()
    {
        var resposta = await App.Current?.MainPage?.DisplayAlert(
            "Atenção",
            "Deseja finalizar a coleta?",
            "SIM",
            "FECHAR"
        )!;

        if (resposta)
            await _navigationService.Push<FinalizarColetaView>();
    }

    [RelayCommand]
    public void LerQRCode()
    {
        _navigationService.Push<QrCodeView>();
    }

    public void Pesquisar() => ExibirPesquisar = !ExibirPesquisar;
}
