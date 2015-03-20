namespace DemoLib.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository<T> where T : class
    {
        T Get(long id);

        IEnumerable<T> GetAll(); 

        void Add(T entity);
    }
}
