namespace DemoLib.Refactoring
{
    using System;
    using Interfaces;
    using Model;
    using Model.Vehicles;

    public class BicycleRegisterationProcess : IVehicleRegisterationProcess
    {
        private readonly IRepository<Bicycle> _bicycleRepository;
        private readonly ILogging _logging;
        private readonly IBicycleFrameNumberRegistrationQueue _queue;

        public BicycleRegisterationProcess(
            IRepository<Bicycle> bicycleRepository,
            ILogging logging,
            IBicycleFrameNumberRegistrationQueue queue)
        {
            _bicycleRepository = bicycleRepository;
            _logging = logging;
            _queue = queue;
        }

        public bool RegisterVehicle(Vehicle vehicle, Owner owner)
        {
            _logging.Info(string.Format("Id : {0}", vehicle.Id));

            var bicycle = vehicle as Bicycle;
            if (bicycle == null)
                return false;

            _bicycleRepository.Add(bicycle);
            _queue.RegisterFrameNumber(bicycle.FrameNumber);

            return true;
        }
    }
}
