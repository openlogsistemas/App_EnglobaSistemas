using System;
using System.Collections.ObjectModel;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;

namespace App.ViewModels;

public partial class FinalizarColetaViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly OrientacaoService _orientacaoService;

    [ObservableProperty]
    private string? foto;

    [ObservableProperty]
    private ObservableCollection<Line> assinatura;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirAssinatura))]
    private bool exibirFoto;


    public bool ExibirAssinatura => !ExibirFoto;


    public FinalizarColetaViewModel(NavigationService navigationService, OrientacaoService orientacaoService)
    {
        (_navigationService, _orientacaoService) = (navigationService, orientacaoService);

        ExibirFoto = true;
        Assinatura = new();

        _ = TirarFoto();
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
    }

    [RelayCommand]
    private void SalvarFoto()
    {
        ExibirFoto = false;
        _orientacaoService.Landscape();
    }


    [RelayCommand]
    private async Task SalvarAssinatura()
    {
        var resposta = await App.Current?.MainPage?.DisplayAlert("Atenção", "Deseja salvar a assinatura digital?", "SIM", "NÃO")!;

        if (resposta)
            _navigationService.Main<InicialView>();
    }

    [RelayCommand]
    private void Limpar() => Assinatura = new ObservableCollection<Line>();

    [RelayCommand]
    private void Voltar() => _navigationService.Pop();
}