using PosSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PosSystem.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext context;
        public PurchaseRepository()
        {
            context = new ApplicationDbContext();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public Purchase Details(int Id)
        {
            return context.Purchase.Where(f => f.Id == Id).Include(fs => fs.Details).SingleOrDefault();

        }

        public Product Product(int Id)
        {
            return context.Products.Where(f => f.Id == Id).SingleOrDefault();
        }

        //public bool IsProductAvailable(int storeId, int ProductId, int quntity)
        //{
        //    var inp = context.InProducts.Where(f => f.StoreId == storeId && f.ProductId == ProductId)?.Sum(f => f.InQuantity) ?? 0;
        //    var outp = context.InProducts.Where(f => f.StoreId == storeId && f.ProductId == ProductId)?.Sum(f => f.InQuantity) ?? 0;
        //    if (inp - outp > quntity)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public List<ListItem> ProductsList()
        {
            return context.Products.OrderBy(f => f.Title).ToList().Select(s => new ListItem
            {
                Text =  "Cat: "+ s.Category.Name +" - Br: " + s.Brand.Name +" "+ s.Title + " Code: "+ s.Code,
                Value = s.Id.ToString()
            }).ToList();
        }

        public List<Purchase> Reports()
        {
            return context.Purchase.OrderByDescending(f => f.PurchaseDate).ToList();
        }

        public void Save(Purchase model, List<PurchaseDetails> data)
        {
            using (var save = context.Database.BeginTransaction())
            {
                try
                {
                    context.Purchase.Add(model);
                    context.SaveChanges();
                    foreach (var item in data)
                    {
                        var amount = item.Rate * item.Quantity;

                        var details = new PurchaseDetails()
                        {
                            PurchaseId = model.Id,
                            Quantity = item.Quantity,
                            ProductId = item.Product.Id,
                            Rate = item.Rate
                        };
                        context.PurchaseDetails.Add(details);

                        var inp = new InCommingProducts()
                        {
                            Id = Guid.NewGuid(),
                            InQuantity = item.Quantity,
                            ProductId = item.Product.Id,
                            StoreId = model.StoreId,
                            Amount = amount ,
                            TransDate = model.PurchaseDate,
                            Type = ProdTransType.Purchase,
                        };
                        context.InProducts.Add(inp);
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