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
        private IUserRepository _userRepository;
        private User _user;

        public SaleRepository(IUserRepository userRepository,
            FarmerDbContext dbContext,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void SetUser(int userId)
        {
            _user = _userRepository.GetById(userId);
        }

        public List<Sale> GetAll() => _user.Sales.ToList();

        public void Add(Sale sale)
        {
            sale.UserId = _user.Id;
            _dbContext.Sales.Add(sale);
			_dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            _dbContext.Sales.Remove(_dbContext.Sales.SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Sale GetById(int id) => _user.Sales.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Sale> GetSalesByProductId(int id) => _user.Sales.Where(x => x.ProductId == id);

        public IEnumerable<Sale> GetSalesByCustomerId(int id) => _user.Sales.Where(x => x.CustomerId == id);

        public void Update(Sale sale)
        {
            sale.UserId = _user.Id;
            var saleToUpdate = _dbContext.Sales.SingleOrDefault(x => x.Id == sale.Id);

            _mapper.Map(sale, saleToUpdate);
            var check = saleToUpdate;
            _dbContext.SaveChanges();
        }
    }
}

