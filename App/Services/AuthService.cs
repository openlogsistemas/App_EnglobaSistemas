using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using App.Models;
using App.Views;
using Microsoft.Maui.Storage;

namespace App.Services;

public class AuthService
{
    NavigationService _navigationService;

    public AuthService(NavigationService navigationService) =>
        (_navigationService) = (navigationService);

    public string Token
    {
        get => Preferences.Get("Token", "");
        set => Preferences.Set("Token", value);
    }

    public string Nome
    {
        get => Preferences.Get("Nome", "");
        set => Preferences.Set("Nome", value);
    }

    public string Email
    {
        get => Preferences.Get("Email", "");
        set => Preferences.Set("Email", value);
    }

    public string Id
    {
        get => Preferences.Get("Id", "");
        set => Preferences.Set("Id", value);
    }

    public DateTime Expiracao
    {
        get => Preferences.Get("Expiracao", DateTime.MinValue);
        set => Preferences.Set("Expiracao", value);
    }

    public double Latitude
    {
        get => Preferences.Get("Latitude", 0.0);
        set => Preferences.Set("Latitude", value);
    }

    public double Longitude
    {
        get => Preferences.Get("Longitude", 0.0);
        set => Preferences.Set("Longitude", value);
    }

    public bool EstaLogado() => !string.IsNullOrEmpty(Nome);

    public void Logar(AutenticacaoDados dados)
    {
        Token = dados.AccessToken ?? string.Empty;
        Nome = dados.Nome ?? string.Empty;
        Email = dados.Email ?? string.Empty;
        Id = dados.Id ?? string.Empty;
        Expiracao = dados.DataExpiracao ?? DateTime.MinValue;

        _navigationService.Main<InicialView>();
    }

    public void Deslogar()
    {
        Preferences.Clear();
        LocationTrackingService.StopTracking();
        _navigationService.Main<LoginView>();
    }
}
