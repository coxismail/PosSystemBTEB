using System.ComponentModel.DataAnnotations;

namespace PosSystem.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 55, MinimumLength = 3), DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string Address { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}