using PosSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace PosSystem.Repository
{
    public class ReportService
    {
        private readonly ApplicationDbContext context;
        public ReportService()
        {
            context = new ApplicationDbContext();
        }

        internal IEnumerable<object> Customers()
        {
            var re = context.Sales.OrderByDescending(f => f.SalesDate).Take(10).ToList().Select(s => new
            {
                Name = s.CustomerName,
                Date = s.SalesDate.ToString("dd-MM-yyyy"),
                Amount = s.Details.Sum(f => f.TotalAmount)
            }).ToList();
            return re;
        }
        internal IEnumerable<object> Suppliers()
        {
            var re = context.Purchase.OrderByDescending(f => f.PurchaseDate).Take(10).ToList().Select(s => new
            {
                Name = s.Supplier,
                Date = s.PurchaseDate.ToString("dd-MM-yyyy"),
                Amount = s.Details.Sum(f => f.Rate * f.Quantity)
            }).ToList();
            return re;
        }
        internal IEnumerable<object> Sales()
        {
            var re = context.Sales.OrderByDescending(f => f.SalesDate).GroupBy(f => f.SalesDate, (x, y) => new { key = x, data = y })
                .ToList()
                .Take(10).Select(f => new
                {
                    Date = f.key.ToString("dd-MM-yyyy"),
                    Amount = f.data.Sum(s => s.Details.Sum(a => a.TotalAmount))
                }).ToList();

            return re;
        }
        internal IEnumerable<object> Purchases()
        {
            var re = context.Purchase.OrderByDescending(f => f.PurchaseDate).GroupBy(f => f.PurchaseDate, (x, y) => new { key = x, data = y })
                .ToList()
                .Take(10).Select(f => new
                {
                    Date = f.key.ToString("dd-MM-yyyy"),
                    Amount = f.data.Sum(s => s.Details.Sum(a => a.Rate * a.Quantity))
                }).ToList();

            return re;
        }
    }
}