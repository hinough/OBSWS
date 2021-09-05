using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev.Types
{
    public class OBSStats
    {
        [JsonProperty(PropertyName = "average-frame-time")]         public double avgframetime;
        [JsonProperty(PropertyName = "cpu-usage")]                  public double cpu;
        [JsonProperty(PropertyName = "free-disk-space")]            public double disk;
        [JsonProperty(PropertyName = "fps")]                        public double fps;
        [JsonProperty(PropertyName = "memory-usage")]               public double memory;
        
        [JsonProperty(PropertyName = "output-skipped-frames")]      public int outputskipped;
        [JsonProperty(PropertyName = "output-total-frames")]        public int outputtotal;
        [JsonProperty(PropertyName = "render-missed-frames")]       public int rendermissed;
        [JsonProperty(PropertyName = "render-total-frames")]        public int rendertotal;
        
        public OBSStats(double avgframetime, double cpu, double disk, double fps, double memory,
                        int outputskipped, int outputtotal, int rendermissed, int rendertotal)
        {
            this.avgframetime = avgframetime;
            this.cpu = cpu;
            this.disk = disk;
            this.fps = fps;
            this.memory = memory;

            this.outputskipped = outputskipped;
            this.outputtotal = outputtotal;
            this.rendermissed = rendermissed;
            this.rendertotal = rendertotal;
        }
    }
}
