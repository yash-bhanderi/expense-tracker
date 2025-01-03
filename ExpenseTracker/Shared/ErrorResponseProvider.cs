using System.Net;

namespace CodeCommandos.Shared;

public class ErrorResponseProvider : IErrorResponseProvider
{
    public static readonly ErrorResponseProvider InvalidRequestParameters =
        new("invalid_request_parameters", "One or more parameters invalid",
            "invalid_request_parameters", HttpStatusCode.BadRequest);

    public static readonly ErrorResponseProvider UnhandledException = new("internal_server_error",
        "Something went wrong", "internal_server_error", HttpStatusCode.InternalServerError);

    private static List<ErrorResponseProvider> ErrorResponses { get; } = new();

    public ErrorResponseProvider(
        string? code,
        string? message,
        string internalCode,
        HttpStatusCode httpStatusCode)
    {
        Message = message;
        InternalCode = internalCode ?? string.Empty;
        HttpStatusCode = httpStatusCode;
        Code = code;
        ErrorResponses.Add(this);
    }

    public string? Message { get; private set; }

    public string InternalCode { get; private set; }

    public string? Code { get; private set; }

    public HttpStatusCode HttpStatusCode { get; }

    public ErrorResponseProvider GetErrorResponse(string errorCode)
    {
        return ErrorResponses.First(er =>
            er.InternalCode.ToLowerInvariant().Equals(errorCode.ToLowerInvariant()));
    }
}

public interface IErrorResponseProvider
{
    ErrorResponseProvider GetErrorResponse(string errorCode);
}