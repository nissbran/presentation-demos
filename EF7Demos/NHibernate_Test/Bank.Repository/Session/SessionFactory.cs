namespace Bank.Repository.Session
{
    using System;
    using FluentNHibernate.Cfg;
    using Mappings.Customers;
    using Mappings.Transacations;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    using NHibernate.Driver;
    using Environment = NHibernate.Cfg.Environment;

    public static class SessionFactory
    {
        private static ISessionFactory _sessionFactory;

        public static void Initialize(string connectionString)
        {
            var configuration = new Configuration();
            configuration = configuration.SetProperty(Environment.Dialect, typeof(MsSql2012Dialect).AssemblyQualifiedName);
            configuration = configuration.SetProperty(Environment.ConnectionDriver, typeof(SqlClientDriver).AssemblyQualifiedName);
            configuration = configuration.SetProperty(Environment.ConnectionString, connectionString);
            configuration = configuration.SetProperty(Environment.PropertyUseReflectionOptimizer, "true");
            configuration = configuration.SetProperty(Environment.Isolation, "ReadCommitted");
            configuration = configuration.SetProperty(Environment.BatchSize, "15");
            configuration = configuration.SetProperty(Environment.OrderInserts, "false");
            configuration = configuration.SetProperty(Environment.ShowSql, "false");
            //configuration = configuration.SetProperty(Environment.CacheProvider, "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            //configuration = configuration.SetProperty(Environment.UseSecondLevelCache, "true");
            configuration = configuration.SetProperty(Environment.UseQueryCache, "true");

            var fluentConfiguration = Fluently.Configure(configuration);
            fluentConfiguration.Mappings(
                c =>
                {
                    //c.FluentMappings.Add(Assembly.GetAssembly(typeof(SessionFactory)));
                    c.FluentMappings.Add(typeof(BankTransactionMap));
                    c.FluentMappings.Add(typeof(BankCustomerMap));
                    c.FluentMappings.Add(typeof(PrivatePersonMap));
                    c.FluentMappings.Add(typeof(CompanyMap));
                });

            _sessionFactory = fluentConfiguration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            if (_sessionFactory == null)
                throw new InvalidOperationException();

            return _sessionFactory.OpenSession();
        }
    }
}
