using ProfileService.Application.Extensions;

namespace ProfileService.Application.Common;

public interface IDateTimeProvider
{
    DateTimeOffset GetDateTimeNow();
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset GetDateTimeNow()
        => DateTimeOffset.Now.Truncate();
}