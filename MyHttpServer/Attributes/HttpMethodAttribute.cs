namespace MyHttpServer.Attributes;

public interface IHttpMethodAttribute
{
    public string ActionName { get; set; }
}