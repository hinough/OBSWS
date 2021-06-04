using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public void getProfile()
        {
            ws.Send(obs.generateRequest(RequestType.getcurrentprofile));
        }
        
        public void getProfileList()
        {
            ws.Send(obs.generateRequest(RequestType.listprofiles));
        }

        public void getStats()
        {
            ws.Send(obs.generateRequest(RequestType.getstats));
        }

        public string getVersion()
        {
            return "V0.1 Alpha";
        }

        public void sendChat(string message)
        {
            ws.Send(obs.generateRequest(RequestType.broadcastmessage, new customMessageData(CustomType.chat, message)));
        }

        public void setScene(string name)
        {
            ws.Send(obs.generateRequest(RequestType.setcurrentscene, name));
        }

        public void setSceneCollection(string name)
        {
            ws.Send(obs.generateRequest(RequestType.setcurrentscenecollection, name));
        }

        public void setProfile(string name)
        {
            ws.Send(obs.generateRequest(RequestType.setcurrentprofile, name));
        }

        private void handleEvent(Dictionary<string, object> eventdata)
        {
            switch(eventdata["update-type"])
            {
                case EventType.broadcastreceived:
                    {
                        onInformation?.Invoke(this, new Information("Received: ", (string)eventdata["realm"], (string)eventdata["realm"]));
                        onCustomMessage?.Invoke(this, new CustomMessage((string)eventdata["realm"], eventdata["data"]));
                        break;
                    }
        
                case EventType.exiting:
                    {
                        onInformation?.Invoke(this, new Information("OBS Closed", "OBS was closed", "handleEvent:exiting"));
                        break;
                    }

                case EventType.heartbeat:
                    {
                        /*onHeartbeat?.Invoke(this, new Heartbeat((bool)eventdata["pulse"], (bool)eventdata["recording"], (bool)eventdata["streaming"],
                                                                (string)eventdata["current-profile"], (string)eventdata["current-scene"], 
                                                                (long)eventdata["total-stream-time"], (long)eventdata["total-stream-bytes"], (long)eventdata["total-stream-frames"],
                                                                (long)eventdata["total-record-time"], (long)eventdata["total-record-bytes"], (long)eventdata["total-record-frames"],
                                                                (JObject)eventdata["stats"]));*/
                        break;
                    }

                case EventType.profilechanged:
                    {
                        obs.currentProfile = (string)eventdata["profile"];
                        onProfilechange?.Invoke(this, (string)eventdata["profile"]);
                        break;
                    }

                case EventType.profilelistchanged:
                    {
                        onProfileListChange?.Invoke(this, obs.updateProfileList(eventdata, false));
                        break;
                    }

                case EventType.recordpaused:
                    {
                        onRecordingInformation?.Invoke(this, new Information("Recording Paused", "Recording was paused"));
                        break;
                    }

                case EventType.recordresumed:
                    {
                        onRecordingInformation?.Invoke(this, new Information("Recording Resumed", "Recording was resumed"));
                        break;
                    }

                case EventType.recordstarted:
                    {
                        onRecordingInformation?.Invoke(this, new Information("Recording Started", "Recording was started", null, (string)eventdata["recordingFilename"]));
                        break;
                    }

                case EventType.recordstarting:
                    {
                        onRecordingInformation?.Invoke(this, new Information("Recording Starting", "Recording is starting"));
                        break;
                    }

                case EventType.recordstopped:
                    {
                        onRecordingInformation?.Invoke(this, new Information("Recording Stopped", "Recording was stopped", null, (string)eventdata["recordingFilename"]));
                        break;
                    }

                case EventType.recordstopping:
                    {
                        onRecordingInformation?.Invoke(this, new Information("Recording Stopping", "Recording is stopping"));
                        break;
                    }

                case EventType.replaystarted:
                    {
                        onReplayInformation?.Invoke(this, new Information("Replay Started", "Replay was started"));
                        break;
                    }

                case EventType.replaystarting:
                    {
                        onReplayInformation?.Invoke(this, new Information("Replay Starting", "Replay is starting"));
                        break;
                    }

                case EventType.replaystopped:
                    {
                        onReplayInformation?.Invoke(this, new Information("Replay Stopped", "Replay was stopped"));
                        break;
                    }

                case EventType.replaystopping:
                    {
                        onReplayInformation?.Invoke(this, new Information("Replay Stopping", "Replay is stopping"));
                        break;
                    }

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

                case EventType.streamstarted:
                    {
                        onStreamingInformation?.Invoke(this, new Information("Stream Started", "Stream was started"));
                        break;
                    }

                case EventType.streamstarting:
                    {
                        onStreamingInformation?.Invoke(this, new Information("Stream Starting", "Stream is starting"));
                        break;
                    }

                case EventType.streamstopped:
                    {
                        onStreamingInformation?.Invoke(this, new Information("Stream Stopped", "Stream was stopped"));
                        break;
                    }

                case EventType.streamstopping:
                    {
                        onStreamingInformation?.Invoke(this, new Information("Stream Stopping", "Stream is stopping"));
                        break;
                    }

                case EventType.transitionbegin:
                    {
                        onTransitionBegin?.Invoke(this, new Transition((string)eventdata["name"], (string)eventdata["type"], (string)eventdata["from-scene"], (string)eventdata["to-scene"], (long)eventdata["duration"]));
                        break;
                    }

                case EventType.transitionend:
                    {
                        onTransitionEnd?.Invoke(this, new Transition((string)eventdata["name"], (string)eventdata["type"], null, (string)eventdata["to-scene"], (long)eventdata["duration"]));
                        break;
                    }

                case EventType.transitionvideoend:
                    {
                        onTransitionEnd?.Invoke(this, new Transition((string)eventdata["name"], (string)eventdata["type"], (string)eventdata["from-scene"], (string)eventdata["to-scene"], (long)eventdata["duration"]));
                        break;
                    }

                case EventType.transitionswitch:
                    {
                        onTransitionChanged?.Invoke(this, (string)eventdata["transition-name"]);
                        break;
                    }

                case EventType.transitionlist:
                    {
                        onTransitionListChanged?.Invoke(this, obs.updateTransitionList(eventdata));
                        break;
                    }

                case EventType.transitionduration:
                    {
                        onTransitionDurationChanged?.Invoke(this, (long)eventdata["new-duration"]);
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
                            ws.Send(obs.generateRequest(RequestType.listprofiles));
                            ws.Send(obs.generateRequest(RequestType.getscenelist));
                        }
                        else
                        {
                            onError?.Invoke(this, new Error("Wrong Password", "Given password was wrong","handleResponse:authenticate"));
                        }
                        break;
                    }

                case RequestType.broadcastmessage:
                    {
                        onCustomMessage?.Invoke(this, new CustomMessage(CustomType.chatsent, ""));
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
                            ws.Send(obs.generateRequest(RequestType.listprofiles));
                            ws.Send(obs.generateRequest(RequestType.getscenelist));
                        }
                        break;
                    }

                case RequestType.getcurrentprofile:
                    {
                        obs.currentProfile = (string)response["profile-name"];
                        onProfilechange?.Invoke(this, obs.currentProfile);
                        break;
                    }

                case RequestType.getcurrentscenecollection:
                    {
                        obs.currentSceneCollection = (string)response["sc-name"];
                        onSceneCollectionChanged?.Invoke(this, obs.currentSceneCollection);
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

                case RequestType.getstats:
                    {

                        break;
                    }

                case RequestType.listprofiles:
                    {
                        onProfileListChange?.Invoke(this, obs.updateProfileList(response, true));
                        break;
                    }

                case RequestType.listscenecollections:
                    {
                        onSceneCollectionListChanged?.Invoke(this, obs.updateSceneCollectionList(response));
                        ws.Send(obs.generateRequest(RequestType.getcurrentscenecollection));
                        break;
                    }

                case RequestType.setcurrentprofile:
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
                if (response["message-id"].Equals(RequestType.getstats))
                {
                    ObsStats stats = ((JObject)response["stats"]).ToObject<ObsStats>();
                    onStats?.Invoke(this, stats);
                }
                //Console.Out.WriteLine(e.Data);
                handleResponse(response);
            }
            else if (response.ContainsKey("update-type"))
            {
                //Console.Out.WriteLine(e.Data);
                handleEvent(response);
            }
        }

        private void wsOpened(object sender, EventArgs e)
        {
            ws.Send(obs.generateRequest(RequestType.getauthreq));
            ws.Send(obs.generateRequest(RequestType.getversion));
        }

        ///////////////////////////////////////////GENERAL EVENT///////////////////////////////////////////
        public event EventHandler<CustomMessage> onCustomMessage = null;
        public event EventHandler<ObsStats> onStats = null;

        ///////////////////////////////////////////EVENTHANDLERS///////////////////////////////////////////
        public event EventHandler<Connected> onConnect = null;
        public event EventHandler<Disconnected> onDisconnect = null;
        public event EventHandler<Error> onError = null;
        public event EventHandler<Information> onInformation = null;
        public event EventHandler<Information> onRecordingInformation = null;
        public event EventHandler<Information> onReplayInformation = null;
        public event EventHandler<Information> onStreamingInformation = null;

        ///////////////////////////////////////////PROFILE EVENTS///////////////////////////////////////////
        public event EventHandler<string> onProfilechange = null;
        public event EventHandler<List<string>> onProfileListChange = null;

        ////////////////////////////////////////////SCENE EVENTS////////////////////////////////////////////
        public event EventHandler<Scene> onActiveSceneChanged = null;
        public event EventHandler<List<Scene>> onSceneListChanged = null;

        ///////////////////////////////////////SCENE COLLECTION EVENTS//////////////////////////////////////
        public event EventHandler<string> onSceneCollectionChanged = null;
        public event EventHandler<List<string>> onSceneCollectionListChanged = null;

        //////////////////////////////////////////TRANSITION EVENTS/////////////////////////////////////////
        public event EventHandler<Transition> onTransitionBegin = null;
        public event EventHandler<Transition> onTransitionEnd = null;
        public event EventHandler<string> onTransitionChanged = null;
        public event EventHandler<long> onTransitionDurationChanged = null;
        public event EventHandler<List<string>> onTransitionListChanged = null;
    }
}
