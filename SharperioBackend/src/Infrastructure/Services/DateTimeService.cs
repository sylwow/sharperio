using SharperioBackend.Application.Common.Interfaces;

namespace SharperioBackend.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
