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
    private string? pesquisarTexto;

    [ObservableProperty]
    public IEnumerable<ColetaPendenteModel>? historico;

    [ObservableProperty]
    public ObservableCollection<ColetaPendenteModel> lista = new();

    public async Task Carregar()
    {
        try
        {
            EstaCarregando = true;

            Lista.Clear();

            Historico = await _coletaService.Pendentes();

            foreach (var item in Historico!)
            {
                Lista.Add(item);
            }
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

    [RelayCommand]
    public void IrParaDetalhe(ColetaPendenteModel model)
    {
        App.Request["coleta"] = model;
        _navigationService.Push<ColetaDetalheView>();
    }

    [RelayCommand]
    public async Task OtimizarRoteiroAsync()
    {
        EstaCarregando = true;

        Lista.Clear();

        var historico = Historico?.OrderBy(x => x.Status).ThenBy(x => x.DistanciaKm).ToList();

        foreach (var item in historico!)
        {
            Lista.Add(item);
        }

        await Toast.Make("Roteiro otimizado").Show();

        EstaCarregando = false;
    }

    [RelayCommand]
    public void Pesquisar(string texto)
    {
        PesquisarTexto = texto;

        EstaCarregando = true;

        Lista.Clear();

        Historico
            ?.Where(
                x =>
                    x.NumeroPedido!.ToLower().Contains(texto.ToLower())
                    || x.DestinatarioEnderecoCompleto!.ToLower().Contains(texto.ToLower())
                    || x.DestinatarioNome!.ToLower().Contains(texto.ToLower())
                    || x.RemetenteNome!.ToLower().Contains(texto.ToLower())
            )
            .ToList()
            .ForEach(x => Lista.Add(x));

        EstaCarregando = false;
    }

    [RelayCommand]
    public void PesquisarClique() => Pesquisar(PesquisarTexto ?? string.Empty);

    [RelayCommand]
    public async Task AceitarTodos()
    {
        var existePendentes = Historico?.Any(x => x.Status == Enums.ColetaStatusEnum.Pendente);

        if (existePendentes ?? false)
        {
            if (
                await App.Current?.MainPage?.DisplayAlert(
                    "Atenção",
                    "Deseja realmente aceitar todas as coletas?",
                    "SIM",
                    "FECHAR"
                )!
            )
            {
                EstaCarregando = true;

                var numeros = Historico
                    ?.Where(x => x.Status == Enums.ColetaStatusEnum.Pendente)
                    .Select(x => x.NumeroPedido)
                    .ToList();

                if (numeros?.Any() == true)
                {
                    await _coletaService.AceitarColetas(numeros!);

                    await Carregar();
                }

                EstaCarregando = false;
            }
        }
        else
        {
            await Toast
                .Make("Não há coletas pendente de aceite")
                .Show();
        }
    }
}
