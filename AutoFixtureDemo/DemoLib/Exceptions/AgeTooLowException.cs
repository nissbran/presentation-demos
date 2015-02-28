namespace DemoLib.Exceptions
{
    using System;

    public class AgeTooLowException : Exception
    {
        public AgeTooLowException() : base("Age is too low")
        {
            
        }
    }
}
