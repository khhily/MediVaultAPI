namespace MediVault.Common.Exceptions;

public class AppException : Exception
{
    public AppErrors Code { get; set; }

    public int Status { get; set; }

    public AppException(AppErrors code, string message, Exception innerException) : base(message, innerException)
    {
        Code = code;
    }

    public AppException(AppErrors code) : base(code.ToString())
    {
        Code = code;
    }

    public AppException(string message) : base(message)
    {
    }

    public AppException(AppErrors code, string message) : base(message)
    {
        Code = code;
    }

    public AppException(string message, Exception innerException) : base(message, innerException)
    {
    }
}