using App.ViewModels;

namespace App.Views;

public partial class InsucessoView : ContentPage
{
    public InsucessoView(InsucessoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
