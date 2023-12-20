using App.ViewModels;

namespace App.Views;

public partial class LerPackingView : ContentPage
{
    public LerPackingView(LerPackingViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    void TopoPesquisar_CommandPesquisarExecuted(System.Object sender, System.EventArgs e)
    {
        var viewModel = (LerPackingViewModel)BindingContext;

        viewModel.Pesquisar();
    }

    void TopoPesquisar_CommandAcaoExecuted(System.Object sender, System.EventArgs e)
    {
        var viewModel = (LerPackingViewModel)BindingContext;

        _ = viewModel.ColetarTodos();
    }
}
