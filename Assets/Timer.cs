using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class Timer : MonoBehaviour
{
    public float timeInterval = 10;
    public float timeRemaining = 10;
    public Texture[] Images;
    public Material wrapper;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        wrapper.SetTexture("_MainTex", Images[0]);

    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
        } else {
            if(index == Images.Length -1){
                wrapper.SetTexture("_MainTex", Images[0]);
                index = 0;
            } else{
                index++;
                wrapper.SetTexture("_MainTex", Images[index]);
            }
            timeRemaining = timeInterval;
        }
    }
}
