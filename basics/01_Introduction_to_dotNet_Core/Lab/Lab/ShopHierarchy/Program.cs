using Microsoft.EntityFrameworkCore;
using ShopHierarchy.Models;
using System;
using System.Linq;

namespace ShopHierarchy
{
    class Program
    {
        private const string END_COMMAND = "END";
        private const string REGISTER_COMMAND = "REGISTER";
        private const string ORDER_COMMAND = "ORDER";
        private const string REVIEW_COMMAND = "REVIEW";
        static void Main(string[] args)
        {
            using (ShopDbContext db = new ShopDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                RunCommands(db);
                PrintAllSalesmanWithCustomerCount(db);
                PrintAllCustomersWithOrderAndReviewCount(db);
            }
        }

        private static void RunCommands(ShopDbContext db)
        {
            string[] salesmanInput = Console.ReadLine().Split(';');
            CreateSalesmanCommand(db, salesmanInput);

            string commandInput;

            while (!(commandInput = Console.ReadLine()).Equals(END_COMMAND))
            {
                CreateItemCommand(db, commandInput.Split(';'));
            }

            while (!(commandInput = Console.ReadLine()).Equals(END_COMMAND))
            {
                string[] commandArgs = commandInput.Split(new char[] { '-', ';' }, StringSplitOptions.RemoveEmptyEntries);
                string commandName = commandArgs[0];

                switch (commandName.ToUpper())
                {
                    case REGISTER_COMMAND:
                        RegisterCustomerCommand(db, commandArgs);
                        break;
                    case ORDER_COMMAND:
                        CreateOrderCommand(db, commandArgs);
                        break;
                    case REVIEW_COMMAND:
                        CreateReviewCommand(db, commandArgs);
                        break;
                }
            }

            int customerId = int.Parse(Console.ReadLine());
            PrintCustomerInformation(db, customerId);
            PrintCustomerOrdersCountWithMoreThanOneItem(db, customerId);
        }
        private static void CreateSalesmanCommand(ShopDbContext db, string[] salesmanInput)
        {
            foreach (string salesmanName in salesmanInput)
            {
                Salesman salesman = new Salesman { Name = salesmanName };
                db.Add(salesman);
                db.SaveChanges();
            }
        }

        private static void RegisterCustomerCommand(ShopDbContext db, string[] customerArgs)
        {
            string customerName = customerArgs[1];
            int salesmanId = int.Parse(customerArgs[2]);
            Customer customer = new Customer { Name = customerName, SalesmanId = salesmanId };
            db.Add(customer);
            db.SaveChanges();
        }

        private static void CreateOrderCommand(ShopDbContext db, string[] orderArgs)
        {
            int customerId = int.Parse(orderArgs[1]);
            Order order = new Order { CustomerId = customerId };

            for (int i = 2; i < orderArgs.Length; i++)
            {
                int itemId = int.Parse(orderArgs[i]);

                order.Items.Add(new OrderItem
                {
                    ItemId = itemId
                });
            }

            db.Add(order);
            db.SaveChanges();
        }

        private static void CreateReviewCommand(ShopDbContext db, string[] reviewArgs)
        {
            int customerId = int.Parse(reviewArgs[1]);
            int itemId = int.Parse(reviewArgs[2]);
            Review review = new Review { CustomerId = customerId, ItemId = itemId };
            db.Add(review);
            db.SaveChanges();
        }

        private static void CreateItemCommand(ShopDbContext db, string[] itemArgs)
        {
            string itemName = itemArgs[0];
            double itemPrice = double.Parse(itemArgs[1]);

            Item item = new Item { Name = itemName, Price = itemPrice };
            db.Add(item);
            db.SaveChanges();
        }

        private static void PrintAllSalesmanWithCustomerCount(ShopDbContext db)
        {
            var salesmen = db.Salesmens
                .Include(s => s.Customers)
                .OrderByDescending(s => s.Customers.Count)
                .ThenBy(s => s.Name);

            foreach (Salesman salesman in salesmen)
            {
                Console.WriteLine($"{salesman.Name} - {salesman.Customers.Count} customers");
            }
        }

        private static void PrintAllCustomersWithOrderAndReviewCount(ShopDbContext db)
        {
            var customers = db.Customers
                .OrderByDescending(c => c.Orders.Count)
                .ThenByDescending(c => c.Reviews.Count);

            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.Name);
                Console.WriteLine($"Orders: {customer.Orders.Count}");
                Console.WriteLine($"Reviews: {customer.Reviews.Count}");
            }
        }

        private static void PrintCustomerInformation(ShopDbContext db, int customerId)
        {
            Customer customer = db.Customers
                .Where(c => c.Id == customerId)
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine($"Orders count: {customer.Orders.Count}");

            foreach (Order order in customer.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.Items.Count} items");
            }

            Console.WriteLine($"Reviews counts: {customer.Reviews.Count}");
            Console.WriteLine($"Salesman: {customer.Salesman.Name}");
        }

        private static void PrintCustomerOrdersCountWithMoreThanOneItem(ShopDbContext db, int customerId)
        {
            Customer customer = db.Customers
                .Where(c => c.Id == customerId)
                .FirstOrDefault();

            Console.WriteLine($"Orders: {customer.Orders.Where(o => o.Items.Count > 1).Count()}");
        }
    }
}
