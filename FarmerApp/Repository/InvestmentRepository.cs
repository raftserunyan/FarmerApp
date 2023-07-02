using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;

namespace FarmerApp.Repository
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private User _user;

        public InvestmentRepository(IUserRepository userRepository,
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

        public List<Investment> GetAll() => _user.Investors.SelectMany(x => x.Investments).ToList();

        public void Add(Investment investment)
        {
            _dbContext.Investments.Add(investment);
            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            _dbContext.Investments.Remove(_dbContext.Investments.SingleOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public void Update(Investment investment)
        {
            var investmentToUpdate = _dbContext.Investments.SingleOrDefault(x => x.Id == investment.Id);

            _mapper.Map(investment, investmentToUpdate);

            _dbContext.SaveChanges();
        }

        public Investment GetById(int id) => _dbContext.Investments.SingleOrDefault(x => x.Id == id);

    }
}
