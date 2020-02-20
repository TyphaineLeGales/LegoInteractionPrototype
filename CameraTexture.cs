using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{
    static WebCamTexture camTex;
    private Texture2D rasterizedTex;
    private Color[] pixels;
    private Color[] newPixelArray;

    public int texScale = 10;

   
    // Start is called before the first frame update
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
        
        
        rasterizedTex = new Texture2D(camTex.width/texScale, camTex.height/texScale); //Array of 1024 pixels
        newPixelArray = new Color[pixels.Length/texScale];

        GetComponent<Renderer>().material.mainTexture = rasterizedTex ;
        //Debug.Log(rasterizedTex.width);
    }

    // Update is called once per frame
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
