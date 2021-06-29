
using System.ComponentModel.DataAnnotations;

namespace SimpleSales.WebAdmin.Models.Product
{
    public class UpdateProductViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
