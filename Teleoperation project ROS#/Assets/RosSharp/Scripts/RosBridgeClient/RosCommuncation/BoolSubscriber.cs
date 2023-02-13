using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class BoolSubscriber : UnitySubscriber<MessageTypes.Std.Bool>
    {
        public bool messageData;

        private bool success;
        public bool isMessageReceived;

        protected override void Start()
        {
            base.Start();
        }

         private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        private void ProcessMessage()
        {
            messageData = success;
        }

        protected override void ReceiveMessage(MessageTypes.Std.Bool message)
        {
            messageData = message.data;
        }
    }
}