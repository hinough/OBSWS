using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev
{
    class EventHandler
    {
        public void handle(string rawData, Dictionary<string, object> data)
        {
            switch(data["update-type"])
            {
                default:
                    {
                        //Default case
                        break;
                    }
            }
        }
    }
}
