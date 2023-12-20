using App.ViewModels;

namespace App.Views;

public partial class ColetaDetalheView : ContentPage
{
    public ColetaDetalheView(ColetaDetalheViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}