using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QudTexture : MonoBehaviour
{
    private Texture2D texture;
    public int resolution = 256;
    // Start is called before the first frame update
    void OnEnable()
    {
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24,true);
        texture.name = "Procedural Texture";
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        FillTexture();
    }


    void FillTexture()
    {
        float stepSize = 1f / resolution;
        for (int y=0; y< resolution; y++)
        {
           for (int x=0; x< resolution; x++)
           {
                texture.SetPixel(x, y, new Color(x * stepSize, y * stepSize, 0f));
           }
        }
        texture.Apply();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
