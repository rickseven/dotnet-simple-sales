using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleSales.WebAdmin.Models.Product;

namespace SimpleSales.WebAdmin.Models.Order
{
    public class OrderModel
    {
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(36)]
        public string ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
