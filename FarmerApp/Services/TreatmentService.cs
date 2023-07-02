using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
	public class TreatmentService : ITreatmentService
	{
		private ITreatmentRepository _treatmentRepository;

		public TreatmentService(ITreatmentRepository treatmentRepository)
		{
			_treatmentRepository = treatmentRepository;
        }

        public void SetUser(int userId)
        {
            _treatmentRepository.SetUser(userId);
        }

        public List<Treatment> GetAll() => _treatmentRepository.GetAll();
		public void Add(Treatment treatment) => _treatmentRepository.Add(treatment);

		public void Remove(int id) => _treatmentRepository.Remove(id);

		public void Update(Treatment treatment) => _treatmentRepository.Update(treatment);

        public Treatment GetById(int id) => _treatmentRepository.GetById(id);
    }
}

