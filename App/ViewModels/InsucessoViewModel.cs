using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace App.ViewModels;

public partial class InsucessoViewModel : ObservableObject
{
    readonly NavigationService _navigationService;

    [ObservableProperty]
    private string? foto;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirFormulario))]
    private bool exibirFoto;

    public bool ExibirFormulario => !ExibirFoto;

    public InsucessoViewModel(NavigationService navigationService)
    {
        (_navigationService) = (navigationService);

        ExibirFoto = false;
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
    private void Lancar()
    {
        ExibirFoto = true;
        _ = TirarFoto();
    }


    [RelayCommand]
    private async Task Salvar()
    {
        var resposta = await App.Current?.MainPage?.DisplayAlert("Atenção", "Deseja salvar?", "SIM", "NÃO")!;

        if (resposta)
            _navigationService.Main<InicialView>();
    }

}

