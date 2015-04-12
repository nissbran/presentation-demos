namespace DemoLib.Interfaces
{
    using Model.Vehicles;

    public interface ICarRegistrationExternalQueue
    {
        bool Send(string registrationNumber, FuelType fuelType);
    }
}