using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
	public class ProductRepository : IProductRepository
	{
		private FarmerDbContext _dbContext;
		private IMapper _mapper;
        private IUserRepository _userRepository;
        private User _user;

        public ProductRepository(IUserRepository userRepository,
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

        public List<Product> GetAll() => _user.Products.ToList();

		public void Add(Product product)
		{
            product.UserId = _user.Id;

            _dbContext.Products.Add(product);
			_dbContext.SaveChanges();
		}

		public void Remove(int id)
        {
            _dbContext.Products.Remove(_dbContext.Products.SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Product GetById(int id) => _user.Products.SingleOrDefault(x => x.Id == id);

        public void Update(Product product)
        {
            product.UserId = _user.Id;
			var productToUpdate = _dbContext.Products.SingleOrDefault(x => x.Id == product.Id);

            _mapper.Map(product, productToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

