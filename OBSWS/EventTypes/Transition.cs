using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.EventTypes
{
    public class Transition
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("from-scene")]
        public string from { get; set; }

        [JsonProperty("to-scene")]
        public string to { get; set; }

        [JsonProperty("duration")]
        public long duration { get; set; }

        public Transition(string name, string type, string from, string to, long duration)
        {
            this.name = name;
            this.type = type;
            this.from = from;
            this.to = to;

            this.duration = duration;
        }
    }
}
