using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This will allow us to get InputDevice
using UnityEngine.XR;
using TMPro;
using System.IO;


public class InputManager : MonoBehaviour
{
    //Creating a List of Input Devices to store our Input Devices in
    List<InputDevice> inputDevices = new List<InputDevice>();
    public Canvas ui;
    public Canvas stats;
    public Camera cam;
    double waitTimeUI = 0.75;
    double waitTimeStats = 0.75;
    bool triggerValue;
    bool previousTriggerState = false;
    bool gripValue;
    bool previousGripState = false;
    bool menuValue;
    bool previousMenuState = false;
    bool primaryValue;
    bool previousPrimaryState = false;
    bool secondaryValue;
    bool previousSecondaryState = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //cam.stereoTargetEye = StereoTargetEyeMask.None;
        //timer = GameObject.Find("ScriptHandler").GetComponents<Timer>();
        //We will try to Initialize the InputReader here, but all components may not be loaded
        InitializeInputReader();
    }

    //This will try to initialize the InputReader by getting all the devices and printing them to the debugger.
    void InitializeInputReader()
    {

        InputDevices.GetDevices(inputDevices);
        //Debug.Log("Getting");

        
    }

    // Update is called once per frame
    void Update()
    {
        waitTimeUI -= Time.deltaTime;
        waitTimeStats -= Time.deltaTime;
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
                //Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
                //ui.enabled = !ui.enabled;
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue){
                    Debug.Log("Trigger button is pressed.");
                    previousTriggerState = true;
                }
                else{
                    previousTriggerState = false;
                }


                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue){
                    if(!previousGripState){
                        if(waitTimeUI < 0){
                            Debug.Log("Grip button is pressed.");
                            ui.enabled = !ui.enabled;
                            waitTimeUI = 0.75;
                        }
                    }
                    previousGripState = true;
                }
                else{
                    previousGripState = false;
                }


                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuValue) && menuValue){
                    Debug.Log("Menu button is pressed.");
                    previousMenuState = true;
                    cam.fieldOfView = 60f;
                }
                else{
                    previousMenuState = false;
                }


                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryValue) && primaryValue){
                    Debug.Log("Primary button is pressed.");
                    previousPrimaryState = true;
                    cam.fieldOfView += 0.1f;
                    if(waitTimeStats < 0){
                        stats.enabled = !stats.enabled;
                        waitTimeStats = 0.75;
                    }
                }
                else{
                    previousPrimaryState = false;
                }


                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryValue) && secondaryValue){
                    Debug.Log("Seconday button is pressed.");
                    previousSecondaryState = true;
                    cam.fieldOfView -= 0.1f;
                }
                else{
                    previousSecondaryState = false;
                }
            }
        }
    }

    public static void statePressed(string name){
        Debug.Log(name);
        //Debug.Log(name == timer[0].getCurrentState());
    }
}