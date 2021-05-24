using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWS.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using System.Text;

namespace OBSWS
{
    class Obs
    {
        public Obs(string ip, string port, string password = null)
        {
            if (ip.StartsWith("ws://")) this._ip = ip.Substring(5);
            else this._ip = ip;

            this._pass = password;
            this._port = port;

            scenes = new List<Scene>();
            sceneCollections = new List<string>();
        }

        public string getIp()
        {
            return this._ip;
        }

        public void setIp(string ip)
        {
            this._ip = ip;
        }

        public string getPassword()
        {
            return this._pass;
        }

        public void setPassword(string password)
        {
            this._pass = password;
        }

        public string getPort()
        {
            return this._port;
        }

        public void setPort(string port)
        {
            this._port = port;
        }

        public string getUri()
        {
            return "ws://" + this._ip + ":" + this._port;
        }

        public string generateAuthentication(string challenge, string salt)
        {
            string authResp = generateAuthString(challenge, salt);

            string[] headers = new string[] { "auth" };
            string[] rdata = new string[] { authResp };

            return generateJson(RequestType.authenticate, RequestType.authenticate, headers, rdata);
        }

        public string generateRequest(string type, object data = null)
        {
            switch(type)
            {
                case RequestType.broadcastmessage:
                    {
                        var add = new JObject
                        {
                            {"realm", ((EventTypes.customMessageData)data).messagetype },
                            {"data", new JObject { {"A Chat", (string)((EventTypes.customMessageData)data).data } } }
                        };

                        return generateJson(type, type, add);
                    }

                case RequestType.setcurrentscene:
                    {
                        string[] headers = { "scene-name" };
                        object[] values =  { data };

                        return generateJson(type, type, headers, values);
                    }

                case RequestType.setcurrentscenecollection:
                    {
                        string[] headers = { "sc-name" };
                        object[] values = { data };

                        return generateJson(type, type, headers, values);
                    }

                default:
                    {
                        return generateJson(type, type, null, null);
                    }
            }
        }

        public Scene updateActiveScene(string name)
        {
            foreach (Scene scene in scenes)
            {
                if (scene.name.Equals(name))
                    currentScene = scene;
            }

            return currentScene;
        }

        public Scene updateScenelist(Dictionary<string, object> response)
        {
            scenes = ((JArray)response["scenes"]).ToObject<List<Scene>>();

            foreach(Scene scene in scenes)
            {
                if (scene.name.Equals((string)response["current-scene"]))
                    this.currentScene = scene;
            }

            return currentScene;
        }

        public List<string> updateSceneCollectionList(Dictionary<string, object> response)
        {
            sclist = ((JArray)response["scene-collections"]).ToObject<List<SceneCollection>>();

            sceneCollections.Clear();

            foreach(SceneCollection sc in sclist)
            {
                sceneCollections.Add(sc.scname);
            }


            return sceneCollections;
        }



        private string generateAuthString(string challenge, string salt)
        {
            string secret_string;
            byte[] secret_hash;
            string secret;

            string auth_resp_string;
            byte[] auth_resp_hash;
            string auth_resp;

            using (SHA256 sha256hash = SHA256.Create())
            {
                secret_string = this._pass + salt;
                secret_hash = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(secret_string));
                secret = System.Convert.ToBase64String(secret_hash);

                auth_resp_string = secret + challenge;
                auth_resp_hash = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(auth_resp_string));
                auth_resp = System.Convert.ToBase64String(auth_resp_hash);
            }

            return auth_resp;
        }

        private string generateJson(string header, string id, string[] headers = null, object[] data = null)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter  writer = new StringWriter(builder);

            using (JsonWriter jWriter = new JsonTextWriter(writer))
            {
                jWriter.Formatting = Formatting.Indented;

                jWriter.WriteStartObject();
                jWriter.WritePropertyName("request-type");
                jWriter.WriteValue(header);
                jWriter.WritePropertyName("message-id");
                jWriter.WriteValue(id);

                if(headers != null)
                {
                    for (int i = 0; i < headers.Length; i++)
                    {
                        jWriter.WritePropertyName(headers[i]);

                        if (data[i].GetType() == typeof(bool))
                            jWriter.WriteValue((bool)data[i]);

                        if (data[i].GetType() == typeof(string))
                            jWriter.WriteValue((string)data[i]);

                        if (data[i].GetType() == typeof(JObject))
                            jWriter.WriteValue((JObject)data[i]);
                    }
                }

                jWriter.WriteEndObject();
            }

            return builder.ToString();
        }

        private string generateJson(string header, string id, JObject additionaldata = null)
        {
            var body = new JObject
            {
                {"request-type", header },
                {"message-id",  id}
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

        ////////////////////////////////PRIVATE VALUES////////////////////////////////
        private List<SceneCollection> sclist = null;
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////WEBSOCKET SETTINGS//////////////////////////////
        private string _ip = null;                          //OBSWS Server IP
        private string _port = null;                        //OBSWS Server Port
        private string _pass = null;                        //OBSWS Server Password
        //////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////OBS VARIABLES////////////////////////////////
        public double obscomp { get; set; }                 //Compatible API version
        public string obswsver { get; set; }                //OBS Websockets plugin version
        public string obsver { get; set; }                  //OBS Studio version

        public Scene currentScene { get; set; }             //Current active scene in OBS
        public string currentSceneCollection { get; set; }  //Current active scenecollection in OBS
        public List<Scene> scenes { get; set; }             //List of all scenes in OBS
        public List<string> sceneCollections { get; set; }  //List of all scenecollections in OBS
        //////////////////////////////////////////////////////////////////////////////
    }
}
