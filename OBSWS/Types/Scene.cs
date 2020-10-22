using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.Types
{
    public class Scene
    {
        public string name { get; set; }

        public List<SceneItem> sources { get; set; }

        public Scene(string name, List<SceneItem> sources)
        {
            this.name = name;
            this.sources = sources;
        }
    }
}
