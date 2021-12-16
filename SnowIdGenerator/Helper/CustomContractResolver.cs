using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SnowIdGenerator.Helper
{
    public class CustomContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// 实现首字母小写
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
        }

        /// <summary>
        /// 对长整形处理
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (objectType == typeof(long))
            {
                return new JsonConverterLong();
            }

            return base.ResolveContractConverter(objectType);
        }
    }

    public class JsonConverterLong : JsonConverter
    {
        /// <summary>
        /// 是否可以转换
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        /// <summary>
        /// 读json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if((reader.ValueType == null || reader.ValueType == typeof(long?)) && reader.Value == null)
            {
                return null;
            }
            else
            {
                long.TryParse(reader.Value != null ? reader.Value.ToString() : "", out var value);
                return value;
            }
        }

        /// <summary>
        /// 写json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value == null)
                writer.WriteValue(value);
            else
                writer.WriteValue(value.ToString());
        }
    }

}
