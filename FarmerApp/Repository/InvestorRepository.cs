using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;

namespace FarmerApp.Repository
{
    public class InvestorRepository : IInvestorRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private User _user;

        public InvestorRepository(IUserRepository userRepository,
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

        public List<Investor> GetAll() => _user.Investors.ToList();

        public void Add(Investor investor)
        {
            investor.UserId = _user.Id;
            _dbContext.Investors.Add(investor);
            _dbContext.SaveChanges();
        }

        public void Remove(int Id)
        {
            _dbContext.Investors.Remove(_dbContext.Investors.SingleOrDefault(x => x.Id == Id));
            _dbContext.SaveChanges();
        }

        public Investor GetById(int Id) => _user.Investors.SingleOrDefault(x => x.Id == Id);

        public void Update(Investor investor)
        {
            investor.UserId = _user.Id;
			var investorToUpdate = _dbContext.Investors.SingleOrDefault(x => x.Id == investor.Id);

            _mapper.Map(investor, investorToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

