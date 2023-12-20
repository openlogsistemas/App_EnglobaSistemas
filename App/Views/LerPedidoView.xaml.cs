using App.ViewModels;

namespace App.Views;

public partial class LerPedidoView : ContentPage
{
    public LerPedidoView(LerPedidoViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    void TopoPesquisar_CommandPesquisarExecuted(System.Object sender, System.EventArgs e)
    {
        var viewModel = (LerPedidoViewModel)BindingContext;

        viewModel.Pesquisar();
    }
}
