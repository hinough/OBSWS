using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev.Types
{
    public class Scene
    {
        [JsonProperty(PropertyName = "name")]       public string name;
        [JsonProperty(PropertyName = "sources")]    public List<SceneItem> sources;

        public Scene(string name, List<SceneItem> sources)
        {
            this.name = name;

            this.sources = sources;
        }
    }
}
