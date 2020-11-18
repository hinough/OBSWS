using Newtonsoft.Json;
using OBSWS.EventTypes;
using OBSWS.Types;
using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace OBSWS
{
    public class ObsConnection
    {
        private Obs obs = null;
        private WebSocket ws = null;

        public ObsConnection(string ip, string port, string password = null)
        {
            obs = new Obs(ip, port, password);
            ws = new WebSocket(obs.getUri());

            ws.OnClose += wsClosed;
            ws.OnMessage += wsMessage;
            ws.OnOpen += wsOpened;
        }

        public void connect()
        {
            onInformation?.Invoke(this, new Information("Connecting", "Connecting to OBS...", "OBSC.Connect()"));

            ws.ConnectAsync();
        }

        public void disconnect()
        {
            ws.CloseAsync();
        }


        public string getObsVersion()
        {
            return obs.obsver;
        }

        public string getObsWsVersion()
        {
            return obs.obswsver;
        }

        public string getVersion()
        {
            return "V0.1 Alpha";
        }

        public void setScene(string name)
        {
            ws.Send(obs.generateRequest(RequestType.setcurrentscene, name));
        }

        public void setSceneCollection(string name)
        {
            ws.Send(obs.generateRequest(RequestType.setcurrentscenecollection, name));
        }


        private void handleEvent(Dictionary<string, object> eventdata)
        {
            switch(eventdata["update-type"])
            {
                case EventType.sceneschanged:
                    {
                        ws.Send(obs.generateRequest(RequestType.getscenelist));
                        break;
                    }

                case EventType.scenecollectionchanged:
                    {
                        ws.Send(obs.generateRequest(RequestType.getcurrentscenecollection));
                        ws.Send(obs.generateRequest(RequestType.getscenelist));
                        ws.Send(obs.generateRequest(RequestType.listscenecollections));
                        break;
                    }

                case EventType.scenecollectionlistchanged:
                    {
                        ws.Send(obs.generateRequest(RequestType.getcurrentscenecollection));
                        ws.Send(obs.generateRequest(RequestType.getscenelist));
                        ws.Send(obs.generateRequest(RequestType.listscenecollections));
                        break;
                    }

                case EventType.sceneswitch:
                    {
                        onActiveSceneChanged?.Invoke(this, obs.updateActiveScene((string)eventdata["scene-name"]));
                        break;
                    }

                default:
                    {
                        onInformation?.Invoke(this, new Information("Unknown Event", "UNKNOWN EVENT: " + (string)eventdata["update-type"], "handleEvent:default"));
                        break;
                    }
            }
        }

        private void handleResponse(Dictionary<string, object> response)
        {
            switch(response["message-id"]) 
            {
                case RequestType.authenticate:
                    {
                        if(response["status"].Equals("ok"))
                        {
                            onInformation?.Invoke(this, new Information("Authenticated", "Successfully authenticated", "handleResponse:authenticate"));
                            onConnect?.Invoke(this, new Connected("Connected to OBS", "Connected to OBS", "handleResponse:authenticate"));
                            
                            ws.Send(obs.generateRequest(RequestType.listscenecollections));
                            ws.Send(obs.generateRequest(RequestType.getscenelist));
                        }
                        else
                        {
                            onError?.Invoke(this, new Error("Wrong Password", "Given password was wrong","handleResponse:authenticate"));
                        }
                        break;
                    }

                case RequestType.getauthreq:
                    {
                        if ((bool)response["authRequired"])
                        {
                            onInformation?.Invoke(this, new Information("Authentication Required", "Authentication Required", "handleResponse:getAuthReq"));
                            ws.Send(obs.generateAuthentication((string)response["challenge"], (string)response["salt"]));
                        }
                        else
                        {
                            onInformation?.Invoke(this, new Information("No Authentication Required", "No Authentication Required", "handleResponse:getAuthReq"));
                            onConnect?.Invoke(this, new Connected("Connected to OBS", "Connected to OBS", "handleResponse:getAuthReq"));
                            
                            ws.Send(obs.generateRequest(RequestType.listscenecollections));
                            ws.Send(obs.generateRequest(RequestType.getscenelist));
                        }
                        break;
                    }

                case RequestType.getcurrentscenecollection:
                    {
                        obs.currentSceneCollection = (string)response["sc-name"];
                        onSceneCollectionChanged?.Invoke(this, (string)response["sc-name"]);
                        break;
                    }

                case RequestType.getscenelist:
                    {
                        onActiveSceneChanged?.Invoke(this, obs.updateScenelist(response));
                        onSceneListChanged?.Invoke(this, obs.scenes);
                        break;
                    }

                case RequestType.getversion:
                    {
                        obs.obscomp = (double)response["version"];
                        obs.obswsver = (string)response["obs-websocket-version"];
                        obs.obsver = (string)response["obs-studio-version"];
                        break;
                    }

                case RequestType.listscenecollections:
                    {
                        onSceneCollectionListChanged?.Invoke(this, obs.updateSceneCollectionList(response));
                        ws.Send(obs.generateRequest(RequestType.getcurrentscenecollection));
                        break;
                    }

                case RequestType.setcurrentscenecollection:
                    {
                        //Handled
                        break;
                    }

                default:
                    {
                        onInformation?.Invoke(this, new Information("Unknown response", "UNKNOWN RESPONSE: " + response["message-id"], "handleResponse:default"));
                        break;
                    }
            }
        }

        private void wsClosed(object sender, CloseEventArgs e)
        {
            switch(e.Code)
            {
                //Connection lost (OBS closed, network down)
                case 1001:
                    {
                        onDisconnect?.Invoke(this, new Disconnected("Lost connection", "Connection to OBS was lost", "wsClosed:1001"));
                        break;
                    }

                //User induced disconnection
                case 1005:
                    {
                        onDisconnect?.Invoke(this, new Disconnected("Disconnected", "Disconnected from OBS", "wsClosed:1005"));
                        break;
                    }

                //Unknown IP.
                case 1006:
                    {
                        onError?.Invoke(this, new Error("Wrong IP given", "Given IP was unsupported or unreachable", "wsClosed:1006"));
                        break;
                    }

                default:
                    {
                        onInformation?.Invoke(this, new Information("Unknown response", "Websocket gave a unsupported code: " + e.Code.ToString(), "wsClosed:default"));
                        break;
                    }
            }  
        }

        private void wsMessage(object sender, MessageEventArgs e)
        {
            Dictionary<string, object> response = JsonConvert.DeserializeObject<Dictionary<string, object>>(e.Data);

            if (response.ContainsKey("message-id"))
            {
                handleResponse(response);
            }
            else if (response.ContainsKey("update-type"))
            {
                handleEvent(response);
            }
        }

        private void wsOpened(object sender, EventArgs e)
        {
            ws.Send(obs.generateRequest(RequestType.getauthreq));
            ws.Send(obs.generateRequest(RequestType.getversion));
        }

        ///////////////////////////////////////////EVENTHANDLERS///////////////////////////////////////////
        public event EventHandler<Connected> onConnect = null;
        public event EventHandler<Disconnected> onDisconnect = null;
        public event EventHandler<Error> onError = null;
        public event EventHandler<Information> onInformation = null;

        ///////////////SCENE EVENTS
        public event EventHandler<Scene> onActiveSceneChanged = null;
        public event EventHandler<List<Scene>> onSceneListChanged = null;

        ///////////////SCENE COLLECTION EVENTS
        public event EventHandler<string> onSceneCollectionChanged = null;
        public event EventHandler<List<string>> onSceneCollectionListChanged = null;
    }
}
