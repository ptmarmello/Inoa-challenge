using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Controller
{
    class MailController
    { 
        public static void MailCall( String MessageType )
        {
            MimeMessage message = new MimeMessage();
            String jsonFile = File.ReadAllText("appsettings.json");
            dynamic infoInJson = JsonConvert.DeserializeObject(jsonFile);
            int port = 0;

            String emailAddress = infoInJson["Smtp"]["Email"].Value.ToString();
            String password = infoInJson["Smtp"]["Password"].Value.ToString() ;
            String toWho = infoInJson["EmailConfiguration"]["toWho"].Value.ToString();
            string smtpClient = infoInJson["Smtp"]["Host"].Value.ToString();
            Int32.TryParse( infoInJson["Smtp"]["Port"].Value , out port );

            message.From.Add(new MailboxAddress("Stock Warning", emailAddress));
            message.To.Add( MailboxAddress.Parse(toWho) );
            
            if ( MessageType == "Sell" )
            {
                message.Subject = "Vende ai amigo";
                message.Body = new TextPart("plain")
                {
                    Text= @"Yes,
                    you can Sell!"
                };
            }
            else
            {
                message.Subject = "Compra ai amigo";
                message.Body = new TextPart("plain")
                {
                    Text= @"Yes,
                         you can Buyyy!"
                };
            }

            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect(smtpClient, port, true);
                
                client.Authenticate(emailAddress, password);
                client.Send(message);
            }
            catch (System.Exception ex)
            {
                
                Console.Write(ex);
                throw;
            }
            finally
            {
                client.Disconnect(true);

                client.Dispose();
            }
        }
    }
}