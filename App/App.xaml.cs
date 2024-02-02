using App.Services;
using App.Views;

namespace App;

public partial class App : Application
{
    public static App? Aplicacao;

    public static Page? RootPage => Aplicacao?.MainPage;

    public static Dictionary<string, object> Request = new Dictionary<string, object>();

    public App(LoginView loginView, InicialView inicialView, AuthService authService)
    {
        InitializeComponent();

        _ = LocationTrackingService.StartTrackingAsync();

        if (authService.EstaLogado())
            MainPage = new NavigationPage(inicialView);
        else
            MainPage = new NavigationPage(loginView);

        Aplicacao = this;
    }
}
