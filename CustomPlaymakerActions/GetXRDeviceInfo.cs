// Custom Action by DumbGameDev
// www.dumbgamedev.com

//using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;

namespace DGD
{
    [ActionCategory("Unity XR Input")]
    [Tooltip("Get Grip value between 0 to 1")]
    public class GetXRDeviceInfo : FsmStateAction
    {
        [ObjectType(typeof(XRNode))]
        public FsmEnum xrController;

        [ActionSection("Output")]
        [UIHint(UIHint.Variable)]
        public FsmString name;
        [UIHint(UIHint.Variable)]
        public FsmBool isValid;
        [UIHint(UIHint.Variable)]
        public FsmString manufacturer;
        [UIHint(UIHint.Variable)]
        public FsmString serialNumber;

        private XRControllerInput input;
        private XRNode _xrController = XRNode.LeftHand;
        private List<InputDevice> devices = new List<InputDevice>();
        private InputDevice device;

        public override void Reset()
        {
            name = null;
            isValid = false;
            manufacturer = null;
            serialNumber = null;
            xrController = null;
        }

        public override void OnEnter()
        {
            if (!GetDevice())
            {
                return;
            }

            GetValue();
            Finish();
        }

        private bool GetDevice()
        {
            _xrController = (XRNode) xrController.Value;
            InputDevices.GetDevicesAtXRNode(_xrController, devices);

            if (devices != null)
            {
                device = devices.FirstOrDefault();
                return true;
            }
            else
            {
                return false;
            }
        }

        void GetValue()
        {
            name.Value = device.name;
            isValid.Value = device.isValid;
            manufacturer.Value = device.manufacturer;
            serialNumber.Value = device.serialNumber;
        }
    }
}