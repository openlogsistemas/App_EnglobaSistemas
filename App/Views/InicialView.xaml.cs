using App.ViewModels;

namespace App.Views;

public partial class InicialView : FlyoutPage
{
    public InicialView(InicialViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    void TopoPrincipal_CommandMenuExecuted(System.Object sender, System.EventArgs e)
    {
        pagina.IsPresented = !pagina.IsPresented;
    }
}
