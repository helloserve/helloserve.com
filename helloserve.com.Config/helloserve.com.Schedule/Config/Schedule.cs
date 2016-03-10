using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Schedule.Config
{
    public class ScheduleRepeatElement : ConfigurationElement
    {
        [ConfigurationProperty("occurance", IsRequired = false)]
        public int Occurance
        {
            get { return (int)this["occurance"]; }
            set { this["occurance"] = value; }
        }
    }

    public class ScheduleStartElement : ConfigurationElement
    {
        [ConfigurationProperty("time", IsRequired = false)]
        public TimeSpan Time
        {
            get { return (TimeSpan)this["time"]; }
            set { this["time"] = value; }
        }
    }

    public class ScheduleDaysOfWeekElement : ConfigurationElement
    {
        [ConfigurationProperty("sunday", IsRequired = false)]
        public bool Sunday
        {
            get { return (bool)this["sunday"]; }
            set { this["sunday"] = value; }
        }

        [ConfigurationProperty("monday", IsRequired = false)]
        public bool Monday
        {
            get { return (bool)this["monday"]; }
            set { this["monday"] = value; }
        }

        [ConfigurationProperty("tuesday", IsRequired = false)]
        public bool Tuesday
        {
            get { return (bool)this["tuesday"]; }
            set { this["tuesday"] = value; }
        }

        [ConfigurationProperty("wednesday", IsRequired = false)]
        public bool Wednesday
        {
            get { return (bool)this["wednesday"]; }
            set { this["wednesday"] = value; }
        }

        [ConfigurationProperty("thursday", IsRequired = false)]
        public bool Thursday
        {
            get { return (bool)this["thursday"]; }
            set { this["thursday"] = value; }
        }

        [ConfigurationProperty("friday", IsRequired = false)]
        public bool Friday
        {
            get { return (bool)this["friday"]; }
            set { this["friday"] = value; }
        }

        [ConfigurationProperty("saturday", IsRequired = false)]
        public bool Saturday
        {
            get { return (bool)this["saturday"]; }
            set { this["saturday"] = value; }
        }
    }

    public class ScheduleDayOfMonthElement : ConfigurationElement
    {
        [ConfigurationProperty("dayNumber", IsRequired = false)]
        public int DayNumber
        {
            get { return (int)this["dayNumber"]; }
            set { this["dayNumber"] = value; }
        }

        [ConfigurationProperty("firstDay", IsRequired = false)]
        public bool FirstDay
        {
            get { return (bool)this["firstDay"]; }
            set { this["firstDay"] = value; }
        }

        [ConfigurationProperty("lastDay", IsRequired = false)]
        public bool LastDay
        {
            get { return (bool)this["lastDay"]; }
            set { this["lastDay"] = value; }
        }
    }

    public class ScheduleElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("repeat", IsRequired = false)]
        public ScheduleRepeatElement Repeats
        {
            get { return this["repeat"] as ScheduleRepeatElement; }
            set { this["repeat"] = value; }
        }

        [ConfigurationProperty("start", IsRequired = false)]
        public ScheduleStartElement Start
        {
            get { return this["start"] as ScheduleStartElement; }
            set { this["start"] = value; }
        }

        [ConfigurationProperty("days", IsRequired = false)]
        public ScheduleDaysOfWeekElement DaysOfWeek
        {
            get { return this["days"] as ScheduleDaysOfWeekElement; }
            set { this["days"] = value; }
        }

        [ConfigurationProperty("day", IsRequired = false)]
        public ScheduleDayOfMonthElement DayOfMonth
        {
            get { return this["day"] as ScheduleDayOfMonthElement; }
            set { this["day"] = value; }
        }
    }

    [ConfigurationCollection(typeof(ScheduleElement), AddItemName = "add", RemoveItemName = "remove")]
    public class SchedulesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ScheduleElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new ScheduleElement() { Name = elementName };
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ScheduleElement schedule = element as ScheduleElement;

            if (schedule == null)
                throw new ArgumentException();

            return schedule.Name;
        }

        public ScheduleElement this[int index]
        {
            get { return BaseGet(index) as ScheduleElement; }
        }
    }
}
