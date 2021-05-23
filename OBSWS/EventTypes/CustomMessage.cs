using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.EventTypes
{
    public class CustomMessage
    {
        public string messagetype;
        public object data;

        public CustomMessage(string messagetype, object data)
        {
            this.messagetype = messagetype;
            this.data = data;
        }
    }

    public class customMessageData
    {
        public string messagetype { get; set; }
        public object data { get; set; }

        public customMessageData(string type, object data)
        {
            this.messagetype = type;
            this.data = data;
        }
    }
}
