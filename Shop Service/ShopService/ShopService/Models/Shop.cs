using System.ComponentModel.DataAnnotations;

namespace ShopService.Models
{
    public class Shop
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
