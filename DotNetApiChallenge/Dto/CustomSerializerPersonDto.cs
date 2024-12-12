using DotNetApiChallenge.Services;
using Newtonsoft.Json;

namespace DotNetApiChallenge.Dto
{
    public class CustomSerializerPersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public int? ColorId { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public virtual ColorDto? Color { get; set; }
    }
}
