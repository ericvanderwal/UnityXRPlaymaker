// Custom Action by DumbGameDev
// www.dumbgamedev.com

using HutongGames.PlayMaker;

namespace DGD
{
    [ActionCategory("Unity XR Input")]
    [Tooltip("Get Trigger value between 0 to 1")]
    public class GetTriggerValue : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(XRControllerInput))]
        [Tooltip("Gameobject with the XR Controller Input script")]
        public FsmOwnerDefault inputGameObject;

        [ActionSection("Output")]
        [Tooltip("Trigger value between 0 - 1")]
        [UIHint(UIHint.Variable)]
        public FsmFloat triggerValue;

        [ActionSection("Options")]
        public FsmBool everyFrame;

        XRControllerInput input;

        public override void Reset()
        {
            inputGameObject = null;
            everyFrame = false;
            triggerValue = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(inputGameObject);
            input = go.GetComponent<XRControllerInput>();
            if (go == null || input == null)
            {
                return;
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

        void GetValue()
        {
            var go = Fsm.GetOwnerDefaultTarget(inputGameObject);
            if (go == null || input == null)
            {
                return;
            }

            triggerValue.Value = input.triggerValue;
        }
    }
}