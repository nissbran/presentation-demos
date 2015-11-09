using System;
using Bank.Domain.Models;
using Bank.Repository.Mapping.Interfaces;
using Microsoft.Data.Entity;

namespace Bank.Repository.Mapping.Entities
{
    public class CreditCheckResultMap : IEntityMap
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CreditCheckResult>()
                .HasKey(e => e.CreditCheckResultId);
            
        }
    }
}
