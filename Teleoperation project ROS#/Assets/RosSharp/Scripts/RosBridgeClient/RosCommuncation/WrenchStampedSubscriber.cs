/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class WrenchStampedSubscriber : UnitySubscriber<MessageTypes.Geometry.WrenchStamped>
    {
        public ConstantForce PublishedWrench;

        private Vector3 force;
        private Vector3 torque;
        private bool isMessageReceived;

        protected override void Start()
        {
			base.Start();
		}
		
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.WrenchStamped message)
        {
            force = GetForce(message).Ros2Unity();
            torque = GetTorque(message).Ros2Unity();
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            PublishedWrench.force = force;
            PublishedWrench.torque = torque;
        }

        private Vector3 GetForce(MessageTypes.Geometry.WrenchStamped message)
        {
            return new Vector3(
                (float)message.wrench.force.x,
                (float)message.wrench.force.y,
                (float)message.wrench.force.z);
        }

        private Vector3 GetTorque(MessageTypes.Geometry.WrenchStamped message)
        {
            return new Vector3(
                (float)message.wrench.torque.x,
                (float)message.wrench.torque.y,
                (float)message.wrench.torque.z);
        }
    }
}