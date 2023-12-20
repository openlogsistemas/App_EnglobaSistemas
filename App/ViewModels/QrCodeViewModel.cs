using System;
using App.Services;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class QrCodeViewModel : BaseViewModel
{
    readonly NavigationService _navigationService;

    public QrCodeViewModel(NavigationService navigationService)
    {
        (_navigationService) = (navigationService);
    }

    [RelayCommand]
    private void Voltar() => _navigationService.Pop();
}

