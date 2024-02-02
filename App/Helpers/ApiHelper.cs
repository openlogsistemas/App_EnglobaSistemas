using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using App.Models;
using App.Services;
using Newtonsoft.Json;

namespace App.Helpers;

public class ApiHelper
{
    private readonly AuthService authService;

    public ApiHelper(AuthService _authService) => authService = _authService;

    public string Url = "https://dev.englobasistemas.com.br/v1/";

    public async Task<T?> Get<T>(string url) => await Send<T>(HttpMethod.Get, url);

    public async Task<T?> Post<T>(string url, object objeto) =>
        await Send<T>(HttpMethod.Post, url, objeto);

    public async Task<T?> Put<T>(string url, object objeto) =>
        await Send<T>(HttpMethod.Put, url, objeto);

    public async Task<T?> Delete<T>(string url) => await Send<T>(HttpMethod.Delete, url);

    public async Task<T?> Send<T>(HttpMethod metodo, string url, object? objeto = null)
    {
        HttpClient client = new() { BaseAddress = new Uri(Url) };

        if (authService.EstaLogado())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authService.Token}");
        }

        var requestMessage = new HttpRequestMessage(metodo, url);

        if (metodo == HttpMethod.Post || metodo == HttpMethod.Put)
        {
            var json = JsonConvert.SerializeObject(objeto);

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response = await client.SendAsync(requestMessage);

        string data = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            try
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
                catch
                {
                    return (T)Convert.ChangeType(data, typeof(T));
                }
            }
            catch
            {
                throw new Exception(data);
            }
        }
        else
        {
            string mensagem = "";
            try
            {
                mensagem = JsonConvert.DeserializeObject<ErrorResponseModel>(data)?.Message ?? data;
            }
            catch
            {
                throw new Exception(data);
            }

            throw new Exception(mensagem);
        }
    }

    public static async Task<T?> SendExtern<T>(HttpMethod metodo, string url, object? objeto = null)
    {
        Console.WriteLine(url);

        HttpClient client = new() { BaseAddress = new Uri(url) };

        var requestMessage = new HttpRequestMessage(metodo, "");

        if (metodo == HttpMethod.Post || metodo == HttpMethod.Put)
        {
            var json = JsonConvert.SerializeObject(objeto);

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response = await client.SendAsync(requestMessage);

        string data = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            try
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
                catch
                {
                    return (T)Convert.ChangeType(data, typeof(T));
                }
            }
            catch
            {
                throw new Exception(data);
            }
        }
        else
        {
            throw new Exception(data);
        }
    }

    public async Task<T?> PostImageBase64<T>(
        string url,
        string base64Image,
        string fileName = "imagem.jpg"
    )
    {
        HttpClient client = new() { BaseAddress = new Uri(Url) };

        if (authService.EstaLogado())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authService.Token}");
        }

        byte[] fileBytes = Convert.FromBase64String(base64Image);

        using var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(fileContent, "arquivo", fileName);

        var response = await client.PostAsync(url, content);
        var data = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        else
        {
            var mensagem = JsonConvert.DeserializeObject<ErrorResponseModel>(data)?.Message ?? data;
            throw new Exception(mensagem);
        }
    }
}
