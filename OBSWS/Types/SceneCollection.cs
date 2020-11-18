using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.Types
{
    class SceneCollection
    {
        [JsonProperty(PropertyName="sc-name")]
        public string scname { get; set; }

        public SceneCollection(string scname)
        {
            this.scname = scname;
        }
    }
}
