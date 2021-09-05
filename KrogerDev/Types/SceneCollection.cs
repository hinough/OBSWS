using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev.Types
{
    public class SceneCollection
    {
        [JsonProperty(PropertyName = "sc-name")] public string name;

        public SceneCollection(string name)
        {
            this.name = name;
        }
    }
}
