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
    [HutongGames.PlayMaker.Tooltip("Set XR Device Transform information. Note, this action does not return the transform, but rather sets position and rotation by the api.")]
    public class SetXRDeviceTransformInfo : FsmStateAction
    {
        [ObjectType(typeof(XRNode))]
        public FsmEnum xrController;

        [ActionSection("Input")]
        public FsmVector3 position;

        public FsmQuaternion rotation;

        [ActionSection("Options")]
        public FsmBool everyFrame;

                [ActionSection("Event")]
public FsmEvent noDeviceFound;

        private XRNode _xrController = XRNode.LeftHand;
        private List<XRNodeState> nodeStates = new List<XRNodeState>();
        private XRNodeState _nodeState;

        public override void Reset()
        {
            rotation = new FsmQuaternion() {UseVariable = true};
            position = new FsmVector3() {UseVariable = true};
            everyFrame = false;
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
            if (!position.IsNone) _nodeState.position = position.Value;
            if (!rotation.IsNone) _nodeState.rotation = rotation.Value;
        }
    }
}