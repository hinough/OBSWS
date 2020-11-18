using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS
{
    public static class EventType
    {
        public const string sceneschanged = "ScenesChanged";
        public const string scenecollectionchanged = "SceneCollectionChanged";
        public const string scenecollectionlistchanged = "SceneCollectionListChanged";
        public const string sceneswitch = "SwitchScenes";
    }

    public static class RequestType
    {
        public const string authenticate = "Authenticate";
        
        public const string getauthreq = "GetAuthRequired";
        public const string getstats = "GetStats";
        public const string getscenelist = "GetSceneList";
        public const string getversion = "GetVersion";

        public const string getcurrentscenecollection = "GetCurrentSceneCollection";
        public const string listscenecollections = "ListSceneCollections";
        public const string setcurrentscenecollection = "SetCurrentSceneCollection";

        public const string setcurrentscene = "SetCurrentScene";
    }
}
