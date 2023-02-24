using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Assets;

public class Timer : MonoBehaviour
{
    public float timeInterval = 10;
    public float timeRemaining = 10;
    public List<Texture> Images;
    public Material wrapper;
    private List<Texture> usedImages;

    // Start is called before the first frame update
    void Start()
    {
        string path1 = Directory.GetCurrentDirectory();
        string path = Directory.GetParent(path1).FullName;
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles( "*.jpg" );
 
        foreach ( FileInfo f in info )
        {
            print ( "Found: " + f.Name );
        }

        for(int i = 0; i < Images.Count; i++){
            usedImages.Add(Images[i]);
        }

        int num = Random.Range(0, usedImages.Count - 1);
        wrapper.SetTexture("_MainTex", usedImages[num]);
        usedImages.RemoveAt(num);

    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
        } else {
            if(usedImages.Count != 0){
                int num = Random.Range(0, usedImages.Count - 1);
                wrapper.SetTexture("_MainTex", usedImages[num]);
                usedImages.RemoveAt(num);
            }
        }
    }
}
