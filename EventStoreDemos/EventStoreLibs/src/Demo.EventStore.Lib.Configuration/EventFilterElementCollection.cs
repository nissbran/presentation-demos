namespace Demo.EventStore.Lib.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public class EventFilterElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

        protected override ConfigurationElement CreateNewElement()
        {
            return new EventFilterElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EventFilterElement)element).EventPrefix;
        }

        public EventFilterElement this[int index]
        {
            get
            {
                return (EventFilterElement)BaseGet(index);
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

        public new EventFilterElement this[string Name] => (EventFilterElement)BaseGet(Name);

        public IEnumerable<string> GetAllPrefixes()
        {
            return BaseGetAllKeys().Select(o => o.ToString());
        }
    }
}