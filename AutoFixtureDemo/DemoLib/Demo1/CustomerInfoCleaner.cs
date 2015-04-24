namespace DemoLib.Demo1
{
    using System;
    using Model;

    public class CustomerInfoCleaner
    {
        public Customer CleanSensitiveCustomerInfo(Customer customer)
        {
            customer.SocialSecurityNumber = null;
            return customer;
        }
    }
}