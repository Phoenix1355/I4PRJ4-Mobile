using System;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Converters
{
    // From: https://www.c-sharpcorner.com/UploadFile/20c06b/deserializing-interface-properties-with-json-net/
    public class ConcreteConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
