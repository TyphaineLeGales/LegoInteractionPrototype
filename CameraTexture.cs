using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraTexture : MonoBehaviour
{
    static WebCamTexture camTex;
    private Texture2D rasterizedTex;
    private Color32[] pixels;
    private Color32[] newPixelArray;
    private Transform[] instances;
    private GameObject selected;

    public bool debugTex;
    public bool rasterized;
    public int texScale = 32;
    public Transform prefab;
    public float size = 0.1f;
    public bool renderOther;


    [Range(0,100)]
    public int stepMod = 5;

    void Start()
    {
        if(camTex == null)
        {
            camTex = new WebCamTexture(); //input height = 720 iput width=1280 16:9 ratio length of pixel array 256
            pixels = camTex.GetPixels32();
        }
       
        if(!camTex.isPlaying)
        {
            camTex.Play();
        }
        
        rasterizedTex = new Texture2D(camTex.width/texScale, camTex.height/texScale); //Array of 1024 pixels
        newPixelArray = new Color32[pixels.Length/texScale];
        instances = new Transform[texScale*texScale];

        if(rasterized == true) {
            GetComponent<Renderer>().material.mainTexture = rasterizedTex ;
        } else {
            GetComponent<Renderer>().material.mainTexture = camTex ;
        }
        
        int arrayIndex = 0;
        for(int y = 0; y< rasterizedTex.height; y++){
           for(int x = 0; x< rasterizedTex.width; x++){
            instances[arrayIndex]= Instantiate(prefab, new Vector3(x, 0, y), Quaternion.identity);
            arrayIndex += 1;
           }
        }

    }

    void Update()
    {
       
        if(Time.frameCount % stepMod == 0) {  
                TexToInstance();
        }
    }

    private Color32 GetColorOfSelected()
    {
        return Selection.activeGameObject.GetComponent<Renderer>().material.color;
    }

    private bool isWhite (Color32 color) {
         if(color.r > 100 && color.g > 100 && color.b > 100) {
           return true;
         } else {
           return false;
         }
    }

    private void TexToInstance() 
    {
         pixels = camTex.GetPixels32();
        int arrayIndex = 0;
        for(int y = 0; y< rasterizedTex.height; y++){
           for(int x = 0; x< rasterizedTex.width; x++){
                Color32 color = camTex.GetPixel(x*texScale, y*texScale);
                rasterizedTex.SetPixel(x, y, color);  
                if(color.r > 100 && color.g > 100 && color.b > 100) {
                    instances[arrayIndex].GetComponent<MeshRenderer>().enabled = true;
                    instances[arrayIndex].GetComponent<Renderer>().material.color = new Color32(255,255,255, 255);      
                } else if(color.r > 180 && color.g < 80 && color.b <80 ){  //red
                    instances[arrayIndex].GetComponent<MeshRenderer>().enabled = true;
                    instances[arrayIndex].GetComponent<Renderer>().material.color = new Color32(255,0,0, 255); 
                } else if(color.r > 100 && color.g > 100 && color.b < 80){  //yellow
                    instances[arrayIndex].GetComponent<MeshRenderer>().enabled = true;
                    instances[arrayIndex].GetComponent<Renderer>().material.color = new Color32(230,230,0, 255);
                } else if(color.b > 180 && color.r < 80 && color.g <80){  //blue
                    instances[arrayIndex].GetComponent<MeshRenderer>().enabled = true;
                    instances[arrayIndex].GetComponent<Renderer>().material.color = new Color32(0,0,255, 255);
                } else {
                    instances[arrayIndex].GetComponent<MeshRenderer>().enabled = false;
                    if(renderOther) {
                        instances[arrayIndex].GetComponent<MeshRenderer>().enabled = true;
                        instances[arrayIndex].GetComponent<Renderer>().material.color = new Color32(color.r,color.g,color.b, 255);
                    }
                }

                arrayIndex += 1;
            }  
        }

        rasterizedTex.Apply();
        Debug.Log(GetColorOfSelected());   
    }
}
