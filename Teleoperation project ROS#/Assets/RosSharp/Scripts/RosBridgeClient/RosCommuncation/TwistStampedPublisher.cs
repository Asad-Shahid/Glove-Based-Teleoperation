using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class TwistStampedPublisher : UnityPublisher<MessageTypes.Geometry.TwistStamped>
    {
        public Transform PublishedTransform;
        public string FrameId = "Unity";

        private MessageTypes.Geometry.TwistStamped message;
        private float previousRealTime;        
        private Vector3 previousPosition = Vector3.zero;
        private Quaternion previousRotation = Quaternion.identity;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.TwistStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                },
                twist = new MessageTypes.Geometry.Twist()
                {
                    linear = new MessageTypes.Geometry.Vector3(),
                    angular = new MessageTypes.Geometry.Vector3()
                }
            };
            
           
        }

        private void UpdateMessage()
        {
            message.header.Update();
            Vector3 linearVelocity = (PublishedTransform.localPosition - previousPosition) / Time.fixedDeltaTime;
            Vector3 angularVelocity = (PublishedTransform.localRotation.eulerAngles - previousRotation.eulerAngles) / Time.fixedDeltaTime;

            GetGeometryVector3(linearVelocity.Unity2Ros());
            GetGeometryVector3(-angularVelocity.Unity2Ros());

            message.twist.linear = GetGeometryVector3(linearVelocity.Unity2Ros());
            message.twist.angular = GetGeometryVector3(-angularVelocity.Unity2Ros());

            previousPosition = PublishedTransform.localPosition;
            previousRotation = PublishedTransform.localRotation;

            Publish(message);
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;
            return geometryVector3;
        }

    }
}
