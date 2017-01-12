namespace Demo.EventStore.Lib.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public class ClusterNodeElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

        protected override ConfigurationElement CreateNewElement()
        {
            return new ClusterNodeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ClusterNodeElement)element).Number;
        }

        public ClusterNodeElement this[int index]
        {
            get
            {
                return (ClusterNodeElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new ClusterNodeElement this[string name] => (ClusterNodeElement)BaseGet(name);

        public void Add(int nodeNumber, string ip)
        {
            BaseAdd(new ClusterNodeElement { Number = nodeNumber, IpAddress = ip});
        }

        public IEnumerable<ClusterNodeElement> GetAllClusterNodes()
        {
            return BaseGetAllKeys().Select(BaseGet).Select(element => (ClusterNodeElement)element);
        }
    }
}