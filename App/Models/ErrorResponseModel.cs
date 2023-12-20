using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Models;

public class ErrorResponseModel
{
    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("detail")]
    public object? Detail { get; set; }

    [JsonProperty("instance")]
    public string? Instance { get; set; }
}