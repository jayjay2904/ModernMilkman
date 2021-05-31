using Microsoft.AspNetCore.Mvc;
using Modern_Milkman_Tech_Test.Interfaces;
using Modern_Milkman_Tech_Test.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Modern_Milkman_Tech_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IDataServices _dataServices;
        public AddressController(IDataServices dataservice)
        {
            _dataServices = dataservice;
        }
        
        [HttpGet("getAllCustomerAddresses")]
        public List<Address> getAllAddressesforCustomer(int CustomerId)
        {
            return _dataServices.getCustomerAddresses(CustomerId);
        }

        [HttpPost]
        [Route("AddNewAddress")]
        public object AddNewAddress([FromBody] Address Address)
        {

            //check if customer already exists using email address as they are unique
            var exists = _dataServices.checkAddressExists(Address.Address1, Address.PostCode);
            if (!exists)
            {
                _dataServices.addNewAddress(Address);
                return Ok();
            }
            else
            {
                return BadRequest("Customer already exists");
            }
        }

        [HttpDelete("DeleteSingleAddress")]
        public object DeleteSingleAddress([FromBody] int CustomerId, string Postcode)
        {
            var deleted = _dataServices.DeleteSingleAddress(CustomerId, Postcode);
            if (deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest("The customer only has a single address registered");
            }
             
        }
    }
}
