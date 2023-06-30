﻿using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
	public class ProductService : IProductService
	{
		private IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
        }

		public List<Product> GetAll() => _productRepository.GetAll();

		public void Add(Product product) => _productRepository.Add(product);

		public void Remove(int id)
		{
			var product = _productRepository.GetById(id);

			if(!product.Sales.Any())
		 		_productRepository.Remove(id);
			else
				throw new Exception("Unable to Remove Product");
		}

        public Product GetById(int id) => _productRepository.GetById(id);

        public void Update(Product product) => _productRepository.Update(product);
    }
}

