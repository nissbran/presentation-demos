namespace DemoLib
{
    using Exceptions;
    using Interfaces;
    using Model;
    using Model.Vehicles;
    
    public class VehicleRegisterationProcess : IVehicleRegisterationProcess
    {
        private readonly IRepository<Bicycle> _bicycleRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly ILogging _logging;
        private readonly ICarRegistrationExternalQueue _queue;
        private readonly IBicycleFrameNumberRegistrationQueue _frameNumberRegistration;

        public VehicleRegisterationProcess(
            IRepository<Bicycle> bicycleRepository,
            IRepository<Car> carRepository,
            ILogging logging,
            ICarRegistrationExternalQueue queue,
            IBicycleFrameNumberRegistrationQueue frameNumberRegistration)
        {
            _bicycleRepository = bicycleRepository;
            _carRepository = carRepository;
            _logging = logging;
            _queue = queue;
            _frameNumberRegistration = frameNumberRegistration;
        }

        public bool RegisterVehicle(Vehicle vehicle, Owner owner)
        {
            _logging.Info(string.Format("Id : {0}", vehicle.Id));

            RegisterCar(vehicle as Car);
            RegisterBicycle(vehicle as Bicycle);

            return true;
        }

        private void RegisterBicycle(Bicycle bicycle)
        {
            if (bicycle == null)
                return;

            _bicycleRepository.Add(bicycle);
            _frameNumberRegistration.RegisterFrameNumber(bicycle.FrameNumber);
        }

        private void RegisterCar(Car car)
        {
            if (car == null)
                return;

            if (car.RegistrationNumber.Length != 6)
                throw new CarRegistrationNumberIsIncorrectException();

            _carRepository.Add(car);
            _queue.Send(car.RegistrationNumber, car.FuelType);
        }
    }
}
