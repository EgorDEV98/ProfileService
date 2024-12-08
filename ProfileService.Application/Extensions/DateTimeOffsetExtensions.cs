using FluentAssertions.Extensions;

namespace ProfileService.Application.Extensions;

public static class DateTimeOffsetExtensions
{
    public static DateTimeOffset Truncate(this DateTimeOffset dateTime)
        => dateTime.AddMicroseconds(-dateTime.Microsecond).AddNanoseconds(-dateTime.Nanosecond);
}