using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Services
{
	public class CustomerRepository : ICustomerRepository
	{
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        public CustomerRepository(
            FarmerDbContext dbContext,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Customer> GetAll() => _dbContext.Customers.Include(x => x.Sales).ToList();

        public void Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        //TODO: If exists Sale with this customer.
            //Do the same with Product and return message.
        public void Remove(int id)
        {
            _dbContext.Customers.Remove(_dbContext.Customers.SingleOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var customerToUpdate = _dbContext.Customers.SingleOrDefault(x => x.Id == customer.Id);

            _mapper.Map(customer, customerToUpdate);

            _dbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetCustomersByLocation(string address) => _dbContext.Customers
            .Where(x => x.Address.ToLower().Contains(address.ToLower()));

        public Customer GetById(int id) => _dbContext.Customers.Include(x => x.Sales)
            .SingleOrDefault(x => x.Id == id);

    }
}