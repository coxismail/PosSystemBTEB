using PosSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace PosSystem.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationDbContext context;
        public SalesRepository()
        {
            context = new ApplicationDbContext();
        }

        public Sales Details(int id)
        {
           return context.Sales.Where(f => f.Id == id).Include(f=>f.Details).SingleOrDefault();
        }

        public bool IsProductAvailable(int storeid, int productid, int qty)
        {
            var inp = context.InProducts.Where(f => f.StoreId == storeid && f.ProductId == productid)?.ToList().Sum(f => f.InQuantity) ?? 0;
            var outp = context.OutProducts.Where(f => f.StoreId == storeid && f.ProductId == productid)?.ToList().Sum(f => f.OutQuantity) ?? 0;
            if (inp - outp >= qty)
            {
                return true;
            }
            return false;
        }

        public Product Product(int id)
        {
            return context.Products.Where(f => f.Id == id).SingleOrDefault();
        }

        public List<ListItem> ProductsList()
        {
            return context.Products.OrderBy(f => f.Title).ToList().Select(s => new ListItem
            {
                Text = "Cat: " + s.Category.Name + " - Br: " + s.Brand.Name + " " + s.Title + " Code: " + s.Code,
                Value = s.Id.ToString()
            }).ToList();
        }

        public List<Sales> Reports()
        {
          return  context.Sales.Include(f => f.Details).OrderByDescending(s=>s.SalesDate).ToList();
        }

        public void Save(Sales model, List<SalesDetails> data)
        {
            using (var save = context.Database.BeginTransaction())
            {
                try
                {
                    context.Sales.Add(model);
                    context.SaveChanges();
                    foreach (var item in data)
                    {
                        var amount = item.Rate * item.Quantity;

                        var details = new SalesDetails()
                        {
                            SalesId = model.Id,
                            Quantity = item.Quantity,
                            ProductId = item.Product.Id,
                            Rate = item.Rate,
                            TotalAmount = item.TotalAmount
                        };
                        context.SalesDetails.Add(details);

                        var inp = new OutGoingProducts()
                        {
                            Id = Guid.NewGuid(),
                            OutQuantity = item.Quantity,
                            ProductId = item.Product.Id,
                            StoreId = model.StoreId,
                            Amount = item.TotalAmount,
                            TransDate = model.SalesDate,
                            Type = ProdTransType.Sale,
                        };
                        context.OutProducts.Add(inp);
                        context.SaveChanges();
                    }


                    //   context.SaveChanges();
                    save.Commit();
                }
                catch (Exception ex)
                {
                    save.Rollback();
                    throw;
                }
            }
        }

        public List<ListItem> StoreList()
        {
            var stores = context.Store.OrderBy(f => f.Name).ToList().Select(s => new ListItem
            {
                Text = s.Name + " -Loc: " + s.Address,
                Value = s.Id.ToString()
            }).ToList();
            return stores;
        }
    }
}