using Microsoft.AspNetCore.Mvc;
using Modern_Milkman_Tech_Test.Interfaces;
using Modern_Milkman_Tech_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Modern_Milkman_Tech_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IDataServices _dataServices;
        public CustomerController(IDataServices dataservice)
        {
            _dataServices = dataservice;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public List<Customer> GetAllCutomers()
        {
            // returns all customers both active and inactive
            return _dataServices.getAllCustomers();   
        }

        [HttpGet]
        [Route("GetAllActiveCustomers")]
        public List<Customer> GetAllActiveCutomers()
        {
            //returns all active customers
            return _dataServices.getAllActiveCustomers();
        }

        [HttpPost]
        [Route("AddNewCustomer")]
        public object AddNewCustomer([FromBody] Customer customer)
        {
            //check if customer already exists using email address as they are unique
            bool exists = _dataServices.checkCustomerExists(customer.Email_Address);
            if (!exists)
            {
                 _dataServices.addNewCustomer(customer);
                return Ok();
            }
            else
            {
                return BadRequest("Customer already exists");
            }


        }
        [HttpPost]
        [Route("setCustomerInActive")]
        public object setCustomerInActive([FromBody] Customer customer)
        {
            //check if customer already exists using email address as they are unique
            bool updated = _dataServices.setCustomertoInActive(customer.Email_Address);
            if (updated)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Customer already exists");
            }
        }
        
        [HttpDelete("DeleteCustomer")]
        public object DeleteCustomer(int id, string email)
        {
            bool deleted = _dataServices.DeleteCustomer(id, email);
            if(deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Unable to delete this customer");
            }
        }
    }
}
