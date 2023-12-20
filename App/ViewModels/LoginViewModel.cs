using App.Helpers;
using App.Services;
using App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly AuthService _authService;
    readonly LoginService _loginService;

    public LoginViewModel(NavigationService navigationService, AuthService authService, LoginService loginService)
    {
        (_navigationService, _authService, _loginService) = (navigationService, authService, loginService);

        ExibirSenha = true;
    }

    [ObservableProperty]
    private string? email;

    [ObservableProperty]
    private string? senha;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Icone))]
    public bool exibirSenha;

    public string Icone => (ExibirSenha) ? IconFont.Eye : IconFont.EyeSlash;


    [RelayCommand]
    private async Task Entrar()
    {
        if (Validar())
        {
            try
            {
                EstaCarregando = true;

                var login = await _loginService.GerarToken(new()
                {
                    Email = Email,
                    Senha = Senha,
                });

                _authService.Logar(login?.AutenticacaoDados!);
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
    private void IrParaEsqueci() => _navigationService.Push<EsqueciView>();

    [RelayCommand]
    private void VisualizarSenha() => ExibirSenha = !ExibirSenha;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirErroEmail))]
    private string? erroEmail;
    public bool ExibirErroEmail => !string.IsNullOrEmpty(ErroEmail ?? string.Empty);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirErroSenha))]
    private string? erroSenha;
    public bool ExibirErroSenha => !string.IsNullOrEmpty(ErroSenha ?? string.Empty);

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

        if (string.IsNullOrEmpty(Senha))
        {
            ErroSenha = "Informe a senha";
            valido = false;
        }
        else
        {
            ErroSenha = null;
        }

        return valido;
    }

}

