using Football.Common.Domain;

namespace Football.Common.Application.Exceptions;

public sealed class FootballException : Exception
{
    public FootballException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
