namespace Demo.EventStore.Lib.Configuration
{
    using System.Configuration;
    using System.Linq;
    using Common;

    public class EventStoreConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("useSingleNode", DefaultValue = false, IsRequired = false, IsKey = false)]
        public bool UseSingleNode
        {
            get
            {
                return (bool)this["useSingleNode"];
            }
            set
            {
                this["useSingleNode"] = value;
            }
        }

        [ConfigurationProperty("useEnvironmentVariableSetting", DefaultValue = false, IsRequired = false, IsKey = false)]
        public bool UseEnvironmentVariableSetting
        {
            get
            {
                return (bool)this["useEnvironmentVariableSetting"];
            }
            set
            {
                this["useEnvironmentVariableSetting"] = value;
            }
        }

        [ConfigurationProperty("singleNode", IsRequired = false)]
        public SingleNodeElement SingleNode
        {
            get
            {
                return (SingleNodeElement)this["singleNode"];
            }
            set
            {
                this["singleNode"] = value;
            }
        }

        [ConfigurationProperty("clusterConfig", IsRequired = false)]
        public ClusterConfigElement ClusterConfig
        {
            get
            {
                return (ClusterConfigElement)this["clusterConfig"];
            }
            set
            {
                this["clusterConfig"] = value;
            }
        }

        [ConfigurationProperty("clusterNodes", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ClusterNodeElementCollection),
        AddItemName = "add",
        ClearItemsName = "clear",
        RemoveItemName = "remove")]
        public ClusterNodeElementCollection ClusterNodes
        {
            get
            {
                return (ClusterNodeElementCollection)base["clusterNodes"];
            }
            set
            {
                base["clusterNodes"] = value;
            }
        }

        public IEventStoreConfiguration GetConfiguration()
        {
            return new EventStoreConfiguration
            {
                SingleNodeConnectionUri = SingleNode.ConnectionUri,
                UseEnvironmentVariableSetting = UseEnvironmentVariableSetting,
                UseSingleNode = UseSingleNode,
                ClusterConfiguration = new EventStoreClusterConfiguration
                {
                    UseSsl = ClusterConfig.UseSsl,
                    ClusterNodes = ClusterNodes.GetAllClusterNodes().Select(
                        element => new EventStoreClusterNode
                        {
                            Number = element.Number,
                            IpAddress = element.IpAddress,
                            HostName = element.HostName,
                            ExternalPort = element.ExternalPort
                        })
                }
            };
        }
    }
}