using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Logger.Scribe.Config
{
    public class ScribesConfigCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return null;
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            ScribeConfigFactory factory = new ScribeConfigFactory();
            return factory.DeserializeElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ScribeConfig).Type;
        }

        protected override string ElementName
        {
            get
            {
                return "scribes";
            }
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
        {
            string attribute = reader.GetAttribute("type");
            ScribeConfigFactory factory = new ScribeConfigFactory();
            ScribeConfig config = factory.DeserializeElement(attribute);
            if (config != null)
            {
                if (elementName == "add")
                {
                    config.FactoryDeserializeElement(reader, false);
                    BaseAdd(config);
                }
                if (elementName == "remove")
                    BaseRemove(GetElementKey(config));
                return true;
            }

            return base.OnDeserializeUnrecognizedElement(elementName, reader);
        }
    }

    public abstract class ScribeConfig : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        public abstract BaseScribe GetScribe();

        internal void FactoryDeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            DeserializeElement(reader, serializeCollectionKey);
        }
    }

    public class ScribeConfigFactory
    {
        public virtual ScribeConfig DeserializeElement(string name)
        {
            if (!name.ToLower().EndsWith("config"))
                name = string.Format("{0}Config", name);

            switch (name.ToLower())
            {
                case "sqlcommandscribeconfig":
                    return new SqlCommandScribeConfig();
                case "filescribeconfig":
                    return new FileScribeConfig();
                default:
                    throw new ArgumentException(string.Format("Could not deserialize type '{0}'", name));
            }
        }
    }

    public class SqlCommandScribeConfig : ScribeConfig
    {
        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }

        public override BaseScribe GetScribe()
        {
            string connectionString = ConnectionString;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionString];
            if (settings != null)
                connectionString = settings.ConnectionString;

            SqlCommandScribe scribe = new SqlCommandScribe(connectionString);

            return scribe;
        }
    }

    public class FileScribeConfig : ScribeConfig
    {
        [ConfigurationProperty("filename", IsRequired = false, DefaultValue = @".\")]
        public string Filename
        {
            get { return (string)this["filename"]; }
            set { this["filename"] = value; }
        }

        [ConfigurationProperty("fileSize", IsRequired = false, DefaultValue = 3145728)]
        public int FileSize
        {
            get { return (int)this["fileSize"]; }
            set { this["fileSize"] = value; }
        }

        [ConfigurationProperty("rotationCount", IsRequired = false, DefaultValue = 10)]
        public int FileRotationCount
        {
            get { return (int)this["rotationCount"]; }
            set { this["rotationCount"] = value; }
        }

        public override BaseScribe GetScribe()
        {
            FileScribe scribe = new FileScribe(Filename)
            {
                MaxFileSize = FileSize,
                RotationCount = FileRotationCount
            };

            return scribe;
        }
    }
}
