using Football.Common.Application.Clock;

namespace Football.Common.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
