using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Util
{
  public  class EnviaEmail
    {
        private readonly SmtpClient client;
        public EnviaEmail(string from, string password, string host, int port)
        {
            client = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(from, password),
                Port = port,
                Host = host
            };
        }

        public MailMessage CreateMessage(string subject, string menssage, string from, string to, Attachment attachment)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from, "DCICopasa", Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = menssage;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            if(attachment != null)
            {               
                mail.Attachments.Add(attachment);
            }
            
            return mail;
        }

        public bool Send(MailMessage message)
        {
            try
            {
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
//get /api/crm/cliente/Confirma/statusFaturaPorEmail/{Matricula}/{IdProtocolo} 