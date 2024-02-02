using System;
using App.Converters;
using App.Enums;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace App.Models;

public class UsuarioModel
{
    [JsonProperty("nome")]
    public string? Nome { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("dataUltimoAcesso")]
    public string? DataUltimoAcesso { get; set; }

    [JsonProperty("imagemPerfil")]
    public string? ImagemPerfil { get; set; }

    [JsonProperty("qtdeAcessos")]
    public int QtdeAcessos { get; set; }
}
