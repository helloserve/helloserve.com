using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Messaging.Config
{
    public class SmtpServer : ConfigurationElement
    {
        [ConfigurationProperty("address", IsRequired = false, DefaultValue = "localhost")]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }

        [ConfigurationProperty("port", IsRequired = false)]
        public int? Port
        {
            get { return (int?)this["port"]; }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("username", IsRequired = false)]
        public string Username
        {
            get { return (string)this["username"]; }
            set { this["username"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = false)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("ssl", IsRequired = false, DefaultValue = false)]
        public bool Ssl
        {
            get { return (bool)this["ssl"]; }
            set { this["ssl"] = value; }
        }
    }

    public class SmtpServers : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SmtpServer();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return base.CreateNewElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as SmtpServer).Address;
        }

        public new SmtpServer this[string server]
        {
            get
            {
                return BaseGet(server) as SmtpServer;
            }
            set
            {
                int index = BaseIndexOf(value);
                if (index > 0)
                {
                    BaseRemoveAt(index);
                    BaseAdd(index, value);
                }
                else
                    BaseAdd(value);
            }
        }
    }

    public class Email : ConfigurationSection
    {
        [ConfigurationProperty("servers", IsRequired = true)]
        [ConfigurationCollection(typeof(SmtpServers))]
        public SmtpServers Servers
        {
            get { return this["servers"] as SmtpServers; }
            set { this["servers"] = value; }
        }
    }
}
