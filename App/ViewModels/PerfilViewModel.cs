using System;
using App.Services;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class PerfilViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;
    readonly AuthService _authService;
    readonly PerfilService _perfilService;

    public PerfilViewModel(
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
                Imagem = $"data:image/png;base64,{perfil.ImagemPerfil}";
            else
                Imagem = "user";
        }
    }

    [ObservableProperty]
    private string? nome;

    [ObservableProperty]
    private string? imagem;

    [ObservableProperty]
    private string? base64imagem;

    [ObservableProperty]
    private string? senhaAtual;

    [ObservableProperty]
    private string? senha;

    [ObservableProperty]
    private string? confirmarSenha;

    [RelayCommand]
    public async Task Camera()
    {
        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo != null)
        {
            using (Stream stream = await photo.OpenReadAsync())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();
                    Imagem = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                }
            }
        }
    }

    [RelayCommand]
    public async Task Galeria()
    {
        FileResult photo = await MediaPicker.Default.PickPhotoAsync();

        if (photo != null)
        {
            using (Stream stream = await photo.OpenReadAsync())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();
                    Imagem = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                }
            }
        }
    }

    [RelayCommand]
    public void Excluir()
    {
        Imagem = "user";
    }

    [RelayCommand]
    public async Task Salvar()
    {
        if (PrecisaValidarSenha())
        {
            if (!ValidarSenha())
            {
                await Toast.Make("As senhas não conferem!").Show();
                return;
            }
        }

        EstaCarregando = true;

        try
        {
            if (ValidarSenha())
            {
                await _perfilService.AlterarSenha(
                    SenhaAtual ?? "",
                    Senha ?? "",
                    ConfirmarSenha ?? ""
                );
            }

            await _perfilService.AlterarImagem(Imagem?.Replace("data:image/png;base64,", "") ?? "");

            await Toast.Make("Perfil atualizado com sucesso!").Show();
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }

        EstaCarregando = false;
    }

    private bool PrecisaValidarSenha() =>
        !string.IsNullOrEmpty(SenhaAtual)
        || !string.IsNullOrEmpty(Senha)
        || !string.IsNullOrEmpty(ConfirmarSenha);

    private bool ValidarSenha() =>
        !string.IsNullOrEmpty(SenhaAtual)
        && !string.IsNullOrEmpty(Senha)
        && !string.IsNullOrEmpty(ConfirmarSenha)
        && Senha == ConfirmarSenha;
}
