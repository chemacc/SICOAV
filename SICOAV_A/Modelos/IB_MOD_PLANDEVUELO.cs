using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELO.PLANDEVUELO
{
   


    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var welcome = Welcome.FromJson(jsonString);

    //namespace QuickType
    
        using System;
        using System.Collections.Generic;

        using System.Globalization;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;

        public partial class IB_MOD_PLANDEVUELO
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("fromICAO")]
            public string FromIcao { get; set; }

            [JsonProperty("toICAO")]
            public string ToIcao { get; set; }

            [JsonProperty("fromName")]
            public string FromName { get; set; }

            [JsonProperty("toName")]
            public string ToName { get; set; }

            [JsonProperty("flightNumber")]
            public object FlightNumber { get; set; }

            [JsonProperty("distance")]
            public double Distance { get; set; }

            [JsonProperty("maxAltitude")]
            public long MaxAltitude { get; set; }

            [JsonProperty("waypoints")]
            public long Waypoints { get; set; }

            [JsonProperty("likes")]
            public long Likes { get; set; }

            [JsonProperty("downloads")]
            public long Downloads { get; set; }

            [JsonProperty("popularity")]
            public long Popularity { get; set; }

            [JsonProperty("notes")]
            public string Notes { get; set; }

            [JsonProperty("encodedPolyline")]
            public string EncodedPolyline { get; set; }

            [JsonProperty("createdAt")]
            public DateTimeOffset CreatedAt { get; set; }

            [JsonProperty("updatedAt")]
            public DateTimeOffset UpdatedAt { get; set; }

            [JsonProperty("tags")]
            public string[] Tags { get; set; }

            [JsonProperty("user")]
            public object User { get; set; }

            [JsonProperty("application")]
            public object Application { get; set; }

            [JsonProperty("cycle")]
            [JsonIgnore]
            public Cycle Cycle { get; set; }

            [JsonProperty("route")]
            public Route Route { get; set; }
        }

        public partial class Cycle
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("ident")]
            public string Ident { get; set; }

            [JsonProperty("year")]
            public long Year { get; set; }

            [JsonProperty("release")]
            public long Release { get; set; }
        }

        public partial class Route
        {
            [JsonProperty("nodes")]
            public Node[] Nodes { get; set; }
        }

        public partial class Node
        {
            [JsonProperty("type")]
            public NodeType Type { get; set; }

            [JsonProperty("ident")]
            public string Ident { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }

            [JsonProperty("lon")]
            public double Lon { get; set; }

            [JsonProperty("alt")]
            public long Alt { get; set; }

            [JsonProperty("via")]
            public Via Via { get; set; }
        }

        public partial class Via
        {
            [JsonProperty("type")]
            public ViaType Type { get; set; }

            [JsonProperty("ident")]
            public string Ident { get; set; }
        }

        public enum NodeType { Apt, Fix, Vor };

        public enum ViaType { AwyHi, AwyLo };

        public partial class IB_MOD_PLANDEVUELO
    {
            public static IB_MOD_PLANDEVUELO[] FromJson(string json) => JsonConvert.DeserializeObject<IB_MOD_PLANDEVUELO[]>(json, MODELO.PLANDEVUELO.Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this IB_MOD_PLANDEVUELO[] self) => JsonConvert.SerializeObject(self, MODELO.PLANDEVUELO.Converter.Settings);
        }

        internal static class Converter
    {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
                NodeTypeConverter.Singleton,
                ViaTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        internal class NodeTypeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(NodeType) || t == typeof(NodeType?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "APT":
                        return NodeType.Apt;
                    case "FIX":
                        return NodeType.Fix;
                    case "VOR":
                        return NodeType.Vor;
                }
                throw new Exception("Cannot unmarshal type NodeType");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (NodeType)untypedValue;
                switch (value)
                {
                    case NodeType.Apt:
                        serializer.Serialize(writer, "APT");
                        return;
                    case NodeType.Fix:
                        serializer.Serialize(writer, "FIX");
                        return;
                    case NodeType.Vor:
                        serializer.Serialize(writer, "VOR");
                        return;
                }
                throw new Exception("Cannot marshal type NodeType");
            }

            public static readonly NodeTypeConverter Singleton = new NodeTypeConverter();
        }

        internal class ViaTypeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(ViaType) || t == typeof(ViaType?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "AWY-HI":
                        return ViaType.AwyHi;
                    case "AWY-LO":
                        return ViaType.AwyLo;
                }
                throw new Exception("Cannot unmarshal type ViaType");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (ViaType)untypedValue;
                switch (value)
                {
                    case ViaType.AwyHi:
                        serializer.Serialize(writer, "AWY-HI");
                        return;
                    case ViaType.AwyLo:
                        serializer.Serialize(writer, "AWY-LO");
                        return;
                }
                throw new Exception("Cannot marshal type ViaType");
            }

            public static readonly ViaTypeConverter Singleton = new ViaTypeConverter();
        }
    
}
