using PosSystem.Models;
using System.Collections.Generic;

namespace PosSystem.Repository
{
    public interface IProductRepository
    {
        Product GetSingle(int Id);
        void Create(Product model);
        void Update(Product model);
        bool TryDelete(int Id);
        List<ProductViewModel> List();
        bool IsExist(Product model);

        List<Brand> BrandList();
        List<Category> CategoryList();
    }
}