namespace DemoLib.Model.Vehicles
{
    using System;
    using Exceptions;

    public class CarRegistrationNumber
    {
        public string Value { get; private set; }

        public CarRegistrationNumber(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentException("registrationNumber can't be null or empty");
            if (registrationNumber.Length != 6)
                throw new CarRegistrationNumberIsIncorrectException();

            Value = registrationNumber;
        }

        public static explicit operator string(CarRegistrationNumber registrationNumber)
        {
            return registrationNumber.Value;
        }

    }
}