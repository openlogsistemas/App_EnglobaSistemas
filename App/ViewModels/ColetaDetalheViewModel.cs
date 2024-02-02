using System;
using App.Models;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using App.Views;
using CommunityToolkit.Mvvm.Messaging;
using App.Enums;

namespace App.ViewModels;

public partial class ColetaDetalheViewModel : BaseViewModel
{
    NavigationService _navigationService;

    ColetaService _coletaService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Ordem))]
    [NotifyPropertyChangedFor(nameof(Nome))]
    [NotifyPropertyChangedFor(nameof(Endereco))]
    [NotifyPropertyChangedFor(nameof(Remetente))]
    ColetaPendenteModel model;

    public string? Ordem => Model.NumeroPedido;
    public string? Nome => Model.DestinatarioNome;
    public string? Endereco => Model.DestinatarioEnderecoCompleto;
    public string? Remetente => Model.RemetenteNome;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EstaPendente))]
    [NotifyPropertyChangedFor(nameof(EstaAceito))]
    private ColetaStatusEnum status = new();

    public bool EstaPendente => Status == ColetaStatusEnum.Pendente;
    public bool EstaAceito => Status == ColetaStatusEnum.Aceito;

    public ColetaDetalheViewModel(NavigationService navigationService, ColetaService coletaService)
    {
        (_navigationService, _coletaService) = (navigationService, coletaService);

        Model = (ColetaPendenteModel)App.Request["coleta"]!;

        Status = Model.Status;
    }

    [RelayCommand]
    public async Task Aceitar()
    {
        bool aceito = await App.Current?.MainPage?.DisplayAlert(
            "Atenção",
            "Deseja realmente aceitar esta coleta?",
            "SIM",
            "NÃO"
        )!;
        if (aceito)
        {
            try
            {
                EstaCarregando = true;

                await _coletaService.AceitarColetas([Ordem ?? ""]);

                await Toast.Make("Coleta aceita com sucesso", ToastDuration.Long).Show();

                App.Request["reload"] = true;

                Status = ColetaStatusEnum.Aceito;

            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }
            finally
            {
                EstaCarregando = false;
            }
        }
    }

    [RelayCommand]
    public async Task Recusar()
    {
        bool recusado = await App.Current?.MainPage?.DisplayAlert(
            "Atenção",
            "Deseja realmente recusar esta coleta?",
            "SIM",
            "NÃO"
        )!;
        if (recusado)
        {
            try
            {
                EstaCarregando = true;

                await _coletaService.RecusarColetas([Ordem ?? ""]);

                await Toast.Make("Coleta recusada com sucesso", ToastDuration.Long).Show();
                App.Request["reload"] = true;

                await _navigationService.Pop();
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }
            finally
            {
                EstaCarregando = false;
            }
        }
    }

    [RelayCommand]
    public Task LerPedidos()
    {
        App.Request["coleta"] = Model;
        return _navigationService.Push<LerPedidoView>();
    }

    [RelayCommand]
    public Task LerPacking()
    {
        App.Request["coleta"] = Model;
        return _navigationService.Push<LerPackingView>();
    }

    [RelayCommand]
    public Task Insucesso()
    {
        App.Request["coleta"] = Model;
        return _navigationService.Push<InsucessoView>();
    }

    [RelayCommand]
    private async Task Mapa()
    {
        string acao = await App.Current?.MainPage?.DisplayActionSheet(
            "Selecione um mapa:",
            "FECHAR",
            null,
            "Waze",
            "Google Maps"
        )!;

        switch (acao)
        {
            case "Waze":
                await OpenWaze(Endereco ?? "");
                break;
            case "Google Maps":
                await OpenGoogleMaps(Endereco ?? "");
                break;
        }
    }

    public async Task OpenGoogleMaps(string address)
    {
        var addressEncoded = Uri.EscapeDataString(address);
        var googleMapsUrl = $"https://www.google.com/maps/search/?api=1&query={addressEncoded}";

        await Launcher.Default.OpenAsync(new Uri(googleMapsUrl));
    }

    public async Task OpenWaze(string address)
    {
        var addressEncoded = Uri.EscapeDataString(address);
        var wazeUrl = $"https://waze.com/ul?q={addressEncoded}";

        await Launcher.Default.OpenAsync(new Uri(wazeUrl));
    }
}
