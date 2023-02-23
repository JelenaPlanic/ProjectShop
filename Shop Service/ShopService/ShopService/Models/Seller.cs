using System.ComponentModel.DataAnnotations;

namespace ShopService.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Year { get; set; }

        public int ShopId { get; set; }
        public Shop? Shop { get; set; }
    }
}
