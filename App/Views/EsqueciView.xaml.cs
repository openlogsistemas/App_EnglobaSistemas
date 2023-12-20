using App.ViewModels;

namespace App.Views;

public partial class EsqueciView : ContentPage
{
	public EsqueciView(EsqueciViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
