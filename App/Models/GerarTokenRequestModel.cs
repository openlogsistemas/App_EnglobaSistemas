using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Models;

public class GerarTokenRequestModel
{
    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("senha")]
    public string? Senha { get; set; }

    [JsonProperty("ipLocalOrigem")]
    public string IpLocalOrigem { get; set; } = "";

    [JsonProperty("ipRemotoOrigem")]
    public string IpRemotoOrigem { get; set; } = "";

    [JsonProperty("remoteHost")]
    public string RemoteHost { get; set; } = "";
}
