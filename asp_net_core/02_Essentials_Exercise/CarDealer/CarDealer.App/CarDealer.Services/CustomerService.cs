namespace CarDealer.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Car;
    using Models.Customer;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext dbContext;

        public CustomerService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string name, DateTime birthDate)
        {
            this.dbContext
            .Add(new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = true,
            });

            dbContext.SaveChanges();
        }

        public CustomerModel Find(int id)
        {
            var customers = this.dbContext
                .Customers
                .AsQueryable();

            if (!customers.Any(c => c.Id == id))
            {
                return null;
            }

            return customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate
                }).FirstOrDefault();
        }

        public CustomerSalesModel FindSales(int id)
        {
            return this.dbContext
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerSalesModel
                {
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    BoughtCars = c.Sales.Select(s => new CarPriceModel
                    { 
                        Price = s.Car.Parts.Sum(p => p.Part.Price),
                        Discount = s.Discount
                    }).ToList()
                }).FirstOrDefault();
        }

        public ICollection<CustomerModel> OrderedCustomers(OrderType order)
        {
            var customers = this.dbContext.Customers.AsQueryable();

            switch (order)
            {
                case OrderType.Ascending:
                    customers = customers
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);
                    break;
                case OrderType.Descending:
                    customers = customers
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}");
            }

            return customers
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();
        }
    }
}
