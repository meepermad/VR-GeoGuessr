using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This will allow us to get InputDevice
using UnityEngine.XR;
using TMPro;


public class Inputs2 : MonoBehaviour
{
    //Creating a List of Input Devices to store our Input Devices in
    List<InputDevice> inputDevices = new List<InputDevice>();
    
    // Start is called before the first frame update
    void Start()
    {
        //We will try to Initialize the InputReader here, but all components may not be loaded
        InitializeInputReader();
    }

    //This will try to initialize the InputReader by getting all the devices and printing them to the debugger.
    void InitializeInputReader()
    {

        InputDevices.GetDevices(inputDevices);

        foreach (var inputDevice in inputDevices)
        {
            Debug.Log(inputDevice.name + " " + inputDevice.characteristics);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //We should have a total of 3 Input Devices. If it’s less, then we try to initialize them again.
        if(inputDevices.Count < 2)
        {
            InitializeInputReader();
        } else{
            InputDevices.GetDevices(inputDevices);

            var inputDevices1 = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevices(inputDevices1);

            foreach (var device in inputDevices1)
            {
                Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
                bool triggerValue;
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    Debug.Log("Trigger button is pressed.");
                }
            }
        }
    }
}