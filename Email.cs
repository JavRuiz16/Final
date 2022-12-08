using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
namespace Final;
internal class Final
{
    private static string serviceConnectionString;

    static async Task Main(string[] args)
    {
  
        EmailClient emailClient = new EmailClient(serviceConnectionString);
        var subject = "Package Management System";
        var emailContent = new EmailContent(subject);
        // use Multiline String @ to design html content
        emailContent.Html= @"
                    <html>
                        <body>
                            <h1 style=color:red>Package Notificaiton System</h1>
                            <h4>This is a HTML content</h4>
                            <p>Your package has arrived!!</p>
                        </body>
                    </html>";


  
        var sender = "Domain";

        Console.WriteLine("jrruiz1@buffs.wtamu.edu: ");
        string inputEmail = Console.ReadLine();
        var emailRecipients = new EmailRecipients(new List<EmailAddress> {
            new EmailAddress(inputEmail) { DisplayName = "package Notificaiotn" },
        });

        var emailMessage = new EmailMessage(sender, emailContent, emailRecipients);

        try
        {
            SendEmailResult sendEmailResult = emailClient.Send(emailMessage);

            string messageId = sendEmailResult.MessageId;
            if (!string.IsNullOrEmpty(messageId))
            {
                Console.WriteLine($"Email sent, MessageId = {messageId}");
            }
            else
            {
                Console.WriteLine($"Failed to send email.");
                return;
            }

            var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            do
            {
                SendStatusResult sendStatus = emailClient.GetSendStatus(messageId);
                Console.WriteLine($"Send mail status for MessageId : <{messageId}>, Status: [{sendStatus.Status}]");

                if (sendStatus.Status != SendStatus.Queued)
                {
                    break;
                }
                await Task.Delay(TimeSpan.FromSeconds(10));
               
            } while (!cancellationToken.IsCancellationRequested);

            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"Looks like we timed out for email");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in sending email, {ex}");
        }
    }
}



 
