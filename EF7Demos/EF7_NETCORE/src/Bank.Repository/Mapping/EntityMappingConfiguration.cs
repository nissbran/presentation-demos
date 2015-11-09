namespace Bank.Repository.Mapping
{
    using System;
    using Entities;
    using System.Collections.Generic;
    using Interfaces;

    public static class EntityMappingConfiguration
    {
        public static readonly List<IEntityMap> EntityMappings = new List<IEntityMap>
        {
            new CustomerMap(),
            new BankTransactionMap(),
            new CreditCheckResultMap()
        };
    }
}
