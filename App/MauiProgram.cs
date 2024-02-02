using Microsoft.Extensions.Logging;
using App.ViewModels;
using App.ViewModels.Componentes;
using App.Views;
using App.Services;
using CommunityToolkit.Maui;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using DotNet.Meteor.HotReload.Plugin;
using App.Helpers;

namespace App;

public static class MauiProgram
{
    private static MauiApp? _mauiApp;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
#if DEBUG
            .EnableHotReload()
#endif
            .UseMauiCommunityToolkit()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Inter-Black.ttf", "Black");
                fonts.AddFont("Inter-Bold.ttf", "Bold");
                fonts.AddFont("Inter-ExtraBold.ttf", "ExtraBold");
                fonts.AddFont("Inter-ExtraLight.ttf", "ExtraLight");
                fonts.AddFont("Inter-Light.ttf", "Light");
                fonts.AddFont("Inter-Medium.ttf", "Medium");
                fonts.AddFont("Inter-Regular.ttf", "Regular");
                fonts.AddFont("Inter-SemiBold.ttf", "SemiBold");
                fonts.AddFont("Inter-Thin.ttf", "Thin");
                fonts.AddFont("fa-regular-400.ttf", "Icon");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        //View Models
        builder.Services.AddTransient<ColetaDetalheViewModel>();
        builder.Services.AddTransient<ColetaListaViewModel>();
        builder.Services.AddTransient<EsqueciViewModel>();
        builder.Services.AddTransient<FinalizarColetaViewModel>();
        builder.Services.AddTransient<InicialViewModel>();
        builder.Services.AddTransient<InsucessoViewModel>();
        builder.Services.AddTransient<LerPackingViewModel>();
        builder.Services.AddTransient<LerPedidoViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<PerfilViewModel>();
        builder.Services.AddTransient<RoteiroViewModel>();
        builder.Services.AddTransient<QrCodeViewModel>();
        builder.Services.AddTransient<CodigoSenhaViewModel>();

        //View Models Componentes
        builder.Services.AddTransient<TopoPrincipalViewModel>();
        builder.Services.AddTransient<TopoPrincipalVoltarViewModel>();
        builder.Services.AddTransient<TopoPesquisarViewModel>();
        builder.Services.AddTransient<RodapeViewModel>();

        // Views
        builder.Services.AddTransient<ColetaDetalheView>();
        builder.Services.AddTransient<ColetaListaView>();
        builder.Services.AddTransient<EsqueciView>();
        builder.Services.AddTransient<FinalizarColetaView>();
        builder.Services.AddTransient<InicialView>();
        builder.Services.AddTransient<InsucessoView>();
        builder.Services.AddTransient<LerPackingView>();
        builder.Services.AddTransient<LerPedidoView>();
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<PerfilView>();
        builder.Services.AddTransient<RoteiroView>();
        builder.Services.AddTransient<QrCodeView>();
        builder.Services.AddTransient<CodigoSenhaView>();

        // Helpers
        builder.Services.AddSingleton<ApiHelper>();

        // Services
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<OrientacaoService>();
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<ColetaService>();
        builder.Services.AddSingleton<LoginService>();
        builder.Services.AddSingleton<PerfilService>();

        _mauiApp = builder.Build();

        return _mauiApp;
    }

    public static IServiceProvider Services =>
        _mauiApp?.Services
        ?? throw new InvalidOperationException("O aplicativo não foi inicializado corretamente.");
}
