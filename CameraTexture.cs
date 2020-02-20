using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{
    static WebCamTexture camTex;
    private Texture2D rasterizedTex;
    public Color[] pixels;
    public Color[] newPixelArray;
    private int texScale = 10;
   
    // Start is called before the first frame update
    void Start()
    {
        if(camTex == null)
        {
            camTex = new WebCamTexture(); //input height = 720 iput width=1280 16:9 ratio
            rasterizedTex = new Texture2D(camTex.width/texScale, camTex.height/texScale); //Array of 1024 pixels

            pixels = camTex.GetPixels();

            int counter = 0;
            for(int x = 0 ; x<10; x+=1){
                newPixelArray[counter] = pixels[x];
                counter += 1;
            }  

            Debug.Log(newPixelArray.Length);     
            
        }

         GetComponent<Renderer>().material.mainTexture = camTex ;
        //Debug.Log(camTex.GetPixels());

        if(!camTex.isPlaying)
        {
            camTex.Play();
        }
        
    }


    // Update is called once per frame
    void Update()
    {
      
            // for(int i = 0; i < 10; i++) {
            //     Debug.Log(pixels[i]);
            // }
        // for(int x = 0; x < 32; x++){
        //     newPixelArray[x] = pixels[x*2];
        // }

      //  rasterizedTex.SetPixels(newPixelArray, 1);
        
    }
}
