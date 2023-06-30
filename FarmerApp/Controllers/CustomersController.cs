using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _customerService;
        private IMapper _mapper;

        public CustomersController(
            IMapper mapper,
            ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAll();

            var response = new List<CustomerResponseModel>();

            foreach (var customer in customers)
                response.Add(_mapper.Map<CustomerResponseModel>(customer));

            return Ok(response);
        }

        [HttpGet("GetByID")]
        public IActionResult GetById(int id) => Ok(_mapper.Map<CustomerResponseModel>(_customerService.GetById(id)));

        [HttpPost]
        public IActionResult Add(CustomerRequestModel customerRequest)
        {
            _customerService.Add(_mapper.Map<Customer>(customerRequest));

            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            _customerService.Remove(id);
            return Ok();
        }

        [HttpGet("GetByLocation")]
        public IActionResult GetCustomersByLocation(string address) => Ok(_customerService.GetCustomersByLocation(address));

        [HttpPut]
        public IActionResult UpdateCustomer(int id, CustomerRequestModel customerRequest)
        {
            var customerToUpdate = _mapper.Map<Customer>(customerRequest);
            customerToUpdate.Id = id;

            _customerService.Update(customerToUpdate);
            return Ok();
        }
    }
}

