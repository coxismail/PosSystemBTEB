using PosSystem.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PosSystem.Repository
{
    interface IPurchaseRepository
    {
        void Save(Purchase model, List<PurchaseDetails>  data);
        void Delete();
        List<ListItem> ProductsList();
        List<ListItem> StoreList();
        List<Purchase> Reports();
        Purchase Details(int Id);
        Product Product(int Id);
    }
}
