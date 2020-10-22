using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.EventTypes
{
    public class Information
    {
        public string message { get; set; }
        public string title { get; set; }
        public string location { get; set; }

        public Information(string title, string message, string location)
        {
            this.message = message;
            this.title = title;
            this.location = location;
        }
    }
}
