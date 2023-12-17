using System.Net;

namespace TodoList.BLL.Exceptions;

public abstract class AppExceptionBase : Exception
{
    protected AppExceptionBase(string userMessage, string systemMessage = null) : base(userMessage)
    {
        SystemMessage = systemMessage;
    }

    public abstract ResponseStatus Status { get; }

    public string SystemMessage { get; protected set; }
}

public enum ResponseStatus
{
    NotImplemented = -1,

    Success = 1,

    NotFound = 2,

    FailedValidation = 3,

    InvalidOperation = 4,

    BadRequest = 5,

    UnexpectedError = 255
}

public static class ResponseStatusExtensions
{
    public static HttpStatusCode ToHttpStatusCode(this ResponseStatus responseTypeCode)
    {
        var result = responseTypeCode switch
        {
            ResponseStatus.NotImplemented => HttpStatusCode.NotImplemented,
            ResponseStatus.Success => HttpStatusCode.OK,
            ResponseStatus.InvalidOperation => HttpStatusCode.Forbidden,
            ResponseStatus.NotFound => HttpStatusCode.NotFound,
            ResponseStatus.FailedValidation => HttpStatusCode.BadRequest,
            ResponseStatus.BadRequest => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };
        return result;
    }
}