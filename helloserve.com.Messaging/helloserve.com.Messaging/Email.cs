using helloserve.com.Messaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Messaging
{
    public class Email : IEmail
    {
        private static Action<System.Net.Mail.MailMessage> _sendEmail;
        public static void SetSendEmail(Action<System.Net.Mail.MailMessage> sendEmail)
        {
            _sendEmail = sendEmail;
        }

        private Config.SmtpServer _server;

        public Email(string smtpServer, bool smtpSsl, int? port = null, string smtpUsername = null, string smtpPassword = null)
        {
            _server = new Config.SmtpServer()
            {
                Address = smtpServer,
                Port = port,
                Username = smtpUsername,
                Password = smtpPassword,
                Ssl = smtpSsl
            };
        }

        public Email(string smtpServer, string configSection = "messaging")
        {
            Config.Email config = ConfigurationManager.GetSection(configSection) as Config.Email;
            _server = config.SmtpServers[smtpServer];
        }

        public void SendEmail(System.Net.Mail.MailMessage message)
        {
            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
            {
                if (string.IsNullOrEmpty(_server.Address) && _sendEmail != null)
                {
                    _sendEmail(message);
                };

                if (_server.Address.ToLower() != "localhost")
                {
                    client.Host = _server.Address;
                    if (_server.Port.HasValue)
                        client.Port = _server.Port.Value;

                    string username = _server.Username;
                    if (!string.IsNullOrEmpty(username))
                        client.Credentials = new System.Net.NetworkCredential(username, _server.Password);
                }

                client.EnableSsl = _server.Ssl;
                client.Send(message);
            }

            message.Dispose();
        }

    }
}
