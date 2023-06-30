using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
	internal class SaleService : ISaleService
	{
		private ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public List<Sale> GetAll() => _saleRepository.GetAll();

        public void Add(Sale sale)
        {
            _saleRepository.Add(sale);
        }

        public void Remove(int id)
        {
            _saleRepository.Remove(id);
        }

        public Sale GetById(int id) => _saleRepository.GetById(id);

        public IEnumerable<Sale> GetSalesByProductId(int id) => _saleRepository.GetSalesByProductId(id);

        public IEnumerable<Sale> GetSalesByCustomerId(int id) => _saleRepository.GetSalesByCustomerId(id);

        public void Update(Sale sale) => _saleRepository.Update(sale);
    }
}

