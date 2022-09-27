using PosSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PosSystem.Repository
{
    public class InventoryRepository : IInentoryRepository
    {
        private readonly ApplicationDbContext db;
        public InventoryRepository()
        {
            db = new ApplicationDbContext();

        }
        public IList<InventoryViewModel> Balance()
        {
            var ind = db.InProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();

            var pro = db.Products.ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;
        }



        public IList<InventoryViewModel> BalanceByStore(List<int> stores)
        {
            var ind = db.InProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();

            var pro = db.Products.ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;
        }

        public IList<InventoryViewModel> BalanceBySingleStore(int storeId)
        {
            var ind = db.InProducts.Where(s => s.StoreId == storeId).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.Where(s => s.StoreId == storeId).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();
            List<InventoryViewModel> data = new List<InventoryViewModel>();
            var pro = db.Products.ToList();
            foreach (var f in pro)
            {
                var oup = outd.Where(s => s.id == f.Id)?.SingleOrDefault();
                var inp = ind.Where(s => s.id == f.Id)?.SingleOrDefault();
                var sda = new InventoryViewModel();
                sda.Code = f.Code;
                sda.ProductId = f.Id;
                sda.ProductTitle = f.Title;
                sda.Brand = f.Brand.Name;
                sda.Category = f.Category.Name;
                sda.Price = inp == null ? 0 : inp.Amount - (oup == null ? 0 : oup.Amount);
                sda.BalanceQ = inp==null? 0: inp.Quantity - (oup == null ? 0 : oup.Quantity) ;
                data.Add(sda);
            }

            return data;
        }
        public IList<InventoryViewModel> BalancebyBrand(List<int> brands)
        {
            var ind = db.InProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();

            var pro = db.Products.Where(s => brands.Contains(s.BrandId)).ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;
        }
        public IList<InventoryViewModel> BalanceByCategory(List<int> categoris)
        {
            var ind = db.InProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();

            var pro = db.Products.Where(s => categoris.Contains(s.CategoryId)).ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;
        }
        public IList<InventoryViewModel> BalanceByStoreandBrand(List<int> stores, List<int> brands)
        {
            var ind = db.InProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();

            var pro = db.Products.Where(s => brands.Contains(s.BrandId)).ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;
        }
        public IList<InventoryViewModel> BalanceByStoreandCat(List<int> stores, List<int> categoris)
        {
            var ind = db.InProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();
            var pro = db.Products.Where(s => categoris.Contains(s.CategoryId)).ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;

        }
        public IList<InventoryViewModel> BalanceByBrandandCategory(List<int> brands, List<int> categoris)
        {
            var ind = db.InProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();

            var pro = db.Products.Where(s => categoris.Contains(s.CategoryId) && brands.Contains(s.BrandId)).ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id).FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;
        }
        public IList<InventoryViewModel> BalanceByAll(List<int> stores, List<int> brands, List<int> categoris)
        {
            var ind = db.InProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, InData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.InData.Sum(k => k.InQuantity),
                Amount = m.InData.Sum(k => k.Amount)
            }).ToList();
            var outd = db.OutProducts.Where(s => stores.Contains(s.StoreId)).GroupBy(f => f.ProductId, (x, y) => new { ProductId = x, OutData = y }).Select(m => new
            {
                id = m.ProductId,
                Quantity = m.OutData.Sum(k => k.OutQuantity),
                Amount = m.OutData.Sum(k => k.Amount)
            }).ToList();
            var pro = db.Products.Where(s => categoris.Contains(s.CategoryId) && brands.Contains(s.BrandId)).ToList().Select(f => new InventoryViewModel
            {
                Code = f.Code,
                ProductId = f.Id,
                ProductTitle = f.Title,
                Brand = f.Brand.Name,
                Category = f.Category.Name,
                Price = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id)
                .FirstOrDefault().Amount) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Amount),
                BalanceQ = (ind.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : ind.Where(s => s.id == f.Id)
                .FirstOrDefault().Quantity) - (outd.Where(s => s.id == f.Id).FirstOrDefault() == null ? 0 : outd.Where(s => s.id == f.Id).FirstOrDefault().Quantity)
            }).ToList();
            return pro;
        }


        public List<Brand> BrandList()
        {
            return db.Brands.ToList();
        }
        public List<ListItem> StoreListItems()
        {
            var stores = db.Store.OrderBy(f => f.Name).ToList().Select(s => new ListItem
            {
                Text = s.Name + " -Loc: " + s.Address,
                Value = s.Id.ToString()
            }).ToList();
            return stores;
        }
        public List<Category> CategoryList()
        {
            return db.Categories.ToList();
        }

        public List<Store> StoreList()
        {
            return db.Store.ToList();
        }

        public void Transfer(InCommingProducts inProducts, OutGoingProducts outGoingProducts)
        {
            var pro = db.Products.Where(f => f.Id == inProducts.ProductId).SingleOrDefault();
            var amount = pro.UnitPrice * inProducts.InQuantity;
            inProducts.Amount = amount;
            inProducts.Id = Guid.NewGuid();
            outGoingProducts.Amount = amount;
            outGoingProducts.Id = Guid.NewGuid();
            db.InProducts.Add(inProducts);
            db.OutProducts.Add(outGoingProducts);
            db.SaveChanges();

        }

        public bool IsProductAvailable(int storeid, int productid, int qty)
        {
            var inp = db.InProducts.Where(f => f.StoreId == storeid && f.ProductId == productid)?.ToList().Sum(f => f.InQuantity) ?? 0;
            var outp = db.OutProducts.Where(f => f.StoreId == storeid && f.ProductId == productid)?.ToList().Sum(f => f.OutQuantity) ?? 0;
            if (inp - outp >= qty)
            {
                return true;
            }
            return false;
        }
    }
}