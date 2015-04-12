namespace DemoLib.Model.Vehicles
{
    public class Car : Vehicle
    {
        public int NumberOfSeats { get; set; }

        public string RegistrationNumber { get; set; }

        public CarRegistrationNumber ValueObjectRegistrationNumber { get; set; }

        public decimal TopSpeed { get; set; }

        public decimal AverageFuelConsumption { get; set; }

        public FuelType FuelType { get; set; }
    }
}