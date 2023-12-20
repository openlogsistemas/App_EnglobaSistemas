using App.Helpers;
using App.Services;
using App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class CodigoSenhaViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly LoginService _loginService;

    public CodigoSenhaViewModel(NavigationService navigationService, LoginService loginService)
    {
        (_navigationService, _loginService) = (navigationService, loginService);
        ExibirSenha = false;
    }

    [ObservableProperty]
    private string? codigo;

    [ObservableProperty]
    private string? senha;

    [ObservableProperty]
    private string? confirmacaoSenha;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Icone))]
    public bool exibirSenha;

    public string Icone => (ExibirSenha) ? IconFont.Eye : IconFont.EyeSlash;

    [RelayCommand]
    private void VisualizarSenha() => ExibirSenha = !ExibirSenha;

    [RelayCommand]
    private void Voltar() => _navigationService.Pop();

    [RelayCommand]
    private async Task Enviar()
    {
        if (Validar())
        {
            try
            {
                EstaCarregando = true;

                var enviar = await _loginService.AlterarSenha(
                    new()
                    {
                        Codigo = Codigo,
                        Senha = Senha,
                        ConfirmacaoSenha = ConfirmacaoSenha,
                    }
                );

                await Toast.Make(enviar?.Mensagem ?? "").Show();

                _navigationService.Main<LoginView>();
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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirErroCodigo))]
    private string? erroCodigo;
    public bool ExibirErroCodigo => !string.IsNullOrEmpty(ErroCodigo ?? string.Empty);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirErroSenha))]
    private string? erroSenha;
    public bool ExibirErroSenha => !string.IsNullOrEmpty(ErroSenha ?? string.Empty);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExibirErroConfirmacaoSenha))]
    private string? erroConfirmacaoSenha;
    public bool ExibirErroConfirmacaoSenha =>
        !string.IsNullOrEmpty(ErroConfirmacaoSenha ?? string.Empty);

    private bool Validar()
    {
        bool valido = true;

        if (string.IsNullOrEmpty(Codigo))
        {
            ErroCodigo = "Informe o código";
            valido = false;
        }
        else
        {
            ErroCodigo = null;
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

        if (string.IsNullOrEmpty(ConfirmacaoSenha))
        {
            ErroConfirmacaoSenha = "Informe a confirmação da senha";
            valido = false;
        }
        else
        {
            if (Senha != ConfirmacaoSenha)
            {
                ErroConfirmacaoSenha = "As senhas não conferem";

                valido = false;
            }
            else
                ErroConfirmacaoSenha = null;
        }

        return valido;
    }
}
