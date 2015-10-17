﻿namespace Bank.Repository.Mapping.Entities
{
    using Domain.Models.Customers;
    using Interfaces;
    using Microsoft.Data.Entity;

    public class CustomerMap : IEntityMap
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BankCustomer>()
                .HasKey(customer => customer.CustomerId);

            //modelBuilder.Entity<BankCustomer>().Ignore(customer => customer.RegistrationNumber);
           // modelBuilder.Entity<BankCustomer>().Property<string>("RegistrationNumber");
           // modelBuilder.Entity<BankCustomer>().Ignore(customer => customer.RegistrationNumber);

            modelBuilder.Entity<PrivatePerson>().BaseType<BankCustomer>();
            modelBuilder.Entity<Company>().BaseType<BankCustomer>();

            // modelBuilder.Entity<BankCustomer>().Discriminator("CustomerType", typeof (string));
            modelBuilder.Entity<BankCustomer>().Discriminator("CustomerType", typeof(int))
                .HasValue(typeof(PrivatePerson), 1)
                .HasValue(typeof(Company), 2);
        }
    }
}