﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using DeReplaysManager;
//
//    var replayParser = ReplayParser.FromJson(jsonString);

namespace DeReplaysManager
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ReplayParser
    {
        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("Recname", NullValueHandling = NullValueHandling.Ignore)]
        public string Recname { get; set; }

        [JsonProperty("USERprem", NullValueHandling = NullValueHandling.Ignore)]
        public string UseRprem { get; set; }

        [JsonProperty("SecretId", NullValueHandling = NullValueHandling.Ignore)]
        public string SecretId { get; set; }

        [JsonProperty("DeciKey", NullValueHandling = NullValueHandling.Ignore)]
        public string DeciKey { get; set; }

        [JsonProperty("TicketKey", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TicketKey { get; set; }

        [JsonProperty("Nickname", NullValueHandling = NullValueHandling.Ignore)]
        public string Nickname { get; set; }

        [JsonProperty("Video", NullValueHandling = NullValueHandling.Ignore)]
        public string Video { get; set; }

        [JsonProperty("isVerified", NullValueHandling = NullValueHandling.Ignore)]
        public bool isVerified { get; set; }
    }

    public partial class ReplayParser
    {
        public static ReplayParser FromJson(string json) => JsonConvert.DeserializeObject<ReplayParser>(File.ReadAllText(json), DeReplaysManager.Converter.Settings);
        public static ReplayParser FromJsonText(string json) => JsonConvert.DeserializeObject<ReplayParser>(json, DeReplaysManager.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ReplayParser self) => JsonConvert.SerializeObject(self, DeReplaysManager.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}