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

		public ProductRepository(
			IMapper mapper,
			FarmerDbContext dbContext)
		{
			_mapper = mapper;
			_dbContext = dbContext;
        }

		public List<Product> GetAll() => _dbContext.Products.Include(x => x.Sales).ToList();

		public void Add(Product product)
		{
			_dbContext.Products.Add(product);
			_dbContext.SaveChanges();
		}

		public void Remove(int id)
        {
            _dbContext.Products.Remove(_dbContext.Products.SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Product GetById(int id) => _dbContext.Products.Include(x => x.Sales).SingleOrDefault(x => x.Id == id);

        public void Update(Product product)
        {
			var productToUpdate = _dbContext.Products.SingleOrDefault(x => x.Id == product.Id);

            _mapper.Map(product, productToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

