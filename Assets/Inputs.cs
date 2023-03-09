using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.InputSystem;

[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }

public class Inputs : MonoBehaviour
{
    public PrimaryButtonEvent primaryButtonPress;

    private bool lastButtonState = false;
    private List<UnityEngine.XR.InputDevice> devicesWithPrimaryButton;

    private void Awake()
    {
        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
            
        }

        devicesWithPrimaryButton = new List<UnityEngine.XR.InputDevice>();
    }

    void OnEnable()
    {
        List<UnityEngine.XR.InputDevice> allDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach(UnityEngine.XR.InputDevice device in allDevices){
            InputDevices_deviceConnected(device);
            Debug.Log(device);
        }
        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithPrimaryButton.Clear();
    }

    private void InputDevices_deviceConnected(UnityEngine.XR.InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out discardedValue))
        {
            devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(UnityEngine.XR.InputDevice device)
    {
        if (devicesWithPrimaryButton.Contains(device))
            devicesWithPrimaryButton.Remove(device);
    }

    void Update()
    {
        bool tempStatePrim = false;
        foreach (var device in devicesWithPrimaryButton)
        {
            bool primaryButtonState = false;
            tempStatePrim = device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonState) // did get a value
                        && primaryButtonState // the value we got
                        || tempStatePrim; // cumulative result from other controllers
            print(device);
            print(device.name);
            //print("Left: " + device.Left);
            //print("Right: " + device.Right);
        }

        if (tempStatePrim != lastButtonState) // Button state changed since last frame
        {
            primaryButtonPress.Invoke(tempStatePrim);
            lastButtonState = tempStatePrim;
            print(1);
        }


        /*if (Gamepad.current.aButton.isPressed) {
            print("A is pressed!");
        }*/
    }
}