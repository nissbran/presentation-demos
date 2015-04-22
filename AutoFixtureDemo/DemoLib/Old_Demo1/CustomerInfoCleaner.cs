namespace DemoLib.Old_Demo1
{
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
