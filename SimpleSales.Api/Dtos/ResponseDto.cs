
using Newtonsoft.Json;

namespace SimpleSales.Api.Dtos
{
    public class ResponseDto
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }
    }
}
