﻿namespace DemoLib.Web.Tests.ApiControllerCustomizations
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    internal class ApiControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new FilteringSpecimenBuilder(
                    new Postprocessor(
                        new MethodInvoker(
                            new ModestConstructorQuery()),
                        new ApiControllerFiller()),
                    new ApiControllerSpecification()));
        }

        private class ApiControllerFiller : ISpecimenCommand
        {
            public void Execute(object specimen, ISpecimenContext context)
            {
                var target = specimen as ApiController;
                if (target == null)
                    throw new ArgumentException(
                        "The specimen must be an instance of ApiController.",
                        "specimen");

                target.Request = (HttpRequestMessage) context.Resolve(typeof (HttpRequestMessage));
            }
        }

        private class ApiControllerSpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                var requestType = request as Type;
                if (requestType == null)
                    return false;

                return typeof (ApiController).IsAssignableFrom(requestType);
            }
        }
    }
}
