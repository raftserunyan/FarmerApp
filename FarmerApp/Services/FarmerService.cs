using System.Runtime.CompilerServices;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class FarmerService : IFarmerService
    {
        private ISaleRepository _saleRepository;
        private IInvestorRepository _investorRepository;
        private IExpenseRepository _expenseRepository;
        private IProductRepository _productRepository;

        public FarmerService(
            IProductRepository productRepository,
            IExpenseRepository expenseRepository,
            IInvestorRepository investorRepository,
            ISaleRepository saleRepository)
        {
            _productRepository = productRepository;
            _expenseRepository = expenseRepository;
            _investorRepository = investorRepository;
            _saleRepository = saleRepository;
        }

        public Balance GetBalance()
        {
            var sales = _saleRepository.GetAll();
            var investors = _investorRepository.GetAll();
            var expenses = _expenseRepository.GetAll();

            var balance = new Balance();

            var payed = sales.Sum(x => x.Payed);

            balance.Leftover += payed;
            balance.Leftover += investors.Sum(x => x.InvestedAmount);
            balance.Leftover -= expenses.Sum(x => x.ExpenseAmount);

            balance.Debt = (int)sales.Sum(x=> x.PriceKG * x.Weight) - payed;

            return balance;
        }

        public Balance GetBalance(int id)
        {
            var sales = _saleRepository.GetSalesByCustomerId(id);

            var balance = new Balance
            {
                Leftover = sales.Sum(x => x.Payed),
                Debt = (int)sales.Sum(x=> x.PriceKG * x.Weight) - sales.Sum(x => x.Payed)
            };

            return balance;
        }

        public Balance GetInvestorBalance(int id)
        {
            var investor = _investorRepository.GetById(id);
            var expenses = _expenseRepository.GetAll();

            var balance = new Balance
            {
                Leftover = investor.InvestedAmount - expenses.Where(x => x.IsFromInvestor).Sum(x => x.ExpenseAmount)
            };

            return balance;
        }

        public Balance GetInvestorBalance(Investor investor)
        {
            var expenses = _expenseRepository.GetAll();

            var balance = new Balance
            {
                Leftover = investor.InvestedAmount - expenses.Where(x => x.IsFromInvestor).Sum(x => x.ExpenseAmount)
            };

            return balance;
        }

        public ProductBalanceResponseModel GetBalanceByProductId(int id)
        {
            var sales = _saleRepository.GetSalesByProductId(id);

            var balance = new ProductBalanceResponseModel
            {
                Weight = sales.Sum(x=> x.Weight),
                Leftover = sales.Sum(x => x.Payed),
                Debt = (int)sales.Sum(x=> x.PriceKG * x.Weight) - sales.Sum(x => x.Payed)
            };

            return balance;
        }

        public Balance GetBalanceByCustomerId(int id)
        {
            var sales = _saleRepository.GetSalesByCustomerId(id);

            var balance = new Balance
            {
                Leftover = sales.Sum(x => x.Payed),
                Debt = (int)sales.Sum(x=> x.PriceKG * x.Weight) - sales.Sum(x => x.Payed)
            };

            return balance;
        }

        public Dictionary<string, object> GetProductsIncome()
        {
            Dictionary<string, object> incomes = new Dictionary<string, object>();

            var products = _productRepository.GetAll();

            foreach (var product in products)
            {
                var balance = GetBalanceByProductId(product.Id);

                incomes.Add(product.Name, balance);
            }

            return incomes;
        }

        public Dictionary<string, object> GetInvestorsBalance()
        {
            Dictionary<string, object> balances = new Dictionary<string, object>();

            var investors = _investorRepository.GetAll();

            foreach (var investor in investors)
            {
                var balance = GetInvestorBalance(investor);

                balances.Add(investor.Name, balance);
            }

            return balances;
        }

        public IEnumerable<CustomerBalanceResponseModel> GetCustomersBalance()
        {
            List<CustomerBalanceResponseModel> balances = new List<CustomerBalanceResponseModel>();

            var sales = _saleRepository.GetAll();

            foreach (var sale in sales)
            {
                var balance = GetBalanceByCustomerId(sale.CustomerId);

                balances.Add(new CustomerBalanceResponseModel
                {
                    CustomerId = sale.CustomerId,
                    Leftover = balance.Leftover,
                    Debt = balance.Debt
                });
            }

            return balances.DistinctBy(x => x.CustomerId);
        }

        // public List<Dictionary<string, object>> GetProductsIncome(){
            // List<Dictionary<string, object>> incomes = new List<Dictionary<string, object>>();

            // var products = _productRepository.GetAll();

            // foreach (var product in products)
            // {
            //     var balance = GetBalanceByProductId(product.Id);

            //     Dictionary<string, object> dict = new Dictionary<string, object>();

            //     dict.Add("Product", product.Name);
            //     dict.Add("Balance", balance);

            //     incomes.Add(dict);
            // }

            // return incomes;
        //}
    }
}