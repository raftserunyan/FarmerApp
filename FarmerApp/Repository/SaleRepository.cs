using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
	internal class SaleRepository : ISaleRepository
	{
		private FarmerDbContext _dbContext;
		private IMapper _mapper;

        public SaleRepository(
            IMapper mapper,
            FarmerDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<Sale> GetAll() => _dbContext.Sales.Include(x => x.CurrentCustomer).Include(x => x.CurrentProduct).ToList();

        public void Add(Sale sale)
        {
            _dbContext.Sales.Add(sale);
			_dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            _dbContext.Sales.Remove(_dbContext.Sales.SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Sale GetById(int id) => _dbContext.Sales.Include(x => x.CurrentCustomer).Include(x => x.CurrentProduct).SingleOrDefault(x => x.Id == id);

        public IEnumerable<Sale> GetSalesByProductId(int id) => _dbContext.Sales.Include(x => x.CurrentCustomer).Include(x => x.CurrentProduct).Where(x => x.ProductId == id);

        public IEnumerable<Sale> GetSalesByCustomerId(int id) => _dbContext.Sales.Include(x => x.CurrentCustomer).Include(x => x.CurrentProduct).Where(x => x.CustomerId == id);

        public void Update(Sale sale)
        {
            var saleToUpdate = _dbContext.Sales.SingleOrDefault(x => x.Id == sale.Id);

            _mapper.Map(sale, saleToUpdate);
            var check = saleToUpdate;
            _dbContext.SaveChanges();
        }
    }
}

