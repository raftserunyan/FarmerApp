using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;

namespace FarmerApp.Repository
{
	public class TreatmentRepository : ITreatmentRepository
	{
		private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private User _user;

        public TreatmentRepository(IUserRepository userRepository,
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

        public List<Treatment> GetAll() => _user.Treatments.ToList();

		public void Add(Treatment treatment)
		{
            treatment.UserId = _user.Id;

            _dbContext.Treatments.Add(treatment);
			_dbContext.SaveChanges();
		}

		public void Remove(int id)
        {
            _dbContext.Treatments.Remove(_user.Treatments.SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Treatment GetById(int id) => _user.Treatments.SingleOrDefault(x => x.Id == id);

        public void Update(Treatment treatment)
        {
            treatment.UserId = _user.Id;
           var treatmentToUpdate = _dbContext.Treatments.SingleOrDefault(x => x.Id == treatment.Id);

            _mapper.Map(treatment, treatmentToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

