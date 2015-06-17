using helloserve.com.Logger.Scribe;
using helloserve.com.Logger.Scribe.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace helloserve.com.Logger.Config
{
    public class LoggerConfig : ConfigurationSection
    {
        [ConfigurationProperty("dumpInterval", IsRequired = false, DefaultValue = 30)]
        public int DumpInterval
        {
            get { return (int)this["dumpInterval"]; }
            set { this["dumpInterval"] = value; }
        }

        [ConfigurationProperty("byteSizeLimit", IsRequired = false, DefaultValue = 5242880)]
        public int ByteSizeLimit
        {
            get { return (int)this["byteSizeLimit"]; }
            set { this["byteSizeLimit"] = value; }
        }

        [ConfigurationProperty("scribes", IsRequired=true)]
        [ConfigurationCollection(typeof(ScribesConfigCollection))]
        public ScribesConfigCollection Scribes
        {
            get { return this["scribes"] as ScribesConfigCollection; }
            set { this["scribes"] = value; }
        }

        public static LoggerConfig GetConfig()
        {
            LoggerConfig config = ConfigurationManager.GetSection(Logger.LoggerConfigName) as LoggerConfig;
            return config;
        }
    }
}
