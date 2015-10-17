namespace Bank.Repository.Mapping.Interfaces
{
    using Microsoft.Data.Entity;

    public interface IEntityMap
    {
        void ConfigureModel(ModelBuilder modelBuilder);
    }
}
