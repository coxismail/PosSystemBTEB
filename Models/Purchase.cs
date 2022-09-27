using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Models
{
 
    public class Purchase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, Display(Name ="Supplier Name"), MinLength(5), StringLength(55)]
        public string Supplier { get; set; }
        [Display(Name ="Supplier Address")]
        public string Address { get; set; }
        public string Notes { get; set; }
        [ForeignKey(nameof(Store)), Required, Display(Name ="Store")]
        public int StoreId { get; set; }
        [Display(Name ="Net Amount")]
        public decimal NetAmount { get; set; }
        [Display(Name ="Purchase Date"), Required]
        public DateTime PurchaseDate { get; set; }
        public virtual ICollection<PurchaseDetails> Details { get; set; }
        public virtual Store Store { get; set; }
    }
    
    public class PurchaseDetails 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, Display(Name ="Product"), ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [ForeignKey(nameof(Purchase))]
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase  { get; set; }
        public virtual Product Product { get; set; }
    }
}