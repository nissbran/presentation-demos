namespace DemoLibUnitTest.VehicleRegistrationTests.Refactoring
{
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using DemoLib.Model.Vehicles;
    using DemoLib.Refactoring;
    using global::NUnit.Framework;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    [TestFixture]
    public class BicycleRegistrationProcessTest
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Customize(new AutoNSubstituteCustomization());
        }

        [Test]
        public void When_a_vehicle_is_registered_it_Then_it_should_info_log_the_id()
        {
            var logging = _fixture.Freeze<ILogging>();
            var vehicle = _fixture.Create<Vehicle>();
            var sut = _fixture.Create<BicycleRegisterationProcess>();

            sut.RegisterVehicle(vehicle, _fixture.Create<Owner>());

            logging.Received().Info(Arg.Is<string>(s => s.Contains(vehicle.Id.ToString())));
        }

        [Test]
        public void When_a_bicycle_is_registered_Then_it_should_be_stored_in_bicycle_repository()
        {
            var repository = _fixture.Freeze<IRepository<Bicycle>>();
            var sut = _fixture.Create<BicycleRegisterationProcess>();

            sut.RegisterVehicle(_fixture.Create<Bicycle>(), _fixture.Create<Owner>());

            repository.Received().Add(Arg.Any<Bicycle>());
        }

        [Test]
        public void When_a_bicycle_is_registered_Then_it_should_be_send_frame_number_to_external_queue()
        {
            var repository = _fixture.Freeze<IBicycleFrameNumberRegistrationQueue>();
            var bicycle = _fixture.Create<Bicycle>();
            var sut = _fixture.Create<BicycleRegisterationProcess>();

            sut.RegisterVehicle(bicycle, _fixture.Create<Owner>());

            repository.Received().RegisterFrameNumber(bicycle.FrameNumber);
        }
    }
}