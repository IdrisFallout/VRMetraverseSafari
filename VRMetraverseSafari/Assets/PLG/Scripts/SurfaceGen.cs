using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SurfaceGen : MonoBehaviour
{
    [Range(1, 200)] 
    public int res = 10;
    private int _currentRes;
    
    private Mesh _mesh;

    void Start()
    {
    }
    private void OnEnable()
    {
        if (_mesh == null)
        {
            _mesh = new Mesh();
            _mesh.name = "Surface Mesh";
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        Refresh();
    }

    public void Refresh()
    {
        if (res != _currentRes)
        {
            CreateGrid();
        }
    }

    private void CreateGrid()
    {
        _currentRes = res;
        _mesh.Clear();
        
        Vector3[] verts = new Vector3[(res + 1) * (res + 1)];
        float stepSize = 1f / res;

        for (int v = 0,y = 0; y <= res; y++)
        {
            for (int x = 0; x <=res; x++, v++)
            {
                verts[v] = new Vector3(x * stepSize - 0.5f, y * stepSize - 0.5f);
            }
        }

        int[] tris = new int[res * res * 6];
        for (int t = 0, v = 0, y = 0; y < res; y++, v++)
        {
            
            for (int x = 0; x < res; x++, v++, t+=6) 
            {
                tris[t] = v;
                tris[t + 1] = v + res + 1;
                tris[t + 2] = v + 1;
                tris[t + 3] = v +1;
                tris[t + 4] = v + res + 1;
                tris[t + 5] = v + res + 2;
            }
        }

        _mesh.vertices = verts;
        _mesh.triangles = tris;
    }


}
