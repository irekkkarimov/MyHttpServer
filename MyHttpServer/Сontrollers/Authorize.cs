using System.Net;
using System.Web;
using MyHttpServer.Model;
using MyHttpServer.utils;
using MyHttpServer.Attributes;

namespace MyHttpServer.Handlers.controllers;

[Controller("Authorize")]
public class Authorize
{

    [Post("SendToEmail")]
    public static async void SendToEmail(string city,
        string address,
        string profession,
        string name,
        string lastname,
        string birthday,
        string phone,
        string email)
    {
        var serverData = ServerData.Instance();
        var emailSenderService = new EmailSenderService(serverData.AppSettings.Configuration.MailSender,
            serverData.AppSettings.Configuration.PasswordSender,
            serverData.AppSettings.Configuration.SmtpServerHost,
            serverData.AppSettings.Configuration.SmtpServerPort);
        
        emailSenderService.SendEmailAsync(city, address, profession, name, lastname, birthday, phone, email);
        
    }

    [Get("GetEmailList")]
    public static string GetEmailList()
    {
        Console.WriteLine("passed");
        var htmlCode = "<html><body><h1>Вы вызвали GetEmailList</h1</body></hml>";
        return htmlCode;
    }
    
    [Get("GetAccountsList")]
    public static Account[] GetAccountsList()
    {
        var accounts = new Account[]
        {
            new() { Email = "email-1", Password = "password-1" },
            new() { Email = "email-1", Password = "password-1" }
        };
        
        return accounts;
    }
}