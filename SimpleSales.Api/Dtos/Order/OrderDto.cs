
using System;
using Newtonsoft.Json;
using SimpleSales.Api.Dtos.Product;

namespace SimpleSales.Api.Dtos.Order
{
    public class OrderDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "unit_price")]
        public string UnitPrice { get; set; }

        [JsonProperty(PropertyName = "product")]
        public ProductDto Product { get; set; }
    }
}
