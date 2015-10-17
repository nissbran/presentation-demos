namespace Bank.Repository.Context
{
    using System.Data.Entity;
    using Mapping;

    public class BankContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            EntityMappingConfiguration.EntityMappings.ForEach(map => map.ConfigureModel(modelBuilder));
        }
    }
}
