namespace DemoLib.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Interfaces;
    using Model;

    public class CustomerController : ApiController
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerController(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> Get()
        {
            return _customerRepository.GetAll();
        }

        public Customer Get(int id)
        {
            return _customerRepository.Get(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
