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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            string? valor = App.Request["qrcode"]?.ToString();

            if (!string.IsNullOrEmpty(valor))
            {
                var viewModel = (LerPedidoViewModel)BindingContext;
                viewModel.NumeroPedido = valor;
                _ = viewModel.PesquisarNumeroPedidoAsync();
            }
        }
        catch { }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        App.Request["qrcode"] = "";
    }
}
