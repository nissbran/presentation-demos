namespace DemoLibUnitTest.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DemoLib.Interfaces;

    public class CollectionRepository<T> : IRepository<T> where T : class
    {
        private readonly Func<T, object> _idSelector;
        private readonly ISet<object> _allData = new HashSet<object>();

        public CollectionRepository()
            : this(null)
        {
        }

        public CollectionRepository(Func<T, object> idSelector)
        {
            _idSelector = idSelector;
        }

        public T Get(object id)
        {
            return _allData.OfType<T>().SingleOrDefault(o => _idSelector(o).Equals(id));
        }

        public IEnumerable<T> GetAll()
        {
            return _allData.OfType<T>();
        }

        public void Add(T entity)
        {
            _allData.Add(entity);
        }
    }
}
