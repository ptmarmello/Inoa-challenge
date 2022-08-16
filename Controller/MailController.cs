using System;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        string smtpClient = infoInJson["Smtp"]["Host"].Value.ToString();
        Int32.TryParse( infoInJson["Smtp"]["Port"].Value , out port );

        message.From.Add(new MailboxAddress("Stock Warning", emailAddress));

        if(infoInJson["EmailConfiguration"]["toWho"].GetType().ToString() == "Newtonsoft.Json.Linq.JArray"){
            foreach (string email in infoInJson["EmailConfiguration"]["toWho"])
            {
                message.To.Add( MailboxAddress.Parse(email) );
            }
        }
        else{
            message.To.Add( MailboxAddress.Parse( infoInJson["EmailConfiguration"]["toWho"].Value ) );
        }

        // Caso queira a mensagem com HTML Body, descomente
        // var builder = new BodyBuilder();


        if ( MessageType == "Sell" )
        {
            message.Subject = "Vende ai amigo";
            
        // Retire esses comentários para criar uma mensagem com HTML  
            // builder.HtmlBody = string.Format(@"<h1>Opa!</h1>
            //     <p>Você pode Vender!</p>
            //     <p>---</p>
            //     <p>Ass.: Stock Warning</p>
            // ");
            // message.Body = builder.ToMessageBody();
            
            // Caso queira só colocar como um Texto simples:
            message.Body = new TextPart("plain")
            {
                Text= @"Yes,
                you can Sell!"
            };
        }
        else
        {
            message.Subject = "Compra ai amigo";


            // Retire esses comentários para criar uma mensagem com HTML  
            // builder.HtmlBody = string.Format(@"<h1>Opa!</h1>
            //     <p>Você pode Vender!</p>
            //     <p>---</p>
            //     <p>Ass.: Stock Warning</p>
            // ");
            // message.Body = builder.ToMessageBody();
            
            // Caso queira só colocar como um Texto simples:
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
            Console.WriteLine("Email Sent!");
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
