using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers
                .Where(customer => customer.Orders.Sum(order => order.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers
                .Select(customer => (customer, suppliers.Where(supplier => supplier.City.Equals(customer.City, StringComparison.OrdinalIgnoreCase) && supplier.Country.Equals(customer.Country, StringComparison.OrdinalIgnoreCase))));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            var suppliersGroup = suppliers
                .GroupBy(supplier => new { supplier.City, supplier.Country }, (supplierAddress, supplier) => new { Address = supplierAddress, Suppliers = supplier });

            return customers
                .Select(customer => (customer, suppliersGroup.FirstOrDefault(supplier => supplier.Address.City.Equals(customer.City, StringComparison.OrdinalIgnoreCase) && supplier.Address.Country.Equals(customer.Country, StringComparison.OrdinalIgnoreCase))?.Suppliers ?? Enumerable.Empty<Supplier>()));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers
                .Where(customer => (customer.Orders.Any() ? customer.Orders.Max(order => order.Total) : -1) > limit);
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers
                .Where(customer => customer.Orders.Any())
                .Select(customer => (customer, customer.Orders.Min(order => order.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return Linq4(customers)
                .OrderBy(customer => customer.dateOfEntry.Year)
                .ThenBy(customer => customer.dateOfEntry.Month)
                .ThenByDescending(customer => customer.customer.Orders.Sum(order => order.Total))
                .ThenBy(customer => customer.customer.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            var nonDigitPostalCodeRegex = new Regex(@"\D+");
            var haveOperatorCodeRegex = new Regex(@"\(\d+\)");

            return customers
                .Where(customer => nonDigitPostalCodeRegex.IsMatch(customer.PostalCode) || customer.Phone == null || (customer.Phone != null && !haveOperatorCodeRegex.IsMatch(customer.Phone)) || customer.Fax == null || (customer.Fax != null && !haveOperatorCodeRegex.IsMatch(customer.Fax)) || customer.Region == null);
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return products
                .GroupBy(product => product.Category, (category, products) => new Linq7CategoryGroup { Category = category, UnitsInStockGroup = products.GroupBy(product => product.UnitsInStock, (unitsInStock, products) => new Linq7UnitsInStockGroup { UnitsInStock = unitsInStock, Prices = products.Select(p => p.UnitPrice) }) });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            Func<decimal, decimal> findCategory = (price) => price <= cheap ? cheap : price <= middle ? middle : expensive;

            return products
                .GroupBy(product => findCategory(product.UnitPrice), (category, products) => (category, products));
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers
                .GroupBy(customer => customer.City, (city, customers) => (city, (int)customers.Average(customer => Math.Round(customer.Orders.Sum(order => order.Total))), (int)customers.Average(customer => customer.Orders.Count())));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null)
            {
                throw new ArgumentNullException(nameof(suppliers));
            }
            
            return string.Join(
                "", 
                suppliers
                    .Select(supplier => supplier.Country)
                    .Distinct()
                    .OrderBy(country => country.Length)
                    .ThenBy(country => country));
        }
    }
}