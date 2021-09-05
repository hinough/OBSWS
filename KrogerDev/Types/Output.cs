using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev.Types
{
    public class Output
    {
        [JsonProperty(PropertyName = "acitve")]                 public bool active;
        [JsonProperty(PropertyName = "reconnecting")]           public bool reconnecting;

        [JsonProperty(PropertyName = "congestion")]             public double congestion;

        [JsonProperty(PropertyName = "droppedFrames")]          public int droppedFrames;
        [JsonProperty(PropertyName = "height")]                 public int height;
        [JsonProperty(PropertyName = "totalBytes")]             public int totalBytes;
        [JsonProperty(PropertyName = "totalFrames")]            public int totalFrames;
        [JsonProperty(PropertyName = "width")]                  public int width;

        [JsonProperty(PropertyName = "settings")]               public object settings;

        [JsonProperty(PropertyName = "name")]                   public string name;
        [JsonProperty(PropertyName = "type")]                   public string type;

        [JsonProperty(PropertyName = "flags")]                  public Flags flags;

        public Output(bool active, bool reconnecting, double congestion, int droppedFrames, int height, int totalBytes, int totalFrames, int width,
                      object settings, string name, string type, Flags flags)
        {
            this.active = active;
            this.reconnecting = reconnecting;

            this.congestion = congestion;
            this.droppedFrames = droppedFrames;
            this.height = height;
            this.totalBytes = totalBytes;
            this.totalFrames = totalFrames;
            this.width = width;

            this.settings = settings;

            this.name = name;
            this.type = type;

            this.flags = flags;
        }
    }

    public class Flags
    {
        [JsonProperty(PropertyName = "audio")]                  public bool audio;
        [JsonProperty(PropertyName = "encoded")]                public bool encoded;
        [JsonProperty(PropertyName = "multiTrack")]             public bool multiTrack;
        [JsonProperty(PropertyName = "service")]                public bool service;
        [JsonProperty(PropertyName = "video")]                  public bool video;

        [JsonProperty(PropertyName = "rawValue")]               public int rawValue;

        public Flags(bool audio, bool  encoded, bool multiTrack, bool service, bool video, int rawValue) 
        {
            this.audio = audio;
            this.encoded = encoded;
            this.multiTrack = multiTrack;
            this.service = service;
            this.video = video;

            this.rawValue = rawValue;
        }
    }
}
