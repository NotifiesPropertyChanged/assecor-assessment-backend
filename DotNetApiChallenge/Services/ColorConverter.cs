using DotNetApiChallenge.Dto;
using DotNetApiChallenge.Models;
using Newtonsoft.Json;

namespace DotNetApiChallenge.Services
{
    public class ColorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var color = value as ColorDto;
            if (color != null)
            {
                //Here will the json manipulated and formatted
                //writer.WriteValue(color.Name);
                //serializer.Serialize(writer, value);
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color);
        }
    }
}
