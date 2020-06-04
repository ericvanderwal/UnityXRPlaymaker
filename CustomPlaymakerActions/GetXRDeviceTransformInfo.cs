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
    [HutongGames.PlayMaker.Tooltip("Get XR Device Transform information. Note, this action does not return the transform, but rather the information provided by the XR input device api.")]
    public class GetXRDeviceTransformInfo : FsmStateAction
    {
        [ObjectType(typeof(XRNode))]
        public FsmEnum xrController;

        [ActionSection("Output")]

        [UIHint(UIHint.Variable)]
        public FsmVector3 position;

        [UIHint(UIHint.Variable)]
        public FsmQuaternion rotation;

        [ActionSection("Options")]
        public FsmBool everyFrame;

                [ActionSection("Event")]
public FsmEvent noDeviceFound;

        private XRNode _xrController = XRNode.LeftHand;
        private List<XRNodeState> nodeStates = new List<XRNodeState>();
        private XRNodeState _nodeState;
        private Vector3 _position;
        private Quaternion _rotation;

        public override void Reset()
        {
            rotation = null;
            position = null;
            everyFrame = false;
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
            position.Value = _nodeState.TryGetPosition(out _position) ? _position : Vector3.zero;
            rotation.Value = _nodeState.TryGetRotation(out _rotation) ? _rotation : Quaternion.identity;
        }
    }
}