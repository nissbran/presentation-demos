namespace DemoLibUnitTest.VehicleRegistrationTests
{
    using Customizations;
    using DemoLib;
    using DemoLib.Exceptions;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using DemoLib.Model.Vehicles;
    using global::NUnit.Framework;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    [TestFixture]
    public class VehicleRegistrationProcessTest
    {
        private IFixture _fixture;

        private string _registrationNumber;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Customize(new AutoNSubstituteCustomization());

            _fixture.Customizations.Add(new CarRegistrationNumberGenerator());

            _registrationNumber = _fixture.CreateConstrainedString(6);

            _fixture.Customize<Car>(composer => composer.With(c => c.RegistrationNumber, _registrationNumber));
            //_fixture.Register<Vehicle>(() => _fixture.Create<Car>());
            _fixture.Inject<Vehicle>(_fixture.Create<Car>());
        }

        [Test]
        public void When_a_vehicle_is_registered_it_Then_it_should_info_log_the_id()
        {
            var logging = _fixture.Freeze<ILogging>();
            var vehicle = _fixture.Create<Vehicle>();
            var sut = _fixture.Create<VehicleRegisterationProcess>();

            sut.RegisterVehicle(vehicle, _fixture.Create<Owner>());

            logging.Received().Info(Arg.Is<string>(s => s.Contains(vehicle.Id.ToString())));
        }

        [Test, ExpectedException(typeof(CarRegistrationNumberIsIncorrectException))]
        public void When_a_car_is_has_a_incorrect_registration_number_Then_cast_an_exception()
        {
            var car = _fixture.Build<Car>()
                              .With(c => c.RegistrationNumber, _fixture.CreateConstrainedString(9))
                              .Create();
            var sut = _fixture.Create<VehicleRegisterationProcess>();

            sut.RegisterVehicle(car, _fixture.Create<Owner>());
        }

        [Test]
        public void When_a_car_is_registered_Then_it_should_be_stored_in_car_repository()
        {
            var repository = _fixture.Freeze<IRepository<Car>>();
            var sut = _fixture.Create<VehicleRegisterationProcess>();

            sut.RegisterVehicle(_fixture.Create<Car>(), _fixture.Create<Owner>());

            repository.Received().Add(Arg.Any<Car>());
        }

        [Test]
        public void When_a_car_is_registered_Then_it_should_be_send_registration_number_to_external_queue()
        {
            var repository = _fixture.Freeze<ICarRegistrationExternalQueue>();
            var car = _fixture.Create<Car>();
            var sut = _fixture.Create<VehicleRegisterationProcess>();

            sut.RegisterVehicle(car, _fixture.Create<Owner>());

            repository.Received().Send(car.RegistrationNumber, Arg.Any<FuelType>());
        }

        [Test]
        public void When_a_car_is_registered_Then_it_should_be_send_fuel_type_to_external_queue()
        {
            var repository = _fixture.Freeze<ICarRegistrationExternalQueue>();
            var car = _fixture.Create<Car>();
            var sut = _fixture.Create<VehicleRegisterationProcess>();

            sut.RegisterVehicle(car, _fixture.Create<Owner>());

            repository.Received().Send(Arg.Any<string>(), car.FuelType);
        }

        [Test]
        public void When_a_bicycle_is_registered_Then_it_should_be_stored_in_bicycle_repository()
        {
            var repository = _fixture.Freeze<IRepository<Bicycle>>();
            var sut = _fixture.Create<VehicleRegisterationProcess>();

            sut.RegisterVehicle(_fixture.Create<Bicycle>(), _fixture.Create<Owner>());

            repository.Received().Add(Arg.Any<Bicycle>());
        }

        [Test]
        public void When_a_bicycle_is_registered_Then_it_should_be_send_frame_number_to_external_queue()
        {
            var repository = _fixture.Freeze<IBicycleFrameNumberRegistrationQueue>();
            var bicycle = _fixture.Create<Bicycle>();
            var sut = _fixture.Create<VehicleRegisterationProcess>();

            sut.RegisterVehicle(bicycle, _fixture.Create<Owner>());

            repository.Received().RegisterFrameNumber(bicycle.FrameNumber);
        }
    }
}