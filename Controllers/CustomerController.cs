using Microsoft.AspNetCore.Mvc;
using PERT_2.Models.DB;
using PERT_2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PERT_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //dibuat
        private readonly CustomerServices _customerServices;

        public CustomerController(CustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            var customerList = _customerServices.GetListCustommer();

            return Ok(customerList);
        }

        //Pertemuan 3////////////////////////////////////////////////////////////////////////////////////////////////
        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult post(Customer customer)
        {
            var insertCustomer = _customerServices.CreateCustomer(customer);
            if (insertCustomer)
            {

                return Ok("Insert Customer Success");
            }
            return BadRequest("Insert Customer Failed");
        }

        [HttpPut]

        public IActionResult put(Customer customer)
        {
            try
            {
                var updateCustomer = _customerServices.UpdateCustomer(customer);
                if (updateCustomer)
                {
                    return Ok("Update Customer Success");
                }

                return BadRequest("Update Customer Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                throw;
            }

        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return new string[] { "value1", "value2" };
            var customerList = _customerServices.GetListCustommerById(id);

            return Ok(customerList);
        }
        /*public IActionResult delete(Customer customer)
        {
            
        }*/

        //end dibuat

        /*//default Vs Code
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool cek = _customerServices.DeleteCustomer(id);
                if (cek)
                {
                    return Ok("Data Di Hapus");
                }
                else
                {

                    return BadRequest("Not Found\n" + "Update Customer Failed");
                }
            }
            catch (Exception)
            {
                return BadRequest("Not Found\n" + "Update Customer Failed");
                throw;
            }
        }
    }
}
