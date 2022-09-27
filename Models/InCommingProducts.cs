using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Models
{
    public class InCommingProducts
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "In Quantity")]
        public int InQuantity { get; set; }
        [Display(Name ="Trans. Type")]
        public ProdTransType Type { get; set; }
        public DateTime TransDate { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey(nameof(Product)), Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(Store))]
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public virtual Product Product { get; set; }
    }
}