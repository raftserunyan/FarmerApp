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
        public InvestorRepository(
            IMapper mapper,
            FarmerDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<Investor> GetAll() => _dbContext.Investors.ToList();

        public void Add(Investor investor)
        {
            _dbContext.Investors.Add(investor);
            _dbContext.SaveChanges();
        }

        public void Remove(int Id)
        {
            _dbContext.Investors.Remove(_dbContext.Investors.SingleOrDefault(x => x.Id == Id));
            _dbContext.SaveChanges();
        }

        public Investor GetById(int Id) => _dbContext.Investors.SingleOrDefault(x => x.Id == Id);

        public void Update(Investor investor)
        {
			var investorToUpdate = _dbContext.Investors.SingleOrDefault(x => x.Id == investor.Id);

            _mapper.Map(investor, investorToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

