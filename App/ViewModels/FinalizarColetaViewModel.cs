using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;

namespace App.ViewModels;

public partial class FinalizarColetaViewModel : BaseViewModel
{
	readonly NavigationService _navigationService;
	readonly OrientacaoService _orientacaoService;
	readonly ColetaService _coletaService;

	[ObservableProperty]
	ColetaPendenteModel model;

	[ObservableProperty]
	private string? foto;

	[ObservableProperty]
	private ObservableCollection<IDrawingLine> assinatura;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ExibirAssinatura))]
	private bool exibirFoto;

	public bool ExibirAssinatura => !ExibirFoto;

	public FinalizarColetaViewModel(
		NavigationService navigationService,
		OrientacaoService orientacaoService,
		ColetaService coletaService
	)
	{
		(_navigationService, _orientacaoService, _coletaService) = (
			navigationService,
			orientacaoService,
			coletaService
		);

		Model = (ColetaPendenteModel)App.Request["coleta"]!;

		ExibirFoto = true;
		Assinatura = new();

		_ = TirarFoto();
	}

	[RelayCommand]
	private async Task TirarFoto()
	{
		FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

		if (photo != null)
		{
			using (Stream stream = await photo.OpenReadAsync())
			{
				using (MemoryStream ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					byte[] imageBytes = ms.ToArray();
					Foto = Convert.ToBase64String(imageBytes);
				}
			}
		}
		else
		{
			Voltar();
		}
	}

	[RelayCommand]
	private void SalvarFoto()
	{
		ExibirFoto = false;

		try
		{
			_ = _coletaService.EnviarComprovanteColeta(
				Model.NumeroPedido ?? "",
				"Foto",
				Foto ?? ""
			);
		}
		catch { }

		// _orientacaoService.Landscape();
	}

	[RelayCommand]
	private async Task SalvarAssinatura()
	{
		var resposta = await App.Current?.MainPage?.DisplayAlert(
			"Atenção",
			"Deseja salvar a assinatura digital?",
			"SIM",
			"NÃO"
		)!;

		if (resposta)
		{
			try
			{
				string json = JsonConvert.SerializeObject(Assinatura);
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
				string base64 = Convert.ToBase64String(bytes);

				_ = _coletaService.EnviarComprovanteColeta(
					Model.NumeroPedido ?? "",
					"Assinatura",
					base64
				);

				await _coletaService.ConfirmarColeta([Model.NumeroPedido ?? ""]);

				await Toast.Make(
					"Coleta confirmada com sucesso!"
				).Show();
			}
			catch { }

			_navigationService.Main<InicialView>();
		}
	}

	[RelayCommand]
	public void Limpar() => Assinatura.Clear();

	[RelayCommand]
	private void Voltar() => _navigationService.Pop();
}
