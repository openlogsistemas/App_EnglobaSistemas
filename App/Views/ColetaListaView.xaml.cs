using App.ViewModels;

namespace App.Views;

public partial class ColetaListaView : ContentPage
{
    public ColetaListaView(ColetaListaViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    void TopoPesquisar_CommandPesquisarExecuted(System.Object sender, System.EventArgs e)
    {
        var viewModel = (ColetaListaViewModel)BindingContext;

        viewModel.Pesquisar();
    }

    void TopoPesquisar_CommandAcaoExecuted(System.Object sender, System.EventArgs e)
    {
        var viewModel = (ColetaListaViewModel)BindingContext;

        _ = viewModel.AceitarTodos();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            bool recarregar = (bool)(App.Param ?? false);
            if (recarregar)
            {
                var viewModel = (ColetaListaViewModel)BindingContext;
                _ = viewModel.Carregar();
            }
        }
        catch { }
    }
}
