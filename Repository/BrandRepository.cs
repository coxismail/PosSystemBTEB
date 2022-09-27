using PosSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace PosSystem.Repository
{
    public class BrandRepository
    {
        private readonly ApplicationDbContext context;
        public BrandRepository()
        {
            context = new ApplicationDbContext();
        }
        public void Create(Brand brand)
        {
            context.Brands.Add(brand);
            context.SaveChanges();
        }
        public void Update (Brand brand) 
        {

            context.Entry(brand).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public bool IsExist(Brand brand)
        {
            return context.Brands.Where(f => f.Name.ToUpper() == brand.Name.ToUpper() && f.Id != brand.Id).Any();
        }
        public IList<Brand> List()
        {
            return context.Brands.ToList();
        }

    }
}