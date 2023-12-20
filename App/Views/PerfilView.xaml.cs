using App.ViewModels;

namespace App.Views;

public partial class PerfilView : ContentPage
{
    public PerfilView(PerfilViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
