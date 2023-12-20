﻿using App.ViewModels;

namespace App.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
