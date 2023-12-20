using System;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class InicialViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    public InicialViewModel(NavigationService navigationService, AuthService authService)
    {
        (_navigationService) = (navigationService);

        Nome = authService.Nome;

        _ = LocationTrackingService.StartTrackingAsync();
    }

    [ObservableProperty]
    private string? nome;

    [RelayCommand]
    private void IrParaPerfil() => _navigationService.Push<PerfilView>();

    [RelayCommand]
    private void IrParaColetas() => _navigationService.Push<ColetaListaView>();

    [RelayCommand]
    private void Sair() => _navigationService.Main<LoginView>();
}
