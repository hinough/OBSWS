using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.EventTypes
{
    public class Heartbeat
    {
        [JsonProperty("pulse")]
        public bool pulse { get; set; }
        [JsonProperty("streaming")]
        public bool streaming { get; set; }
        [JsonProperty("recording")]
        public bool recording { get; set; }

        [JsonProperty("current-profile")]
        public string profile { get; set; }
        [JsonProperty("current-scene")]
        public string scene { get; set; }

        [JsonProperty("total-stream-time")]
        public long streamtime { get; set; }
        [JsonProperty("total-stream-bytes")]
        public long streambytes { get; set; }
        [JsonProperty("total-stream-frames")]
        public long streamframes { get; set; }
        [JsonProperty("total-record-time")]
        public long recordtime { get; set; }
        [JsonProperty("total-record-bytes")]
        public long recordbytes { get; set; }
        [JsonProperty("total-record-frames")]
        public long recordframes { get; set; }

        [JsonProperty("stats")]
        public Types.ObsStats stats { get; set; }

        public Heartbeat(bool pulse, bool recording, bool streaming,
                         string profile, string scene,
                         long streamtime, long streambytes, long streamframes, long recordtime, long recordbytes, long recordframes,
                         JObject statsdata)
        {
            this.pulse = pulse;
            this.recording = recording;
            this.streaming = streaming;

            this.profile = profile;
            this.scene = scene;

            this.streamtime = streamtime;
            this.streambytes = streambytes;
            this.streamframes = streamframes;
            this.recordtime = recordtime;
            this.recordbytes = recordbytes;
            this.recordframes = recordframes;

            stats = statsdata.ToObject<Types.ObsStats>();
        }
    }
}
