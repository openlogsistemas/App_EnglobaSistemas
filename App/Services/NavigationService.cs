using System;

namespace App.Services;

public class NavigationService
{
    readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    public Page RootPage => App.RootPage!;
    public Page MainPage => App.Current!.MainPage!;

    public Task Push<T>()
        where T : Page
    {
        var page = _serviceProvider.GetService<T>();
        return MainPage.Navigation.PushAsync(page);
    }

    public Task PushModal<T>()
        where T : Page
    {
        var page = _serviceProvider.GetService<T>();
        return MainPage.Navigation.PushModalAsync(page);
    }

    public Task Pop() => RootPage.Navigation.PopAsync();

    public Task PopModal() => RootPage.Navigation.PopModalAsync();

    public void Main<T>()
        where T : Page
    {
        var page = _serviceProvider.GetService<T>();
        App.Current!.MainPage = new NavigationPage(page);
    }
}
