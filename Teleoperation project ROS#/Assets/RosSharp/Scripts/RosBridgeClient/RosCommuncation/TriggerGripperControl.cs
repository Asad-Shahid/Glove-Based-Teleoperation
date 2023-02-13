using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class TriggerGripperControl : UnityPublisher<MessageTypes.Std.Float32>
    {
        public OnCollision script;

        [Range(0.0f, 1.0f)]
        public float sliderValue;

        [Range(0.0f, 1.0f)]
        public float triggerValue;

        float previousClosure;

        private MessageTypes.Std.Float32 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Float32 { data = sliderValue };
        }

        private void Update()
        {
            CheckClosure();
            CheckTrigger();

            Publish (message);
        }

        public void CheckClosure()
        {
            if (!script.contactFlag)
            {
                message.data = sliderValue;
                previousClosure = sliderValue;
            }
            else
            {
                message.data = previousClosure;
                //sliderValue = Mathf.Clamp(sliderValue, 0f, previousClosure);
                if (sliderValue < previousClosure)
                {
                    script.contactFlag = false;
                }
            }
        }

        public void CheckTrigger()
        {
            if (triggerValue >= 0.7)
            {
               message.data += 10;
            }
        }
    }
}
