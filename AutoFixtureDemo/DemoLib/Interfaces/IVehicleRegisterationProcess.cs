namespace DemoLib.Interfaces
{
    using Model;
    using Model.Vehicles;

    public interface IVehicleRegisterationProcess
    {
        bool RegisterVehicle(Vehicle vehicle, Owner owner);
    }
}