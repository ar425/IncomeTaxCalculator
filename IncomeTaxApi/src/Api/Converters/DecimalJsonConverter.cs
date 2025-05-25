using System.Text.Json;
using System.Text.Json.Serialization;

namespace IncomeTaxApi.Api.Converters
{
    // Custom json converter to ensure that decimal is restricted to 2 decimal places for monetary value
    public class DecimalJsonConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDecimal();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Math.Round(value, 2));
        }
    }
}