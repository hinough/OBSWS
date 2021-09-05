using System;

using Websocket.Client;

namespace KrogerDev
{
    public class ObsClient
    {
        WebsocketClient obsClient = null;

        public ObsClient(string ip, string port, string password = null)
        {
            obsClient = new WebsocketClient(new Uri("ws://" + ip + ":" + port));
        }
    }
}
