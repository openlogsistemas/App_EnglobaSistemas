using App.Services;
using App.ViewModels;

namespace App.Views;

public partial class FinalizarColetaView : ContentPage
{
    public FinalizarColetaView(FinalizarColetaViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
