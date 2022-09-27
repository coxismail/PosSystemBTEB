using System;

namespace PosSystem.Models
{
    [Serializable]
    public class InventoryViewModel
    {
        public int Code { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public int BalanceQ { get; set; }
        public decimal Price { get; set; }
    }
}