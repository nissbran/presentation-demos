namespace DemoLibUnitTest.NUnit
{
    using DemoLib;
    using DemoLib.Exceptions;
    using DemoLib.Model;
    using global::NUnit.Framework;

    [TestFixture]
    public class PersonRegisterProcessTest 
    {
        [Test, ExpectedException(typeof(AgeTooLowException))]
        public void When_register_an_person_and_age_is_under_18_Then_cast_an_age_too_low_exception()
        {
            var person = new Person
            {
                Age = 10
            };
            var sut = new PersonRegisterProcess();

            sut.RegisterPerson(person);
        }
    }
}
