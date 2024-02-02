using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;
using App.Models;
using CommunityToolkit.Maui.Alerts;

namespace App.ViewModels;

public partial class InsucessoViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly ColetaService _coletaService;
    readonly AuthService _authService;

    public InsucessoViewModel(
        NavigationService navigationService,
        ColetaService coletaService,
        AuthService authService
    )
    {
        (_navigationService, _coletaService, _authService) = (
            navigationService,
            coletaService,
            authService
        );

        ExibirFoto = false;

        Model = (ColetaPendenteModel)App.Request["coleta"]!;

        _ = CarregarOcorrencias();
    }

    [ObservableProperty]
    ColetaPendenteModel model;

    [ObservableProperty]
    private string? foto;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirFormulario))]
    private bool exibirFoto;

    public bool ExibirFormulario => !ExibirFoto;

    [ObservableProperty]
    public ObservableCollection<OcorrenciaModel> ocorrencias = new();

    [ObservableProperty]
    public OcorrenciaModel? ocorrencia;

    [ObservableProperty]
    private string? observacao;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirErroObservacao))]
    private string? erroObservacao;
    public bool ExibirErroObservacao => !string.IsNullOrEmpty(ErroObservacao ?? string.Empty);

    public async Task CarregarOcorrencias()
    {
        try
        {
            EstaCarregando = true;

            var ocorrencias = await _coletaService.SituacoesOcorrencias();

            foreach (var item in ocorrencias!)
            {
                Ocorrencias.Add(item);
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

    [RelayCommand]
    private async Task TirarFoto()
    {
        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo != null)
        {
            using (Stream stream = await photo.OpenReadAsync())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();
                    Foto = Convert.ToBase64String(imageBytes);
                }
            }
        }
        else
        {
            ExibirFoto = false;
        }
    }

    [RelayCommand]
    private void Lancar()
    {
        if (Validar())
        {
            ExibirFoto = true;

            _ = TirarFoto();
        }
    }

    [RelayCommand]
    private async Task Salvar()
    {
        var resposta = await App.Current?.MainPage?.DisplayAlert(
            "Atenção",
            "Deseja salvar?",
            "SIM",
            "NÃO"
        )!;

        if (resposta)
        {
            try
            {
                _ = _coletaService.EnviarComprovanteColeta(
                    Model.NumeroPedido ?? "",
                    "Foto",
                    Foto ?? ""
                );

                await _coletaService.LancarOcorrencia(
                    Model.NumeroPedido ?? "",
                    new InsucessoModel
                    {
                        Observacao = Observacao,
                        Latitude = _authService.Latitude.ToString(),
                        Longitude = _authService.Longitude.ToString(),
                        SituacaoOcorrencia = Ocorrencia?.Id
                    }
                );
            }
            catch { }

            await Toast.Make("Ocorrência lançada com sucesso").Show();

            _navigationService.Main<InicialView>();
            await _navigationService.Push<ColetaListaView>();
        }
    }

    private bool Validar()
    {
        bool valido = true;

        if (string.IsNullOrEmpty(Observacao))
        {
            ErroObservacao = "Informe a observação";
            valido = false;
        }
        else
        {
            ErroObservacao = null;
        }

        return valido;
    }
}
