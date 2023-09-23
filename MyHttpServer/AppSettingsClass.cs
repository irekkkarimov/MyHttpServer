namespace MyHttpServer;

public class AppSettingsClass
{
    public string Port { get; set; }
    public string Address { get; set; }
    public string StaticFilesPath { get; set; }

    public AppSettingsClass(string port = "", string address = "", string staticFilesPath = "")
    {
        Port = port;
        Address = address;
        StaticFilesPath = staticFilesPath;
    }
}