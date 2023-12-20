using System;
using System.Windows.Input;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels.Componentes;

public partial class TopoPesquisarViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    [ObservableProperty]
    private string? titulo;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirAcao))]
    private string? textoAcao;

    public bool ExibirAcao => !string.IsNullOrEmpty(TextoAcao);


    public TopoPesquisarViewModel(NavigationService navigationService) => (_navigationService) = (navigationService);

    [RelayCommand]
    private void Voltar() => _navigationService.Pop();
}

