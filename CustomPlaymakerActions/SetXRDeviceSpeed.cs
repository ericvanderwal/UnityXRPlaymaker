// Custom Action by DumbGameDev
// www.dumbgamedev.com

//using UnityEngine;

using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.XR;

namespace DGD
{
    [ActionCategory("Unity XR Input")]
    [HutongGames.PlayMaker.Tooltip("Set device's velocity and acceleration, both normal and angular")]
    public class SetXRDeviceSpeed : FsmStateAction
    {
        [ObjectType(typeof(XRNode))]
        public FsmEnum xrController;

        [ActionSection("Input")]
        public FsmVector3 acceleration;

        public FsmVector3 angularAcceleration;

        public FsmVector3 velocity;

        public FsmVector3 angularVelocity;

        [ActionSection("Options")]
        public FsmBool everyFrame;
        
        [ActionSection("Event")]
        public FsmEvent noDeviceFound;

        private XRNode _xrController = XRNode.LeftHand;
        private List<XRNodeState> nodeStates = new List<XRNodeState>();
        private XRNodeState _nodeState;
        private Vector3 _acceleration;
        private Vector3 _velocity;
        private Vector3 _angularAcceleration;
        private Vector3 _angularVelocity;

        public override void Reset()
        {
            velocity = new FsmVector3() {UseVariable = true};
            angularAcceleration = new FsmVector3() {UseVariable = true};
            everyFrame = false;
            angularVelocity = new FsmVector3() {UseVariable = true};
            acceleration = new FsmVector3() {UseVariable = true};
            noDeviceFound = null;
        }

        public override void OnEnter()
        {
            if (!GetNodeState())
            {
                if (noDeviceFound != null) Fsm.Event(noDeviceFound);
                Finish();
            }

            GetValue();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                GetValue();
            }
        }

        private bool GetNodeState()
        {
            _xrController = (XRNode) xrController.Value;
            InputTracking.GetNodeStates(nodeStates);

            for (var index = 0; index < nodeStates.Count; index++)
            {
                XRNodeState nodeState = nodeStates[index];
                if (nodeState.nodeType == _xrController)
                {
                    _nodeState = nodeState;
                    return true;
                }
            }

            return false;
        }


        void GetValue()
        {
            if (!acceleration.IsNone) _nodeState.acceleration = acceleration.Value;
            if (velocity.IsNone) _nodeState.velocity = velocity.Value;
            if (angularAcceleration.IsNone) _nodeState.angularAcceleration = angularAcceleration.Value;
            if (angularVelocity.IsNone) _nodeState.angularVelocity = angularVelocity.Value;
        }
    }
}