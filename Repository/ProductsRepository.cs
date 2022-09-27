using PosSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace PosSystem.Repository
{
    public class ProductsRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductsRepository()
        {
            context = new ApplicationDbContext();
        }

        public Product GetSingle(int Id)
        {
            return context.Products.Where(f => f.Id == Id).SingleOrDefault();
        }
        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
        public bool TryDelete(int id)
        {
            var inp = context.InProducts.Where(f => f.ProductId == id).ToList();
            var outp = context.OutProducts.Where(f => f.ProductId == id).ToList();
            if (inp.Count == 0 && outp.Count == 0)
            {
                var p = context.Products.Where(f => f.Id == id).SingleOrDefault();
                context.Products.Remove(p);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool IsExist(Product product)
        {
            return context.Products.Where(f => f.Title.ToUpper() == product.Title.ToUpper() && f.CategoryId == product.CategoryId && f.BrandId == product.BrandId && f.Id != product.Id).Any();
        }

        public void Update(Product model)
        {
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }



        public List<ProductViewModel> List()
        {
            var result = context.Products.Include(f=>f.Category).Include(f=>f.Brand).ToList().Select(f => new ProductViewModel
            {
                Title = f.Title,
                BrandName = f.Brand.Name,
                CategoryName = f.Category.Name,
                Price = f.UnitPrice,
                Code = f.Code,
                Description = f.Description,
                Id = f.Id
            }).ToList();
            return result;
        }

        public List<Brand> BrandList()
        {
            return context.Brands.ToList();
        }

        public List<Category> CategoryList()
        {
            return context.Categories.ToList();
        }
    }


}