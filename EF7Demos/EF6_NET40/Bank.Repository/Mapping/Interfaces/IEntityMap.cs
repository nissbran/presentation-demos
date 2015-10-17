namespace Bank.Repository.Mapping.Interfaces
{
    using System.Data.Entity;

    public interface IEntityMap
    {
        void ConfigureModel(DbModelBuilder modelBuilder);
    }
}
