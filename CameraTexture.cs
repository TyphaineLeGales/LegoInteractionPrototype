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
        pixels = camTex.GetPixels32();
        int arrayIndex = 0;
        for(int y = 0; y< rasterizedTex.height; y++){
           for(int x = 0; x< rasterizedTex.width; x++){
                Color32 color = camTex.GetPixel(x*texScale, y*texScale);
                rasterizedTex.SetPixel(x, y, color);
                // Debug.Log (color.r);
                if(color.a <180) {
                //     // instances[arrayIndex].GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1f);
                //     Debug.Log(arrayIndex);
                   // instances[arrayIndex].GetComponent<MeshRenderer>().enabled = false;
                    instances[arrayIndex].GetComponent<Renderer>().material.color = new Color32(255,0,0, 255);
                } else {
                   // instances[arrayIndex].GetComponent<MeshRenderer>().enabled = true;
                    instances[arrayIndex].GetComponent<Renderer>().material.color = new Color32(color.r,color.g,color.b, 255);
                }

                arrayIndex += 1;
            }  
        }

        rasterizedTex.Apply();
        Debug.Log( GetColorOfSelected());   

    }

    private Color32 GetColorOfSelected()
    {
        return Selection.activeGameObject.GetComponent<Renderer>().material.color;
    }
}
