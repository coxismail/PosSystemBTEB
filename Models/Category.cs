using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required,  StringLength(maximumLength: 55, MinimumLength =  3), Display(Name="Category Name"), DataType(DataType.Text)]
        public string Name { get; set; }

        public virtual  ICollection<Product> Products { get; set; }

    }
}