namespace DemoLib.Refactoring
{
    using System;
    using Exceptions;
    using Interfaces;
    using Model;
    using Model.Vehicles;

    public class CarRegisterationProcess : IVehicleRegisterationProcess
    {
        private readonly IRepository<Car> _carRepository;
        private readonly ILogging _logging;
        private readonly ICarRegistrationExternalQueue _queue;

        public CarRegisterationProcess(
            IRepository<Car> carRepository,
            ILogging logging,
            ICarRegistrationExternalQueue queue)
        {
            _carRepository = carRepository;
            _logging = logging;
            _queue = queue;
        }

        public bool RegisterVehicle(Vehicle vehicle, Owner owner)
        {
            _logging.Info(string.Format("Id : {0}", vehicle.Id));

            var car = vehicle as Car;
            if (car == null)
                return false;

            if (_carRepository.Get(car.Id) != null)
                throw new CarRegistrationNumberAlreadyExistException();

            _carRepository.Add(car);
            _queue.Send(car.RegistrationNumber, car.FuelType);

            return true;
        }
    }
}
