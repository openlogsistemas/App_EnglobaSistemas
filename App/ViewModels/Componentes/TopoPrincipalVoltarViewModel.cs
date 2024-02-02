using System;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels.Componentes;

public partial class TopoPrincipalVoltarViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    [ObservableProperty]
    private string? titulo;

    public TopoPrincipalVoltarViewModel(NavigationService navigationService)
    {
        (_navigationService) = (navigationService);
    }

    [RelayCommand]
    private void Voltar() => _navigationService.Pop();
}
