using System;
using App.Helpers;
using App.Services;
using App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class EsqueciViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly LoginService _loginService;

    public EsqueciViewModel(NavigationService navigationService, LoginService loginService) =>
        (_navigationService, _loginService) = (navigationService, loginService);

    [ObservableProperty]
    private string? email;

    [RelayCommand]
    private async Task Enviar()
    {
        if (Validar())
        {
            try
            {
                EstaCarregando = true;

                var enviar = await _loginService.RecuperacaoSenha(Email!);

                await Toast.Make(enviar?.Mensagem ?? "").Show();

                await _navigationService.Push<CodigoSenhaView>();
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }
            finally
            {
                EstaCarregando = false;
            }
        }
    }

    [RelayCommand]
    private void Voltar() => _navigationService.Pop();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirErroEmail))]
    private string? erroEmail;
    public bool ExibirErroEmail => !string.IsNullOrEmpty(ErroEmail ?? string.Empty);

    private bool Validar()
    {
        bool valido = true;

        if (string.IsNullOrEmpty(Email))
        {
            ErroEmail = "Informe o e-mail";
            valido = false;
        }
        else
        {
            ErroEmail = null;
        }

        return valido;
    }
}
