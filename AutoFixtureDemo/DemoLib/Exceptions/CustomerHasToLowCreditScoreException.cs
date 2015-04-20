namespace DemoLib.Exceptions
{
    using System;

    public class CustomerHasToLowCreditScoreException : Exception
    {
        public CustomerHasToLowCreditScoreException()
            : base("The score from credit check for the customer is too low")
        {
            
        }
    }
}
