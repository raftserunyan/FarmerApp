using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;

namespace FarmerApp.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;

        public ExpenseRepository(
            IMapper mapper,
            FarmerDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<Expense> GetAll() => _dbContext.Expenses.ToList();

        public void Add(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
        }

        public void Remove(int Id)
        {
            _dbContext.Expenses.Remove(_dbContext.Expenses.SingleOrDefault(x => x.Id == Id));
            _dbContext.SaveChanges();
        }

        public IEnumerable<Expense> GetByPurpose(string purpose) => _dbContext.Expenses.Where(x => x.ExpensePurpose.ToLower().Contains(purpose.ToLower()));

        public Expense GetById(int Id) => _dbContext.Expenses.SingleOrDefault(x => x.Id == Id);

        public void Update(Expense expense)
        {
            var expenseToUpdate = _dbContext.Expenses.SingleOrDefault(x => x.Id == expense.Id);

            _mapper.Map(expense, expenseToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

