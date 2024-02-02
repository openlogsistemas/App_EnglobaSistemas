using System;
using Newtonsoft.Json;

namespace App.Models;

public class OcorrenciaModel
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("descricao")]
    public string? Descricao { get; set; }
}
