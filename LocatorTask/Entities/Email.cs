using System;

namespace LocatorTask.Entities;
public class Email
{
    public string Addressee { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public Email(string addressee, string subject, string body)
    {
        this.Addressee = addressee;
        this.Body = body;
        this.Subject = subject;
    }
}