using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository _expenseRepository;
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public List<Expense> GetAll() => _expenseRepository.GetAll();

        public void Add(Expense expense) => _expenseRepository.Add(expense);

        public void Remove(int id) => _expenseRepository.Remove(id);

        public IEnumerable<Expense> GetByPurpose(string purpose) => _expenseRepository.GetByPurpose(purpose);

        public Expense GetById(int id) => _expenseRepository.GetById(id);

        public void Update(Expense expense) => _expenseRepository.Update(expense);
    }
}