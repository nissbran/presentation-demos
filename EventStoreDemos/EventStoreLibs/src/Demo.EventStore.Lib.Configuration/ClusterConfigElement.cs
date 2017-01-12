namespace Demo.EventStore.Lib.Configuration
{
    using System.Configuration;

    public class ClusterConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("useSsl", DefaultValue = false, IsRequired = false, IsKey = false)]
        public bool UseSsl
        {
            get
            {
                return (bool)this["useSsl"];
            }
            set
            {
                this["useSsl"] = value;
            }
        }
    }
}