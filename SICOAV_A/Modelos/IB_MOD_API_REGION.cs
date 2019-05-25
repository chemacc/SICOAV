
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///var coordinate = IbModApiRegion.FromJson(jsonString);
namespace SICOAV_A.Modelos
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class IbModApiRegion
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[][][] Coordinates { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("centlat")]
        public double Centlat { get; set; }

        [JsonProperty("REGION")]
        public string Region { get; set; }

        [JsonProperty("centlong")]
        public double Centlong { get; set; }

        [JsonProperty("FIRname")]
        public string FiRname { get; set; }

        [JsonProperty("ICAOCODE")]
        public string Icaocode { get; set; }

        [JsonProperty("StateCode")]
        public string StateCode { get; set; }

        [JsonProperty("StateName")]
        public string StateName { get; set; }
    }

    public partial class IbModApiRegion
    {
        public static IbModApiRegion[] FromJson(string json) => JsonConvert.DeserializeObject<IbModApiRegion[]>(json, SICOAV_A.Modelos.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this IbModApiRegion[] self) => JsonConvert.SerializeObject(self, SICOAV_A.Modelos.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
