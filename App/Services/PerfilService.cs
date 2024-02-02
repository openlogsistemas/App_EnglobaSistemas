using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;

namespace App.Services;

public class PerfilService
{
    ApiHelper api;

    public PerfilService(ApiHelper _api) => api = _api;

    public async Task<UsuarioModel?> Usuario() => await api.Get<UsuarioModel>("api/Perfil");

    public async Task<object?> AlterarImagem(string imagem) =>
        await api.PostImageBase64<object>("api/Perfil/AlterarImagem", imagem);

    public async Task<object?> AlterarSenha(
        string senhaAtual,
        string senhaNova,
        string confirmacaoSenhaNova
    ) =>
        await api.Put<object>(
            "api/Perfil/AlterarSenha",
            new
            {
                senhaAtual,
                senhaNova,
                confirmacaoSenhaNova
            }
        );
}
