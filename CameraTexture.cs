using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{
    static WebCamTexture camTex;
    private Texture2D rasterizedTex;
    public Color[] pixels;
    public Color[] newPixelArray;
    private int texScale = 8;
   
    // Start is called before the first frame update
    void Start()
    {
        if(camTex == null)
        {
            camTex = new WebCamTexture(); //input height = 720 iput width=1280 16:9 ratio length of pixel array 256
            rasterizedTex = new Texture2D(camTex.width/texScale, camTex.height/texScale); //Array of 1024 pixels
            pixels = camTex.GetPixels();
            
        }
       
        if(!camTex.isPlaying)
        {
            camTex.Play();
        }
        
        GetComponent<Renderer>().material.mainTexture = rasterizedTex ;
        
    }

    // Update is called once per frame
    void Update()
    {
        pixels = camTex.GetPixels();
        newPixelArray = new Color[pixels.Length/texScale];

        for(int x = 0 ; x<newPixelArray.Length; x+=1){

            newPixelArray[x] = pixels[x*texScale];
        }
            
        rasterizedTex.SetPixels(newPixelArray);
        
        Debug.Log(rasterizedTex.GetPixel(1, 1));
        Debug.Log(camTex.GetPixel(1, 1));
        
    }
}
