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
    [HutongGames.PlayMaker.Tooltip("Get device's velocity and acceleration, both normal and angular")]
    public class GetXRDeviceSpeed : FsmStateAction
    {
        [ObjectType(typeof(XRNode))]
        public FsmEnum xrController;

        [ActionSection("Output")]
        [UIHint(UIHint.Variable)]
        public FsmVector3 acceleration;

        [UIHint(UIHint.Variable)]
        public FsmVector3 angularAcceleration;

        [UIHint(UIHint.Variable)]
        public FsmVector3 velocity;

        [UIHint(UIHint.Variable)]
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
            velocity = null;
            angularAcceleration = null;
            everyFrame = false;
            angularVelocity = null;
            acceleration = null;
            noDeviceFound = null;
        }

        public override void OnEnter()
        {
            if (!GetNodeState())
            {
                Fsm.Event(noDeviceFound);
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
            acceleration.Value = _nodeState.TryGetAcceleration(out _acceleration) ? _acceleration : Vector3.zero;
            velocity.Value = _nodeState.TryGetVelocity(out _velocity) ? _velocity : Vector3.zero;
            angularAcceleration.Value = _nodeState.TryGetAngularAcceleration(out _angularAcceleration) ? _angularAcceleration : Vector3.zero;
            angularVelocity.Value = _nodeState.TryGetAngularVelocity(out _angularVelocity) ? _angularVelocity : Vector3.zero;
        }
    }
}