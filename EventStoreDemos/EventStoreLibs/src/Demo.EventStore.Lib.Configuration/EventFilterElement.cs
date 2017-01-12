namespace Demo.EventStore.Lib.Configuration
{
    using System.Configuration;

    public class EventFilterElement : ConfigurationElement
    {
        [ConfigurationProperty("prefix", DefaultValue = "",
        IsRequired = true, IsKey = true)]
        public string EventPrefix
        {
            get
            {
                return (string)this["prefix"];
            }
            set
            {
                this["prefix"] = value;
            }
        }
    }
}