﻿using System.Text.Json.Serialization;

namespace OpenDotaApi.Api.Matches.Model
{
    public class WardLog
    {
        [JsonPropertyName("attackername")]
        public string Attackername { get; set; }

        [JsonPropertyName("ehandle")]
        public long? Ehandle { get; set; }

        [JsonPropertyName("entityleft")]
        public bool? Entityleft { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("player_slot")]
        public int? PlayerSlot { get; set; }

        [JsonPropertyName("slot")]
        public int? Slot { get; set; }

        [JsonPropertyName("time")]
        public int? Time { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("x")]
        public float? X { get; set; }

        [JsonPropertyName("y")]
        public float? Y { get; set; }

        [JsonPropertyName("z")]
        public float? Z { get; set; }
    }
}