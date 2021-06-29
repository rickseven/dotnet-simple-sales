using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleSales.Api.Models
{
    public class ProductModel
    {
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int Quantity { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }

        [ConcurrencyCheck]
        public DateTime? UpdatedDate { get; set; }
    }
}
