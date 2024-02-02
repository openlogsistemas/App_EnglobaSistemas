using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;

namespace App.Services;

public class ColetaService
{
    ApiHelper api;

    public ColetaService(ApiHelper _api) => api = _api;

    public async Task<IEnumerable<ColetaPendenteModel>?> Pendentes() =>
        await api.Get<IEnumerable<ColetaPendenteModel>>("api/tms/operacoes/Coletas/Pendentes");

    public async Task<IEnumerable<ColetaPendenteModel>?> AceitarColetas(List<string> coletas) =>
        await api.Put<IEnumerable<ColetaPendenteModel>>(
            "api/tms/operacoes/Coletas/AceitarColetas",
            coletas
        );

    public async Task<IEnumerable<ColetaPendenteModel>?> RecusarColetas(List<string> coletas) =>
        await api.Put<IEnumerable<ColetaPendenteModel>>(
            "api/tms/operacoes/Coletas/RecusarColetas",
            coletas
        );

    public async Task<IEnumerable<ColetaPendenteModel>?> ConfirmarColeta(List<string> coletas) =>
        await api.Put<IEnumerable<ColetaPendenteModel>>(
            "api/tms/operacoes/Coletas/ConfirmarColeta",
            coletas
        );

    public async Task<IEnumerable<ColetaPendenteModel>?> PedidosPendentes(string numero) =>
        await api.Get<IEnumerable<ColetaPendenteModel>>(
            $"api/tms/operacoes/Coletas/PedidosPendentesColeta?numero={numero}"
        );

    public async Task<object?> EnviarComprovanteColeta(string numero, string tipo, string imagem) =>
        await api.Post<object>(
            $"api/tms/operacoes/Coletas/{numero}/EnviarComprovanteColeta/{tipo}",
            new string[] { imagem }
        );

    public async Task<IEnumerable<OcorrenciaModel>?> SituacoesOcorrencias() =>
        await api.Get<IEnumerable<OcorrenciaModel>>(
            "api/tms/operacoes/Coletas/Dropdown/SituacoesOcorrencias"
        );

    public async Task<object?> LancarOcorrencia(string numero, InsucessoModel model) =>
        await api.Post<object>($"api/tms/operacoes/Coletas/{numero}/LancarOcorrencia", model);
}
