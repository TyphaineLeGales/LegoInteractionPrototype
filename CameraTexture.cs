using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{
    static WebCamTexture camTex;
    private Texture2D rasterizedTex;
    private Color[] pixels;
    private Color[] newPixelArray;
    private Transform t;
    private Transform[] instances;

    public bool rasterized;
    public int texScale = 32;
    public Transform prefab;
    public float size = 0.1f;

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
        instances = new Transform[texScale*texScale];
        Debug.Log(instances.Length);

        if(rasterized == true) {
            GetComponent<Renderer>().material.mainTexture = rasterizedTex ;
        } else {
            GetComponent<Renderer>().material.mainTexture = camTex ;
        }
        
        int arrayIndex = 0;
        for(int y = 0; y< rasterizedTex.height; y++){
           for(int x = 0; x< rasterizedTex.width; x++){
            t = Instantiate(prefab, new Vector3(x, 0, y), Quaternion.identity);
            instances[arrayIndex]= t;
            arrayIndex += 1;
           }
        }


    }

    void Update()
    {
        pixels = camTex.GetPixels();
        int arrayIndex = 0;
        for(int y = 0; y< rasterizedTex.height; y++){
           for(int x = 0; x< rasterizedTex.width; x++){
               Color color = camTex.GetPixel(x*texScale, y*texScale);
               rasterizedTex.SetPixel(x, y, color);
                // Transform t = Instantiate(prefab);
                // t.localPosition =  new Vector3(x,0, y);
                // t.transform.localScale = new Vector3(size, size, size);
                instances[arrayIndex].GetComponent<Renderer>().material.color = color;
                arrayIndex += 1;
                // Debug.Log(arrayIndex);

            }  
        } 

        rasterizedTex.Apply();   
    }
}
