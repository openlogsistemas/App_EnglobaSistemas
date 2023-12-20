using System;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels.Componentes;

public partial class TopoPrincipalViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    [ObservableProperty]
    private string? titulo;

    [ObservableProperty]
    private bool exibirVoltar;

    [ObservableProperty]
    private bool exibirMenu;

    public TopoPrincipalViewModel(NavigationService navigationService)
    {
        (_navigationService) = (navigationService);

        ExibirVoltar = false;
        ExibirMenu = true;
    }

    [RelayCommand]
    private void Voltar() => _navigationService.Pop();

    [ObservableProperty]
    private Command? menu;
}