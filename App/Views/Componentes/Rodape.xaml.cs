using System.Windows.Input;
using App.ViewModels.Componentes;

namespace App.Views.Componentes;

public partial class Rodape : ContentView
{
    public delegate void CommandOtimizarRoteiroEventHandler(object sender, EventArgs e);
    public event CommandOtimizarRoteiroEventHandler? CommandOtimizarRoteiroExecuted;

    public Rodape()
    {
        InitializeComponent();
        BindingContext = MauiProgram.Services.GetService<RodapeViewModel>();
    }

    void OtimizarRoteiro_Clicked(System.Object sender, System.EventArgs e)
    {
        CommandOtimizarRoteiroExecuted?.Invoke(this, EventArgs.Empty);
    }
}
