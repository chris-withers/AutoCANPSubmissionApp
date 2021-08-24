using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoCANP.Api.Types
{
    public class Value
    {
        public string time { get; set; }
        [JsonConverter(typeof(EmptyDoubleToDoubleConverter<double>))]
        public double value { get; set; }

       [JsonPropertyName("Fcst Pd")]
       [JsonConverter(typeof(EmptyDoubleToDoubleConverter<double>))]
        public double FcstPd { get; set; }
    }

    public class Result
    {
        public string parameter { get; set; }
        public string alias { get; set; }
        public string units { get; set; }
        public List<Value> values { get; set; }
    }

    public class GetRaspBlipspotResults
    {
        public string mapinfo { get; set; }
        public string region { get; set; }
        public string grid { get; set; }

        [JsonPropertyNameAttribute("grid-i")]
        public int GridI { get; set; }

        [JsonPropertyNameAttribute ("grid-j")]
        public int GridJ { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Created { get; set; }
        public List<Result> Results { get; set; }
    }

    public class RaspResult
    {
        public GetRaspBlipspotResults get_rasp_blipspot_results { get; set; }
    }



    public class EmptyDoubleToDoubleConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            var rootElement = JsonDocument.ParseValue(ref reader);

            var text = rootElement.RootElement.GetRawText();

            if (!double.TryParse(text, out _))
                return JsonSerializer.Deserialize<T>("0", options);

            return JsonSerializer.Deserialize<T>(text, options);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<T>(writer, value, options);
        }
    }
}




