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

    public async Task<List<ColetaPendenteModel>?> Pendentes() =>
        await api.Get<List<ColetaPendenteModel>>("api/tms/operacoes/Coletas/Pendentes");

    public async Task<List<ColetaPendenteModel>?> AceitarColetas(List<string> coletas) =>
        await api.Put<List<ColetaPendenteModel>>(
            "api/tms/operacoes/Coletas/AceitarColetas",
            coletas
        );

    public async Task<List<ColetaPendenteModel>?> RecusarColetas(List<string> coletas) =>
        await api.Put<List<ColetaPendenteModel>>(
            "api/tms/operacoes/Coletas/RecusarColetas",
            coletas
        );
}
