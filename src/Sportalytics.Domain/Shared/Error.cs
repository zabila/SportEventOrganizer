namespace Sportalytics.Domain.Shared;

public sealed class Error(string code, string message) : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

    public string Code { get; } = code;
    public string Message { get; } = message;
    public static implicit operator string(Error error) => error.Code;

    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }
}