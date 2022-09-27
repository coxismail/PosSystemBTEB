using PosSystem.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PosSystem.Repository
{
    interface IInentoryRepository
    {
        IList<InventoryViewModel> Balance();
        IList<InventoryViewModel> BalanceByStore(List<int> stores);
        IList<InventoryViewModel> BalanceBySingleStore(int stores);
        IList<InventoryViewModel> BalancebyBrand(List<int> brands);
        IList<InventoryViewModel> BalanceByCategory(List<int> categoris);
        IList<InventoryViewModel> BalanceByStoreandBrand(List<int> stores, List<int> brands);
        IList<InventoryViewModel> BalanceByStoreandCat(List<int> stores, List<int> categoris);
        IList<InventoryViewModel> BalanceByBrandandCategory(List<int> brands, List<int> categoris);
        IList<InventoryViewModel> BalanceByAll(List<int> stores, List<int> brands, List<int> categoris);
        List<Store> StoreList();
        List<ListItem> StoreListItems();
        List<Brand> BrandList();
        List<Category> CategoryList();
        bool IsProductAvailable(int storeid, int productid, int qty);
        void Transfer(InCommingProducts inProducts, OutGoingProducts outGoingProducts);
    }
}
