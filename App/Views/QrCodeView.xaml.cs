using App.Services;
using App.ViewModels;

namespace App.Views;

public partial class QrCodeView : ContentPage
{
    readonly NavigationService _navigationService;

    public QrCodeView(QrCodeViewModel viewModel, NavigationService navigationService)
    {
        (_navigationService) = (navigationService);

        InitializeComponent();

        BindingContext = viewModel;
    }

    void cameraBarcodeReaderView_BarcodesDetected(
        System.Object sender,
        ZXing.Net.Maui.BarcodeDetectionEventArgs e
    )
    {
        if (e.Results.Any())
        {
            var viewModel = (QrCodeViewModel)this.BindingContext;
            viewModel.LerQRCode(e.Results.FirstOrDefault()!.Value);
        }
    }
}
