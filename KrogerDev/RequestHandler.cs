using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev
{
    class RequestHandler
    {
        public void handle(string rawData, Dictionary<string, object> data)
        {
            switch(data["message-id"])
            {
                case "GetAuthRequired":
                    {
                        Console.Out.WriteLine(rawData);
                        break;
                    }

                default:
                    {
                        //Default case
                        break;
                    }
            }
        }
    }
}
