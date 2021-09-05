using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev.Types
{
    public class SceneItemTransform
    {
        [JsonProperty(PropertyName = "locked")]             public bool locked;
        [JsonProperty(PropertyName = "visible")]            public bool visible;

        [JsonProperty(PropertyName = "heigh")]              public double height;
        [JsonProperty(PropertyName = "rotation")]           public double rotation;
        [JsonProperty(PropertyName = "width")]              public double width;

        [JsonProperty(PropertyName = "sourceHeight")]       public int sourceHeight;
        [JsonProperty(PropertyName = "sourceWidth")]        public int sourceWidth;

        [JsonProperty(PropertyName = "parentGroupName")]    public string parentGroupName;

        [JsonProperty(PropertyName = "groupChildren")]      public List<SceneItemTransform> groupChildren;

        [JsonProperty(PropertyName = "bounds")]             public Bounds bounds;
        [JsonProperty(PropertyName = "crop")]               public Crop crop;
        [JsonProperty(PropertyName = "position")]           public Position position;
        [JsonProperty(PropertyName = "scale")]              public Scale scale;
    }

    public class Bounds
    {
        [JsonProperty(PropertyName = "x")]                  public double x;
        [JsonProperty(PropertyName = "y")]                  public double y;

        [JsonProperty(PropertyName = "alignment")]          public int alignment;
        
        [JsonProperty(PropertyName = "type")]               public string type;
        
        public Bounds(double x, double y, int alignment, string type)
        {
            this.x = x;
            this.y = y;
            this.alignment = alignment;
            this.type = type;
        }
    }

    public class Crop
    {
        [JsonProperty(PropertyName = "top")]                public int top;
        [JsonProperty(PropertyName = "right")]              public int right;
        [JsonProperty(PropertyName = "bottom")]             public int bottom;
        [JsonProperty(PropertyName = "left")]               public int left;

        public Crop(int top, int right, int bottom, int left)
        {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
        }
    }

    public class Position
    {
        [JsonProperty(PropertyName = "x")]                  public double x;
        [JsonProperty(PropertyName = "y")]                  public double y;

        [JsonProperty(PropertyName = "alignment")]          public int alignment;

        public Position(double x, double y, int alignment)
        {
            this.x = x;
            this.y = y;
            this.alignment = alignment;
        }
    }

    public class Scale
    {
        [JsonProperty(PropertyName = "x")]                  public double x;
        [JsonProperty(PropertyName = "y")]                  public double y;

        [JsonProperty(PropertyName = "filter")]             public string filter;

        public Scale(double x, double y, string filter)
        {
            this.x = x;
            this.y = y;

            this.filter = filter;
        }
    }
}
