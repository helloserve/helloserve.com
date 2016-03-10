using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Feature.Config
{
    public class FeatureElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("isEnabled", IsRequired = false, DefaultValue = true)]
        public bool IsEnabled
        {
            get { return (bool)this["isEnabled"]; }
            set { this["isEnabled"] = value; }
        }
    }

    [ConfigurationCollection(typeof(FeatureElement))]
    public class FeatureCollection : ConfigurationElementCollection
    {
        public FeatureElement this[int index]
        {
            get { return BaseGet(index) as FeatureElement; }
        }

        public new FeatureElement this[string name]
        {
            get { return BaseGet(name) as FeatureElement; }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            FeatureElement feature = element as FeatureElement;

            if (feature == null)
                return null;

            return feature.Name;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FeatureElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new FeatureElement() { Name = elementName };
        }
    }

    public class FeatureSection : ConfigurationSection
    {
        [ConfigurationProperty("features", IsRequired = true)]
        public FeatureCollection Features
        {
            get { return this["features"] as FeatureCollection; }
            set { this["features"] = value; }
        }

        public static FeatureSection GetConfig(string name = null)
        {
            FeatureSection section = ConfigurationManager.GetSection(name ?? "features") as FeatureSection;
            return section;
        }
    }
}
