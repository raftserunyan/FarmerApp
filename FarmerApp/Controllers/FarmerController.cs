using System;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerController : ControllerBase
    {
        private IFarmerService _farmerService;

        public FarmerController(IFarmerService farmerService)
        {
            _farmerService = farmerService;
        }

        [HttpGet("GetBalance")]
        public IActionResult GetBalance() => Ok(_farmerService.GetBalance());

        [HttpGet("GetBalance/{id}")]
        public IActionResult GetCustomerBalance(int id) => Ok(_farmerService.GetBalance(id));
        
        [HttpGet("GetCustomersBalance")]
        public IActionResult GetCustomersBalance() => Ok(_farmerService.GetCustomersBalance());

        [HttpGet("GetInvestorBalance/{id}")]
        public IActionResult GetInvestorBalance(int id) => Ok(_farmerService.GetInvestorBalance(id));

        [HttpGet("GetInvestorsBalance")]
        public IActionResult GetInvestorsBalance() => Ok(_farmerService.GetInvestorsBalance());

        [HttpGet("GetProductsBalance")]
        public IActionResult GetProductsBalance() => Ok(_farmerService.GetProductsIncome());
    }
}