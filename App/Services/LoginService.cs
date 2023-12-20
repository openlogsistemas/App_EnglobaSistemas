using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;

namespace App.Services;

public class LoginService
{
    ApiHelper api;

    public LoginService(ApiHelper _api) => api = _api;

    public async Task<GerarTokenResponseModel?> GerarToken(GerarTokenRequestModel model) =>
        await api.Post<GerarTokenResponseModel>("api/autenticacao/token/GerarToken", model);

    public async Task<GerarTokenResponseModel?> AtualizarToken(GerarTokenRequestModel model) =>
        await api.Post<GerarTokenResponseModel>("api/autenticacao/token/AtualizarToken", model);

    public async Task<StatusMensagemModel?> RecuperacaoSenha(string email) =>
        await api.Post<StatusMensagemModel>(
            $"api/autenticacao/token/RecuperacaoSenha?emailDestinatario={email}",
            new()
        );

    public async Task<StatusMensagemModel?> AlterarSenha(AlterarSenhaCodigoModel model) =>
        await api.Post<StatusMensagemModel>("api/autenticacao/token/AlterarSenha", model);
}
