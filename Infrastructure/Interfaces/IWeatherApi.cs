using Client.Models.Models.Common.Results;
using Client.Models.Models.ExternalApi;

namespace Infrastructure.Interfaces;

public interface IWeatherApi
{
    Task<Result<WeatherInfo>> GetCurrentWeatherAsync(CancellationToken ct = default);
}