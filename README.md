# UnityXRPlaymaker
Unity XR Playmaker Integration

These c# files have been created to be used in conjunction with the Unity3d asset Playmaker avaliable on the Unity Asset Store.

The following project and files contain within are officially released under the MIT license as stated below. 

## License

**Copyright 2020 Eric Vander Wal**

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Current Author and Contributors

Currently this managed and authored by **Eric Vander Wal**

If you are interested in contributing to this project, please contact me privately at tcmeric@gmail.com, as well as join our playmaker slack channel: https://invite-playmaker-slack.herokuapp.com

## Thank You

Thank you to Alex Coulombe @ibrews: https://github.com/ibrews/XRInputExamples for the init commit, and others who have contributed to the Unity Forums on XR. Without those examples and comments, this project would not be so far along.

## Project Source

This project can be downloaded in full for free from : https://github.com/dumbgamedev/UnityXRPlaymaker

## Tutorials

Currently no tutorials exist. 

**Required**

Setup your scene as you would with an Unity XR Input (search youtube). Once you have your initial scene setup and your desired XR device selected in player settings, go ahead and add the component (script) named XRControllerInput.cs onto any gameobject in the scene. Set to left controller. Then again for right, headset or any other controller you wish to track.

The XRControllerInput.cs script contains options for UnityEvents, standard Delegate/Events for C# as well as Playmaker Send Events. 

To use the playmaker send events, simply supply the FSM gameobject you would like to send the event to, on the XRControllerInput component. Copy the event name from the component, and add it to your playmaker FSM (no need tag it as a global event). Now, when the event fires on the XRControllerInput component, it will call your playmaker event of the same name.

This package also comes with custom playmaker actions for getting controller or device information. Such as trigger, touchpad, grip, primary button, secondary button, menu button value and states. All of these actions require the XRControllerInput component on a gameobject to function correctly.

More general purpose actions will be added later.
