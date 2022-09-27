using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PosSystem.Models
{
    public class TransferViewModel
    {
        [Required, Display(Name="From Store")]
        public int FromStoreId { get; set; }
        [Required, Display(Name = "Qty"), Range(1, 10000000)]
        public int Qty { get; set; }
        [Required, Display(Name = "To Store")]
        public int ToStoreId { get; set; }
        [Required, Display(Name = "Product")]
        public int ProductId { get; set; }
    }
}