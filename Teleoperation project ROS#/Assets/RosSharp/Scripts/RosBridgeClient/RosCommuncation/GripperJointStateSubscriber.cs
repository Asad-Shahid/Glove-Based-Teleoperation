/*
© Siemens AG, 2017-2019
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

using System.Collections.Generic;
using System.Linq;

namespace RosSharp.RosBridgeClient
{
    public class GripperJointStateSubscriber : UnitySubscriber<MessageTypes.Sensor.JointState>
    {
        public List<string> JointNames;
        public double[] Effort;
        public double averageEffort;
        //private bool isMessageReceived;

        protected override void ReceiveMessage(MessageTypes.Sensor.JointState message)
        {
            Effort = new double[2];
            //isMessageReceived = true;
            int index;
            for (int i = 0; i < message.name.Length; i++)
            {
                index = JointNames.IndexOf(message.name[i]);
                Effort[index] = message.effort[i];
            }
            averageEffort = Effort.Average();
        }
    }
}

