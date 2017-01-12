namespace Demo.EventStore.Lib.Configuration
{
    using System.Configuration;

    public class ClusterNodeElement : ConfigurationElement
    {
        [ConfigurationProperty("node", DefaultValue = 0, IsRequired = true, IsKey = true)]
        public int Number
        {
            get
            {
                return (int)this["node"];
            }
            set
            {
                this["node"] = value;
            }
        }

        [ConfigurationProperty("ip", DefaultValue = "", IsRequired = false, IsKey = false)]
        public string IpAddress
        {
            get
            {
                return (string)this["ip"];
            }
            set
            {
                this["ip"] = value;
            }
        }

        [ConfigurationProperty("hostname", DefaultValue = "", IsRequired = false, IsKey = false)]
        public string HostName
        {
            get
            {
                return (string)this["hostname"];
            }
            set
            {
                this["hostname"] = value;
            }
        }

        [ConfigurationProperty("externalPort", DefaultValue = 2112, IsRequired = false, IsKey = false)]
        public int ExternalPort
        {
            get
            {
                return (int)this["externalPort"];
            }
            set
            {
                this["externalPort"] = value;
            }
        }

        public bool HostNameSpecified => !string.IsNullOrWhiteSpace(HostName);
    }
}