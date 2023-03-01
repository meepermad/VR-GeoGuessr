using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Inputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach(var item in devices){
            print(item.name + item.characteristics);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
