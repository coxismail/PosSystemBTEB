using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosSystem.Models
{
    public class ProductViewModel
    {
        public string Title { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}