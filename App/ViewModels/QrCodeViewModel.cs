using System;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZXing.Net.Maui;

namespace App.ViewModels;

public partial class QrCodeViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    public QrCodeViewModel(NavigationService navigationService)
    {
        (_navigationService) = (navigationService);

        Options = new() { AutoRotate = true, Formats = BarcodeFormat.QrCode, };
    }

    [ObservableProperty]
    private BarcodeReaderOptions options;

    [RelayCommand]
    private void Voltar() => _navigationService.PopModal();

    public void LerQRCode(string resultado)
    {
        App.Request["qrcode"] = resultado;
        Voltar();
    }
}
