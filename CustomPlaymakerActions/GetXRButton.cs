using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace DGD
{
    [ActionCategory("Unity XR Input")]
    [HutongGames.PlayMaker.Tooltip("Sends an Event when a Button is pressed.")]
    public class GetXRButton : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(XRControllerInput))]
        [HutongGames.PlayMaker.Tooltip("Specify Controller Type.")]
        public GameObject XRController;
        [HutongGames.PlayMaker.Tooltip("Select the button to be pressed.")]
        public Buttons button;

        public Action buttonAction;

        [HutongGames.PlayMaker.Tooltip("Event to send if the button is pressed.")]
        public FsmEvent sendEvent;

        [HutongGames.PlayMaker.Tooltip("Set to True if the button is pressed.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult = false;

        private XRControllerInput inputtracking;
        private UnityEvent theEvent;

        public override void OnEnter()
        {
            inputtracking = XRController.GetComponent<XRControllerInput>();
            SetupEvent();
        }

        public override void OnExit()
        {
            theEvent.RemoveListener(FireEvent);
            base.OnExit();
        }

        public void SetupEvent()
        {
            switch (buttonAction)
            {
                case Action.ButtonDown:
                    {
                        switch (button)
                        {
                            case Buttons.GripButton:
                                {
                                    theEvent = inputtracking.OnGripPress;
                                    inputtracking.OnGripPress.AddListener(FireEvent);                                    
                                    break;
                                }
                            case Buttons.MenuButton:
                                {
                                    theEvent = inputtracking.OnMenuButtonPress;
                                    inputtracking.OnMenuButtonPress.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.Primary2DAxisButton:
                                {
                                    theEvent = inputtracking.OnPrimary2DAxisPress;
                                    inputtracking.OnPrimary2DAxisPress.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.PrimaryButton:
                                {
                                    theEvent = inputtracking.OnPrimaryButtonPress;
                                    inputtracking.OnPrimaryButtonPress.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.Secondary2DAxisButton:
                                {
                                    theEvent = inputtracking.OnSecondary2DAxisPress;
                                    inputtracking.OnSecondary2DAxisPress.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.SecondaryButton:
                                {
                                    theEvent = inputtracking.OnSecondaryButtonPress;
                                    inputtracking.OnSecondaryButtonPress.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.TriggerButton:
                                {
                                    theEvent = inputtracking.OnTriggerPress;
                                    inputtracking.OnTriggerPress.AddListener(FireEvent);
                                    break;
                                }
                        }
                        break;
                    }
                case Action.ButtonUp:
                    {
                        switch (button)
                        {
                            case Buttons.GripButton:
                                {
                                    theEvent = inputtracking.OnGripRelease;
                                    inputtracking.OnGripRelease.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.MenuButton:
                                {
                                    theEvent = inputtracking.OnMenuButtonRelease;
                                    inputtracking.OnMenuButtonRelease.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.Primary2DAxisButton:
                                {
                                    theEvent = inputtracking.OnPrimary2DAxisRelease;
                                    inputtracking.OnPrimary2DAxisRelease.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.PrimaryButton:
                                {
                                    theEvent = inputtracking.OnPrimaryButtonRelease;
                                    inputtracking.OnPrimaryButtonRelease.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.Secondary2DAxisButton:
                                {
                                    theEvent = inputtracking.OnSecondary2DAxisRelease;
                                    inputtracking.OnSecondary2DAxisRelease.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.SecondaryButton:
                                {
                                    theEvent = inputtracking.OnSecondaryButtonRelease;
                                    inputtracking.OnSecondaryButtonRelease.AddListener(FireEvent);
                                    break;
                                }
                            case Buttons.TriggerButton:
                                {
                                    theEvent = inputtracking.OnTriggerRelease;
                                    inputtracking.OnTriggerRelease.AddListener(FireEvent);
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public void FireEvent()
        {
            storeResult.Value = true;
            Fsm.Event(sendEvent);            
        }
    }

    public enum Controllers
    {
        LeftHand = XRNode.LeftHand,
        RightHand = XRNode.RightHand
    }

    public enum Buttons
    {
        PrimaryButton,
        SecondaryButton,
        TriggerButton,
        GripButton,
        Primary2DAxisButton,
        Secondary2DAxisButton,
        MenuButton
    }

    public enum Action
    {
        ButtonDown,
        ButtonUp
    }
}