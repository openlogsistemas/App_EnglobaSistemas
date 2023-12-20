using System;
using System.Windows.Input;
using App.ViewModels.Componentes;

namespace App.Views.Componentes;

public partial class TopoPesquisar : ContentView
{
    public static readonly BindableProperty TituloProperty = BindableProperty.Create(
        nameof(Titulo), typeof(string), typeof(TopoPesquisar), default(string),
        propertyChanged: OnTituloChanged);

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public static readonly BindableProperty TextoAcaoProperty = BindableProperty.Create(
        nameof(TextoAcao), typeof(string), typeof(TopoPesquisar), default(string),
        propertyChanged: OnTextoAcaoChanged);

    public string TextoAcao
    {
        get => (string)GetValue(TextoAcaoProperty);
        set => SetValue(TextoAcaoProperty, value);
    }

    public delegate void CommandAcaoEventHandler(object sender, EventArgs e);
    public event CommandAcaoEventHandler? CommandAcaoExecuted;

    public delegate void CommandPesquisarEventHandler(object sender, EventArgs e);
    public event CommandPesquisarEventHandler? CommandPesquisarExecuted;



    public TopoPesquisar()
    {
        InitializeComponent();
        BindingContext = MauiProgram.Services.GetService<TopoPesquisarViewModel>();
    }

    private static void OnTituloChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TopoPesquisar)bindable;
        var viewModel = (TopoPesquisarViewModel)control.BindingContext;
        viewModel.Titulo = (string)newValue;
    }

    private static void OnTextoAcaoChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TopoPesquisar)bindable;
        var viewModel = (TopoPesquisarViewModel)control.BindingContext;
        viewModel.TextoAcao = (string)newValue;
    }


    void Pesquisar_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        CommandPesquisarExecuted?.Invoke(this, EventArgs.Empty);
    }

    void Acao_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        CommandAcaoExecuted?.Invoke(this, EventArgs.Empty);
    }
}
