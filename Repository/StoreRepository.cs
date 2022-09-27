using PosSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace PosSystem.Repository
{
    public class StoreRepository
    {
        private readonly ApplicationDbContext context;
        public StoreRepository()
        {
            context = new ApplicationDbContext();
        }
        public void Create(Store  store) {
            context.Store.Add(store);
            context.SaveChanges();
        }

        public void  Update(Store store) 
        {
            context.Entry(store).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public bool IsExist(Store store) 
        {
          return  context.Store.Where(f => f.Name.ToUpper() == store.Name.ToUpper() && f.Id != store.Id).Any();
        }
        public IList<Store> List() 
        {
          return  context.Store.ToList();
        }
    }
}