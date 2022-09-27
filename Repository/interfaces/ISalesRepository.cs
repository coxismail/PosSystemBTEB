using PosSystem.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PosSystem.Repository
{
    interface ISalesRepository
    {
        void Save(Sales model, List<SalesDetails> data);
        List<ListItem> ProductsList();
        List<ListItem> StoreList();
        List<Sales> Reports();
        Product Product(int id);
         Sales Details(int id);
        bool IsProductAvailable(int storeid, int productid, int qty); 
    }
}
