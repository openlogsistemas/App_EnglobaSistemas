using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Models;

public class GerarTokenResponseModel
{
	[JsonProperty("autenticacaoDados")]
	public AutenticacaoDados? AutenticacaoDados { get; set; }
}

public class AutenticacaoDados
{
	[JsonProperty("id")]
	public string? Id { get; set; }

	[JsonProperty("nome")]
	public string? Nome { get; set; }

	[JsonProperty("usuario")]
	public object? Usuario { get; set; }

	[JsonProperty("email")]
	public string? Email { get; set; }

	[JsonProperty("accessToken")]
	public string? AccessToken { get; set; }

	[JsonProperty("dataUltimoAcesso")]
	public DateTime? DataUltimoAcesso { get; set; }

	[JsonProperty("dataExpiracao")]
	public DateTime? DataExpiracao { get; set; }

	[JsonProperty("versaoApi")]
	public string? VersaoApi { get; set; }

	[JsonProperty("trocarSenha")]
	public bool TrocarSenha { get; set; }

	[JsonProperty("mensagemErro")]
	public object? MensagemErro { get; set; }
}


