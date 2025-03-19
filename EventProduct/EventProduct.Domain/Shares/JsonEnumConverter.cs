using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Formats.Asn1;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;


namespace EventProduct.Domain.Shares
{


    public class JsonEnumConverter<TEnum> : StringEnumConverter where TEnum : struct, Enum
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                int value = Convert.ToInt32(reader.Value);
                if (Enum.IsDefined(typeof(TEnum), value))
                {
                    return (TEnum)Enum.ToObject(typeof(TEnum), value);
                }
            }
            else if (reader.TokenType == JsonToken.String)
            {
                string enumString = reader.Value.ToString();
                if (Enum.TryParse(enumString, true, out TEnum result))
                {
                    return result;
                }
            }

            throw new JsonSerializationException($"Invalid {typeof(TEnum).Name} value.");
        }
    }
}

