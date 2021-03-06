﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using DownTube.Core;
//
//    var videoApi = VideoApi.FromJson(jsonString);

namespace DownTube.Core
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class VideoApi
    {
        [JsonProperty("Videos")]
        public List<AudioLayer> Videos { get; set; }

        [JsonProperty("AudioLayers")]
        public List<AudioLayer> AudioLayers { get; set; }

        [JsonProperty("VideoLayers")]
        public List<AudioLayer> VideoLayers { get; set; }

        [JsonProperty("Details")]
        public Details Details { get; set; }
    }

    public partial class AudioLayer
    {
        [JsonProperty("URL")]
        public Uri Url { get; set; }

        [JsonProperty("Size")]
        public string Size { get; set; }

        [JsonProperty("ContentLength")]
        public long ContentLength { get; set; }

        [JsonProperty("Meta")]
        public Meta Meta { get; set; }

        [JsonProperty("Allowed")]
        public bool Allowed { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("IsHDR")]
        public bool IsHdr { get; set; }

        [JsonProperty("IsLive")]
        public bool IsLive { get; set; }

        [JsonProperty("Is3D")]
        public bool Is3D { get; set; }

        [JsonProperty("FPS")]
        public long Fps { get; set; }

        [JsonProperty("Quality")]
        public string Quality { get; set; }

        [JsonProperty("Bitrate")]
        public string Bitrate { get; set; }

        [JsonProperty("Type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("Itag")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Itag { get; set; }
    }

    public partial class Details
    {
        [JsonProperty("VideoID")]
        public string VideoId { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("LengthSeconds")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long LengthSeconds { get; set; }

        [JsonProperty("ChannelId")]
        public string ChannelId { get; set; }

        [JsonProperty("ShortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("ViewCount")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ViewCount { get; set; }

        [JsonProperty("Author")]
        public string Author { get; set; }
    }

    public enum TypeEnum { M4A, Mp4, Webm };

    public partial class VideoApi
    {
        public static VideoApi FromJson(string json) => JsonConvert.DeserializeObject<VideoApi>(json, DownTube.Core.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this VideoApi self) => JsonConvert.SerializeObject(self, DownTube.Core.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
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

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "m4a":
                    return TypeEnum.M4A;
                case "mp4":
                    return TypeEnum.Mp4;
                case "webm":
                    return TypeEnum.Webm;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.M4A:
                    serializer.Serialize(writer, "m4a");
                    return;
                case TypeEnum.Mp4:
                    serializer.Serialize(writer, "mp4");
                    return;
                case TypeEnum.Webm:
                    serializer.Serialize(writer, "webm");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
