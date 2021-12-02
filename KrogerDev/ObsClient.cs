using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using WebSocketSharp;

namespace KrogerDev
{
    public class ObsClient
    {
        EventHandler eventHandler = null;
        RequestHandler requestHandler = null;

        Notifier notifier = null;
        WebSocket obsClient = null;

        public ObsClient(string ip, string port, string password = null)
        {
            //HUSK DEBUGLOGGER
            notifier = new Notifier(onEvent, onRequest);

            eventHandler = new EventHandler();
            requestHandler = new RequestHandler();

            obsClient = new WebSocket("ws://" + ip + ":" + port);

            obsClient.OnMessage += onMessage;
        }

        public void connect()
        {
            obsClient.ConnectAsync();
        }

        public void onMessage(object sender, MessageEventArgs e)
        {
            Dictionary<string, object> response = JsonConvert.DeserializeObject <Dictionary<string, object>>(e.Data);

            if(response.ContainsKey("message-id"))
            {
                requestHandler.handle(e.Data, response);
            }
            else if(response.ContainsKey("update-type"))
            {
                eventHandler.handle(e.Data, response);
            }
        }

        public event EventHandler<EventNotification> onEvent;
        public event EventHandler<RequestNotification> onRequest;
    }
}
