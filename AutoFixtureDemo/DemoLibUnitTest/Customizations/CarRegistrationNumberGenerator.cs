namespace DemoLibUnitTest.Customizations
{
    using System;
    using DemoLib.Model;
    using DemoLib.Model.Vehicles;
    using Ploeh.AutoFixture.Kernel;

    public class CarRegistrationNumberGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var registrationNumber = request as Type;
            if (registrationNumber != null &&
                registrationNumber == typeof(CarRegistrationNumber))
            {
                var randomRegistrationNumber = (string)context.Resolve(new ConstrainedStringRequest(6, 6));

                return new CarRegistrationNumber(randomRegistrationNumber);
            }

            return new NoSpecimen(request);
        }
    }
}