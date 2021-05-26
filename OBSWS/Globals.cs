using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWS
{
    public static class CustomType
    {
        public const string chat = "chatmessage";
        public const string chatsent = "chatsent";
    }

    public static class EventType
    {
        public const string broadcastreceived = "BroadcastCustomMessage";

        public const string exiting = "Exiting";

        public const string profilechanged = "ProfileChanged";
        public const string profilelistchanged = "ProfileListChanged";

        public const string recordstarting = "RecordingStarting";
        public const string recordstarted = "RecordingStarted";
        public const string recordstopping = "RecordingStopping";
        public const string recordstopped = "RecordingStopped";
        public const string recordpaused = "RecordingPaused";
        public const string recordresumed = "RecordingResumed";

        public const string replaystarting = "ReplayStarting";
        public const string replaystarted = "ReplayStarted";
        public const string replaystopping = "ReplayStopping";
        public const string replaystopped = "ReplayStopped";

        public const string streamstarting = "StreamStarting";
        public const string streamstarted = "StreamStarted";
        public const string streamstopping = "StreamStopping";
        public const string streamstopped = "StreamStopped";

        public const string sceneschanged = "ScenesChanged";
        public const string scenecollectionchanged = "SceneCollectionChanged";
        public const string scenecollectionlistchanged = "SceneCollectionListChanged";
        public const string sceneswitch = "SwitchScenes";

        public const string transitionbegin = "TransitionBegin";
        public const string transitionswitch = "SwitchTransition";
        public const string transitionlist = "TransitionListChanged";
        public const string transitionduration = "TransitionDurationChanged";
    }

    public static class RequestType
    {
        public const string authenticate = "Authenticate";

        public const string broadcastmessage = "BroadcastCustomMessage";

        public const string getauthreq = "GetAuthRequired";
        public const string getstats = "GetStats";
        public const string getscenelist = "GetSceneList";
        public const string getversion = "GetVersion";

        public const string getcurrentscenecollection = "GetCurrentSceneCollection";
        public const string listscenecollections = "ListSceneCollections";
        public const string setcurrentscenecollection = "SetCurrentSceneCollection";

        public const string listprofiles = "ListProfiles";
        public const string getcurrentprofile = "GetCurrentProfile";
        public const string setcurrentprofile = "SetCurrentProfile";

        public const string setcurrentscene = "SetCurrentScene";
    }
}
