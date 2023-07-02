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
        private IUserRepository _userRepository;
        private User _user;

        public ExpenseRepository(IUserRepository userRepository,
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

        public List<Expense> GetAll() => _user.Expenses.ToList();

        public void Add(Expense expense)
        {
            expense.UserId = _user.Id;
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
        }

        public void Remove(int Id)
        {
            _dbContext.Expenses.Remove(_dbContext.Expenses.SingleOrDefault(x => x.Id == Id));
            _dbContext.SaveChanges();
        }

        public IEnumerable<Expense> GetByPurpose(string purpose) => _user.Expenses.Where(x => x.ExpensePurpose.ToLower().Contains(purpose.ToLower()));

        public Expense GetById(int Id) => _user.Expenses.SingleOrDefault(x => x.Id == Id);

        public void Update(Expense expense)
        {
            expense.UserId = _user.Id;
            var expenseToUpdate = _user.Expenses.SingleOrDefault(x => x.Id == expense.Id);

            _mapper.Map(expense, expenseToUpdate);

            _dbContext.SaveChanges();
        }
    }
}

