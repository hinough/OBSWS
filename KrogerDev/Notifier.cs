using System;
using System.Collections.Generic;
using System.Text;

namespace KrogerDev
{
    class Notifier
    {
        public event EventHandler<EventNotification> onEvent;
        public event EventHandler<RequestNotification> onRequest;

        public Notifier(EventHandler<EventNotification> onEvent, EventHandler<RequestNotification> onRequest)
        {
            this.onEvent = onEvent;
            this.onRequest = onRequest;
        }

        public void notifyEvent()
        {

        }

        public void notifyRequest()
        {

        }


    }

    public class EventNotification
    {

    }

    public class RequestNotification
    {

    }
}
