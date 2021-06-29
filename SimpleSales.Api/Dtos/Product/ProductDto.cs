
using Newtonsoft.Json;

namespace SimpleSales.Api.Dtos.Product
{
    public class ProductDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "unit_price")]
        public string UnitPrice { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }
}
