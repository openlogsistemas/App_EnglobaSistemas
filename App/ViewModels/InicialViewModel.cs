using System;
using App.Models;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class InicialViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly AuthService _authService;
    readonly PerfilService _perfilService;

    public InicialViewModel(
        NavigationService navigationService,
        AuthService authService,
        PerfilService perfilService
    )
    {
        (_navigationService, _authService, _perfilService) = (
            navigationService,
            authService,
            perfilService
        );

        _ = CarregarPerfil();
    }

    public async Task CarregarPerfil()
    {
        var perfil = await _perfilService.Usuario();
        if (perfil is not null)
        {
            Nome = perfil.Nome;

            if (!string.IsNullOrEmpty(perfil.ImagemPerfil))
                PerfilFoto = $"data:image/png;base64,{perfil.ImagemPerfil}";
            else
                PerfilFoto = "user";
        }
    }

    [ObservableProperty]
    private string? nome;

    [ObservableProperty]
    private string? perfilFoto;

    [RelayCommand]
    private void IrParaPerfil() => _navigationService.Push<PerfilView>();

    [RelayCommand]
    private void IrParaColetas() => _navigationService.Push<ColetaListaView>();

    [RelayCommand]
    private async Task Sair()
    {
        if (await App.RootPage!.DisplayAlert("Sair", "Deseja realmente sair?", "Sim", "Não"))
            _authService.Deslogar();
    }
}
