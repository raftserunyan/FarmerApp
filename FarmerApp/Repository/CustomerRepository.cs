using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;

namespace FarmerApp.Services
{
    public class CustomerRepository : ICustomerRepository
	{
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private User _user;        

        public CustomerRepository(IUserRepository userRepository,
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

        public List<Customer> GetAll() => _user.Customers.ToList();

        public void Add(Customer customer)
        {
            customer.UserId = _user.Id;
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
            customer.UserId = _user.Id;
            var customerToUpdate = _dbContext.Customers.SingleOrDefault(x => x.Id == customer.Id);

            _mapper.Map(customer, customerToUpdate);

            _dbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetCustomersByLocation(string address) => _user.Customers
            .Where(x => x.Address.ToLower().Contains(address.ToLower()));

        public Customer GetById(int id) => _user.Customers.SingleOrDefault(x => x.Id == id);

    }
}