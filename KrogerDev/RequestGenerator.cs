using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev
{
    public static class RequestGenerator
    {
        /// <summary>
        /// Generates a request string formatted in JSON to send to OBS Server
        /// </summary>
        /// <param name="header">Request Type</param>
        /// <param name="id">Unique ID</param>
        /// <param name="additionaldata">Any additional data that the request requires</param>
        /// <returns>JSON Request as string</returns>
        public static string generateJson(string header, JObject additionaldata = null)
        {
            var body = new JObject
            {
                { "request-type", header },
                { "message-id", header }
            };

            if(additionaldata != null)
            {
                _ = new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                };

                body.Merge(additionaldata);
            }

            return body.ToString();
        }
    }
}
