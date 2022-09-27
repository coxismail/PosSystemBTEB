using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Models
{

    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 55, MinimumLength = 3), DataType(DataType.Text)]
        public string Title { get; set; }
        public int Code { get; set; }
        [Display(Name ="Unit Price"), Range(1, 10000000)]
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Brand)), Required, Display(Name ="Brand")]
        public int BrandId { get; set; }
        [ForeignKey(nameof(Category)), Required, Display(Name ="Category")]
        public int CategoryId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }


    }
}