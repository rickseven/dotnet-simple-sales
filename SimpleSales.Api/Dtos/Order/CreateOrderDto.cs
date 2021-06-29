
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SimpleSales.Api.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        [StringLength(36)]
        [Display(Name = "Product Id")]
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Quantity")]
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }
}
