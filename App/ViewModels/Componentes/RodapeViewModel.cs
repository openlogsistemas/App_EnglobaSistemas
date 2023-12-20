using System;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels.Componentes;

public partial class RodapeViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    public RodapeViewModel(NavigationService navigationService) =>
        (_navigationService) = (navigationService);

    [RelayCommand]
    private void AbrirMapa() => _navigationService.Push<RoteiroView>();

    [ObservableProperty]
    private Command? otimizarRoteiro;
}
