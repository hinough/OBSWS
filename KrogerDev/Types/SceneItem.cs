using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev.Types
{
    public class SceneItem
    {
        [JsonProperty(PropertyName = "locked")]             public bool locked;
        [JsonProperty(PropertyName = "muted")]              public bool muted;
        [JsonProperty(PropertyName = "render")]             public bool render;

        [JsonProperty(PropertyName = "alignment")]          public int alignment;
        [JsonProperty(PropertyName = "cy")]                 public int cy;
        [JsonProperty(PropertyName = "cx")]                 public int cx;
        [JsonProperty(PropertyName = "id")]                 public int id;
        [JsonProperty(PropertyName = "source_cx")]          public int source_cx;
        [JsonProperty(PropertyName = "source_cy")]          public int source_cy;
        [JsonProperty(PropertyName = "volume")]             public int volume;
        [JsonProperty(PropertyName = "x")]                  public int x;
        [JsonProperty(PropertyName = "y")]                  public int y;

        [JsonProperty(PropertyName = "name")]               public string name;
        [JsonProperty(PropertyName = "parentGroupName")]    public string parentGroupName;
        [JsonProperty(PropertyName = "type")]               public string type;

        [JsonProperty(PropertyName = "groupChildren")]      public List<SceneItem> groupChildren;


        /// <summary>
        /// Constructor to take all arguments in forming a SceneItem. Mostly used to deserialize/serialize JSON
        /// </summary>
        /// <param name="locked">Is SceneItem Locked? true/false</param>
        /// <param name="muted">Is SceneItem Muted? true/false</param>
        /// <param name="render">Is SceneItem Rendered? true/false</param>
        /// <param name="alignment">Source that item is manipulated from. 1 = Left, 2 = right, 4 = top, 8 = Bottom, omitted = center</param>
        /// <param name="cy">y coordinate</param>
        /// <param name="cx">x coordinate</param>
        /// <param name="id">Scene Item ID</param>
        /// <param name="source_cx">cx Coordinate</param>
        /// <param name="source_cy">cy Coordinate</param>
        /// <param name="volume">Volume of sceneitem</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="name">SceneItems name</param>
        /// <param name="parentGroupName">SceneItems Parents Name</param>
        /// <param name="type">SceneItem Type</param>
        /// <param name="groupChildren">List of SceneItems Childrens</param>
        public SceneItem(bool locked, bool muted, bool render,
                         int alignment, int cy, int cx, int id, int source_cx, int source_cy, int volume, int x, int y,
                         string name, string parentGroupName, string type,
                         List<SceneItem> groupChildren)
        {
            this.locked = locked;
            this.muted = muted;
            this.render = render;

            this.alignment = alignment;
            this.cy = cy;
            this.cx = cx;
            this.id = id;
            this.source_cx = source_cx;
            this.source_cy = source_cy;
            this.volume = volume;
            this.x = x;
            this.y = y;

            this.name = name;
            this.parentGroupName = parentGroupName;
            this.type = type;

            this.groupChildren = groupChildren;
        }
    }
}
