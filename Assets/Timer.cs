using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeInterval = 10;
    public float timeRemaining = 10;
    public List<Texture> Images = new List<Texture>();
    public Material wrapper;
    private List<Texture> usedImages = new List<Texture>();
    private List<double[]> coordinates = new List<double[]>();

    // Start is called before the first frame update
    void Start()
    {
        // Gets the file path for the Photo Library
        string photoLibraryPath = Directory.GetCurrentDirectory() + @"\Assets\photoLibrary";
        DirectoryInfo dir = new DirectoryInfo(photoLibraryPath);
        FileInfo[] photos = dir.GetFiles( "*.jpg" );
 
        // Goes through the Photo Library and adds them to the master photo library
        foreach (FileInfo f in photos){
            Texture a = null;
            a = LoadJPG(f.FullName);
            Images.Add(a);
        }

        // Makes a secondary list that can be manipulated without the fear of losing all the images
        RestartList();

        // Displays a random photo at the very start of the program so the user doesn't spawn into a blank skybox
        RandomPhoto();
    }

    // Update is called once per frame
    void Update()
    {

        // Checks to see if the elapsed time has passed
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
        } 
        else {
            // Restarts the timer
            timeRemaining = timeInterval;

            // Makes sure there are photos 
            if(usedImages.Count != 0){

                // If there is an image, set the skybox to a random photo
                RandomPhoto();
            } 
            else{

                //If there isn't an image, add all the photos back to the photo library
                RestartList();

            }
        }
    }



// -------------------------------------------- Functions --------------------------------------------

    //Takes a JPG and converts it to a texture
    public static Texture2D LoadJPG(string filePath){
        Texture2D texture;
        byte[] fileData;
        fileData = File.ReadAllBytes(filePath);
        //print(fileData);
        texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        return texture;
    }

    // Sets the skybox to a random photo from the list
    public void RandomPhoto(){
        int num = Random.Range(0, usedImages.Count - 1);
        wrapper.SetTexture("_MainTex", usedImages[num]);
        usedImages.RemoveAt(num);
    }

    // Puts all the photos back to usedImages to be used again
    public void RestartList(){
        for(int i = 0; i < Images.Count; i++){
            usedImages.Add(Images[i]);
        }
    }
}
