namespace Emi.Common.ResponseModel
{
    public record Response(){}

    public record Response<T>(T? Data, string Message) : Response();
}
