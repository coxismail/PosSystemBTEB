using PosSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace PosSystem.Repository
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository()
        {
            context = new ApplicationDbContext();
        }
        public void Create(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public Category GetById(int Id)
        {
          return  context.Categories.Where(f => f.Id == Id).SingleOrDefault();
        }
        public void  Update(Category category) 
        {
            context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public bool IsExist(Category category)
        {
            return context.Categories.Where(f => f.Name.ToUpper() == category.Name.ToUpper() && f.Id != category.Id).Any();
        }
        public IList<Category> List()
        {
            return context.Categories.ToList();
        }

    }
}