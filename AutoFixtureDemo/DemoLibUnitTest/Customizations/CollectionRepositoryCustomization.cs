namespace DemoLibUnitTest.Customizations
{
    using System;
    using System.Collections.Generic;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using Fakes;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Replaces all IRepository<![CDATA[<T>]]> with CollectionRepository<![CDATA[<T>]]>
    /// </summary>
    public class CollectionRepositoryCustomization : ICustomization
    {
        private static readonly Dictionary<Type, dynamic> IdSelectors =
            new Dictionary<Type, dynamic>
            {
                { typeof(Customer), new Func<Customer, object>(c => c.Id) },
            };

        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new FilteringSpecimenBuilder(
                    new CollectionRepositorySpecimenBuilder(),
                    new CollectionRepositorySpecification()));
        }

        private class CollectionRepositorySpecimenBuilder : ISpecimenBuilder
        {
            public object Create(object request, ISpecimenContext context)
            {
                var genericArguments = ((Type)request).GetGenericArguments();

                var genericCollectionType = typeof(CollectionRepository<>).MakeGenericType(genericArguments);

                if (IdSelectors.ContainsKey(genericArguments[0]))
                {
                    return Activator.CreateInstance(genericCollectionType, IdSelectors[genericArguments[0]]);
                }

                return Activator.CreateInstance(genericCollectionType);
            }
        }

        private class CollectionRepositorySpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                var requestType = request as Type;
                if (requestType == null)
                    return false;

                return requestType.IsGenericType &&
                       requestType.GetGenericTypeDefinition() == typeof(IRepository<>);
            }
        }
    }
}