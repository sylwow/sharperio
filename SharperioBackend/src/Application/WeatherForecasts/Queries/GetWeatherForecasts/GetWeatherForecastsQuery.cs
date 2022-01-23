using SharperioBackend.Application.Common.Interfaces;
using MediatR;

namespace SharperioBackend.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>
{
}

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecast>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ICurrentUserService _userService;
    public GetWeatherForecastsQueryHandler(ICurrentUserService userService)
    {
        _userService = userService;
    }
    public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        var user = _userService.UserId;

        var rng = new Random();

        var vm = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });

        return Task.FromResult(vm);
    }
}
