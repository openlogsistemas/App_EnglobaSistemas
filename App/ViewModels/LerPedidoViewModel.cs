using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class LerPedidoViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly ColetaService _coletaService;

    [ObservableProperty]
    ColetaPendenteModel model;

    [ObservableProperty]
    private bool exibirPesquisar;

    [ObservableProperty]
    private string? numeroPedido;

    [ObservableProperty]
    private bool exibirSelecionarPedido;

    [ObservableProperty]
    public ObservableCollection<ColetaPendenteModel> lista = new();

    [ObservableProperty]
    public IEnumerable<ColetaPendenteModel>? historico;

    [ObservableProperty]
    public ObservableCollection<ColetaPendenteModel> listaPesquisa = new();

    [ObservableProperty]
    public bool pedidosEncontrados = false;

    [ObservableProperty]
    private bool estaCarregandoPedidos;

    public LerPedidoViewModel(NavigationService navigationService, ColetaService coletaService)
    {
        (_navigationService, _coletaService) = (navigationService, coletaService);

        Model = (ColetaPendenteModel)App.Request["coleta"]!;

        ExibirPesquisar = false;
        ExibirSelecionarPedido = false;
        _coletaService = coletaService;
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
        {
            App.Request["coletasAdicionadas"] = Lista;
            await _navigationService.Push<FinalizarColetaView>();
        }

    }

    [RelayCommand]
    public void LerQRCode()
    {
        _navigationService.PushModal<QrCodeView>();
    }

    public void Pesquisar() => ExibirPesquisar = !ExibirPesquisar;

    [RelayCommand]
    public async Task PesquisarNumeroPedidoAsync()
    {
        try
        {
            EstaCarregandoPedidos = true;

            ExibirSelecionarPedido = true;

            ListaPesquisa.Clear();

            var pedidos = await _coletaService.PedidosPendentes(NumeroPedido ?? string.Empty);

            foreach (var item in pedidos!)
            {
                ListaPesquisa.Add(item);
            }

            PedidosEncontrados = !ListaPesquisa.Any();
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
        finally
        {
            EstaCarregandoPedidos = false;
        }
    }

    [RelayCommand]
    public void CancelarPesquisa()
    {
        ListaPesquisa.Clear();
        ExibirSelecionarPedido = false;
    }


    [RelayCommand]
    public void AdicionarColeta(ColetaPendenteModel coleta)
    {
        if (!Lista.Where(x => x.NumeroPedido == coleta.NumeroPedido).Any())
            Lista.Add(coleta);

        NumeroPedido = "";
        CancelarPesquisa();
    }
}
