using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.ViewModels;

public partial class BaseViewModel : ObservableObject
{
	[ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NaoEstaCarregando))]
	private bool estaCarregando;

    public bool NaoEstaCarregando => !EstaCarregando;
}

