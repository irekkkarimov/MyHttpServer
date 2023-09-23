namespace MyHttpServer;

public class ServerHandlerClass
{
    public delegate void ServerHandler(string message);
    public event ServerHandler CloseServer;

    public ServerHandlerClass()
    {
        
    }
}