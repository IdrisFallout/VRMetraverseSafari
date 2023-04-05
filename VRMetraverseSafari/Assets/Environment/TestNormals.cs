using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNormals : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject terrain;
    private TerrainCollider terrainCollider;
    void Start()
    {
        terrainCollider = terrain.GetComponent<TerrainCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(transform.position, Vector3.down);
        if(terrainCollider.Raycast(ray, out RaycastHit hit, 1000))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }
}
