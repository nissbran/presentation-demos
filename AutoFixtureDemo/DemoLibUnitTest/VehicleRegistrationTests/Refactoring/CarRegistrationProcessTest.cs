namespace DemoLibUnitTest.VehicleRegistrationTests.Refactoring
{
    using Customizations;
    using DemoLib.Exceptions;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using DemoLib.Model.Vehicles;
    using DemoLib.Refactoring;
    using global::NUnit.Framework;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    [TestFixture]
    public class CarRegistrationProcessTest
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Customize(new AutoNSubstituteCustomization());

            _fixture.Customizations.Add(new CarRegistrationNumberGenerator());
        }

        [Test]
        public void When_a_vehicle_is_registered_it_Then_it_should_info_log_the_id()
        {
            var logging = _fixture.Freeze<ILogging>();
            var vehicle = _fixture.Create<Vehicle>();
            var sut = _fixture.Create<CarRegisterationProcess>();

            sut.RegisterVehicle(vehicle, _fixture.Create<Owner>());

            logging.Received().Info(Arg.Is<string>(s => s.Contains(vehicle.Id.ToString())));
        }

        [Test]
        public void When_a_car_is_registered_Then_it_should_be_stored_in_car_repository()
        {
            var repository = _fixture.Freeze<IRepository<Car>>();
            var sut = _fixture.Create<CarRegisterationProcess>();

            sut.RegisterVehicle(_fixture.Create<Car>(), _fixture.Create<Owner>());

            repository.Received().Add(Arg.Any<Car>());
        }

        [Test]
        public void When_a_car_is_registered_Then_it_should_be_send_registration_number_to_external_queue()
        {
            var repository = _fixture.Freeze<ICarRegistrationExternalQueue>();
            var car = _fixture.Create<Car>();
            var sut = _fixture.Create<CarRegisterationProcess>();

            sut.RegisterVehicle(car, _fixture.Create<Owner>());

            repository.Received().Send(car.RegistrationNumber, Arg.Any<FuelType>());
        }

        [Test]
        public void When_a_car_is_registered_Then_it_should_be_send_fuel_type_to_external_queue()
        {
            var repository = _fixture.Freeze<ICarRegistrationExternalQueue>();
            var car = _fixture.Create<Car>();
            var sut = _fixture.Create<CarRegisterationProcess>();

            sut.RegisterVehicle(car, _fixture.Create<Owner>());

            repository.Received().Send(Arg.Any<string>(), car.FuelType);
        }

        [Test, ExpectedException(typeof(CarRegistrationNumberAlreadyExistException))]
        public void When_a_car_is_registering_and_a_car_with_same_registration_number_exist_in_repo_Then_cast_an_exception()
        {
            var car = _fixture.Create<Car>();
            _fixture.Customize(new CollectionRepositoryCustomization());
            _fixture.Freeze<IRepository<Car>>().Add(car);
            var sut = _fixture.Create<CarRegisterationProcess>();

            sut.RegisterVehicle(car, _fixture.Create<Owner>());
        }
    }
}