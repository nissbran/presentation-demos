namespace Demo.EventStore.Lib.Configuration
{
    using System.Configuration;

    public class SingleNodeElement : ConfigurationElement
    {
        [ConfigurationProperty("connectionUri", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string ConnectionUri
        {
            get
            {
                return (string)this["connectionUri"];
            }
            set
            {
                this["connectionUri"] = value;
            }
        }
    }
}