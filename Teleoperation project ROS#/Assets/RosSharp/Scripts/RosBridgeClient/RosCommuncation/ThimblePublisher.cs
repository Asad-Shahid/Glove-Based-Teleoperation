using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WeArt.Components;
using WeArt.Core;

using Texture = WeArt.Core.Texture;

using static WeArt.Components.WeArtTouchableObject;

namespace RosSharp.RosBridgeClient
{
    public class ThimblePublisher : UnityPublisher<MessageTypes.Std.Float32>
    {
        [SerializeField]
        internal WeArtThimbleTrackingObject

                _thumbThimbleTracking,
                _indexThimbleTracking;

        [SerializeField]
        internal WeArtHapticObject

                _thumbThimbleHaptic,
                _indexThimbleHaptic;

        [Range(0.0f, 1.0f)]
        public float sliderValue;

        float previousClosure;

        private MessageTypes.Std.Float32 message;

        public OnCollision script;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Float32 { data = sliderValue };
        }

        private void FixedUpdate()
        {
            CheckClosure();

            Publish (message);
        }

        private void CheckClosure()
        {
            float sliderValue =(_thumbThimbleTracking.Closure.Value + _indexThimbleTracking.Closure.Value) / 2;
            sliderValue = Mathf.Clamp(sliderValue, 0f, 1f);
            message.data = sliderValue;
            //float closureTreshold = 0.3f;

            /* if (_thumbThimbleTracking.Closure.Value >= closureTreshold &&
              _indexThimbleTracking.Closure.Value >= closureTreshold)
            { */
            /*
            if (!script.contactFlag)
            {
                sliderValue = Mathf.Clamp(sliderValue, 0f, 1.0f);
                message.data = sliderValue;
                previousClosure = sliderValue;
            }
            else
            {
                message.data = previousClosure;
                if (sliderValue < previousClosure)
                {
                    script.contactFlag = false;
                }
            }
            //}
            */
            /* else
            {
                message.data = Mathf.Min(_thumbThimbleTracking.Closure.Value, _indexThimbleTracking.Closure.Value);
            } */
        }
    }
}
