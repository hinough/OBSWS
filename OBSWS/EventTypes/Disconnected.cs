using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.EventTypes
{
    public class Disconnected
    {
        public string message { get; set; }
        public string title { get; set; }
        public string location { get; set; }

        public Disconnected(string title, string message, string location)
        {
            this.message = message;
            this.title = title;
            this.location = location;
        }
    }
}
