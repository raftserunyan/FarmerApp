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
    public class SalesController : ControllerBase
    {
        private ISaleService _saleService;
        private IMapper _mapper;

        public SalesController(
            IMapper mapper,
            ISaleService saleService)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sales = _saleService.GetAll();

            var Responses = new List<SaleResponseModel>();

            foreach (var sale in sales)
            {
                Responses.Add(_mapper.Map<SaleResponseModel>(sale));
            }

            return Ok(Responses);
        }

        [HttpPost]
        public IActionResult Add(SaleRequestModel saleRequest)
        {
            var sale = _mapper.Map<Sale>(saleRequest);

            _saleService.Add(sale);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            _saleService.Remove(id);
            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id) => Ok(_mapper.Map<SaleResponseModel>(_saleService.GetById(id)));

        [HttpGet("GetByProductId")]
        public IActionResult GetByProductId(int id)
        {
            var sales = _saleService.GetSalesByProductId(id);

            var Responses = new List<SaleResponseModel>();

            foreach (var sale in sales)
            {
                Responses.Add(_mapper.Map<SaleResponseModel>(sale));
            }

            return Ok(Responses);
        }

        [HttpPut]
        public IActionResult UpdateSale(int id, SaleRequestModel saleRequest){
            var saleToUpdate = _mapper.Map<Sale>(saleRequest);
            saleToUpdate.Id = id;

            _saleService.Update(saleToUpdate);
            return Ok();
        }
    }

}