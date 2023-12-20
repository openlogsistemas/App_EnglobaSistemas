using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Models;

public class StatusMensagemModel
{
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("mensagem")]
    public string? Mensagem { get; set; }
}
