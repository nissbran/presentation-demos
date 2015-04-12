namespace DemoLib
{
    using Exceptions;
    using Model;

    public class PersonRegisterProcess
    {
        public void RegisterPerson(Person person)
        {
            if (person.Age < 18)
                throw new AgeTooLowException();

        }
    }
}
