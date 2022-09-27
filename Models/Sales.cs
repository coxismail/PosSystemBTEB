using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Models
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name ="Sales Date"), DataType(DataType.Date), Required]
        public DateTime SalesDate { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        [ForeignKey(nameof(Store)), Required]
        public int StoreId { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<SalesDetails> Details { get; set; }
        public virtual Store Store  { get; set; }
    }
    public class SalesDetails
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Product)), Display(Name ="Product"), Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Display(Name ="Rate"), Required, Range(1, 10000000000)]
        public decimal Rate { get; set; }
        [Display(Name ="Total Amount")]
        public decimal TotalAmount { get; set; }

        [ForeignKey(nameof(Sales)), Required]
        public int SalesId { get; set; }
        public virtual Sales Sales  { get; set; }
        public virtual Product Product  { get; set; }

    }
}