using App.ViewModels;

namespace App.Views;

public partial class QrCodeView : ContentPage
{
    public QrCodeView(QrCodeViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    void cameraBarcodeReaderView_BarcodesDetected(
        System.Object sender,
        ZXing.Net.Maui.BarcodeDetectionEventArgs e
    )
    {
        var a = sender;
    }
}
