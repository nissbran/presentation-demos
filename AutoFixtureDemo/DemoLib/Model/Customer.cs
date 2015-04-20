namespace DemoLib.Model
{
    using System;
    using System.Collections.Generic;

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int AccountNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public DateTime Created { get; set; }
        public Company Company { get; set; }
        public List<CustomerMetaData> MetaData { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
