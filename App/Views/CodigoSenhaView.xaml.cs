using App.ViewModels;

namespace App.Views;

public partial class CodigoSenhaView : ContentPage
{
    public CodigoSenhaView(CodigoSenhaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
