namespace TodoList.BLL.Exceptions;

public class BadRequestException : AppExceptionBase
{
    public BadRequestException(string userMessage, string systemMessage = null) : base(userMessage)
    {
        SystemMessage = systemMessage;
    }

    public override ResponseStatus Status => ResponseStatus.BadRequest;
}
