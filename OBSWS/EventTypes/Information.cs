using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS.EventTypes
{
    public class Information
    {
        public string filename { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        public string location { get; set; }

        public Information(string title, string message, string location = null, string filename = null)
        {
            this.message = message;
            this.title = title;
            this.location = location;
            this.filename = filename;
        }
    }
}
