using System;
using App.Converters;
using App.Enums;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace App.Models;

public class ColetaPendenteModel
{
    [JsonProperty("_id")]
    public string? Id { get; set; }

    [JsonProperty("numeroPedido")]
    public string? NumeroPedido { get; set; }

    [JsonProperty("numeroCliente1")]
    public string? NumeroCliente1 { get; set; }

    [JsonProperty("pesoTaxado")]
    public int? PesoTaxado { get; set; }

    [JsonProperty("qtdeVolumes")]
    public int? QtdeVolumes { get; set; }

    [JsonProperty("situacao")]
    public string? Situacao { get; set; }

    [JsonProperty("ultimaOcorrencia")]
    public string? UltimaOcorrencia { get; set; }

    [JsonProperty("tipoPedido")]
    public string? TipoPedido { get; set; }

    [JsonProperty("servicoDescricao")]
    public string? ServicoDescricao { get; set; }

    [JsonProperty("baseDestinoNome")]
    public string? BaseDestinoNome { get; set; }

    [JsonProperty("dataPrevista")]
    public DateTime? DataPrevista { get; set; }

    [JsonProperty("destinatarioNome")]
    public string? DestinatarioNome { get; set; }

    [JsonProperty("destinatarioEndereco")]
    public string? DestinatarioEndereco { get; set; }

    [JsonProperty("destinatarioEnderecoNumero")]
    public string? DestinatarioEnderecoNumero { get; set; }

    [JsonProperty("destinatarioComplemento")]
    public string? DestinatarioComplemento { get; set; }

    [JsonProperty("destinatarioBairro")]
    public string? DestinatarioBairro { get; set; }

    [JsonProperty("destinatarioCidade")]
    public string? DestinatarioCidade { get; set; }

    [JsonProperty("destinatarioCEP")]
    public int? DestinatarioCEP { get; set; }

    [JsonProperty("destinatarioPontoReferencia")]
    public string? DestinatarioPontoReferencia { get; set; }

    [JsonProperty("destinatarioTelefone")]
    public string? DestinatarioTelefone { get; set; }

    [JsonProperty("destinatarioCelular")]
    public string? DestinatarioCelular { get; set; }

    [JsonProperty("destinatarioEmail")]
    public string? DestinatarioEmail { get; set; }

    [JsonProperty("destinatarioRecebedor1")]
    public string? DestinatarioRecebedor1 { get; set; }

    [JsonProperty("destinatarioEstado")]
    public string? DestinatarioEstado { get; set; }

    [JsonProperty("destinatarioRazaoSocial")]
    public string? DestinatarioRazaoSocial { get; set; }

    [JsonProperty("destinatarioCNPJ_CPF")]
    public string? DestinatarioCNPJCPF { get; set; }

    [JsonProperty("destinatarioInscricaoEstadual")]
    public string? DestinatarioInscricaoEstadual { get; set; }

    [JsonProperty("destinatarioInscricaoMunicipal")]
    public string? DestinatarioInscricaoMunicipal { get; set; }

    [JsonProperty("destinatarioCodigoIBGE")]
    public string? DestinatarioCodigoIBGE { get; set; }

    [JsonProperty("destinatarioLatitude")]
    [JsonConverter(typeof(DoubleComVirgulaConverter))]
    public double? DestinatarioLatitude { get; set; }

    [JsonProperty("destinatarioLongitude")]
    [JsonConverter(typeof(DoubleComVirgulaConverter))]
    public double? DestinatarioLongitude { get; set; }

    [JsonProperty("remetenteNome")]
    public string? RemetenteNome { get; set; }

    [JsonProperty("remetenteEndereco")]
    public string? RemetenteEndereco { get; set; }

    [JsonProperty("remetenteEnderecoNumero")]
    public string? RemetenteEnderecoNumero { get; set; }

    [JsonProperty("remetenteComplemento")]
    public string? RemetenteComplemento { get; set; }

    [JsonProperty("remetenteBairro")]
    public string? RemetenteBairro { get; set; }

    [JsonProperty("remetenteCidade")]
    public string? RemetenteCidade { get; set; }

    [JsonProperty("remetenteCEP")]
    public int? RemetenteCEP { get; set; }

    [JsonProperty("remetentePontoReferencia")]
    public string? RemetentePontoReferencia { get; set; }

    [JsonProperty("remetenteTelefone")]
    public string? RemetenteTelefone { get; set; }

    [JsonProperty("remetenteCelular")]
    public string? RemetenteCelular { get; set; }

    [JsonProperty("remetenteEmail")]
    public string? RemetenteEmail { get; set; }

    [JsonProperty("remetenteContato")]
    public string? RemetenteContato { get; set; }

    [JsonProperty("remetenteRazaoSocial")]
    public string? RemetenteRazaoSocial { get; set; }

    [JsonProperty("remetenteCNPJ_CPF")]
    public string? RemetenteCNPJCPF { get; set; }

    [JsonProperty("remetenteInscricaoEstadual")]
    public string? RemetenteInscricaoEstadual { get; set; }

    [JsonProperty("remetenteInscricaoMunicipal")]
    public string? RemetenteInscricaoMunicipal { get; set; }

    [JsonProperty("remetenteSite")]
    public string? RemetenteSite { get; set; }

    [JsonProperty("remetenteNumeroCliente")]
    public string? RemetenteNumeroCliente { get; set; }

    [JsonProperty("remetenteEstado")]
    public string? RemetenteEstado { get; set; }

    [JsonProperty("remetenteCodigoIBGE")]
    public string? RemetenteCodigoIBGE { get; set; }

    [JsonProperty("remetenteLatitude")]
    [JsonConverter(typeof(DoubleComVirgulaConverter))]
    public double? RemetenteLatitude { get; set; }

    [JsonProperty("remetenteLongitude")]
    [JsonConverter(typeof(DoubleComVirgulaConverter))]
    public double? RemetenteLongitude { get; set; }

    public string? DestinatarioEnderecoCompleto =>
        $"{DestinatarioEndereco} - {DestinatarioBairro}, {DestinatarioCidade}, {DestinatarioEstado}, CEP: {DestinatarioCEP}{((string.IsNullOrEmpty(DestinatarioPontoReferencia) ? "" : $" - {DestinatarioPontoReferencia}"))}";

    public string? RemetenteEnderecoCompleto =>
        $"{RemetenteEndereco} - {RemetenteBairro}, {RemetenteCidade}, {RemetenteEstado}, CEP: {RemetenteCEP}{((string.IsNullOrEmpty(RemetentePontoReferencia) ? "" : $" - {RemetentePontoReferencia}"))}";

    public ColetaStatusEnum Status =>
        UltimaOcorrencia switch
        {
            "Coleta recusada" => ColetaStatusEnum.Recusada,
            "Coleta aceita" => ColetaStatusEnum.Aceito,
            _ => ColetaStatusEnum.Pendente
        };

    public double? DistanciaKm
    {
        get
        {
            AuthService? authService = MauiProgram.Services.GetService<AuthService>();
            if (authService is null)
                return null;

            var location1 = new Location(authService.Latitude, authService.Longitude);
            var location2 = new Location(DestinatarioLatitude!.Value, DestinatarioLongitude!.Value);

            return Location.CalculateDistance(location1, location2, DistanceUnits.Kilometers);
        }
    }
}
