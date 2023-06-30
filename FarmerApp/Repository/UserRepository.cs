using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;

namespace FarmerApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;

        public UserRepository(
            IMapper mapper,
            FarmerDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<User> GetAll() => _dbContext.Users.ToList();

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            _dbContext.Users.Remove(_dbContext.Users.SingleOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public User GetById(int id) => _dbContext.Users.SingleOrDefault(x => x.Id == id);

        public void Update(User user)
        {
            var userToUpdate = _dbContext.Users.SingleOrDefault(x => x.Id == user.Id);

            _mapper.Map(user, userToUpdate);

            _dbContext.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            email = email.ToLower();
            return _dbContext.Users.SingleOrDefault(x => x.Email == email);
        }
    }
}
