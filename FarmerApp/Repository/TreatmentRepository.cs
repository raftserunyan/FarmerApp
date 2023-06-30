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

		public TreatmentRepository(
            IMapper mapper,
            FarmerDbContext dbContext)
		{
            _mapper = mapper;
			_dbContext = dbContext;
        }

		public List<Treatment> GetAll() => _dbContext.Treatments.ToList();

		public void Add(Treatment treatment)
		{
			_dbContext.Treatments.Add(treatment);
			_dbContext.SaveChanges();
		}

		public void Remove(int id)
        {
            _dbContext.Treatments.Remove(_dbContext.Treatments.SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Treatment GetById(int id) => _dbContext.Treatments.SingleOrDefault(x => x.Id == id);

        public void Update(Treatment treatment)
        {
           var treatmentToUpdate = _dbContext.Treatments.SingleOrDefault(x => x.Id == treatment.Id);

            _mapper.Map(treatment, treatmentToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

