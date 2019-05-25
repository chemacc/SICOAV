using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MODELO.AEROPUERTO
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    
    public partial class IbModAeropuerto
    {
        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("_api")]
        public Api Api { get; set; }
    }

    public partial class Api
    {
        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("legalNotice")]
        public string LegalNotice { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("request")]
        public Request Request { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Request
    {
        [JsonProperty("callback")]
        public object Callback { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("device")]
        public object Device { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("plugin")]
        public object Plugin { get; set; }

        [JsonProperty("plugin-setting")]
        public PluginSetting PluginSetting { get; set; }

        [JsonProperty("token")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Token { get; set; }
    }

    public partial class PluginSetting
    {
        [JsonProperty("schedule")]
        public PluginSettingSchedule Schedule { get; set; }
    }

    public partial class PluginSettingSchedule
    {
        [JsonProperty("mode")]
        public object Mode { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("airport")]
        public ResponseAirport Airport { get; set; }

        [JsonProperty("airlines")]
        public Airlines Airlines { get; set; }

        [JsonProperty("aircraftImages")]
        public AircraftImage[] AircraftImages { get; set; }
    }

    public partial class AircraftImage
    {
        [JsonProperty("registration")]
        public string Registration { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }
    }

    public partial class Images
    {
        [JsonProperty("thumbnails")]
        public Large[] Thumbnails { get; set; }

        [JsonProperty("medium")]
        public Large[] Medium { get; set; }

        [JsonProperty("large")]
        public Large[] Large { get; set; }
    }

    public partial class Large
    {
        [JsonProperty("src")]
        public Uri Src { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }
    }

    public partial class Airlines
    {
        [JsonProperty("codeshare")]
        public Codeshare Codeshare { get; set; }
    }

    public partial class Codeshare
    {
        [JsonProperty("AA")]
        public Aa Aa { get; set; }

        [JsonProperty("CZ")]
        public Aa Cz { get; set; }
    }

    public partial class Aa
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }
    }

    public partial class Code
    {
        [JsonProperty("iata")]
        public string Iata { get; set; }

        [JsonProperty("icao")]
        public string Icao { get; set; }
    }

    public partial class ResponseAirport
    {
        [JsonProperty("pluginData")]
        public PluginData PluginData { get; set; }
    }

    public partial class PluginData
    {
        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("flightdiary")]
        public Flightdiary Flightdiary { get; set; }

        [JsonProperty("schedule")]
        public PluginDataSchedule Schedule { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("aircraftCount")]
        public AircraftCount AircraftCount { get; set; }
    }

    public partial class AircraftCount
    {
        [JsonProperty("ground")]
        public long Ground { get; set; }

        [JsonProperty("onGround")]
        public OnGround OnGround { get; set; }
    }

    public partial class OnGround
    {
        [JsonProperty("visible")]
        public long Visible { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class Details
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("delayIndex")]
        public DelayIndex DelayIndex { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }

        [JsonProperty("url")]
        public Url Url { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }

    public partial class DelayIndex
    {
        [JsonProperty("arrivals", NullValueHandling = NullValueHandling.Ignore)]
        public double Arrivals { get; set; }

        [JsonProperty("departures" ,NullValueHandling = NullValueHandling.Ignore)]
        public double Departures { get; set; }
    }

    public partial class Position
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("elevation", NullValueHandling = NullValueHandling.Ignore)]
        public long? Elevation { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("region")]
        public Region Region { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public partial class Region
    {
        [JsonProperty("city")]
        public string City { get; set; }
    }

    public partial class Stats
    {
        [JsonProperty("arrivals")]
        public DeparturesClass Arrivals { get; set; }

        [JsonProperty("departures")]
        public DeparturesClass Departures { get; set; }
    }

    public partial class DeparturesClass
    {
        [JsonProperty("delayIndex")]
        public double DelayIndex { get; set; }

        [JsonProperty("delayAvg")]
        public long DelayAvg { get; set; }

        [JsonProperty("percentage")]
        public ArrivalsPercentage Percentage { get; set; }

        [JsonProperty("recent")]
        public Recent Recent { get; set; }

        [JsonProperty("today")]
        public Day Today { get; set; }

        [JsonProperty("yesterday")]
        public Day Yesterday { get; set; }

        [JsonProperty("tomorrow")]
        public Tomorrow Tomorrow { get; set; }
    }

    public partial class ArrivalsPercentage
    {
        [JsonProperty("delayed")]
        public double Delayed { get; set; }

        [JsonProperty("canceled")]
        public long Canceled { get; set; }

        [JsonProperty("trend")]
        public string Trend { get; set; }
    }

    public partial class Recent
    {
        [JsonProperty("delayIndex")]
        public double DelayIndex { get; set; }

        [JsonProperty("delayAvg")]
        public long DelayAvg { get; set; }

        [JsonProperty("percentage")]
        public ArrivalsPercentage Percentage { get; set; }

        [JsonProperty("quantity")]
        public Quantity Quantity { get; set; }
    }

    public partial class Quantity
    {
        [JsonProperty("onTime")]
        public long OnTime { get; set; }

        [JsonProperty("delayed")]
        public long Delayed { get; set; }

        [JsonProperty("canceled")]
        public long Canceled { get; set; }
    }

    public partial class Day
    {
        [JsonProperty("percentage")]
        public TodayPercentage Percentage { get; set; }

        [JsonProperty("quantity")]
        public Quantity Quantity { get; set; }
    }

    public partial class TodayPercentage
    {
        [JsonProperty("delayed")]
        public double Delayed { get; set; }

        [JsonProperty("canceled")]
        public double Canceled { get; set; }
    }

    public partial class Tomorrow
    {
        [JsonProperty("percentage")]
        public QuantityClass Percentage { get; set; }

        [JsonProperty("quantity")]
        public QuantityClass Quantity { get; set; }
    }

    public partial class QuantityClass
    {
        [JsonProperty("canceled")]
        public long Canceled { get; set; }
    }

    public partial class Timezone
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("abbr")]
        public string Abbr { get; set; }

        [JsonProperty("abbrName")]
        public string AbbrName { get; set; }

        [JsonProperty("isDst")]
        public bool IsDst { get; set; }
    }

    public partial class Url
    {
        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }

        [JsonProperty("webcam")]
        public Uri Webcam { get; set; }

        [JsonProperty("wikipedia")]
        public Uri Wikipedia { get; set; }
    }

    public partial class Flightdiary
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("ratings")]
        public Ratings Ratings { get; set; }

        [JsonProperty("comment")]
        public Comment[] Comment { get; set; }

        [JsonProperty("reviews")]
        public long Reviews { get; set; }

        [JsonProperty("evaluation")]
        public long Evaluation { get; set; }
    }

    public partial class Comment
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("facebookId")]
        public object FacebookId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Ratings
    {
        [JsonProperty("avg")]
        public double Avg { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class PluginDataSchedule
    {
        [JsonProperty("arrivals")]
        public ScheduleArrivals Arrivals { get; set; }

        [JsonProperty("departures")]
        public Departures Departures { get; set; }

        [JsonProperty("ground")]
        public Ground Ground { get; set; }
    }

    public partial class ScheduleArrivals
    {
        [JsonProperty("item")]
        public Item Item { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public ArrivalsDatum[] Data { get; set; }
    }

    public partial class ArrivalsDatum
    {
        [JsonProperty("flight")]
        public PurpleFlight Flight { get; set; }
    }

    public partial class PurpleFlight
    {
        [JsonProperty("identification")]
        public Identification Identification { get; set; }

        [JsonProperty("status")]
        public FlightStatus Status { get; set; }

        [JsonProperty("aircraft")]
        public PurpleAircraft Aircraft { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("airline")]
        public Airline Airline { get; set; }

        [JsonProperty("airport")]
        public PurpleAirport Airport { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }
    }

    public partial class PurpleAircraft
    {
        [JsonProperty("model")]
        public Model Model { get; set; }

        [JsonProperty("hex")]
        public string Hex { get; set; }

        [JsonProperty("registration")]
        public string Registration { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("age")]
        public Age Age { get; set; }

        [JsonProperty("availability")]
        public Availability Availability { get; set; }

        [JsonProperty("onGroundUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public long? OnGroundUpdate { get; set; }

        [JsonProperty("hoursDiff", NullValueHandling = NullValueHandling.Ignore)]
        public double? HoursDiff { get; set; }
    }

    public partial class Age
    {
        [JsonProperty("availability")]
        public bool Availability { get; set; }
    }

    public partial class Availability
    {
        [JsonProperty("serialNo")]
        public bool SerialNo { get; set; }

        [JsonProperty("age")]
        public bool Age { get; set; }
    }

    public partial class Model
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class Airline
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }
    }

    public partial class PurpleAirport
    {
        [JsonProperty("origin")]
        public Origin Origin { get; set; }

        [JsonProperty("destination")]
        public Destination Destination { get; set; }

        [JsonProperty("real")]
        public object Real { get; set; }
    }

    public partial class Destination
    {
        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public partial class Info
    {
        [JsonProperty("terminal")]
        public object Terminal { get; set; }

        [JsonProperty("baggage")]
        public object Baggage { get; set; }

        [JsonProperty("gate")]
        public object Gate { get; set; }
    }

    public partial class Origin
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public partial class Identification
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("row")]
        public long Row { get; set; }

        [JsonProperty("number")]
        public Number Number { get; set; }

        [JsonProperty("callsign")]
        public string Callsign { get; set; }

        [JsonProperty("codeshare")]
        public object Codeshare { get; set; }
    }

    public partial class Number
    {
        [JsonProperty("default")]
        public string Default { get; set; }

        [JsonProperty("alternative")]
        public object Alternative { get; set; }
    }

    public partial class Owner
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }
    }

    public partial class FlightStatus
    {
        [JsonProperty("live")]
        public bool Live { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("estimated")]
        public object Estimated { get; set; }

        [JsonProperty("ambiguous")]
        public bool Ambiguous { get; set; }

        [JsonProperty("generic")]
        public Generic Generic { get; set; }
    }

    public partial class Generic
    {
        [JsonProperty("status")]
        public GenericStatus Status { get; set; }

        [JsonProperty("eventTime")]
        public EventTime EventTime { get; set; }
    }

    public partial class EventTime
    {
        [JsonProperty("utc")]
        public long? Utc { get; set; }

        [JsonProperty("local")]
        public long? Local { get; set; }
    }

    public partial class GenericStatus
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("diverted")]
        public object Diverted { get; set; }
    }

    public partial class Time
    {
        [JsonProperty("scheduled")]
        public Estimated Scheduled { get; set; }

        [JsonProperty("real")]
        public Estimated Real { get; set; }

        [JsonProperty("estimated")]
        public Estimated Estimated { get; set; }

        [JsonProperty("other")]
        public Other Other { get; set; }
    }

    public partial class Estimated
    {
        [JsonProperty("departure")]
        public long? Departure { get; set; }

        [JsonProperty("arrival")]
        public long? Arrival { get; set; }
    }

    public partial class Other
    {
        [JsonProperty("eta")]
        public long? Eta { get; set; }

        [JsonProperty("duration")]
        public object Duration { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("current")]
        public long Current { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }
    }

    public partial class Page
    {
        [JsonProperty("current")]
        public long Current { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class Departures
    {
        [JsonProperty("item")]
        public Item Item { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public DeparturesDatum[] Data { get; set; }
    }

    public partial class DeparturesDatum
    {
        [JsonProperty("flight")]
        public FluffyFlight Flight { get; set; }
    }

    public partial class FluffyFlight
    {
        [JsonProperty("identification")]
        public Identification Identification { get; set; }

        [JsonProperty("status")]
        public FlightStatus Status { get; set; }

        [JsonProperty("aircraft")]
        public FluffyAircraft Aircraft { get; set; }

        [JsonProperty("owner")]
        public object Owner { get; set; }

        [JsonProperty("airline")]
        public Airline Airline { get; set; }

        [JsonProperty("airport")]
        public FluffyAirport Airport { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }
    }

    public partial class FluffyAircraft
    {
        [JsonProperty("model")]
        public Model Model { get; set; }

        [JsonProperty("registration")]
        public string Registration { get; set; }

        [JsonProperty("images")]
        public object Images { get; set; }
    }

    public partial class FluffyAirport
    {
        [JsonProperty("origin")]
        public Destination Origin { get; set; }

        [JsonProperty("destination")]
        public Origin Destination { get; set; }

        [JsonProperty("real")]
        public object Real { get; set; }
    }

    public partial class Ground
    {
        [JsonProperty("item")]
        public Item Item { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public GroundDatum[] Data { get; set; }
    }

    public partial class GroundDatum
    {
        [JsonProperty("flight")]
        public TentacledFlight Flight { get; set; }
    }

    public partial class TentacledFlight
    {
        [JsonProperty("identification")]
        public Identification Identification { get; set; }

        [JsonProperty("aircraft")]
        public PurpleAircraft Aircraft { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("airline")]
        public Airline Airline { get; set; }
    }

    public partial class Weather
    {
        [JsonProperty("metar")]
        public string Metar { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("qnh")]
        public long Qnh { get; set; }

        [JsonProperty("dewpoint")]
        public Dewpoint Dewpoint { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("pressure")]
        public Pressure Pressure { get; set; }

        [JsonProperty("sky")]
        public Sky Sky { get; set; }

        [JsonProperty("flight")]
        public WeatherFlight Flight { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("temp")]
        public Dewpoint Temp { get; set; }

        [JsonProperty("elevation")]
        public Elevation Elevation { get; set; }

        [JsonProperty("cached")]
        public long Cached { get; set; }
    }

    public partial class Dewpoint
    {
        [JsonProperty("celsius")]
        public long Celsius { get; set; }

        [JsonProperty("fahrenheit")]
        public long Fahrenheit { get; set; }
    }

    public partial class Elevation
    {
        [JsonProperty("m")]
        public long M { get; set; }

        [JsonProperty("ft")]
        public long Ft { get; set; }
    }

    public partial class WeatherFlight
    {
        [JsonProperty("category")]
        public object Category { get; set; }
    }

    public partial class Pressure
    {
        [JsonProperty("hg")]
        public long Hg { get; set; }

        [JsonProperty("hpa")]
        public long Hpa { get; set; }
    }

    public partial class Sky
    {
        [JsonProperty("condition")]
        public Condition Condition { get; set; }

        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }
    }

    public partial class Condition
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class Visibility
    {
        [JsonProperty("km")]
        public object Km { get; set; }

        [JsonProperty("mi")]
        public object Mi { get; set; }

        [JsonProperty("nmi")]
        public object Nmi { get; set; }
    }

    public partial class Wind
    {
        [JsonProperty("direction")]
        public Direction Direction { get; set; }

        [JsonProperty("speed")]
        public Speed Speed { get; set; }
    }

    public partial class Direction
    {
        [JsonProperty("degree", NullValueHandling = NullValueHandling.Ignore)]
        public long Degree { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
    }

    public partial class Speed
    {
        [JsonProperty("kmh")]
        public long Kmh { get; set; }

        [JsonProperty("kts")]
        public long Kts { get; set; }

        [JsonProperty("mph")]
        public long Mph { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public enum Source { JetphotosCom, Flightradar24Com };

    public partial class IbModAeropuerto
    {
        

        public static IbModAeropuerto FromJson(string json) => JsonConvert.DeserializeObject<IbModAeropuerto>(json, MODELO.AEROPUERTO.Converter.Settings);
        
    }

    public static class Serialize
    {
        public static string ToJson(this IbModAeropuerto self) => JsonConvert.SerializeObject(self, MODELO.AEROPUERTO.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                SourceConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value.ToLower() == "null") return 0.0;
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class SourceConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Source) || t == typeof(Source?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Jetphotos.com")
            {
                return Source.JetphotosCom;
            }

            if (value == "Flightradar24")
            {
                return Source.Flightradar24Com;
            }
            throw new Exception("Cannot unmarshal type Source");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Source)untypedValue;
            if (value == Source.JetphotosCom)
            {
                serializer.Serialize(writer, "Jetphotos.com");
                return;
            }
            throw new Exception("Cannot marshal type Source");
        }

        public static readonly SourceConverter Singleton = new SourceConverter();
    }
}

namespace SICOAV_A.Modelos
{


    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var ibModAeropuerto = IbModAeropuerto.FromJson(jsonString);

    namespace QuickType
    {
        using System;
        using System.Collections.Generic;

        using System.Globalization;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;

        public partial class IbModAeropuerto
        {
            [JsonProperty("airport")]
            public string Airport { get; set; }

            [JsonProperty("visibility")]
            public long Visibility { get; set; }

            [JsonProperty("wind")]
            public long Wind { get; set; }

            [JsonProperty("precipitation")]
            public double Precipitation { get; set; }

            [JsonProperty("freezing")]
            public long Freezing { get; set; }

            [JsonProperty("dangerous")]
            public double Dangerous { get; set; }

            [JsonProperty("VMC_IMC")]
            public long VmcImc { get; set; }

            [JsonProperty("date")]
            public Date Date { get; set; }

            [JsonProperty("airport_name")]
            public string AirportName { get; set; }

            [JsonProperty("latitude")]
            public double Latitude { get; set; }

            [JsonProperty("longitude")]
            public double Longitude { get; set; }

            [JsonProperty("countryCode")]
            public CountryCode CountryCode { get; set; }

            [JsonProperty("raw_metar")]
            public string RawMetar { get; set; }

            [JsonProperty("datetime")]
            public DateTimeOffset Datetime { get; set; }
        }

        public enum CountryCode { Esp };

        public enum Date { The181027 };

        public partial class IbModAeropuerto
        {
            public static IbModAeropuerto[] FromJson(string json) => JsonConvert.DeserializeObject<IbModAeropuerto[]>(json, QuickType.Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this IbModAeropuerto[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
                CountryCodeConverter.Singleton,
                DateConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        internal class CountryCodeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(CountryCode) || t == typeof(CountryCode?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                if (value == "ESP")
                {
                    return CountryCode.Esp;
                }
                return CountryCode.Esp;
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (CountryCode)untypedValue;
                if (value == CountryCode.Esp)
                {
                    serializer.Serialize(writer, "ESP");
                    return;
                }
                throw new Exception("Cannot marshal type CountryCode");
            }

            public static readonly CountryCodeConverter Singleton = new CountryCodeConverter();
        }

        internal class DateConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Date) || t == typeof(Date?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                if (value == "18-10-27")
                {
                    return Date.The181027;
                }
                return Date.The181027;
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Date)untypedValue;
                if (value == Date.The181027)
                {
                    serializer.Serialize(writer, "18-10-27");
                    return;
                }
                 
            }

            public static readonly DateConverter Singleton = new DateConverter();
        }
    }

    public class IB_MOD_AEROPUERTO
    {
       
        public String airport;
        public String visibility;
        public String wind;
        public String precipitation;
        public String freezing;
        public String dangerous;
        public String VMC_IMC;
        public String date;
        public String airport_name;
        public double latitude;
        public double longitude;
        public String country;
        public String METAR;
        public String DateTime;

        public IB_MOD_AEROPUERTO()
        {

        }

        public IB_MOD_AEROPUERTO(string codeAPI)
        {
            var cadenas = codeAPI.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            foreach(string token in cadenas)
            {
                var Elemento = token.Split(':');
                switch (index)
                {
                    case 0:
                        airport = Elemento[1].Replace('"',' ').Trim();
                        break;
                    case 1:
                        visibility = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 2:
                        wind = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 3:
                        precipitation = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 4:
                        freezing = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 5:
                        dangerous = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 6:
                        VMC_IMC = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 7:
                        date = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 8:
                        airport_name = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 9:
                        latitude = double.Parse(Elemento[1].Replace('"', ' ').Trim().Replace('.',','));
                        break;
                    case 10:
                        longitude = double.Parse(Elemento[1].Replace('"', ' ').Trim().Replace('.', ','));
                        break;
                    case 11:
                        country = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 12:
                        METAR = Elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 13:
                        DateTime = Elemento[1].Replace('"', ' ').Trim();
                        break;
                   
                }

                index++;
            }
        }
    }
}
