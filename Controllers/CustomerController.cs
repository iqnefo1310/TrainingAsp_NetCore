﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PERT_2.Models;
using PERT_2.Models.DB;
using PERT_2.Models.DTO;
using PERT_2.Services;
using PERT_2.Validate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using FluentValidation.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PERT_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //dibuat
        private readonly CustomerServices _customerServices;
        private FluentValidation.Results.ValidationResult _validation;

        public CustomerController(CustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        [Route("MelihatSemuaData")]
        public IActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            try
            {
                var customerList = _customerServices.GetlistCustomer();
                var response = new GeneralResponse
                {
                    StatusCode = "01",
                    StatusDesc = "suksess",
                    Data = customerList
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(response);
            }
        }

        //Pertemuan 3////////////////////////////////////////////////////////////////////////////////////////////////
        // POST api/<CustomerController>
        [HttpPost]
        [Route("MenambahkanDataCustomer")]
        public IActionResult post(CustomerRequestDTO Customer)
        {
            try
            {
                ValidatorRequestCustomer request = new ValidatorRequestCustomer();
                _validation = request.Validate(Customer);

                if (_validation.IsValid)
                {
                    var insertCustomer = _customerServices.CreateCustomer(Customer);
                    if (insertCustomer)
                    {
                        var responseSuccess = new GeneralResponse
                        {
                            StatusCode = "01",
                            StatusDesc = "Sukses Menambah Data",
                            Data = insertCustomer
                        };
                        return Ok(responseSuccess);
                    }
                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        StatusDesc = "Gagal Menambah Data",
                        Data = null
                    };
                    return BadRequest(responseFailed);
                }
                else
                {
                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        StatusDesc = _validation.ToString(),
                        Data = null
                    };
                    return BadRequest(responseFailed);
                }
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "099",
                    StatusDesc = "Fatal Insert | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(responseFailed);
            }
        }

        [HttpPut]
        [Route("Mengedit Data {Id}")]
        public IActionResult put(int Id, CustomerRequestDTO customer)
        {
            try
            {
                var updateCustomer = _customerServices.UpdateCustomer(Id ,customer);
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
        [HttpGet("Mendapatkan Satu Data{id}")]
        public IActionResult Get(int id)
        {
            //return new string[] { "value1", "value2" };
            var customerList = _customerServices.GetListCustommerById(id);

            return Ok(customerList);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("Hapus Data{id}")]
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
