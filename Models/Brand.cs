using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PosSystem.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 55, MinimumLength = 3), DataType(DataType.Text)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}