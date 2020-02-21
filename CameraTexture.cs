using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{
    static WebCamTexture camTex;
    private Texture2D rasterizedTex;
    private Color[] pixels;
    private Color[] newPixelArray;
    public bool rasterized;

    public int texScale = 10;

    void Start()
    {
        if(camTex == null)
        {
            camTex = new WebCamTexture(); //input height = 720 iput width=1280 16:9 ratio length of pixel array 256
            pixels = camTex.GetPixels();
        }
       
        if(!camTex.isPlaying)
        {
            camTex.Play();
        }
        
        rasterizedTex = new Texture2D(72, 128); //Array of 1024 pixels
        newPixelArray = new Color[pixels.Length/texScale];

        if(rasterized == true) {
            GetComponent<Renderer>().material.mainTexture = rasterizedTex ;
        } else {
            GetComponent<Renderer>().material.mainTexture = camTex ;
        }
        //Debug.Log(rasterizedTex.width);
    }

    void Update()
    {
        pixels = camTex.GetPixels();

        for(int y = 0; y< rasterizedTex.height; y++){
           for(int x = 0; x< rasterizedTex.width; x++){
               Color color = camTex.GetPixel(x*texScale, y*texScale);
               rasterizedTex.SetPixel(x, y, color);
    
            }  
        } 

        rasterizedTex.Apply();
          
    }
}
