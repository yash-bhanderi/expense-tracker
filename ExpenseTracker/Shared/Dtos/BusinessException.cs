using System.Diagnostics.CodeAnalysis;

namespace CodeCommandos.Shared.Dtos;

[ExcludeFromCodeCoverage]
public class BusinessException : Exception
{
    public BusinessException()
    {
    }

    public BusinessException(string code) => this.Code = code;

    public BusinessException(string errorMessage, bool shouldLogError)
        : base(errorMessage)
    {
        this.ShouldLogError = shouldLogError;
    }

    public BusinessException(string errorMessage, string code, bool shouldLogError)
        : base(errorMessage)
    {
        this.Code = code;
        this.ShouldLogError = shouldLogError;
    }

    public BusinessException(string code, object data)
    {
        this.Code = code;
        this.ErrorData = data;
    }

    public string Code { get; set; }

    public object ErrorData { get; }

    public bool ShouldLogError { get; }
}