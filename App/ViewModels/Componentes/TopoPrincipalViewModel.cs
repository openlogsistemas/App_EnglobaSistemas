using System;
using App.Services;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels.Componentes;

public partial class TopoPrincipalViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    [ObservableProperty]
    private string? titulo;

    public TopoPrincipalViewModel(NavigationService navigationService)
    {
        (_navigationService) = (navigationService);
    }

    [ObservableProperty]
    private Command? menu;
}
