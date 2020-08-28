namespace DEBoard
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class QueryPlayer
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("leaderboard_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? LeaderboardId { get; set; }

        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public long? Start { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("leaderboard", NullValueHandling = NullValueHandling.Ignore)]
        public Leaderboard[] Leaderboard { get; set; }
    }

    public partial class Leaderboard
    {
        [JsonProperty("profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ProfileId { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rank { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rating { get; set; }

        [JsonProperty("steam_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SteamId { get; set; }

        [JsonProperty("icon")]
        public object Icon { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("clan", NullValueHandling = NullValueHandling.Ignore)]
        public string Clan { get; set; }

        [JsonProperty("country")]
        public object Country { get; set; }

        [JsonProperty("previous_rating", NullValueHandling = NullValueHandling.Ignore)]
        public long? PreviousRating { get; set; }

        [JsonProperty("highest_rating", NullValueHandling = NullValueHandling.Ignore)]
        public long? HighestRating { get; set; }

        [JsonProperty("streak", NullValueHandling = NullValueHandling.Ignore)]
        public long? Streak { get; set; }

        [JsonProperty("lowest_streak", NullValueHandling = NullValueHandling.Ignore)]
        public long? LowestStreak { get; set; }

        [JsonProperty("highest_streak", NullValueHandling = NullValueHandling.Ignore)]
        public long? HighestStreak { get; set; }

        [JsonProperty("games", NullValueHandling = NullValueHandling.Ignore)]
        public long? Games { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wins { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public long? Losses { get; set; }

        [JsonProperty("drops", NullValueHandling = NullValueHandling.Ignore)]
        public long? Drops { get; set; }

        [JsonProperty("last_match", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastMatch { get; set; }

        [JsonProperty("last_match_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastMatchTime { get; set; }
    }

    public partial class QueryPlayer
    {
        public static QueryPlayer FromJson(string json) =>  JsonConvert.DeserializeObject<QueryPlayer>(json, DEBoard.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this QueryPlayer self) => JsonConvert.SerializeObject(self, DEBoard.Converter.Settings);
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
