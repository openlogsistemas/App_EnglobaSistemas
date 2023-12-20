using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace App.ViewModels;

public partial class ColetaListaViewModel : BaseViewModel
{
    NavigationService _navigationService;
    ColetaService _coletaService;

    public ColetaListaViewModel(NavigationService navigationService, ColetaService coletaService)
    {
        (_navigationService, _coletaService) = (navigationService, coletaService);

        _ = Carregar();
    }

    [ObservableProperty]
    private bool exibirPesquisar;

    [ObservableProperty]
    public ObservableCollection<ColetaPendenteModel> lista = new();

    public async Task Carregar()
    {
        try
        {
            EstaCarregando = true;

            Lista.Clear();

            var coletas = await _coletaService.Pendentes();

            coletas?.ForEach(x => Lista.Add(x));
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

    public void Pesquisar() => ExibirPesquisar = !ExibirPesquisar;

    public async Task AceitarTodos()
    {
        await App.Current?.MainPage?.DisplayAlert(
            "Atenção",
            "Deseja realmente aceitar todas as coletas?",
            "SIM",
            "FECHAR"
        )!;
    }

    [RelayCommand]
    public void IrParaDetalhe(ColetaPendenteModel model)
    {
        App.Param = model;
        _navigationService.Push<ColetaDetalheView>();
    }

    [RelayCommand]
    public void OtimizarRoteiro()
    {
        EstaCarregando = true;

        var listaOrdenada = Lista.OrderBy(x => x.DistanciaKm).ToList();

        Lista.Clear();

        listaOrdenada.ForEach(x => Lista.Add(x));

        EstaCarregando = false;
    }
}
