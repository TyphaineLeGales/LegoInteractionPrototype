using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// private int instances;
// public Mesh objMesh;
// public Material objMat;
// Access rasterized tex

public class TexInstancer : MonoBehaviour
{
    public Transform prefab;
    public int instances = 1024;
    public float size = 50f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < instances; i++) {
            Transform t = Instantiate(prefab);
            t.localPosition = Random.insideUnitSphere*size;
            t.SetParent(transform);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     RenderBatches();
    // }

    // private void RenderBatches()
    // {
    //     foreach(int batch in batches){
    //         Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
    //     }
    // }
}
