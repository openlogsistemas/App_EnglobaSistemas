using App.Services;
using App.Views;

namespace App;

public partial class App : Application
{
    public static Page? RootPage;
    public static object? Param;

    public App(LoginView loginView, InicialView inicialView, AuthService authService)
    {
        InitializeComponent();

        if (authService.EstaLogado())
            MainPage = new NavigationPage(inicialView);
        else
            MainPage = new NavigationPage(loginView);

        RootPage = MainPage;
    }
}
