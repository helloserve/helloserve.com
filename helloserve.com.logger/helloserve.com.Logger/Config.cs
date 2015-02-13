using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ALog.Config
{
    public class LoggerConfig : ConfigurationSection
    {
        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }

        [ConfigurationProperty("tableName")]
        public string TableName
        {
            get { return (string)this["tableName"]; }
            set { this["tableName"] = value; }
        }

        [ConfigurationProperty("dumpInterval", IsRequired = false, DefaultValue = 30)]
        public int DumpInterval
        {
            get { return (int)this["dumpInterval"]; }
            set { this["dumpInterval"] = value; }
        }

        [ConfigurationProperty("byteSizeLimit", IsRequired = false, DefaultValue = 5242880)]
        public long ByteSizeLimit
        {
            get { return (long)this["byteSizeLimit"]; }
            set { this["byteSizeLimit"] = value; }
        }

        public static LoggerConfig GetConfig()
        {
            LoggerConfig config = ConfigurationManager.GetSection("LoggerConfig") as LoggerConfig;
            return config;
        }
    }
}
