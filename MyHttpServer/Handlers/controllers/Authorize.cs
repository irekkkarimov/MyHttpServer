using System.Net;
using System.Web;
using MyHttpServer.utils;

namespace MyHttpServer.Handlers.controllers;

[Controller("/Authorize")]
public class Authorize
{

    public static async void SendToEmail(HttpListenerContext context)
    {
        var serverData = ServerData.Instance();
        var email = new EmailSenderService(serverData.AppSettings.Configuration.MailSender,
            serverData.AppSettings.Configuration.PasswordSender,
            serverData.AppSettings.Configuration.SmtpServerHost,
            serverData.AppSettings.Configuration.SmtpServerPort);
        
        var request = context.Request;
        var response = context.Response;
        var stream = new StreamReader(request.InputStream);
        var streamRead = stream.ReadToEnd();
        string decodedData = HttpUtility.UrlDecode(streamRead, System.Text.Encoding.UTF8);
        Console.WriteLine(decodedData);
        string[] str = decodedData.Split("&");

        Console.WriteLine(str.Length);
        email.SendEmailAsync(str[0],str[1],str[2],str[4],str[5],str[6],str[7],str[8]);
        
        byte[] buffer = File.ReadAllBytes(serverData.StaticFolder + "/index.html");
        response.ContentType = "text/html; charset=utf-8";
        response.ContentLength64 = buffer.Length;
        await using Stream output = response.OutputStream;
            
        await output.WriteAsync(buffer);
        await output.FlushAsync();
    } 
}

public class ControllerAttribute : Attribute
{
    public string Name { get; private set; }
    
    public ControllerAttribute(string name)
    {
        Name = name;
    }
}