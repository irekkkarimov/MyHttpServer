using System.Net;
using System.Net.Mail;
using MyHttpServer.services;

namespace MyHttpServer;

public class EmailSenderService: IEmailSenderService
{
    public string MailSender { get; private set; }
    public string PasswordSender { get; private set; }
    public string ToEmail { get; private set; }
    public string SmtpServerHost { get; private set; }
    public ushort SmtpServerPort { get; private set; }

    public EmailSenderService(string mailSender,
        string passwordSender,
        string toEmail,
        string smtpServerHost,
        ushort smtpServerPort)
    {
        MailSender = mailSender;
        PasswordSender = passwordSender;
        ToEmail = toEmail;
        SmtpServerHost = smtpServerHost;
        SmtpServerPort = smtpServerPort;
    }
    
    public async Task SendEmailAsync(string city,
        string address,
        string profession,
        string name,
        string lastname,
        string birthday,
        string phone,
        string social)
    {
        var from = new MailAddress(MailSender, "HttpServer");
        var to = new MailAddress(ToEmail);
        var m = new MailMessage(from, to);
        m.Subject = "Тест";
        m.Body = "Письмо-тест 2 работы smtp-клиента";
        var smtp = new SmtpClient(SmtpServerHost, SmtpServerPort);
        smtp.Credentials = new NetworkCredential(MailSender, PasswordSender);
        smtp.EnableSsl = true;
        Console.WriteLine(MailSender + " " + PasswordSender);
        await smtp.SendMailAsync(m);
        Console.WriteLine("Письмо отправлено");
    }
}