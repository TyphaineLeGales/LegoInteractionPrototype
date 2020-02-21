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

        if(rasterized == true) {
            GetComponent<Renderer>().material.mainTexture = rasterizedTex ;
        } else {
            GetComponent<Renderer>().material.mainTexture = camTex ;
        }

    }

    void Update()
    {
        pixels = camTex.GetPixels();

        for(int y = 0; y< rasterizedTex.height; y++){
           for(int x = 0; x< rasterizedTex.width; x++){
               Color color = camTex.GetPixel(x*texScale, y*texScale);
               rasterizedTex.SetPixel(x, y, color);
                Transform t = Instantiate(prefab);
                t.localPosition =  new Vector3(x,0, y);
                t.transform.localScale = new Vector3(size, size, size);
                t.GetComponent<Renderer>().material.color = color;
            //    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //    cube.transform.position = new Vector3(x*16,0, y*9);
            //    cube.transform.localScale = new Vector3(1, 1, 1);
            //    cube.GetComponent<Renderer>().material.color = color;

            }  
        } 

        rasterizedTex.Apply();   
    }
}
