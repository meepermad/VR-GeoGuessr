using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeInterval = 10;
    private float timeRemaining = 10;
    private List<int> Images = new List<int>();
    public Material wrapper;
    private List<int> usedImages = new List<int>();
    private List<double[]> coordinates = new List<double[]>();
    private FileInfo[] photos;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the file path for the Photo Library and puts them in an array
        string photoLibraryPath = Directory.GetCurrentDirectory() + @"\Assets\photoLibrary";
        DirectoryInfo dir = new DirectoryInfo(photoLibraryPath);
        photos = dir.GetFiles( "*.jpg" );
 
        // Makes a list of indexes for every photo
        for(int i = 0; i < photos.Length - 1; i++){
            Images.Add(i);
        }

        // Makes a secondary list that can be manipulated that will copy from the original list
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

                //If there isn't an image, add all the indexes back to the photo library
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
        Texture newPhoto = NewPhoto(num);
        wrapper.SetTexture("_MainTex", newPhoto);
        usedImages.RemoveAt(num);
    }

    // Puts all the indexes back to usedImages to be used again
    public void RestartList(){
        for(int i = 0; i < Images.Count; i++){
            usedImages.Add(Images[i]);
        }
    }

    // Takes an index to select a photo
    public Texture2D NewPhoto(int index){
        return LoadJPG(photos[usedImages[index]].FullName);
    }
}
