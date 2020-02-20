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
            newPixelArray = new Color[pixels.Length/texScale];
        }
         GetComponent<Renderer>().material.mainTexture = camTex ;
       

        if(!camTex.isPlaying)
        {
            camTex.Play();
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
        int counter = 0;
        for(int x = 0 ; x<pixels.Length; x+=texScale){
            newPixelArray[counter] = pixels[x];
            counter += 1;
        }   
            
        rasterizedTex.SetPixels(newPixelArray);
        
        Debug.Log(rasterizedTex.GetPixel(1, 1));
        Debug.Log(camTex.GetPixel(1, 1));
            // for(int i = 0; i < 10; i++) {
            //     Debug.Log(pixels[i]);
            // }
        // for(int x = 0; x < 32; x++){
        //     newPixelArray[x] = pixels[x*2];
        // }

      //  rasterizedTex.SetPixels(newPixelArray, 1);
        
    }
}
