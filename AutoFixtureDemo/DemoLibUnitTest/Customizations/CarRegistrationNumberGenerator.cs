namespace DemoLibUnitTest.Customizations
{
    using System;
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
                return new CarRegistrationNumber((string)context.Resolve(new ConstrainedStringRequest(6, 6)));
            }

            return new NoSpecimen(request);
        }
    }
}