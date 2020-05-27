// Created by Eric Vander Wal (DumbGameDev).
//Based work by Alex Coulombe @ibrews: https://github.com/ibrews/XRInputExamples
//Go to https://docs.unity3d.com/2019.3/Documentation/Manual/xr_input.html which devices support which input mappings

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
#if PLAYMAKER
using HutongGames.PlayMaker;

#endif


namespace DGD
{
    public class XRControllerInput : MonoBehaviour
    {
        #region VARS

        [Header("Input")]
        [SerializeField]
        [UnityEngine.Tooltip("Left or Right Hand. This script does not need to be on the actual controller object.")]
        private XRNode XRController = XRNode.LeftHand;

        private List<InputDevice> devices = new List<InputDevice>();

        private InputDevice device;

        [Header("Debug")]
        [UnityEngine.Tooltip("When enbaled, disables controller input.")]
        public bool keyboardDebug = false;

        [UnityEngine.Tooltip("How much should each keyboard press of float values change by?")]
        public float debugAxisValueIncrement = 0.1f;

        [UnityEngine.Tooltip("Minimum value that needs to be read of the axes to register. If you're getting input without touching anything, increase this value")]
        public float minAxisValue = 0.15f;

        [UnityEngine.Tooltip("If set to true, debug logs will be printed. Recommended this is set to false for production games")]
        public bool enableDebugLogs = false;

        // private versions of the controller input variables before passed to public versions
        private bool _triggerButton = false;
        private float _triggerValue = 0.0f;
        private bool _gripButton = false;
        private float _gripValue = 0.0f;
        private bool _primary2DAxisButton = false;
        private Vector2 _primary2DAxisValue = Vector2.zero;
        private bool _secondary2DAxisButton = false;
        private Vector2 _secondary2DAxisValue = Vector2.zero;
        private bool _primaryButton = false;
        private bool _secondaryButton = false;
        private bool _menuButton = false;

        [Header("Input Values")]

        // public controller input variables
        public bool triggerButton = false;

        [Range(0, 1)]
        public float triggerValue = 0.0f;

        public bool gripButton = false;

        [Range(0, 1)]
        public float gripValue = 0.0f;

        public bool primary2DAxisButton = false;

        [HideInInspector]
        public Vector2 primary2DAxisValue = Vector2.zero;

        [Range(-1, 1)]
        public float primary2DAxisXValue = 0.0f;

        [Range(-1, 1)]
        public float primary2DAxisYValue = 0.0f;

        public bool secondary2DAxisButton = false;

        [HideInInspector]
        public Vector2 secondary2DAxisValue = Vector2.zero;

        [Range(-1, 1)]
        public float secondary2DAxisXValue = 0.0f;

        [Range(-1, 1)]
        public float secondary2DAxisYValue = 0.0f;

        public bool primaryButton = false;
        public bool secondaryButton = false;
        public bool menuButton = false;

        #endregion

        #region Events

        [Header("Unity Events")]

        // Events
        [UnityEngine.Tooltip("Event when the Trigger starts being pressed")]
        public UnityEvent OnTriggerPress;

        [UnityEngine.Tooltip("Event when the Trigger is released")]
        public UnityEvent OnTriggerRelease;

        [UnityEngine.Tooltip("Event when the Trigger is completely pressed down")]
        public UnityEvent OnTriggerClick;

        [UnityEngine.Tooltip("Event when the Grip starts being pressed")]
        public UnityEvent OnGripPress;

        [UnityEngine.Tooltip("Event when the Grip is released")]
        public UnityEvent OnGripRelease;

        [UnityEngine.Tooltip("Event when the Primary 2D Axis Click starts being pressed")]
        public UnityEvent OnPrimary2DAxisPress;

        [UnityEngine.Tooltip("Event when the Primary 2D Axis Click is released")]
        public UnityEvent OnPrimary2DAxisRelease;

        [UnityEngine.Tooltip("Event when the Primary 2D Axis Access Has Changed")]
        public UnityEvent OnPrimary2DAxisChange;

        [UnityEngine.Tooltip("Event when the Secondary 2D Axis Click starts being pressed")]
        public UnityEvent OnSecondary2DAxisPress;

        [UnityEngine.Tooltip("Event when the Secondary 2D Axis Click is released")]
        public UnityEvent OnSecondary2DAxisRelease;

        [UnityEngine.Tooltip("Event when the Primary Button starts being pressed")]
        public UnityEvent OnPrimaryButtonPress;

        [UnityEngine.Tooltip("Event when the Primary Button is released")]
        public UnityEvent OnPrimaryButtonRelease;

        [UnityEngine.Tooltip("Event when the Secondary Button starts being pressed")]
        public UnityEvent OnSecondaryButtonPress;

        [UnityEngine.Tooltip("Event when the Secondary Button is released")]
        public UnityEvent OnSecondaryButtonRelease;

        [UnityEngine.Tooltip("Event when the Menu Button starts being pressed")]
        public UnityEvent OnMenuButtonPress;

        [UnityEngine.Tooltip("Event when the Menu Button is released")]
        public UnityEvent OnMenuButtonRelease;


        // check if playmaker is installed
#if PLAYMAKER

        [Header("Playmaker Global Events")]

        // Events
        [UnityEngine.Tooltip("Event when the Trigger starts being pressed")]
        public string xrTriggerPress = "xrTriggerPress";

        public PlayMakerFSM xrTriggerPressFSM;

        [UnityEngine.Tooltip("Event when the Trigger is released")]
        public string xrTriggerRelease = "xrTriggerRelease";

        public PlayMakerFSM xrTriggerReleaseFSM;

        [UnityEngine.Tooltip("Event when the Trigger is completely pressed down")]
        public string xrTriggerClick = "xrTriggerClick";

        public PlayMakerFSM xrTriggerClickFSM;

        [UnityEngine.Tooltip("Event when the Grip starts being pressed")]
        public string xrGripPress = "xrGripPress";

        public PlayMakerFSM xrGripPressFSM;


        [UnityEngine.Tooltip("Event when the Grip is released")]
        public string xrGripRelease = "xrGripRelease";

        public PlayMakerFSM xrGripReleaseFSM;


        [UnityEngine.Tooltip("Event when the Primary 2D Axis Click starts being pressed")]
        public string xrPrimary2DAxisPress = "xrPrimary2DAxisPress";

        public PlayMakerFSM xrPrimary2DAxisPressFSM;


        [UnityEngine.Tooltip("Event when the Primary 2D Axis Click is released")]
        public string xrPrimary2DAxisRelease = "xrPrimary2DAxisRelease";

        public PlayMakerFSM xrPrimary2DAxisReleaseFSM;

        [UnityEngine.Tooltip("Event when the Primary 2D Axis Changed")]
        public string xrPrimary2DAxisChanged = "xrPrimary2DAxisChanged";

        public PlayMakerFSM xrPrimary2DAxisChangedFSM;


        [UnityEngine.Tooltip("Event when the Secondary 2D Axis Click starts being pressed")]
        public string xrSecondary2DAxisPress = "xrSecondary2DAxisPress";

        public PlayMakerFSM xrSecondary2DAxisPressFSM;


        [UnityEngine.Tooltip("Event when the Secondary 2D Axis Click is released")]
        public string xrSecondary2DAxisRelease = "xrSecondary2DAxisRelease";

        public PlayMakerFSM xrSecondary2DAxisReleaseFSM;


        [UnityEngine.Tooltip("Event when the Primary Button starts being pressed")]
        public string xrPrimaryButtonPress = "xrPrimaryButtonPress";

        public PlayMakerFSM xrPrimaryButtonPressFSM;


        [UnityEngine.Tooltip("Event when the Primary Button is released")]
        public string xrPrimaryButtonRelease = "xrPrimaryButtonRelease";

        public PlayMakerFSM xrPrimaryButtonReleaseFSM;


        [UnityEngine.Tooltip("Event when the Secondary Button starts being pressed")]
        public string xrSecondaryButtonPress = "xrSecondaryButtonPress";

        public PlayMakerFSM xrSecondaryButtonPressFSM;


        [UnityEngine.Tooltip("Event when the Secondary Button is released")]
        public string xrSecondaryButtonRelease = "xrSecondaryButtonRelease";

        public PlayMakerFSM xrSecondaryButtonReleaseFSM;


        [UnityEngine.Tooltip("Event when the Menu Button starts being pressed")]
        public string xrMenuButtonPress = "xrMenuButtonPress";

        public PlayMakerFSM xrMenuButtonPressFSM;


        [UnityEngine.Tooltip("Event when the Menu Button is released")]
        public string xrMenuButtonRelease = "xrMenuButtonRelease";

        public PlayMakerFSM xrMenuButtonReleaseFSM;

#endif


        // Events & Delegates

        public delegate void EventTriggerPress(float triggerValue);

        public event EventTriggerPress eventTriggerPress;

        public delegate void EventTriggerRelease(float triggerValue);

        public event EventTriggerRelease eventTriggerRelease;

        public delegate void EventTriggerClick(float triggerValue);

        public event EventTriggerClick eventTriggerClick;

        public delegate void EventGripPress(bool gripValue);

        public event EventGripPress eventGripPress;

        public delegate void EventGripRelease(bool gripValue);

        public event EventGripRelease eventGripRelease;

        public delegate void EventPrimary2DAxisPress(Vector2 axisValue);

        public event EventPrimary2DAxisPress eventPrimary2DAxisPress;

        public delegate void EventPrimary2DAxisRelease(Vector2 axisValue);

        public event EventPrimary2DAxisRelease eventPrimary2DAxisRelease;

        public delegate void EventPrimary2DAxisChange(Vector2 axisValue);

        public event EventPrimary2DAxisChange eventPrimary2DAxisChange;

        public delegate void EventSecondary2DAxisPress(Vector2 axisValue);

        public event EventSecondary2DAxisPress eventSecondary2DAxisPress;

        public delegate void EventSecondary2DAxisRelease(Vector2 axisValue);

        public event EventSecondary2DAxisRelease eventSecondary2DAxisRelease;

        public delegate void EventPrimaryButtonPress(bool buttonValue);

        public event EventPrimaryButtonPress eventPrimaryButtonPress;

        public delegate void EventPrimaryButtonRelease(bool buttonValue);

        public event EventPrimaryButtonRelease eventPrimaryButtonRelease;

        public delegate void EventSecondaryButtonPress(bool buttonValue);

        public event EventSecondaryButtonPress eventSecondaryButtonPress;

        public delegate void EventSecondaryButtonRelease(bool buttonValue);

        public event EventSecondaryButtonRelease eventSecondaryButtonRelease;

        public delegate void EventMenuButtonPress(bool buttonValue);

        public event EventMenuButtonPress eventMenuButtonPress;

        public delegate void EventMenuButtonRelease(bool buttonValue);

        public event EventMenuButtonRelease eventMenuButtonRelease;

        #endregion

        #region Functions

        private void OnEnable()
        {
            if (!device.isValid)
            {
                GetDevice();
            }
        }

        private void GetDevice()
        {
            InputDevices.GetDevicesAtXRNode(XRController, devices);
            device = devices.FirstOrDefault();
        }

        public void Recenter()
        {
            UnityEngine.XR.InputTracking.Recenter();
        }

        private bool TriggerButtonAction
        {
            get { return triggerButton; }
            set
            {
                if (value == triggerButton) return;
                triggerButton = value;

                // if trigger is down
                if (value)
                {
                    OnTriggerPress?.Invoke();
                    eventTriggerPress?.Invoke(_triggerValue);
#if PLAYMAKER
                    if (xrTriggerPressFSM != null) xrTriggerPressFSM.SendEvent(xrTriggerPress);
#endif
                }
                // if trigger is up
                else
                {
                    OnTriggerRelease?.Invoke();
                    eventTriggerRelease?.Invoke(_triggerValue);
#if PLAYMAKER
                    if (xrTriggerReleaseFSM != null) xrTriggerReleaseFSM.SendEvent(xrTriggerRelease);
#endif
                }

                if (enableDebugLogs) Debug.Log($"Trigger Press {triggerButton} on {XRController}");
            }
        }

        private bool GripButtonAction
        {
            get { return gripButton; }
            set
            {
                if (value == gripButton) return;
                gripButton = value;

                if (value == true)
                {
                    OnGripPress?.Invoke();
                    eventGripPress?.Invoke(gripButton);
#if PLAYMAKER
                    if (xrGripPressFSM != null) xrGripPressFSM.SendEvent(xrGripPress);
#endif
                }
                else
                {
                    OnGripRelease?.Invoke();
                    eventGripRelease?.Invoke(gripButton);
#if PLAYMAKER
                    if (xrTriggerReleaseFSM != null) xrTriggerReleaseFSM.SendEvent(xrGripRelease);
#endif
                }

                if (enableDebugLogs) Debug.Log($"Grip Press {gripButton} on {XRController}");
            }
        }

        private bool Primary2DAxisButtonAction
        {
            get { return primary2DAxisButton; }
            set
            {
                if (value == primary2DAxisButton) return;
                primary2DAxisButton = value;

                if (value == true)
                {
                    OnPrimary2DAxisPress?.Invoke();
                    eventPrimary2DAxisPress?.Invoke(_primary2DAxisValue);
#if PLAYMAKER
                    if (xrPrimary2DAxisPressFSM != null) xrPrimary2DAxisPressFSM.SendEvent(xrPrimary2DAxisPress);
#endif
                }
                else
                {
                    OnPrimary2DAxisRelease?.Invoke();
                    eventPrimary2DAxisRelease?.Invoke(_primary2DAxisValue);
#if PLAYMAKER
                    if (xrPrimary2DAxisReleaseFSM != null) xrPrimary2DAxisReleaseFSM.SendEvent(xrPrimary2DAxisRelease);
#endif
                }

                if (enableDebugLogs) Debug.Log($"Primary 2D Axis Button Press {primary2DAxisButton} on {XRController}");
            }
        }

        private bool Secondary2DAxisButtonAction
        {
            get { return secondary2DAxisButton; }
            set
            {
                if (value == secondary2DAxisButton) return;
                secondary2DAxisButton = value;

                if (value == true)
                {
                    OnSecondary2DAxisPress?.Invoke();
                    eventSecondary2DAxisPress?.Invoke(_secondary2DAxisValue);
#if PLAYMAKER
                    if (xrSecondary2DAxisPressFSM != null) xrSecondary2DAxisPressFSM.SendEvent(xrSecondary2DAxisPress);
#endif
                }
                else
                {
                    OnSecondary2DAxisRelease?.Invoke();
                    eventSecondary2DAxisRelease?.Invoke(_secondary2DAxisValue);
#if PLAYMAKER
                    if (xrSecondary2DAxisReleaseFSM != null) xrSecondary2DAxisReleaseFSM.SendEvent(xrSecondary2DAxisRelease);
#endif
                }

                if (enableDebugLogs) Debug.Log($"Secondary 2D Axis Button Press {secondary2DAxisButton} on {XRController}");
            }
        }

        private bool PrimaryButtonAction
        {
            get { return primaryButton; }
            set
            {
                if (value == primaryButton) return;
                primaryButton = value;

                if (value)
                {
                    OnPrimaryButtonPress?.Invoke();
                    eventPrimaryButtonPress?.Invoke(primaryButton);
#if PLAYMAKER
                    if (xrPrimaryButtonPressFSM != null) xrPrimaryButtonPressFSM.SendEvent(xrPrimaryButtonPress);
#endif
                }
                else
                {
                    OnPrimaryButtonRelease?.Invoke();
                    eventPrimaryButtonRelease?.Invoke(primaryButton);
#if PLAYMAKER
                    if (xrPrimaryButtonReleaseFSM != null) xrPrimaryButtonReleaseFSM.SendEvent(xrPrimaryButtonRelease);
#endif
                }

                if (enableDebugLogs) Debug.Log($"Primary Button Press {primaryButton} on {XRController}");
            }
        }

        private bool SecondaryButtonAction
        {
            get { return secondaryButton; }
            set
            {
                if (value == secondaryButton) return;
                secondaryButton = value;

                if (value)
                {
                    OnSecondaryButtonPress?.Invoke();
                    eventSecondaryButtonPress?.Invoke(secondaryButton);
#if PLAYMAKER
                    if (xrSecondaryButtonPressFSM != null) xrSecondaryButtonPressFSM.SendEvent(xrSecondaryButtonPress);
#endif
                }
                else
                {
                    OnSecondaryButtonRelease?.Invoke();
                    eventSecondaryButtonRelease?.Invoke(secondaryButton);
#if PLAYMAKER
                    if (xrSecondaryButtonReleaseFSM != null) xrSecondaryButtonReleaseFSM.SendEvent(xrSecondaryButtonRelease);
#endif
                }

                if (enableDebugLogs) Debug.Log($"Secondary Button Press {secondaryButton} on {XRController}");
            }
        }

        private bool MenuButtonAction
        {
            get { return menuButton; }
            set
            {
                if (value == menuButton) return;
                menuButton = value;

                if (value)
                {
                    OnMenuButtonPress?.Invoke();
                    eventMenuButtonPress?.Invoke(menuButton);
#if PLAYMAKER
                    if (xrMenuButtonPressFSM != null) xrMenuButtonPressFSM.SendEvent(xrMenuButtonPress);
#endif
                }
                else
                {
                    OnMenuButtonRelease?.Invoke();
                    eventMenuButtonRelease?.Invoke(menuButton);
#if PLAYMAKER
                    if (xrMenuButtonReleaseFSM != null) xrMenuButtonReleaseFSM.SendEvent(xrMenuButtonRelease);
#endif
                }

                if (enableDebugLogs) Debug.Log($"Menu Button Press {menuButton} on {XRController}");
            }
        }

        // Events that don't invoke events targetable from the editor because they're all ranged float values. Fill in your own actions at ///
        private float TriggerValueAction
        {
            get { return triggerValue; }
            set
            {
                if (value == triggerValue) return;

                triggerValue = value;


                //if trigger is completely down - Click
                if (triggerValue == 1)
                {
                    eventTriggerClick?.Invoke(_triggerValue);
                    OnTriggerClick?.Invoke();
#if PLAYMAKER
                    if (xrTriggerClickFSM != null) xrTriggerClickFSM.SendEvent(xrTriggerClick);
#endif
                    if (enableDebugLogs) Debug.Log($"Trigger Click {triggerButton} on {XRController}");
                }


                if (enableDebugLogs) Debug.Log($"Trigger Value {(Mathf.RoundToInt(triggerValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            }
        }

        private float GripValueAction
        {
            get { return gripValue; }
            set
            {
                if (value == gripValue) return;
                gripValue = value;

                if (enableDebugLogs) Debug.Log($"Trigger Value {(Mathf.RoundToInt(gripValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            }
        }

        private Vector2 Primary2DAxisValueAction
        {
            get { return primary2DAxisValue; }
            set
            {
                if (value == primary2DAxisValue) return;
                primary2DAxisValue = value;
                primary2DAxisXValue = primary2DAxisValue.x;
                primary2DAxisYValue = primary2DAxisValue.y;


                //if axis pad value has changed
                if (CheckAxisValue(primary2DAxisValue, minAxisValue))
                {
                    eventPrimary2DAxisChange?.Invoke(primary2DAxisValue);
                    OnPrimary2DAxisChange?.Invoke();
#if PLAYMAKER
                    if (xrPrimary2DAxisChangedFSM != null) xrPrimary2DAxisChangedFSM.SendEvent(xrPrimary2DAxisChanged);
#endif
                    if (enableDebugLogs) Debug.Log($"Primary Axis Value Changed on {XRController}");
                }


                if (enableDebugLogs) Debug.Log($"Primary2DAxis X value {(Mathf.RoundToInt(primary2DAxisXValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
                if (enableDebugLogs) Debug.Log($"Primary2DAxis Y value {(Mathf.RoundToInt(primary2DAxisYValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            }
        }

        private Vector2 Secondary2DAxisValueAction
        {
            get { return secondary2DAxisValue; }
            set
            {
                if (value == secondary2DAxisValue) return;
                secondary2DAxisValue = value;
                secondary2DAxisXValue = secondary2DAxisValue.x;
                secondary2DAxisYValue = secondary2DAxisValue.y;

                if (enableDebugLogs) Debug.Log($"Secondary2DAxis X value {(Mathf.RoundToInt(secondary2DAxisXValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
                if (enableDebugLogs) Debug.Log($"Secondary2DAxis Y value {(Mathf.RoundToInt(secondary2DAxisYValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            }
        }

        /// <summary>
        /// Check that an Vector2 axis value is being touched outside of the min value range.
        /// </summary>
        /// <param name="axisValue"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        private bool CheckAxisValue(Vector2 axisValue, float minValue)
        {
            if (!axisValue.x.IsWithin(-minValue, minValue))
            {
                return true;
            }

            if (!axisValue.y.IsWithin(-minValue, minValue))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Update

        void Update()
        {
            // Recenter XR player location with R
            if (Input.GetKeyDown(KeyCode.R))
            {
                UnityEngine.XR.InputTracking.Recenter();
            }

            // Keyboard Debug. Disables controller input. For clarity, recommend only turning on with left or right hand script at a time.
            if (keyboardDebug)
            {
                // trigger
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //you could also use GetKeyDown and GetKeyUp for Press (true) and Release (false)
                    TriggerButtonAction = !TriggerButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.PageUp) && triggerValue < 1)
                {
                    TriggerValueAction += debugAxisValueIncrement;
                }

                if (Input.GetKeyDown(KeyCode.PageDown) && triggerValue > 0)
                {
                    TriggerValueAction -= debugAxisValueIncrement;
                }

                // grip
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    GripButtonAction = !GripButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.KeypadPlus) && gripValue < 1)
                {
                    GripValueAction += debugAxisValueIncrement;
                }

                if (Input.GetKeyDown(KeyCode.KeypadMinus) && gripValue > 0)
                {
                    GripValueAction -= debugAxisValueIncrement;
                }

                // primary 2D axis
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Primary2DAxisButtonAction = !Primary2DAxisButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) && primary2DAxisValue.y < 1)
                {
                    _primary2DAxisValue += Vector2.up * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) && primary2DAxisValue.y > -1)
                {
                    _primary2DAxisValue -= Vector2.up * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) && primary2DAxisValue.x < 1)
                {
                    _primary2DAxisValue += Vector2.right * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) && primary2DAxisValue.x > -1)
                {
                    _primary2DAxisValue -= Vector2.right * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                // secondary 2D axis
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Secondary2DAxisButtonAction = !Secondary2DAxisButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.Keypad8) && secondary2DAxisValue.y < 1)
                {
                    _secondary2DAxisValue += Vector2.up * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.Keypad2) && secondary2DAxisValue.y > -1)
                {
                    _secondary2DAxisValue -= Vector2.up * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.Keypad6) && secondary2DAxisValue.x < 1)
                {
                    _secondary2DAxisValue += Vector2.right * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.Keypad4) && secondary2DAxisValue.x > -1)
                {
                    _secondary2DAxisValue -= Vector2.right * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                // primary button
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    PrimaryButtonAction = !PrimaryButtonAction;
                }

                // secondary button
                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    SecondaryButtonAction = !SecondaryButtonAction;
                }

                // menu button
                if (Input.GetKeyDown(KeyCode.BackQuote))
                {
                    MenuButtonAction = !MenuButtonAction;
                }
            }

            if (!keyboardDebug)
            {
                if (!device.isValid)
                {
                    GetDevice();
                }

                // These ranged, non-boolean inputs invoke the events above that are not targetable from the editor

                // Capture trigger value
                if (device.TryGetFeatureValue(CommonUsages.trigger, out _triggerValue))
                {
                    if (_triggerValue > minAxisValue) TriggerValueAction = _triggerValue;
                    else TriggerValueAction = 0f;
                }

                // Capture grip value
                if (device.TryGetFeatureValue(CommonUsages.grip, out _gripValue))
                {
                    if (_gripValue > minAxisValue) GripValueAction = _gripValue;
                    else GripValueAction = 0f;
                }
                //don't forget to use an absolute value for the axes

                // Capture primary 2D Axis
                if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _primary2DAxisValue))
                {
                    if (Mathf.Abs(_primary2DAxisValue.x) > minAxisValue || Mathf.Abs(_primary2DAxisValue.y) > minAxisValue) Primary2DAxisValueAction = _primary2DAxisValue;
                    else Primary2DAxisValueAction = Vector2.zero;
                }

                // Capture secondary 2D Axis
                if (device.TryGetFeatureValue(CommonUsages.secondary2DAxis, out _secondary2DAxisValue))
                {
                    if (Mathf.Abs(_secondary2DAxisValue.x) > minAxisValue || Mathf.Abs(_secondary2DAxisValue.y) > minAxisValue) Secondary2DAxisValueAction = _secondary2DAxisValue;
                    else Secondary2DAxisValueAction = Vector2.zero;
                }


                // These press/release inputs invoke the public, editor-definable events above

                // Capture trigger button      
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out _triggerButton))
                {
                    if (_triggerButton) TriggerButtonAction = true;
                    else TriggerButtonAction = false;
                }

                // Capture grip button
                if (device.TryGetFeatureValue(CommonUsages.gripButton, out _gripButton))
                {
                    if (_gripButton) GripButtonAction = true;
                    else GripButtonAction = false;
                }

                // Capture primary 2d axis button
                if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out _primary2DAxisButton))
                {
                    if (_primary2DAxisButton) Primary2DAxisButtonAction = true;
                    else Primary2DAxisButtonAction = false;
                }

                // Capture secondary 2d axis button
                if (device.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out _secondary2DAxisButton))
                {
                    if (_secondary2DAxisButton) Secondary2DAxisButtonAction = true;
                    else Secondary2DAxisButtonAction = false;
                }

                // Capture primary button
                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out _primaryButton))
                {
                    if (_primaryButton) PrimaryButtonAction = true;
                    else PrimaryButtonAction = false;
                }

                // Capture secondary button
                if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out _secondaryButton))
                {
                    if (_secondaryButton) SecondaryButtonAction = true;
                    else SecondaryButtonAction = false;
                }

                // Capture menu button
                if (device.TryGetFeatureValue(CommonUsages.menuButton, out _menuButton))
                {
                    if (_menuButton) MenuButtonAction = true;
                    else MenuButtonAction = false;
                }
            }
        }

        #endregion
    }
}