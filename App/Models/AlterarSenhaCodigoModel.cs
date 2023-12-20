using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Models;

public class AlterarSenhaCodigoModel
{
    [JsonProperty("codigo")]
    public string? Codigo { get; set; }

    [JsonProperty("senha")]
    public string? Senha { get; set; }

    [JsonProperty("confirmacaoSenha")]
    public string? ConfirmacaoSenha { get; set; }
}