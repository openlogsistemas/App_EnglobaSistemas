using System;
using Newtonsoft.Json;

namespace App.Models;

public class InsucessoModel
{
    [JsonProperty("observacao")]
    public string? Observacao { get; set; }

    [JsonProperty("latitude")]
    public string? Latitude { get; set; }

    [JsonProperty("longitude")]
    public string? Longitude { get; set; }

    [JsonProperty("situacaoOcorrencia")]
    public string? SituacaoOcorrencia { get; set; }
}
