using System;
namespace App.Services;

public class NavigationService
{
	readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public Task Push<T>() where T : Page
    {
        var page = _serviceProvider.GetService<T>();
        return App.Current!.MainPage!.Navigation.PushAsync(page);
    }

    public Task Pop() => App.Current!.MainPage!.Navigation.PopAsync();

    public void Main<T>() where T : Page
    {
        var page = _serviceProvider.GetService<T>();
        App.Current!.MainPage = new NavigationPage(page);
    }
}

