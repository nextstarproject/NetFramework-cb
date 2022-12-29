using Newtonsoft.Json.Linq;
using NextStar.Framework.Core;

namespace Newtonsoft.Json;

/// <summary>
/// 不允许单独使用，此转换器只给 <see cref="DateTimeRange"/> 使用，其他请勿使用
/// <para>如有问题概不负责</para>
/// </summary>
public class DateTimeRangeConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTimeRange);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartArray)
        {
            var jsonObject = JArray.Load(reader);
            var arr = jsonObject.ToObject<DateTime?[]>();
            return new DateTimeRange(arr);
        }
        else
        {
            return null;
        }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteStartArray();
            writer.WriteValue((object)null);
            writer.WriteValue((object)null);
            writer.WriteEndArray();
            return;
        }

        var properties = value.GetType().GetProperties().Where(p => p.PropertyType == typeof(DateTime?));

        writer.WriteStartArray();

        var startTimeProperty = properties.FirstOrDefault(p => p.Name == nameof(DateTimeRange.StartDateTime));
        writer.WriteValue(startTimeProperty.GetValue(value, null));

        var endTimeProperty = properties.FirstOrDefault(p => p.Name == nameof(DateTimeRange.EndDateTime));
        writer.WriteValue(endTimeProperty.GetValue(value, null));

        writer.WriteEndArray();
    }
}