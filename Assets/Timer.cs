using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Assets;

public class Timer : MonoBehaviour
{
    public float timeInterval = 10;
    public float timeRemaining = 10;
    public List<Texture> Images = new List<Texture>();
    public Material wrapper;
    private List<Texture> usedImages = new List<Texture>();

    // Start is called before the first frame update
    void Start()
    {
        string path1 = Directory.GetCurrentDirectory() + @"\Assets\photoLibrary";
        string path = Directory.GetParent(path1).FullName;
        Debug.Log(path1);
        DirectoryInfo dir = new DirectoryInfo(path1);
        FileInfo[] info = dir.GetFiles( "*.jpg" );
 
        foreach ( FileInfo f in info )
        {
            //print ( "Found: " + f.Name );
            //print ( "Found: " + f.FullName );
            Texture a = null;
            a = LoadJPG(f.FullName);
            Images.Add(a);
        }

        for(int i = 0; i < Images.Count; i++){
            //print(i);
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
            timeRemaining = timeInterval;
            if(usedImages.Count != 0){
                int num = Random.Range(0, usedImages.Count - 1);
                wrapper.SetTexture("_MainTex", usedImages[num]);
                usedImages.RemoveAt(num);
            } else{
                usedImages = Images;
            }
        }
    }

    public static Texture2D LoadJPG(string filePath){
        Texture2D tex;
        byte[] fileData;
        fileData = File.ReadAllBytes(filePath);
        //print(fileData);
        tex = new Texture2D(2, 2);
        tex.LoadImage(fileData); //..t$$anonymous$$s will auto-resize the texture dimensions.
        return tex;
    }
}
