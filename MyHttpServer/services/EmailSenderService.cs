using System.Net;
using System.Net.Mail;
using MyHttpServer.services;

namespace MyHttpServer;

public class EmailSenderService: IEmailSenderService
{
    public string MailSender { get; private set; }
    public string PasswordSender { get; private set; }
    public string SmtpServerHost { get; private set; }
    public ushort SmtpServerPort { get; private set; }

    public EmailSenderService(string mailSender,
        string passwordSender,
        string smtpServerHost,
        ushort smtpServerPort)
    {
        MailSender = mailSender;
        PasswordSender = passwordSender;
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
        string email)
    {
        var from = new MailAddress(MailSender, "Dodo Pizza HR");
        var to = new MailAddress(email.Split("=")[1]);
        var m = new MailMessage(from, to);
        m.Subject = "Анкета";
        m.Body = $"Почта: {email.Split("=")[1]}\n" +
                 $"Имя: {name.Split("=")[1]}\n" +
                 $"Фамилия: {lastname.Split("=")[1]}\n" +
                 $"Город: {city.Split("=")[1]}\n" +
                 $"Адрес: {address.Split("=")[1]}\n" +
                 $"Профессия: {profession.Split("=")[1]}\n" +
                 $"День рождения: {birthday.Split("=")[1]}\n" +
                 $"Номер телефона: {phone.Split("=")[1]}\n";
        // m.Attachments.Add(new Attachment("../../../MyHttpServer(Irek).rar"));
        var smtp = new SmtpClient(SmtpServerHost, SmtpServerPort);
        smtp.Credentials = new NetworkCredential(MailSender, PasswordSender);
        smtp.EnableSsl = true;
        Console.WriteLine(MailSender + " " + PasswordSender);
        await smtp.SendMailAsync(m);
        Console.WriteLine("Письмо отправлено");
    }
}