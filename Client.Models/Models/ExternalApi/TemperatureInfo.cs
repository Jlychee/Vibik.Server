using System.Text.Json.Serialization;

namespace Client.Models.Models.ExternalApi;

public class TemperatureInfo
{
    [JsonPropertyName("temp")] public double Temp { get; set; }
}