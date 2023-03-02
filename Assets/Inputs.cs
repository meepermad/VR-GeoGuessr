using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Inputs : MonoBehaviour
{
    public UnityEngine.XR.InputDevice leftHand;
    public UnityEngine.XR.InputDevice rightHand;
    public InputHelpers.Button button;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristicsLeft = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsLeft, leftHandedControllers);

        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristicsRight = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsRight, rightHandedControllers);

        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        foreach (var device in leftHandedControllers){
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }

        foreach (var device in rightHandedControllers){
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }

        foreach(var item in devices){
            print(item.name + item.characteristics);
            if(leftHandDevices.Count == 1){
                leftHand = leftHandDevices[0];
            } else{
                Debug.Log("Not 1 left hand");
            }
            if(rightHandDevices.Count == 1){
                rightHand = rightHandDevices[0];
            } else{
                Debug.Log("Not 1 right hand");
            }
        }




        /*var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();
        if (device.TryGetFeatureUsages(inputFeatures))
        {
            foreach (var feature in inputFeatures)
            {
                if (feature.type == typeof(bool))
                {
                    bool featureValue;
                    if (device.TryGetFeatureValue(feature.As<bool>(), out featureValue))
                    {
                        Debug.Log(string.Format("Bool feature {0}'s value is {1}", feature.name, featureValue.ToString()));
                    }
                }
            }
        }*/
    }

    // Update is called once per frame
    void Update(){
        /*bool pressedRight;
        leftHand.inputDevice.IsPressed(button, out pressedRight);
        if (pressedRight) {
            Debug.Log("Button pressed on left hand: " + button);
        }

        bool pressedLeft;
        leftHand.inputDevice.IsPressed(button, out pressedLeft);
        if (pressedLeft) {
            Debug.Log("Button pressed on right hand: " + button);
        }*/

        
    }

    
}
