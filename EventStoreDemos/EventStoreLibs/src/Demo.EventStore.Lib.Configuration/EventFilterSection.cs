namespace Demo.EventStore.Lib.Configuration
{
    using System.Configuration;

    public class EventFilterSection : ConfigurationSection
    {
        [ConfigurationProperty("allowedEventPrefixes", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(EventFilterElementCollection),
        AddItemName = "add",
        ClearItemsName = "clear",
        RemoveItemName = "remove")]
        public EventFilterElementCollection AllowedEventPrefixes
        {
            get
            {
                return (EventFilterElementCollection)base["allowedEventPrefixes"];
            }
            set
            {
                base["allowedEventPrefixes"] = value;
            }
        }
    }
}