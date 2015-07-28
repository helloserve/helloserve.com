using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Messaging.Interfaces
{
    public interface IEmail
    {
        void SendEmail(MailMessage message);
    }
}
